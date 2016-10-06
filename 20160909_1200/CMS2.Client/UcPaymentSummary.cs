using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using CMS2.BusinessLogic;
using CMS2.Client.ReportModels;
using CMS2.Client.SyncHelper;
using CMS2.Client.ViewModels;
using CMS2.Common.Enums;
using CMS2.Entities;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Microsoft.Synchronization;

namespace CMS2.Client
{
    public partial class UcPaymentSummary : UserControl
    {
        private BindingSource bsEmployees;
        private List<PaymentDetailsViewModel> prepaid;
        private List<PaymentDetailsViewModel> freightcollect;
        private List<PaymentDetailsViewModel> collectconsignee;
        private PaymentTurnoverViewModel paymentTurnoverVm;
        private PaymentTurnover paymentTurnover;
        private List<Payment> verifyPayments;
        private PaymentBL paymentService;
        private PaymentTurnoverBL paymentTurnOverService;
        private EmployeeBL employeeService;

        public UcPaymentSummary()
        {
            InitializeComponent();
        }

        private void UcPaymentSummary_Load(object sender, EventArgs e)
        {
            bsEmployees = new BindingSource();
            paymentService = new PaymentBL(GlobalVars.UnitOfWork);
            paymentTurnOverService = new PaymentTurnoverBL(GlobalVars.UnitOfWork);
            employeeService = new EmployeeBL(GlobalVars.UnitOfWork);
            
            bsEmployees.DataSource = employeeService.FilterActive().OrderBy(x => x.FullName).ToList();

            lstCollectedBy.DataSource = bsEmployees;
            lstCollectedBy.DisplayMember = "Fullname";
            lstCollectedBy.ValueMember = "EmployeeId";

        }

        #region Events

        private void lstCollectedBy_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnReset.Enabled = true;
            btnSave.Enabled = true;
            btnPrint.Enabled = false;
            GetCollections();
        }
        
        private void dateCollectionDate_ValueChanged(object sender, EventArgs e)
        {
            btnReset.Enabled = true;
            btnSave.Enabled = true;
            btnPrint.Enabled = false;
            GetCollections();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnReset.Enabled = false;
            btnSave.Enabled = false;
            btnPrint.Enabled = false;

            paymentTurnover = new PaymentTurnover();
            if (paymentTurnoverVm.PaymentTurnOverId == null || paymentTurnoverVm.PaymentTurnOverId == Guid.Empty)
            {
                paymentTurnover.PaymentTurnOverId = Guid.NewGuid();
                paymentTurnover.CreatedBy = AppUser.User.Id;
                paymentTurnover.CreatedDate = DateTime.Now;
            }
            paymentTurnover.CollectedById = Guid.Parse(lstCollectedBy.SelectedValue.ToString());
            paymentTurnover.CollectionDate = dateCollectionDate.Value;
            paymentTurnover.ReceivedCashAmount = Convert.ToDecimal(txtTotalCashReceived.Text.Trim());
            paymentTurnover.ReceivedCheckAmount = Convert.ToDecimal(txtTotalCheckReceived.Text.Trim());
            paymentTurnover.Remarks = txtRemarks.Text;
            paymentTurnover.ModifiedBy = AppUser.User.Id;
            paymentTurnover.ModifiedDate = DateTime.Now;
            paymentTurnover.RecordStatus = (int) RecordStatus.Active;

            verifyPayments = new List<Payment>();
            foreach (var item in prepaid)
            {
                if (item.IsVerified==false)
                {
                    Payment model = paymentService.GetById(item.AwbSoaId);
                    model.VerifiedById = AppUser.Employee.EmployeeId;
                    model.VerifiedDate = DateTime.Now;
                    model.ModifiedBy = AppUser.User.Id;
                    model.ModifiedDate = DateTime.Now;
                    model.RecordStatus = (int)RecordStatus.Active;
                    verifyPayments.Add(model);
                }
            }
            foreach (var item in freightcollect)
            {
                if (item.IsVerified == false)
                {
                    Payment model = paymentService.GetById(item.AwbSoaId);
                    model.VerifiedById = AppUser.Employee.EmployeeId;
                    model.VerifiedDate = DateTime.Now;
                    model.ModifiedBy = AppUser.User.Id;
                    model.ModifiedDate = DateTime.Now;
                    model.RecordStatus = (int) RecordStatus.Active;
                    verifyPayments.Add(model);
                }
            }
            foreach (var item in collectconsignee)
            {
                if (item.IsVerified == false)
                {
                    Payment model = paymentService.GetById(item.AwbSoaId);
                    model.VerifiedById = AppUser.Employee.EmployeeId;
                    model.VerifiedDate = DateTime.Now;
                    model.ModifiedBy = AppUser.User.Id;
                    model.ModifiedDate = DateTime.Now;
                    model.RecordStatus = (int) RecordStatus.Active;
                    verifyPayments.Add(model);
                }
            }

            ProgressIndicator saving = new ProgressIndicator("Payment Summary", "Saving ...", SavePayment);
            saving.ShowDialog();

            ProgressIndicator uploading = new ProgressIndicator("Payment Summary", "Uploading ...", UploadToCentral);
            uploading.ShowDialog();

            btnReset.Enabled = true;
            btnSave.Enabled = false;
            btnPrint.Enabled = true;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            btnReset.Enabled = false;
            btnSave.Enabled = false;
            btnPrint.Enabled = false;
            Print();
            Reset();
        }

        private void btnReceivedAll_Click(object sender, EventArgs e)
        {
            ReceivedAll();
            RefreshGrids();
        }

        #endregion

        private void GetCollections()
        {
            prepaid = new List<PaymentDetailsViewModel>();
            freightcollect = new List<PaymentDetailsViewModel>();
            collectconsignee = new List<PaymentDetailsViewModel>();
            paymentTurnoverVm = new PaymentTurnoverViewModel();

            Guid collectedById = Guid.Parse(lstCollectedBy.SelectedValue.ToString());
            DateTime dateCollected = dateCollectionDate.Value;

            paymentTurnoverVm.CollectionDate = dateCollected;
            paymentTurnoverVm.CollectedById = collectedById;

            var paymentTurnovers =
               paymentTurnOverService.FilterActiveBy(
                    x =>
                        x.CollectedById == collectedById && x.CollectionDate.Year == dateCollected.Year &&
                        x.CollectionDate.Month == dateCollected.Month && x.CollectionDate.Day == dateCollected.Day);
            if (paymentTurnovers != null && paymentTurnovers.Count > 0)
            {
                var _paymentTurnover = paymentTurnovers.FirstOrDefault();
                paymentTurnoverVm.CollectionDate = _paymentTurnover.CollectionDate;
                paymentTurnoverVm.CollectedById = _paymentTurnover.CollectedById;
                paymentTurnoverVm.PaymentTurnOverId = _paymentTurnover.PaymentTurnOverId;
            }
            
            #region NonCorporate
            var awbPayments =
                paymentService.FilterActiveBy(
                    x => x.StatementOfAccountPaymentId == null && x.ReceivedById == collectedById &&
                         (x.PaymentDate.Year == dateCollected.Year &&
                          x.PaymentDate.Month == dateCollected.Month &&
                          x.PaymentDate.Day == dateCollected.Day));
            
            foreach (var item in awbPayments)
            {
                PaymentDetailsViewModel vm = new PaymentDetailsViewModel();
                vm.AwbSoaId = item.PaymentId;
                vm.AwbSoa = item.Shipment.AirwayBillNo;
                vm.PaymentMode = item.Shipment.PaymentMode.PaymentModeCode;
                vm.AmountPaid = item.Amount;
                vm.TaxWithheld = item.TaxWithheld;
                vm.NetAmount = vm.AmountPaid - vm.TaxWithheld;
                if (vm.PaymentMode.Equals("PP"))
                    vm.PayorName = item.Shipment.Shipper.FullName;
                else if (vm.PaymentMode.Equals("FC"))
                    vm.PayorName = item.Shipment.Consignee.FullName;
                vm.PaymentType = item.PaymentType.PaymentTypeName;
                if (item.CheckDate != null && item.CheckDate >= item.PaymentDate.AddDays(2))
                    vm.PaymentType = "PDC";
                if (vm.PaymentType.Equals("Cash"))
                {
                    paymentTurnoverVm.TotalCashCollection = paymentTurnoverVm.TotalCashCollection + vm.NetAmount;
                }
                else if (vm.PaymentType.Equals("Check"))
                {
                    paymentTurnoverVm.TotalCheckCollection = paymentTurnoverVm.TotalCheckCollection + vm.NetAmount;
                }
                else if (vm.PaymentType.Equals("PDC"))
                {
                    paymentTurnoverVm.TotalPdcAmount = paymentTurnoverVm.TotalPdcAmount + vm.NetAmount;
                }
                paymentTurnoverVm.TotalTaxWithheld = paymentTurnoverVm.TotalTaxWithheld + vm.TaxWithheld;
                vm.OrNo = item.OrNo;
                vm.CollectedById = item.ReceivedById;
                vm.CollectedBy = item.ReceivedBy.FullName;
                vm.Remarks = item.Remarks;
                vm.ReceivedStatus = false;
                if (item.VerifiedById != null)
                {
                    vm.ReceivedStatus = true;
                    vm.IsVerified = true;
                }

                if (vm.PaymentMode.Equals("PP"))
                {
                    prepaid.Add(vm);
                }
                else if (vm.PaymentMode.Equals("FC"))
                {
                    freightcollect.Add(vm);
                }
            }

            #endregion

            #region Corporate
            var soaPayments =
                paymentService.FilterActiveBy(
                    x =>
                        x.StatementOfAccountPaymentId != null && x.ReceivedById == collectedById &&
                        (x.PaymentDate.Year == dateCollected.Year &&
                         x.PaymentDate.Month == dateCollected.Month &&
                         x.PaymentDate.Day == dateCollected.Day));

            foreach (var item in soaPayments)
            {
                if (item.Shipment.PaymentMode.PaymentModeCode.Equals("CAC"))
                {
                    PaymentDetailsViewModel vm = new PaymentDetailsViewModel();
                    vm.AwbSoaId = item.PaymentId;
                    vm.AwbSoa = item.Shipment.AirwayBillNo;
                    vm.PayorName = item.Shipment.Consignee.FullName;
                    vm.PaymentMode = item.Shipment.PaymentMode.PaymentModeCode;
                    vm.AmountPaid = item.Amount;
                    vm.TaxWithheld = item.TaxWithheld;
                    vm.NetAmount = vm.AmountPaid - vm.TaxWithheld;
                    vm.PaymentType = item.PaymentType.PaymentTypeName;
                    if (item.CheckDate != null && item.CheckDate >= item.PaymentDate.AddDays(2))
                        vm.PaymentType = "PDC";
                    if (vm.PaymentType.Equals("Cash"))
                    {
                        paymentTurnoverVm.TotalCashCollection = paymentTurnoverVm.TotalCashCollection + vm.NetAmount;
                    }
                    else if (vm.PaymentType.Equals("Check"))
                    {
                        paymentTurnoverVm.TotalCheckCollection = paymentTurnoverVm.TotalCheckCollection + vm.NetAmount;
                    }
                    else if (vm.PaymentType.Equals("PDC"))
                    {
                        paymentTurnoverVm.TotalPdcAmount = paymentTurnoverVm.TotalPdcAmount + vm.NetAmount;
                    }
                    paymentTurnoverVm.TotalTaxWithheld = paymentTurnoverVm.TotalTaxWithheld + vm.TaxWithheld;
                    vm.OrNo = item.OrNo;
                    vm.CollectedById = item.ReceivedById;
                    vm.CollectedBy = item.ReceivedBy.FullName;
                    vm.Remarks = item.Remarks;
                    vm.ReceivedStatus = false;
                    if (item.VerifiedById != null)
                    {
                        vm.ReceivedStatus = true;
                        vm.IsVerified = true;
                    }

                    collectconsignee.Add(vm);
                }
            }
            RefreshGrids();

            #endregion

            txtTotalCash.Text = paymentTurnoverVm.TotalCashCollectionString;
            txtTotalCheck.Text = paymentTurnoverVm.TotalCheckCollectionString;
            txtTotalCollection.Text = paymentTurnoverVm.TotalCollectionString;
            txtTotalTax.Text = paymentTurnoverVm.TotalTaxWithheldString;
            txtTotalPending.Text = paymentTurnoverVm.TotalPendingString;
            txtTotalPdc.Text = paymentTurnoverVm.TotalPdcAmountString;
        }

        private void Reset()
        {
            dateCollectionDate.Value = DateTime.Now;
            lstCollectedBy.SelectedIndex = 0;
            btnReset.Enabled = true;
            btnSave.Enabled = false;
            btnPrint.Enabled = false;
            prepaid.Clear();
            freightcollect.Clear();
            collectconsignee.Clear();
            verifyPayments.Clear();
        }

        private void SavePayment(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker _worker = sender as BackgroundWorker;
            int percent = 1;
            int index = 1;
            int max = 3; // # of processes

            percent = index*100/max;
            _worker.ReportProgress(percent);
            index++;
            paymentTurnOverService.AddEdit(paymentTurnover);
            percent = index*100/max;
            _worker.ReportProgress(percent);
            index++;
            paymentService.EditMultiple(verifyPayments);
            percent = index*100/max;
            _worker.ReportProgress(percent);
            index++;

        }

        private void UploadToCentral(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker _worker = sender as BackgroundWorker;
            int percent = 1;
            int index = 1;
            int max = 1; // # of processes

            SyncCms sync = new SyncCms();
            sync.OpenConnection();
            if (sync.IsCentralConnected())
            {
                    sync.SyncEntity("PaymentTurnover", SyncDirectionOrder.Upload);
                    percent = index * 100 / max;
                    _worker.ReportProgress(percent);
                    index++;
            }
            sync.CloseConnection();
        }

        private void Print()
        {
            #region SetDatSet

            PaymentSummary paymentSummary = new PaymentSummary();
            DataRow dr = paymentSummary.Tables["PaymentSummary"].NewRow();
            dr["CollectionDate"] = paymentTurnoverVm.CollectionDateString;
            dr["CollectedBy"] = paymentTurnoverVm.CollectionDateString;
            dr["ReceivedCashAmount"] = paymentTurnoverVm.CollectionDateString;
            dr["ReceivedCheckAmount"] = paymentTurnoverVm.CollectionDateString;
            dr["Remarks"] = paymentTurnoverVm.CollectionDateString;
            dr["TotalCashCollection"] = paymentTurnoverVm.CollectionDateString;
            dr["TotalCheckCollection"] = paymentTurnoverVm.CollectionDateString;
            dr["TotalCollection"] = paymentTurnoverVm.CollectionDateString;
            dr["TotalTaxWithheld"] = paymentTurnoverVm.CollectionDateString;
            dr["TotalPending"] = paymentTurnoverVm.CollectionDateString;
            dr["TotalPdcAmount"] = paymentTurnoverVm.CollectionDateString;
            dr["TotalReceivedAmount"] = paymentTurnoverVm.CollectionDateString;
            dr["AmountDifference"] = paymentTurnoverVm.CollectionDateString;

            foreach (var item in prepaid)
            {
                dr = paymentSummary.Tables["PrePaid"].NewRow();
                dr["AwbSoa"] = item.AwbSoa;
                dr["PayorName"] = item.PayorName;
                dr["PaymentMode"] = item.PaymentMode;
                dr["PaymentType"] = item.PaymentType;
                dr["AmountPaid"] = item.AmountPaidString;
                dr["TaxWithheld"] = item.TaxWithheldString;
                dr["NetAmount"] = item.NetAmountString;
                dr["OrPr"] = item.OrNo;
                dr["CollectedBy"] = item.CollectedBy;
                dr["Remarks"] = item.Remarks;
                dr["ReceivedStatus"] = item.ReceivedStatus;
                paymentSummary.Tables["PrePaid"].Rows.Add(dr);
            }

            foreach (var item in freightcollect)
            {
                dr = paymentSummary.Tables["FreighCollect"].NewRow();
                dr["AwbSoa"] = item.AwbSoa;
                dr["PayorName"] = item.PayorName;
                dr["PaymentMode"] = item.PaymentMode;
                dr["PaymentType"] = item.PaymentType;
                dr["AmountPaid"] = item.AmountPaidString;
                dr["TaxWithheld"] = item.TaxWithheldString;
                dr["NetAmount"] = item.NetAmountString;
                dr["OrPr"] = item.OrNo;
                dr["CollectedBy"] = item.CollectedBy;
                dr["Remarks"] = item.Remarks;
                dr["ReceivedStatus"] = item.ReceivedStatus;
                paymentSummary.Tables["FreighCollect"].Rows.Add(dr);
            }

            foreach (var item in collectconsignee)
            {
                dr = paymentSummary.Tables["CAC"].NewRow();
                dr["AwbSoa"] = item.AwbSoa;
                dr["PayorName"] = item.PayorName;
                dr["PaymentMode"] = item.PaymentMode;
                dr["PaymentType"] = item.PaymentType;
                dr["AmountPaid"] = item.AmountPaidString;
                dr["TaxWithheld"] = item.TaxWithheldString;
                dr["NetAmount"] = item.NetAmountString;
                dr["OrPr"] = item.OrNo;
                dr["CollectedBy"] = item.CollectedBy;
                dr["Remarks"] = item.Remarks;
                dr["ReceivedStatus"] = item.ReceivedStatus;
                paymentSummary.Tables["CAC"].Rows.Add(dr);
            }

            #endregion

            try
            {
                PrinterSettings printer = new PrinterSettings();
                ReportDocument report = new ReportDocument();
                report.Load(AppDomain.CurrentDomain.BaseDirectory + "Reports\\PaymentSummary.rpt");
                report.SetDataSource(paymentSummary);
                report.PrintOptions.PaperOrientation = PaperOrientation.Portrait;
                report.PrintOptions.PrinterName = printer.PrinterName;
                report.PrintToPrinter(1, false, 0, 0);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

        }

        private void RefreshGrids()
        {
            RefreshGridPrepaid();
            RefreshGridFreightCollect();
            RefreshGridCAC();
            ComputeReceivedAmount();
        }

        private void RefreshGridPrepaid()
        {
            int gridRowIndex = 0;
            if (gridPrepaid.Rows.Count > 0)
                gridPrepaid.Rows.Clear();
            foreach (var item in prepaid)
            {
                gridPrepaid.Rows.Add();
                gridPrepaid.Rows[gridRowIndex].Cells["colPPPaymentId"].Value = item.AwbSoaId;
                gridPrepaid.Rows[gridRowIndex].Cells["colPPAwbSoa"].Value = item.AwbSoa;
                gridPrepaid.Rows[gridRowIndex].Cells["colPPPayor"].Value = item.PayorName;
                gridPrepaid.Rows[gridRowIndex].Cells["colPPPaymentForm"].Value = item.PaymentType;
                gridPrepaid.Rows[gridRowIndex].Cells["colPPAmountCollected"].Value = item.NetAmountString;
                gridPrepaid.Rows[gridRowIndex].Cells["colPPTaxWithheld"].Value = item.TaxWithheldString;
                gridPrepaid.Rows[gridRowIndex].Cells["colPPOrPr"].Value = item.OrNo;
                gridPrepaid.Rows[gridRowIndex].Cells["colPPCollectedBy"].Value = item.CollectedBy;
                gridPrepaid.Rows[gridRowIndex].Cells["colPPRemarks"].Value = item.Remarks;
                gridPrepaid.Rows[gridRowIndex].Cells["colPPReceivedStatus"].Value = item.ReceivedStatus;
                gridPrepaid.Rows[gridRowIndex].Cells["colPPIsVerified"].Value = item.IsVerified;
                gridRowIndex++;
            }
            txtTotalAmntPrepaid.Text = prepaid.Sum(x => x.NetAmount).ToString("N");
            txtTotalTaxPrepaid.Text = prepaid.Sum(x => x.TaxWithheld).ToString("N");
        }

        private void RefreshGridFreightCollect()
        {
            int gridRowIndex = 0;
            if (gridFreightCollect.Rows.Count > 0)
                gridFreightCollect.Rows.Clear();
            foreach (var item in freightcollect)
            {
                gridFreightCollect.Rows.Add();
                gridFreightCollect.Rows[gridRowIndex].Cells["colFCPaymentId"].Value = item.AwbSoaId;
                gridFreightCollect.Rows[gridRowIndex].Cells["colFCAwbSoa"].Value = item.AwbSoa;
                gridFreightCollect.Rows[gridRowIndex].Cells["colFCPayor"].Value = item.PayorName;
                gridFreightCollect.Rows[gridRowIndex].Cells["colFCPaymentForm"].Value = item.PaymentType;
                gridFreightCollect.Rows[gridRowIndex].Cells["colFCAmountCollected"].Value = item.NetAmountString;
                gridFreightCollect.Rows[gridRowIndex].Cells["colFCTaxWithheld"].Value = item.TaxWithheldString;
                gridFreightCollect.Rows[gridRowIndex].Cells["colFCOrPr"].Value = item.OrNo;
                gridFreightCollect.Rows[gridRowIndex].Cells["colFCCollectedBy"].Value = item.CollectedBy;
                gridFreightCollect.Rows[gridRowIndex].Cells["colFCRemarks"].Value = item.Remarks;
                gridFreightCollect.Rows[gridRowIndex].Cells["colFCReceivedStatus"].Value = item.ReceivedStatus;
                gridFreightCollect.Rows[gridRowIndex].Cells["colFCIsVerified"].Value = item.IsVerified;
                gridRowIndex++;
            }
            txtTotalAmntFreightCollect.Text = freightcollect.Sum(x => x.NetAmount).ToString("N");
            txtTotalTaxFreightCollect.Text = freightcollect.Sum(x => x.TaxWithheld).ToString("N");
        }

        private void RefreshGridCAC()
        {
            int gridRowIndex = 0;
            if (gridCorpAcctConsignee.Rows.Count > 0)
                gridCorpAcctConsignee.Rows.Clear();
            foreach (var item in collectconsignee)
            {
                gridCorpAcctConsignee.Rows.Add();
                gridCorpAcctConsignee.Rows[gridRowIndex].Cells["colCACPaymentId"].Value = item.AwbSoaId;
                gridCorpAcctConsignee.Rows[gridRowIndex].Cells["colCACAwbSoa"].Value = item.AwbSoa;
                gridCorpAcctConsignee.Rows[gridRowIndex].Cells["colCACPayor"].Value = item.PayorName;
                gridCorpAcctConsignee.Rows[gridRowIndex].Cells["colCACPaymentForm"].Value = item.PaymentType;
                gridCorpAcctConsignee.Rows[gridRowIndex].Cells["colCACAmountCollected"].Value = item.NetAmountString;
                gridCorpAcctConsignee.Rows[gridRowIndex].Cells["colCACTaxWithheld"].Value = item.TaxWithheldString;
                gridCorpAcctConsignee.Rows[gridRowIndex].Cells["colCACOrPr"].Value = item.OrNo;
                gridCorpAcctConsignee.Rows[gridRowIndex].Cells["colCACCollectedBy"].Value = item.CollectedBy;
                gridCorpAcctConsignee.Rows[gridRowIndex].Cells["colCACRemarks"].Value = item.Remarks;
                gridCorpAcctConsignee.Rows[gridRowIndex].Cells["colCACReceivedStatus"].Value = item.ReceivedStatus;
                gridCorpAcctConsignee.Rows[gridRowIndex].Cells["colCACIsVerified"].Value = item.IsVerified;
                gridRowIndex++;
            }
            txtTotalAmntCorpAcctConsignee.Text = collectconsignee.Sum(x => x.NetAmount).ToString("N");
            txtTotalTaxCorpAcctConsignee.Text = collectconsignee.Sum(x => x.TaxWithheld).ToString("N");
        }

        private void gridPrepaid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PrepaidReceive(e);
        }

        private void gridPrepaid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            PrepaidReceive(e);
        }

        private void PrepaidReceive(DataGridViewCellEventArgs e)
        {
            Guid paymentId = Guid.Parse(gridPrepaid.Rows[e.RowIndex].Cells[0].Value.ToString());
            if (!Convert.ToBoolean(gridPrepaid.Rows[e.RowIndex].Cells[10].Value))
            {
                if (Convert.ToBoolean(gridPrepaid.Rows[e.RowIndex].Cells[9].Value))
                {
                    prepaid.FirstOrDefault(x => x.AwbSoaId == paymentId).ReceivedStatus = false;
                }
                else
                {
                    prepaid.FirstOrDefault(x => x.AwbSoaId == paymentId).ReceivedStatus = true;
                }
            }
            RefreshGridPrepaid();
            ComputeReceivedAmount();
        }

        private void gridFreightCollect_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            FreightCollectReceive(e);
        }

        private void gridFreightCollect_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            FreightCollectReceive(e);
        }

        private void FreightCollectReceive(DataGridViewCellEventArgs e)
        {
            Guid paymentId = Guid.Parse(gridFreightCollect.Rows[e.RowIndex].Cells[0].Value.ToString());
            if (Convert.ToBoolean(gridFreightCollect.Rows[e.RowIndex].Cells[9].Value))
            {
                freightcollect.FirstOrDefault(x => x.AwbSoaId == paymentId).ReceivedStatus = false;
            }
            else
            {
                freightcollect.FirstOrDefault(x => x.AwbSoaId == paymentId).ReceivedStatus = true;
            }
            RefreshGridFreightCollect();
            ComputeReceivedAmount();
        }

        private void gridCorpAcctConsignee_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CACReceive(e);
        }
        private void gridCorpAcctConsignee_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            CACReceive(e);
        }

        private void CACReceive(DataGridViewCellEventArgs e)
        {
            Guid paymentId = Guid.Parse(gridCorpAcctConsignee.Rows[e.RowIndex].Cells[0].Value.ToString());
            if (Convert.ToBoolean(gridCorpAcctConsignee.Rows[e.RowIndex].Cells[9].Value))
            {
                collectconsignee.FirstOrDefault(x => x.AwbSoaId == paymentId).ReceivedStatus = false;
            }
            else
            {
                collectconsignee.FirstOrDefault(x => x.AwbSoaId == paymentId).ReceivedStatus = true;
            }
            RefreshGridCAC();
            ComputeReceivedAmount();
        }
        private void ReceivedAll()
        {
            foreach (var item in prepaid)
            {
                item.ReceivedStatus = true;
            }

            foreach (var item in freightcollect)
            {
                item.ReceivedStatus = true;
            }

            foreach (var item in collectconsignee)
            {
                item.ReceivedStatus = true;
            }
        }

        private void ComputeReceivedAmount()
        {
            paymentTurnoverVm.ReceivedCashAmount = 0;
            paymentTurnoverVm.ReceivedCheckAmount = 0;
            
            foreach (var item in prepaid)
            {
                if (item.ReceivedStatus)
                {
                    if (item.PaymentType.Equals("Cash"))
                        paymentTurnoverVm.ReceivedCashAmount = paymentTurnoverVm.ReceivedCashAmount + item.AmountPaid;
                    else if (item.PaymentType.Equals("Check"))
                        paymentTurnoverVm.ReceivedCheckAmount = paymentTurnoverVm.ReceivedCheckAmount + item.AmountPaid;
                }
            }

            foreach (var item in freightcollect)
            {
                if (item.ReceivedStatus)
                {
                    if (item.PaymentType.Equals("Cash"))
                        paymentTurnoverVm.ReceivedCashAmount = paymentTurnoverVm.ReceivedCashAmount + item.AmountPaid;
                    else if (item.PaymentType.Equals("Check"))
                        paymentTurnoverVm.ReceivedCheckAmount = paymentTurnoverVm.ReceivedCheckAmount + item.AmountPaid;
                }
            }

            foreach (var item in collectconsignee)
            {
                if (item.ReceivedStatus)
                {
                    if (item.PaymentType.Equals("Cash"))
                        paymentTurnoverVm.ReceivedCashAmount = paymentTurnoverVm.ReceivedCashAmount + item.AmountPaid;
                    else if (item.PaymentType.Equals("Check"))
                        paymentTurnoverVm.ReceivedCheckAmount = paymentTurnoverVm.ReceivedCheckAmount + item.AmountPaid;
                }
            }

            txtTotalCashReceived.Text = paymentTurnoverVm.ReceivedCashAmountString;
            txtTotalCheckReceived.Text = paymentTurnoverVm.ReceivedCheckAmountString;
            txtTotalAmntReceived.Text = paymentTurnoverVm.TotalReceivedAmountString;
            txtDifference.Text = paymentTurnoverVm.AmountDifferenceString;
        }
        
    }
}
