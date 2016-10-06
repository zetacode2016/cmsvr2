using System;
using System.Windows.Forms;

namespace CMS2.Client
{
    public partial class FrmTracking : Form
    {
        private FrmTrackingInsack insackForm;
        private FrmTrackingAirline airlineForm;
        private FrmTrackingInbound inboundForm;
        private FrmTrackingRetrieval retrievalForm;
        private FrmTrackingDailyTrip dailyTripForm;
        private FrmTrackingDeliveryStatus deliveryStatusForm;
        //private FrmTrackingCargoMonitoring cargoMonitoringForm;

        public FrmTracking()
        {
            InitializeComponent();

            insackForm = new FrmTrackingInsack();
            airlineForm = new FrmTrackingAirline();
            inboundForm = new FrmTrackingInbound();
            retrievalForm = new FrmTrackingRetrieval();
            dailyTripForm = new FrmTrackingDailyTrip();
            deliveryStatusForm = new FrmTrackingDeliveryStatus();
            //cargoMonitoringForm = new FrmTrackingCargoMonitoring();

            insackForm.TopLevel = false;
            airlineForm.TopLevel = false;
            inboundForm.TopLevel = false;
            retrievalForm.TopLevel = false;
            dailyTripForm.TopLevel = false;
            deliveryStatusForm.TopLevel = false;
            //cargoMonitoringForm.TopLevel = false;

            panelTrackingContent.Controls.Add(insackForm);
            panelTrackingContent.Controls.Add(airlineForm);
            panelTrackingContent.Controls.Add(inboundForm);
            panelTrackingContent.Controls.Add(retrievalForm);
            panelTrackingContent.Controls.Add(dailyTripForm);
            panelTrackingContent.Controls.Add(deliveryStatusForm);
            //panelTrackingContent.Controls.Add(cargoMonitoringForm);

            panelTrackingContent.Width = this.Width;
            panelTrackingContent.Height = this.Height;
        }

        private void FrmTracking_Load(object sender, EventArgs e)
        {
            //TntMaintBL service = new TntMaintBL();
            //service.UpdateCms();
        }

        private void FrmTracking_Shown(object sender, EventArgs e)
        {
            insackForm.Hide();
            airlineForm.Hide();
            inboundForm.Hide();
            retrievalForm.Hide();
            dailyTripForm.Hide();
            deliveryStatusForm.Hide();
            //cargoMonitoringForm.Hide();
        }

        private void btnInsack_Click(object sender, EventArgs e)
        {
            insackForm.Show();
            airlineForm.Hide();
            inboundForm.Hide();
            retrievalForm.Hide();
            dailyTripForm.Hide();
            deliveryStatusForm.Hide();
            //cargoMonitoringForm.Hide();

            insackForm.Width = panelTrackingContent.Width;
            insackForm.Height = panelTrackingContent.Height;
        }

        private void btnAirline_Click(object sender, EventArgs e)
        {
            insackForm.Hide();
            airlineForm.Show();
            inboundForm.Hide();
            retrievalForm.Hide();
            dailyTripForm.Hide();
            deliveryStatusForm.Hide();
            //cargoMonitoringForm.Hide();

            airlineForm.Width = panelTrackingContent.Width;
            airlineForm.Height = panelTrackingContent.Height;
        }

        private void btnInbound_Click(object sender, EventArgs e)
        {
            insackForm.Hide();
            airlineForm.Hide();
            inboundForm.Show();
            retrievalForm.Hide();
            dailyTripForm.Hide();
            deliveryStatusForm.Hide();
            //cargoMonitoringForm.Hide();

            inboundForm.Width = panelTrackingContent.Width;
            inboundForm.Height = panelTrackingContent.Height;
        }

        private void btnRetrieval_Click(object sender, EventArgs e)
        {
            insackForm.Hide();
            airlineForm.Hide();
            inboundForm.Hide();
            retrievalForm.Show();
            dailyTripForm.Hide();
            deliveryStatusForm.Hide();
            //cargoMonitoringForm.Hide();

            retrievalForm.Width = panelTrackingContent.Width;
            retrievalForm.Height = panelTrackingContent.Height;
        }

        private void btnDailyTrip_Click(object sender, EventArgs e)
        {
            insackForm.Hide();
            airlineForm.Hide();
            inboundForm.Hide();
            retrievalForm.Hide();
            dailyTripForm.Show();
            deliveryStatusForm.Hide();
            //cargoMonitoringForm.Hide();

            retrievalForm.Width = panelTrackingContent.Width;
            retrievalForm.Height = panelTrackingContent.Height;
        }

        private void btnDeliveryStatus_Click(object sender, EventArgs e)
        {
            insackForm.Hide();
            airlineForm.Hide();
            inboundForm.Hide();
            retrievalForm.Hide();
            dailyTripForm.Hide();
            deliveryStatusForm.Show();
            //cargoMonitoringForm.Hide();

            deliveryStatusForm.Width = panelTrackingContent.Width;
            deliveryStatusForm.Height = panelTrackingContent.Height;
        }

        private void btnCargoMonitoring_Click(object sender, EventArgs e)
        {
            insackForm.Hide();
            airlineForm.Hide();
            inboundForm.Hide();
            retrievalForm.Hide();
            dailyTripForm.Hide();
            deliveryStatusForm.Hide();
            //cargoMonitoringForm.Show();

            //cargoMonitoringForm.Width = panelTrackingContent.Width;
            //cargoMonitoringForm.Height = panelTrackingContent.Height;
        }

        private void btnDeliveryScoreCard_Click(object sender, EventArgs e)
        {
            insackForm.Hide();
            airlineForm.Hide();
            inboundForm.Hide();
            retrievalForm.Hide();
            dailyTripForm.Hide();
            deliveryStatusForm.Hide();
            //cargoMonitoringForm.Hide();
        }
    }
}
