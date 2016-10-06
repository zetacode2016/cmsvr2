using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CMS2.BusinessLogic;
using CMS2.Client.ReportModels;
using CrystalDecisions.CrystalReports.Engine;

namespace CMS2.Client
{
    public partial class FrmTrackingInbound : Form
    {
        private TrackingBL service;
        //private GatewayBL gatewayService;
        private BindingSource bsOriginBranch;
        private BindingSource bsAirline;
        BranchCorpOfficeBL bcoService;

        public FrmTrackingInbound()
        {
            InitializeComponent();

           
        }

        private void FrmTrackingInbound_Load(object sender, EventArgs e)
        {
            bcoService = new BranchCorpOfficeBL();
            service = new TrackingBL();
            bsOriginBranch = new BindingSource();
            bsAirline = new BindingSource();
            //gatewayService = new GatewayBL();
            lblMessage.Text = "";
            bsOriginBranch.DataSource = bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).ToList();
            //bsAirline.DataSource = gatewayService.FilterActive().ToList();

            lstOriginBranch.DataSource = bsOriginBranch;
            lstOriginBranch.DisplayMember = "BranchCorpOfficeName";
            lstOriginBranch.ValueMember = "BranchCorpOfficeName";

            lstAirline.DataSource = bsAirline;
            lstAirline.DisplayMember = "GatewayName";
            lstAirline.ValueMember = "GatewayName";
        }

        private void FrmTrackingInbound_Shown(object sender, EventArgs e)
        {
            panel1.Left = (this.Width - panel1.Width) / 2;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            btnSearch.Enabled = false;
            btnSave.Enabled = false;
            btnPrint.Enabled = false;

            lblMessage.Text = "";
            Search(dateInboundDate.Value.Date, lstOriginBranch.SelectedValue.ToString(), lstAirline.SelectedValue.ToString(), txtMasterAwb.Text.Trim());

            btnSearch.Enabled = true;
            btnSave.Enabled = true;
            btnPrint.Enabled = true;
        }

        private void Search(DateTime date, string originBco, string gateway, string masterAwb)
        {
            var models = service.GetInboundByDateOriginAirlineMAwb(date, originBco, gateway, masterAwb);
            gridInbound.Rows.Clear();
            gridInbound.Refresh();
            if (models != null && models.Count > 0)
            {
                int index = 0;
                foreach (var item in models)
                {
                    gridInbound.Rows.Add();
                    gridInbound.Rows[index].Cells["colAirwayBill"].Value = item.AirwayBill;
                    gridInbound.Rows[index].Cells["colDestinationBranch"].Value = item.DestinationBranchCode;
                    gridInbound.Rows[index].Cells["colStatus"].Value = item.Status;
                    gridInbound.Rows[index].Cells["colCountOutbound"].Value = item.CountOutboundString;
                    gridInbound.Rows[index].Cells["colCountInbound"].Value = item.CountInboundString;
                    gridInbound.Rows[index].Cells["colDifference"].Value = item.DifferenceString;
                    gridInbound.Rows[index].Cells["colActualWeight"].Value = item.ActualWeightString;
                    gridInbound.Rows[index].Cells["colTotalAmount"].Value = item.TotalAmountString;
                    index++;
                }
            }
            else
            {
                lblMessage.Text = "No record found.";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnSearch.Enabled = false;
            btnSave.Enabled = false;
            btnPrint.Enabled = false;

            lblMessage.Text = "";
            List<string> awbs = new List<string>();
            for (int x = 0; x < gridInbound.RowCount; x++)
            {
                var awb = gridInbound.Rows[x].Cells["colAirwayBill"].Value;
                if (awb!=null)
                { awbs.Add(awb.ToString()); }
            }
            if (string.IsNullOrEmpty(txtMasterAwb.Text.Trim()))
            {
                lblMessage.Text = "Invalid Master AWB.";
            }
            else
            {Save(awbs,txtMasterAwb.Text.Trim());}

            btnSearch.Enabled = true;
            btnSave.Enabled = true;
            btnPrint.Enabled = true;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            Print(dateInboundDate.Value.Date, lstOriginBranch.SelectedValue.ToString(), lstAirline.SelectedValue.ToString(), txtMasterAwb.Text.Trim());
        }

        private void Save(List<string> awbs, string masterAwb )
        {
            lblMessage.Text = "";
            int affectedRecords = 0;
            affectedRecords = service.UpdateInbound(awbs, masterAwb);
            if (affectedRecords > 0)
            {
                lblMessage.Text = "Successfully Master AWB";
                gridInbound.Rows.Clear();
                txtMasterAwb.Text = "";
            }
        }

        private void Print(DateTime date, string originBranch, string airline, string masterAwb)
        {
            var models = service.GetInboundByDateOriginAirlineMAwb(date, originBranch, airline, masterAwb);
            if (models != null && models.Count > 0)
            {
                TrackingInbound trackingInbound = new TrackingInbound();
                DataRow newRow = trackingInbound.Tables["Inbound"].NewRow();
                newRow["TransactionDate"] = date.Date.ToString("MMM dd, yyyy");
                newRow["Branch"] = originBranch;
                newRow["Airline"] = airline;
                trackingInbound.Tables["Inbound"].Rows.Add(newRow);
                
                foreach (var item in models)
                {
                    newRow = trackingInbound.Tables["InboundAwbs"].NewRow();
                    newRow["AirwayBill"] = item.AirwayBill;
                    newRow["DestinationBranch"] = item.DestinationBranchCode;
                    newRow["Status"] = item.Status;
                    newRow["CountOutbound"] = item.CountOutbound;
                    newRow["CountInbound"] = item.CountInbound;
                    newRow["Difference"] = item.Difference;
                    newRow["ActualWeight"] = item.ActualWeight; 
                    newRow["TotalAmount"] = item.TotalAmount;
                    newRow["ScannedBy"] = item.User;
                    trackingInbound.Tables["InboundAwbs"].Rows.Add(newRow);
                }

                try
                {
                    ReportDocument report = new ReportDocument();
                    report.Load(AppDomain.CurrentDomain.BaseDirectory + "Reports\\TrackingInbound.rpt");
                    report.SetDataSource(trackingInbound);
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
