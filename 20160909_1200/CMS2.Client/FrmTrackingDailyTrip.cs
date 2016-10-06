using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CMS2.BusinessLogic;
using CMS2.Client.ReportModels;
using CrystalDecisions.CrystalReports.Engine;

namespace CMS2.Client
{
    public partial class FrmTrackingDailyTrip : Form
    {
        private TrackingBL service;
        private BindingSource bsBranch;
        BranchCorpOfficeBL bcoService;

        public FrmTrackingDailyTrip()
        {
            InitializeComponent();
        }

        private void FrmTrackingDailyTrip_Load(object sender, EventArgs e)
        {
            service = new TrackingBL();
            bsBranch = new BindingSource();
            bcoService = new BranchCorpOfficeBL();

            lblMessage.Text = "";
            bsBranch.DataSource = bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).ToList();

            lstBranch.DataSource = bsBranch;
            lstBranch.DisplayMember = "BranchCorpOfficeName";
            lstBranch.ValueMember = "BranchCorpOfficeName";
        }

        private void FrmTrackingDailyTrip_Shown(object sender, EventArgs e)
        {
            panel1.Left = (this.Width - panel1.Width) / 2;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            btnSearch.Enabled = false;

            lblMessage.Text = "";
            var models = service.GetDistributionByDateBranch(dateTripDate.Value.Date, lstBranch.SelectedValue.ToString());
            gridDailyTrip.Rows.Clear();
            if (models != null && models.Count > 0)
            {
                int index = 0;
                foreach (var item in models)
                {
                    gridDailyTrip.Rows.Add();
                    gridDailyTrip.Rows[index].Cells["colPlateNo"].Value = item.PlateNo;
                    gridDailyTrip.Rows[index].Cells["colFieldRep"].Value = item.FieldRep;
                    gridDailyTrip.Rows[index].Cells["colDriver"].Value = item.Driver;
                    index++;
                }
            }
            else
            {
                lblMessage.Text = "No record found.";
            }

            btnSearch.Enabled = true;
        }
        
        private void gridDailyTrip_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;
            int row = e.RowIndex;
            DateTime date = Convert.ToDateTime(dateTripDate.Value.Date);
            string bco = lstBranch.SelectedValue.ToString();
            string fieldRep = grid.Rows[row].Cells["colFieldRep"].Value.ToString();
            string driver = grid.Rows[row].Cells["colDriver"].Value.ToString();
            string plateNo = grid.Rows[row].Cells["colPlateNo"].Value.ToString();
            if (e.ColumnIndex == 3)
            {
                Print(date, bco, fieldRep, driver, plateNo);
            }
        }

        private void Print(DateTime date, string bco, string fieldRep,string driver, string plateNo)
        {
            var models = service.GetDailyTripByDateBranchFieldRepDriverPlateNo(date, bco, fieldRep, driver, plateNo);
            if (models != null && models.Count > 0)
            {
                TrackingDailyTrip ds = new TrackingDailyTrip();
                DataRow newRow;
                newRow = ds.Tables["DailyTrip"].NewRow();
                newRow["Date"] = date;
                newRow["PlateNo"] = plateNo;
                newRow["Branch"] = bco;
                newRow["Driver"] = driver;
                newRow["FieldRep"] = fieldRep;
                ds.Tables["DailyTrip"].Rows.Add(newRow);
                foreach (var item in models)
                {
                    if (item.PaymentMode.PaymentModeCode == "CAC" || item.PaymentMode.PaymentModeCode == "FC")
                    {
                        newRow = ds.Tables["ShipmentCAC"].NewRow();
                        newRow["Airwaybill"] = item.AirwayBillNo;
                        newRow["Client"] = item.Shipper.FullName;
                        newRow["Origin"] = item.OriginCity.CityCode;
                        newRow["Destination"] = item.DestinationCity.CityCode;
                        newRow["ServiceMode"] = item.ServiceMode.ServiceModeCode;
                        newRow["PaymentMode"] = item.PaymentMode.PaymentModeCode;
                        newRow["ItemCount"] = item.PackageNumbers.Count;
                        newRow["ActualWeight"] = item.Weight;
                        newRow["TotalAmount"] = item.ShipmentTotal;
                        newRow["ReceivedBy"] = item.AcceptedBy.FullName;
                        ds.Tables["ShipmentCAC"].Rows.Add(newRow);
                    }
                    else
                    {
                        newRow = ds.Tables["ShipmentPP"].NewRow();
                        newRow["Airwaybill"] = item.AirwayBillNo;
                        newRow["Client"] = item.Shipper.FullName;
                        newRow["Origin"] = item.OriginCity.CityCode;
                        newRow["Destination"] = item.DestinationCity.CityCode;
                        newRow["ServiceMode"] = item.ServiceMode.ServiceModeCode;
                        newRow["PaymentMode"] = item.PaymentMode.PaymentModeCode;
                        newRow["ItemCount"] = item.PackageNumbers.Count;
                        newRow["ActualWeight"] = item.Weight;
                        newRow["TotalAmount"] = item.ShipmentTotal;
                        newRow["ReceivedBy"] = item.AcceptedBy.FullName;
                        ds.Tables["ShipmentPP"].Rows.Add(newRow);
                    }
                }

                try
                {
                    ReportDocument report = new ReportDocument();
                    report.Load(AppDomain.CurrentDomain.BaseDirectory + "Reports\\TrackingDailyTrip.rpt");
                    report.SetDataSource(ds);
                    FrmTrackingReportViewer reportViewer = new FrmTrackingReportViewer();
                    reportViewer.reportDoc = report;
                    reportViewer.Show();
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
