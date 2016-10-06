using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using CMS2.BusinessLogic;
using CMS2.Client.ViewModels;
using CMS2.Common.Enums;
using CMS2.Entities;

namespace CMS2.Client
{
    public partial class UcPayment : UserControl
    {
        private StatementOfAccountPayment soaPayment;
        private Payment payment;
        private StatementOfAccount soa;
        private BindingSource bsPaymentType;
        public PaymentDetailsViewModel NewPayment;
        private ShipmentBL shipmentService;
        private StatementOfAccountBL soaService;
        private PaymentBL paymentService;
        private PaymentTypeBL paymentTypeService;

        public UcPayment()
        {
            InitializeComponent();
           
        }

        private void UcPayment_Load(object sender, EventArgs e)
        {
            bsPaymentType = new BindingSource();
            shipmentService = new ShipmentBL(GlobalVars.UnitOfWork);
            soaService = new StatementOfAccountBL(GlobalVars.UnitOfWork);
            paymentService = new PaymentBL(GlobalVars.UnitOfWork);
            paymentTypeService = new PaymentTypeBL(GlobalVars.UnitOfWork);

            soaPayment = null;
            payment = null;
            soa = null;

            datePaymentDate.Value = DateTime.Now;

            bsPaymentType.DataSource = paymentTypeService.FilterActive().OrderBy(x => x.PaymentTypeName).ToList();
            lstPaymentType.DataSource = bsPaymentType;
            lstPaymentType.DisplayMember = "PaymentTypeName";
            lstPaymentType.ValueMember = "PaymentTypeId";
        }

        private void UcPayment_Enter(object sender, EventArgs e)
        {
            groupBox1.Left = (this.Width - groupBox1.Width) / 2;
            if (NewPayment != null)
            {
                txtAwb.Text = NewPayment.AwbSoa;
                txtAmountPaid.Text = NewPayment.AmountPaidString;
            }
        }
        
        private void Reset()
        {
            txtSoaNo.Text = "";
            txtSoaNo.Enabled = true;
            txtAwb.Text = "";
            txtAwb.Enabled = true;
            txtOrNo.Text = "";
            txtPrNo.Text = "";
            datePaymentDate.Value = DateTime.Now;
            txtAmountPaid.Text = "0.00";
            txtTaxWithheld.Text = "0.00";
            txtNetCollection.Text = "0.00";
            lstPaymentType.SelectedItem = "Cash";
            txtCheckBank.Text = "";
            txtCheckBank.Enabled = false;
            txtCheckNo.Text = "";
            txtCheckNo.Enabled = false;
            datePaymentDate.Value = DateTime.Now;
            datePaymentDate.Enabled = false;
            txtRemarks.Text = "";
        }

        private void txtSoaNo_TextChanged(object sender, EventArgs e)
        {
            txtAwb.Enabled = false;
            txtAwb.Text = "";
        }

        private void txtAwb_TextChanged(object sender, EventArgs e)
        {
            txtSoaNo.Enabled = false;
            txtSoaNo.Text = "";
        }

        #region ControlNavigation
        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSoaNo.Text))
            {
                // SOA Payment
                var soa = soaService.FilterActiveBy(x => x.StatementOfAccountNo.Equals(txtSoaNo.Text))
                        .FirstOrDefault();
                if (soa != null)
                {
                    soaPayment = new StatementOfAccountPayment();
                    soaPayment.StatementOfAccountPaymentId = Guid.NewGuid();
                    soaPayment.StatementOfAccountId = soa.StatementOfAccountId;
                    soaPayment.OrNo = txtOrNo.Text.Trim();
                    soaPayment.PrNo = txtPrNo.Text.Trim();
                    soaPayment.PaymentDate = datePaymentDate.Value;
                    soaPayment.Amount = decimal.Parse(txtAmountPaid.Text.Trim());
                    soaPayment.PaymentTypeId = Guid.Parse(lstPaymentType.SelectedValue.ToString());
                    if (lstPaymentType.SelectedText == "Check")
                    {
                        soaPayment.CheckBankName = txtCheckBank.Text.Trim();
                        soaPayment.CheckDate = dateCheckDate.Value;
                        soaPayment.CheckNo = txtCheckNo.Text.Trim();
                    }
                    soaPayment.ReceivedById = AppUser.Employee.EmployeeId;
                    soaPayment.Remarks = txtRemarks.Text;
                    soaPayment.CreatedBy = AppUser.User.Id;
                    soaPayment.CreatedDate = DateTime.Now;
                    soaPayment.ModifiedBy = AppUser.User.Id;
                    soaPayment.ModifiedDate = DateTime.Now;
                    soaPayment.RecordStatus = (int)RecordStatus.Active;

                }
                else
                {
                    MessageBox.Show("Invalid SOA No", "Data Error", MessageBoxButtons.OK);
                    return;
                    //}
                }
            }
            else if (!string.IsNullOrEmpty(txtAwb.Text.Trim()))
            {
                // AWb Payment
                var shipment = shipmentService.FilterActiveBy(x => x.AirwayBillNo.Equals(txtAwb.Text.Trim()))
                        .FirstOrDefault();
                if (shipment != null)
                {
                    payment = new Payment();
                    payment.PaymentId = Guid.NewGuid();
                    payment.ShipmentId = shipment.ShipmentId;
                    payment.OrNo = txtOrNo.Text.Trim();
                    payment.PrNo = txtPrNo.Text.Trim();
                    payment.PaymentDate = datePaymentDate.Value;
                    payment.Amount = decimal.Parse(txtAmountPaid.Text.Trim());
                    payment.TaxWithheld = decimal.Parse(txtTaxWithheld.Text.Trim());
                    payment.PaymentTypeId = Guid.Parse(lstPaymentType.SelectedValue.ToString());
                    if (lstPaymentType.SelectedText == "Check")
                    {
                        payment.CheckBankName = txtCheckBank.Text.Trim();
                        payment.CheckDate = dateCheckDate.Value;
                        payment.CheckNo = txtCheckNo.Text.Trim();
                    }
                    payment.ReceivedById = AppUser.Employee.EmployeeId;
                    payment.Remarks = txtRemarks.Text;
                    payment.CreatedBy = AppUser.User.Id;
                    payment.CreatedDate = DateTime.Now;
                    payment.ModifiedBy = AppUser.User.Id;
                    payment.ModifiedDate = DateTime.Now;
                    payment.RecordStatus = (int)RecordStatus.Active;
                }
                else
                {
                    MessageBox.Show("Invalid AWB No", "Data Error", MessageBoxButtons.OK);
                    return;
                }
            }

            btnAccept.Enabled = false;
            btnCancel.Enabled = false;

            ProgressIndicator saving = new ProgressIndicator("Payment", "Saving ...", SavePayment);
            saving.ShowDialog();

            //ProgressIndicator uploading = new ProgressIndicator("Payment", "Uploading ...", UploadToCentral);
            //uploading.ShowDialog();

            btnAccept.Enabled = true;
            btnCancel.Enabled = true;
            Reset();
            MessageBox.Show("Payment successfully saved.", "Payment", MessageBoxButtons.OK);
        }

        private void SavePayment(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker _worker = sender as BackgroundWorker;
            int percent = 1;
            int index = 1;
            int max = 2; // # of processes

            if (soaPayment != null)
            {
                soaService.MakePayment(soaPayment, soaService.EntityToModel(soa));
                percent = index * 100 / max;
                _worker.ReportProgress(percent);
                index++;
            }
            if (payment != null)
            {
                paymentService.Add(payment);
                percent = index * 100 / max;
                _worker.ReportProgress(percent);
                index++;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void txtSoaNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtOrNo.Focus();
            }
        }

        private void txtAwb_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtOrNo.Focus();
            }
        }

        private void txtOrNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAmountPaid.Focus();
            }
        }

        private void txtPrNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAmountPaid.Focus();
            }
        }

        private void datePaymentDate_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAmountPaid.Focus();
            }
        }

        private void txtAmountPaid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtTaxWithheld.Focus();
            }
        }

        private void txtTaxWithheld_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lstPaymentType.Focus();
            }
        }

        private void lstPaymentType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (lstPaymentType.SelectedText != "Check")
            {
                txtCheckBank.Enabled = true;
                txtCheckNo.Enabled = true;
                dateCheckDate.Enabled = true;
                txtCheckBank.Focus();
            }
            else
            {
                txtCheckBank.Enabled = false;
                txtCheckNo.Enabled = false;
                dateCheckDate.Enabled = false;
                txtRemarks.Focus();
            }
        }

        private void txtCheckBank_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCheckNo.Focus();
            }
        }

        private void txtCheckNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dateCheckDate.Focus();
            }
        }

        private void dateCheckDate_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtRemarks.Focus();
            }
        }

        private void txtRemarks_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAccept.Focus();
            }
        }

        private void btnCancel_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Reset();
            }
        }
        #endregion

        private void txtAmountPaid_Leave(object sender, EventArgs e)
        {
            ComputeNetCollection();
        }

        private void txtTaxWithheld_Leave(object sender, EventArgs e)
        {
            ComputeNetCollection();
        }

        private void ComputeNetCollection()
        {
            txtNetCollection.Text =
                (decimal.Parse(txtAmountPaid.Text) - decimal.Parse(txtTaxWithheld.Text)).ToString("N");
        }

        private void UcPayment_VisibleChanged(object sender, EventArgs e)
        {
            if (NewPayment != null)
            {
                txtAwb.Text = NewPayment.AwbSoa;
                txtAmountPaid.Text = NewPayment.AmountPaidString;
                datePaymentDate.Value = DateTime.Now;
                txtOrNo.Focus();
            }
        }


    }
}
