using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CMS2.BusinessLogic;
using CMS2.Client.ReportModels;
using CrystalDecisions.CrystalReports.Engine;

namespace CMS2.Client
{
    public partial class FrmTrackingInsack : Form
    {
        private BindingSource bsOriginBranch;
        private TrackingBL service;
        BranchCorpOfficeBL bcoService;

        public FrmTrackingInsack()
        {
            InitializeComponent();

           
        }

        private void FrmTrackingInsack_Load(object sender, EventArgs e)
        {
            service = new TrackingBL();
            bsOriginBranch = new BindingSource();
            bcoService = new BranchCorpOfficeBL();

            lblMessage.Text = "";
            bsOriginBranch.DataSource = bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).ToList();

            lstOriginBranch.DataSource = bsOriginBranch;
            lstOriginBranch.DisplayMember = "BranchCorpOfficeName";
            lstOriginBranch.ValueMember = "BranchCorpOfficeId";
        }

        private void FrmTrackingInsack_Shown(object sender, EventArgs e)
        {
            panel1.Left = (this.Width - panel1.Width) / 2;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            string bco =
                bcoService.FilterActiveBy(x => x.BranchCorpOfficeId == Guid.Parse(lstOriginBranch.SelectedValue.ToString())).FirstOrDefault().BranchCorpOfficeName;
            Search(dateTransactionDate.Value.Date, bco);
        }

        private void gridInsack_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;
            int row = e.RowIndex;
            string sackNo = grid.Rows[row].Cells["colSackno"].Value.ToString();
            if (e.ColumnIndex == 4)
            {
                decimal weight = 0;
                if (decimal.TryParse(grid.Rows[row].Cells["colSackWeight"].Value.ToString(), out weight))
                {
                    Save(sackNo, weight);
                }
                else
                {
                    lblMessage.Text = "Invalid weight value.";
                }
            }
            else if (e.ColumnIndex == 5)
            {
                Print(sackNo);
            }

        }

        private void Search(DateTime date, string originBco)
        {
            var models = service.GetBundlesByDateOriginBCO(date, originBco);
            gridInsack.Rows.Clear();
            int index = 0;
            decimal weight = 0;
            if (models != null)
            {
                foreach (var item in models)
                {
                    if (item.Weight != null)
                        weight = item.Weight;
                    gridInsack.Rows.Add();
                    gridInsack.Rows[index].Cells["colSackNo"].Value = item.SackNo;
                    gridInsack.Rows[index].Cells["colItemCount"].Value = item.CargoNos.Count;
                    gridInsack.Rows[index].Cells["colDestinationBranch"].Value = item.DestinationCityCode;
                    gridInsack.Rows[index].Cells["colSackWeight"].Value = weight;
                    index++;
                    weight = 0;
                }
            }
            else
            {
                lblMessage.Text = "No record found.";
            }
        }

        private void Save(string sackNo, decimal weight)
        {
            int affectedRecords = 0;
            affectedRecords = service.UpdateSackWeight(sackNo, weight);
            if (affectedRecords > 0)
            {
                gridInsack.Rows.Clear();
                lblMessage.Text = "Weight successfully saved.";
            }
        }

        private void Print(string sackNo)
        {
            var model = service.GetBundlesBySackNo(sackNo);
            if (model != null)
            {
                TrackingInsack trackingInsack = new TrackingInsack();
                DataRow newRow = trackingInsack.Tables["Bundle"].NewRow();
                newRow["BundleDate"] = model[0].TransactionDateString;
                newRow["SackNo"] = model[0].SackNo;
                newRow["OriginBranch"] = model[0].OriginCity;
                newRow["DestinationBranch"] = model[0].DestinationCityCode;
                newRow["User"] = model[0].User;
                newRow["PrintTime"] = DateTime.Now.ToString("hh:mm tt");
                trackingInsack.Tables["Bundle"].Rows.Add(newRow);

                if (model[0].CargoNos != null && model[0].CargoNos.Count > 0)
                {
                    var awb = model[0].CargoNos.GroupBy(x => x.AirwayBill)
                        .Select(z => new { AirwayBill = z.Key, CargoCount =z.Count()}).ToList();
                    foreach (var item in awb)
                    {
                        newRow = trackingInsack.Tables["BundleItems"].NewRow();
                        newRow["AirwayBill"] = item.AirwayBill;
                        newRow["Count"] = item.CargoCount;
                        newRow["Remarks"] = "";
                        trackingInsack.Tables["BundleItems"].Rows.Add(newRow);    
                    }
                }

                try
                {
                    ReportDocument report = new ReportDocument();
                    report.Load(AppDomain.CurrentDomain.BaseDirectory + "Reports\\TrackingInsack.rpt");
                    report.SetDataSource(trackingInsack);
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
