using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CMS2.BusinessLogic;
using CMS2.Client.ReportModels;
using CrystalDecisions.CrystalReports.Engine;

namespace CMS2.Client
{
    public partial class FrmTrackingAirline : Form
    {
        private TrackingBL service;
        private BindingSource bsOriginBranch;
        private BindingSource bsAirline;
        private BindingSource bsStatus;
        //private GatewayBL gatewayService;
        private BranchCorpOfficeBL bcoService;

        public FrmTrackingAirline()
        {
            InitializeComponent();
        }

        private void FrmTrackingAirline_Load(object sender, EventArgs e)
        {
            service = new TrackingBL();
            bsOriginBranch = new BindingSource();
            bsAirline = new BindingSource();
            bsStatus = new BindingSource();
            bcoService = new BranchCorpOfficeBL();
            //gatewayService = new GatewayBL();
            //var gateways = gatewayService.FilterActive();

            lblMessage.Text = "";
            bsOriginBranch.DataSource = bcoService.FilterActive().OrderBy(x=>x.BranchCorpOfficeName).ToList();
            bsAirline.DataSource = service;
            bsStatus.DataSource = service.GetStatus();

            lstOriginBranch.DataSource = bsOriginBranch;
            lstOriginBranch.DisplayMember = "BranchCorpOfficeName";
            lstOriginBranch.ValueMember = "BranchCorpOfficeName";

            lstAirline.DataSource = bsAirline;
            lstAirline.DisplayMember = "GatewayName";
            lstAirline.ValueMember = "GatewayName";

            lstStatus.DataSource = bsStatus;
            lstStatus.DisplayMember = "cStatusName";
            lstStatus.ValueMember = "cStatusName";
        }

        private void FrmTrackingAirline_Shown(object sender, EventArgs e)
        {
            panel1.Left = (this.Width - panel1.Width) / 2;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            Search(dateTransactionDate.Value.Date, lstAirline.SelectedValue.ToString(), lstOriginBranch.SelectedValue.ToString(), lstStatus.SelectedValue.ToString(), txtMasterAwb.Text.Trim());
        }

        private void Search(DateTime date, string airline, string originBco, string status, string masterAwb)
        {
            var models = service.GetTransmittalByDateAirlineOriginStatusMAwb(date, airline, originBco, status, masterAwb);
            gridAirline.Rows.Clear();
            if (models != null && models.Count > 0)
            {
                string _masterAwb = "";
                int index = 0;
                foreach (var item in models)
                {
                    gridAirline.Rows.Add();
                    gridAirline.Rows[index].Cells["colTransactionDate"].Value = item.TransmittalDateString;
                    gridAirline.Rows[index].Cells["colAirline"].Value = item.Airline;
                    gridAirline.Rows[index].Cells["colOriginBranch"].Value = item.OriginBranch;
                    gridAirline.Rows[index].Cells["colDestinationBranch"].Value = item.DestinationBranchCode;
                    gridAirline.Rows[index].Cells["colStatus"].Value = item.TranmittalStatus;
                    if (string.IsNullOrEmpty(item.MasterAirwayBill))
                    {
                        _masterAwb = "";
                        gridAirline.Rows[index].Cells["colMasterAwb"].ReadOnly = false;
                    }
                    else
                    {
                        _masterAwb = item.MasterAirwayBill;
                        gridAirline.Rows[index].Cells["colMasterAwb"].ReadOnly = true;
                    }
                    gridAirline.Rows[index].Cells["colMasterAwb"].Value = _masterAwb;
                    index++;
                }
            }
            else
            {
                lblMessage.Text = "No record found.";
            }
        }

        private void gridAirline_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;
            int row = e.RowIndex;
            DateTime date = Convert.ToDateTime(grid.Rows[row].Cells["colTransactionDate"].Value);
            string airline = grid.Rows[row].Cells["colAirline"].Value.ToString();
            string originBco = grid.Rows[row].Cells["colOriginBranch"].Value.ToString();
            string destinationBranch = grid.Rows[row].Cells["colDestinationBranch"].Value.ToString();
            string status = grid.Rows[row].Cells["colStatus"].Value.ToString();
            string masterAwb = grid.Rows[row].Cells["colMasterAwb"].Value.ToString();
            if (e.ColumnIndex == 6)
            {
                if (Regex.IsMatch(masterAwb, @"^\d+$"))
                {
                    Save(date, airline, originBco, masterAwb, destinationBranch);
                }
                else
                {
                    lblMessage.Text = "Invalid Master AWB value.";
                }
            }
            else if (e.ColumnIndex == 7)
            {
                Print(masterAwb);
            }
        }

        private void Save(DateTime date, string airline, string originBco, string masterAwb, string destinationBranch)//string status, 
        {
            int affectedRecords = 0;
            affectedRecords = service.UpdateTransmittal(date, airline, originBco,masterAwb, destinationBranch);//, status);
            if (affectedRecords > 0)
            {
                lblMessage.Text = "Successfully Master AWB";
                gridAirline.Rows.Clear();
            }
        }

        private void Print(string masterAwb)
        {
            var models = service.GetTransmittalByMasterAwb(masterAwb);
            if (models != null && models.Count > 0)
            {
                TrackingAirline trackingAirline = new TrackingAirline();
                DataRow newRow = trackingAirline.Tables["Airline"].NewRow();
                newRow["ScannedBy"] = models[0].User;
                trackingAirline.Tables["Airline"].Rows.Add(newRow);

                var awb = models.GroupBy(x => x.AirwayBill)
                    .Select(z => new { AirwayBill = z.Key, CargoCount = z.Count() }).ToList();
                foreach (var item in awb)
                {
                    newRow = trackingAirline.Tables["AirwayBills"].NewRow();
                    newRow["AirwayBill"] = item.AirwayBill;
                    newRow["Count"] = item.CargoCount;
                    newRow["Remarks"] = "";
                    trackingAirline.Tables["AirwayBills"].Rows.Add(newRow);
                }

                try
                {
                    ReportDocument report = new ReportDocument();
                    report.Load(AppDomain.CurrentDomain.BaseDirectory + "Reports\\TrackingAirline.rpt");
                    report.SetDataSource(trackingAirline);
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
