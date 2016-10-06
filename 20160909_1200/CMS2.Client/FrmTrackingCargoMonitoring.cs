using System;
using System.Windows.Forms;
using CMS2.BusinessLogic;
using CMS2.Entities.TrackingEntities.Models;

namespace CMS2.Client
{
    public partial class FrmTrackingCargoMonitoring : Form
    {
        private TrackingBL service;
        private BindingSource bsFieldRep;
        private BindingSource bsBranch;

        public FrmTrackingCargoMonitoring()
        {
            InitializeComponent();

            service = new TrackingBL();
            bsFieldRep = new BindingSource();
            bsBranch = new BindingSource();
        }

        private void FrmTrackingCargoMonitoring_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            bsFieldRep.DataSource = GlobalVars.FieldReps;
            lstDeliveredBy.DataSource = bsFieldRep;
            lstDeliveredBy.DisplayMember = "FullName";
            lstDeliveredBy.ValueMember = "FullName";

            bsBranch.DataSource = GlobalVars.Cities;
            lstBranch.DataSource = bsBranch;
            lstBranch.DisplayMember = "CityName";
            lstBranch.ValueMember = "CityName";

            DateTime _today = DateTime.Now;
            dateReportUntil.Value = _today.Date;
            dateReportFrom.Value = new DateTime(_today.Year, _today.Month, 1);
        }

        private void FrmTrackingCargoMonitoring_Shown(object sender, EventArgs e)
        {
            panel1.Left = (this.Width - panel1.Width) / 2;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            btnSearch.Enabled = false;

            Search(txtAirwayBill.Text.Trim());

            btnSearch.Enabled = true;
        }


        private void Search(string airwayBill)
        {
            var model = service.GetCargoStatusByAirwayBill(airwayBill);
            if (model != null)
            {
                txtAirwayBill.Text = model.AirwayBillNo;
                txtShipmentDate.Text = model.ShipmentDateString;
                txtShipper.Text = model.ShipperName;
                txtConsignee.Text = model.ConsigneeName;
                txtOrigin.Text = model.OriginCity;
                txtDestination.Text = model.DestinationCity;
                txtServiceMode.Text = model.ServiceMode;
                txtPaymentMode.Text = model.PaymentMode;
                lstDeliveredBy.SelectedItem = model.DeliveredBy;
                dateDeliveredOn.Value = model.DeliveredOn;
                lstStatus.SelectedItem = model.Status;
                lstRemarks.SelectedItem = model.Remarks;
                txtNotes.Text = model.Notes;

                if (!string.IsNullOrEmpty(model.ReceivedBy) && !string.IsNullOrEmpty(model.DeliveredBy))
                {
                    lblMessage.Text = "AWB was tagged 'Delivered' by " + model.DeliveredBy + " on " + model.DeliveredOnString + ".";
                    txtReceivedBy.Text = model.ReceivedBy;
                    dateReceivedOn.Value = model.ReceivedOn;
                    lstDeliveredBy.Enabled = false;
                    dateDeliveredOn.Enabled = false;
                    txtReceivedBy.ReadOnly = true;
                    dateReceivedOn.Enabled = false;
                    lstStatus.Enabled = false;
                    lstRemarks.Enabled = false;
                }
                else
                {
                    lstDeliveredBy.Enabled = true;
                    dateDeliveredOn.Enabled = true;
                    txtReceivedBy.ReadOnly = false;
                    dateReceivedOn.Enabled = true;
                    lstStatus.Enabled = true;
                    lstRemarks.Enabled = true;
                }
            }
            else
            {
                lblMessage.Text = "No record found.";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            AirwayBillViewModel vm = new AirwayBillViewModel();
            vm.AirwayBillNo = txtAirwayBill.Text.Trim();
            vm.DeliveredBy = lstDeliveredBy.SelectedItem.ToString();
            vm.ReceivedBy = txtReceivedBy.Text.Trim();
            vm.ReceivedOn = dateReceivedOn.Value;
            vm.Remarks = lstRemarks.SelectedItem.ToString();
            vm.Status = lstStatus.SelectedItem.ToString();
            vm.Notes = txtNotes.Text.Trim();
            vm.CreatedBy = AppUser.Employee.FullName;
            vm.CreatedDate = DateTime.Now;
            vm.ModifiedBy = AppUser.Employee.FullName;
            vm.ModifiedDate = DateTime.Now;
            Save(vm);
        }

        private void Save(AirwayBillViewModel vm)
        {
            if (service.UpdateDeliveryStatus(vm) > 0)
            {
                lblMessage.Text = "Save successfull.";
            }
            else
            {
                lblMessage.Text = "Save unsuccessfull.";
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void Print()
        {

        }
    }
}
