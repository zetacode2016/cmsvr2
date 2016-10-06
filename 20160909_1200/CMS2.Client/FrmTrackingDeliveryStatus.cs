using System;
using System.Windows.Forms;
using CMS2.BusinessLogic;

namespace CMS2.Client
{
    public partial class FrmTrackingDeliveryStatus : Form
    {
        private TrackingBL service;
        public FrmTrackingDeliveryStatus()
        {
            InitializeComponent();

            service = new TrackingBL();
        }

        private void FrmTrackingDeliveryStatus_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
        }

        private void FrmTrackingDeliveryStatus_Shown(object sender, EventArgs e)
        {
            panel1.Left = (this.Width - panel1.Width) / 2;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            btnSearch.Enabled = false;

            lblMessage.Text = "";
            var models = service.GetShippingStatusByAwb(txtAirwayBill.Text.Trim());
            griddeliveryStatus.Rows.Clear();
            if (models != null && models.Count > 0)
            {
                int index = 0;
                foreach (var item in models)
                {
                    griddeliveryStatus.Rows.Add();
                    griddeliveryStatus.Rows[index].Cells["colStatusDate"].Value = item.StatusDateString;
                    griddeliveryStatus.Rows[index].Cells["colLocation"].Value = item.Location;
                    griddeliveryStatus.Rows[index].Cells["colStatus"].Value = item.Status;
                    griddeliveryStatus.Rows[index].Cells["colUser"].Value = item.User;
                    griddeliveryStatus.Rows[index].Cells["colAirline"].Value = item.Airline;
                    griddeliveryStatus.Rows[index].Cells["colRemark"].Value = item.Remark;
                    griddeliveryStatus.Rows[index].Cells["colNote"].Value = item.Note;
                    index++;
                }
            }
            else
            {
                lblMessage.Text = "No record found.";
            }

            btnSearch.Enabled = true;
        }
    }
}
