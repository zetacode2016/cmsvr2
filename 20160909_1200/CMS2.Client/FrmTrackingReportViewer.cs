using System;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

namespace CMS2.Client
{
    public partial class FrmTrackingReportViewer : Form
    {
        public ReportDocument reportDoc;
        public FrmTrackingReportViewer()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            crystalReportViewer1.ReportSource = reportDoc;
            crystalReportViewer1.RefreshReport();
        }
    }
}
