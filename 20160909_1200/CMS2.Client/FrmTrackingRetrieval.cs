using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Windows.Forms;
using CMS2.BusinessLogic;
using CMS2.Client.ReportModels;
using CMS2.Entities;
using CrystalDecisions.CrystalReports.Engine;

namespace CMS2.Client
{
    public partial class FrmTrackingRetrieval : Form
    {
        private TrackingBL service;
        private BindingSource bsDestinationBranch;
        private CityBL cityService;
        private List<City> cities;

        public FrmTrackingRetrieval()
        {
            InitializeComponent();

            service = new TrackingBL();
            bsDestinationBranch = new BindingSource();

        }

        private void FrmTrackingRetrieval_Load(object sender, EventArgs e)
        {
            cityService = new CityBL(GlobalVars.UnitOfWork);

            cities = cityService.FilterActive().OrderBy(x => x.CityName).ToList();

            lblMessage.Text = "";
            bsDestinationBranch.DataSource = cities;
            
            lstDestinationBranch.DataSource = bsDestinationBranch;
            lstDestinationBranch.DisplayMember = "CityName";
            lstDestinationBranch.ValueMember = "CityName";
        }

        private void FrmTrackingRetrieval_Shown(object sender, EventArgs e)
        {
            panel1.Left = (this.Width - panel1.Width) / 2;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            btnSearch.Enabled = false;

            lblMessage.Text = "";
            Search(dateRetrievalDate.Value.Date, lstDestinationBranch.SelectedValue.ToString());

            btnSearch.Enabled = true;
        }

        private void Search(DateTime date, string city)
        {
            var models = service.GetRetrievaByDateDestination(date, city);
            gridRetrieval.Rows.Clear();
            if (models != null && models.Count > 0)
            {
                int index = 0;
                foreach (var item in models)
                {
                    gridRetrieval.Rows.Add();
                    gridRetrieval.Rows[index].Cells["colScannedDate"].Value = item.TransactionDateString;
                    gridRetrieval.Rows[index].Cells["colDestinationBranch"].Value = item.DestinationBranchCode;
                    gridRetrieval.Rows[index].Cells["colOriginBranch"].Value = item.OriginBranch;
                    index++;
                }
            }
            else
            {
                lblMessage.Text = "No record found.";
            }
        }
        private void gridRetrieval_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;
            int row = e.RowIndex;
            if (e.ColumnIndex == 3)
            {
                Print(Convert.ToDateTime(grid.Rows[row].Cells["colScanneddate"].Value.ToString()), grid.Rows[row].Cells["colDestinationBranch"].Value.ToString(), grid.Rows[row].Cells["colOriginBranch"].Value.ToString());
            }
        }

        private void Print(DateTime scannedDate, string city, string branchCode)
        {
            TrackingRetrieval ds = new TrackingRetrieval();
            DataRow newRow = ds.Tables["Retrieval"].NewRow();
            newRow["Date"] = scannedDate.ToString("MMM dd, yyyy");
            newRow["Origin"] = branchCode;
            newRow["Destination"] = city;
            ds.Tables["Retrieval"].Rows.Add(newRow); 
            
            var models = service.GetRetrievaByDateDestinationOrigin(scannedDate, city, branchCode);
            if (models != null && models.Count > 0)
            {
                foreach (var item in models)
                {
                    newRow = ds.Tables["AirwayBills"].NewRow();
                    newRow["AirwayBill"] = item.AirwayBillNo;
                    newRow["Count"] = item.CargoNos.Count;
                    newRow["Airline"] = item.Airline;
                    newRow["Status"] = item.Status;
                    ds.Tables["AirwayBills"].Rows.Add(newRow);
                }

                try
                {
                    ReportDocument report = new ReportDocument();
                    report.Load(AppDomain.CurrentDomain.BaseDirectory + "Reports\\TrackingRetrieval.rpt");
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
