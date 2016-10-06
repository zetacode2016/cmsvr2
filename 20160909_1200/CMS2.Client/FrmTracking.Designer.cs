namespace CMS2.Client
{
    partial class FrmTracking
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelTrackingNavigation = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnInsack = new System.Windows.Forms.Button();
            this.btnDeliveryScoreCard = new System.Windows.Forms.Button();
            this.btnAirline = new System.Windows.Forms.Button();
            this.btnInbound = new System.Windows.Forms.Button();
            this.btnDeliveryStatus = new System.Windows.Forms.Button();
            this.btnRetrieval = new System.Windows.Forms.Button();
            this.btnDailyTrip = new System.Windows.Forms.Button();
            this.panelTrackingContent = new System.Windows.Forms.Panel();
            this.panelTrackingNavigation.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTrackingNavigation
            // 
            this.panelTrackingNavigation.BackColor = System.Drawing.Color.Gray;
            this.panelTrackingNavigation.Controls.Add(this.panel1);
            this.panelTrackingNavigation.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTrackingNavigation.Location = new System.Drawing.Point(0, 0);
            this.panelTrackingNavigation.Name = "panelTrackingNavigation";
            this.panelTrackingNavigation.Size = new System.Drawing.Size(1200, 38);
            this.panelTrackingNavigation.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btnInsack);
            this.panel1.Controls.Add(this.btnDeliveryScoreCard);
            this.panel1.Controls.Add(this.btnAirline);
            this.panel1.Controls.Add(this.btnInbound);
            this.panel1.Controls.Add(this.btnDeliveryStatus);
            this.panel1.Controls.Add(this.btnRetrieval);
            this.panel1.Controls.Add(this.btnDailyTrip);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(451, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(749, 38);
            this.panel1.TabIndex = 8;
            // 
            // btnInsack
            // 
            this.btnInsack.Location = new System.Drawing.Point(131, 1);
            this.btnInsack.Name = "btnInsack";
            this.btnInsack.Size = new System.Drawing.Size(75, 35);
            this.btnInsack.TabIndex = 0;
            this.btnInsack.Text = "Insack";
            this.btnInsack.UseVisualStyleBackColor = true;
            this.btnInsack.Click += new System.EventHandler(this.btnInsack_Click);
            // 
            // btnDeliveryScoreCard
            // 
            this.btnDeliveryScoreCard.Location = new System.Drawing.Point(656, 1);
            this.btnDeliveryScoreCard.Name = "btnDeliveryScoreCard";
            this.btnDeliveryScoreCard.Size = new System.Drawing.Size(75, 35);
            this.btnDeliveryScoreCard.TabIndex = 7;
            this.btnDeliveryScoreCard.Text = "Delivery Score Card";
            this.btnDeliveryScoreCard.UseVisualStyleBackColor = true;
            this.btnDeliveryScoreCard.Click += new System.EventHandler(this.btnDeliveryScoreCard_Click);
            // 
            // btnAirline
            // 
            this.btnAirline.Location = new System.Drawing.Point(206, 1);
            this.btnAirline.Name = "btnAirline";
            this.btnAirline.Size = new System.Drawing.Size(75, 35);
            this.btnAirline.TabIndex = 1;
            this.btnAirline.Text = "Airline";
            this.btnAirline.UseVisualStyleBackColor = true;
            this.btnAirline.Click += new System.EventHandler(this.btnAirline_Click);
            // 
            // btnInbound
            // 
            this.btnInbound.Location = new System.Drawing.Point(281, 1);
            this.btnInbound.Name = "btnInbound";
            this.btnInbound.Size = new System.Drawing.Size(75, 35);
            this.btnInbound.TabIndex = 2;
            this.btnInbound.Text = "Inbound";
            this.btnInbound.UseVisualStyleBackColor = true;
            this.btnInbound.Click += new System.EventHandler(this.btnInbound_Click);
            // 
            // btnDeliveryStatus
            // 
            this.btnDeliveryStatus.Location = new System.Drawing.Point(506, 1);
            this.btnDeliveryStatus.Name = "btnDeliveryStatus";
            this.btnDeliveryStatus.Size = new System.Drawing.Size(75, 35);
            this.btnDeliveryStatus.TabIndex = 5;
            this.btnDeliveryStatus.Text = "Delivery Status";
            this.btnDeliveryStatus.UseVisualStyleBackColor = true;
            this.btnDeliveryStatus.Click += new System.EventHandler(this.btnDeliveryStatus_Click);
            // 
            // btnRetrieval
            // 
            this.btnRetrieval.Location = new System.Drawing.Point(356, 1);
            this.btnRetrieval.Name = "btnRetrieval";
            this.btnRetrieval.Size = new System.Drawing.Size(75, 35);
            this.btnRetrieval.TabIndex = 3;
            this.btnRetrieval.Text = "Retrieval";
            this.btnRetrieval.UseVisualStyleBackColor = true;
            this.btnRetrieval.Click += new System.EventHandler(this.btnRetrieval_Click);
            // 
            // btnDailyTrip
            // 
            this.btnDailyTrip.Location = new System.Drawing.Point(431, 1);
            this.btnDailyTrip.Name = "btnDailyTrip";
            this.btnDailyTrip.Size = new System.Drawing.Size(75, 35);
            this.btnDailyTrip.TabIndex = 4;
            this.btnDailyTrip.Text = "Daily Trip";
            this.btnDailyTrip.UseVisualStyleBackColor = true;
            this.btnDailyTrip.Click += new System.EventHandler(this.btnDailyTrip_Click);
            // 
            // panelTrackingContent
            // 
            this.panelTrackingContent.AutoScroll = true;
            this.panelTrackingContent.BackColor = System.Drawing.Color.Transparent;
            this.panelTrackingContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTrackingContent.Location = new System.Drawing.Point(0, 38);
            this.panelTrackingContent.Name = "panelTrackingContent";
            this.panelTrackingContent.Size = new System.Drawing.Size(1200, 562);
            this.panelTrackingContent.TabIndex = 2;
            // 
            // FrmTracking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(1200, 600);
            this.ControlBox = false;
            this.Controls.Add(this.panelTrackingContent);
            this.Controls.Add(this.panelTrackingNavigation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmTracking";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "FrmTracking";
            this.Load += new System.EventHandler(this.FrmTracking_Load);
            this.Shown += new System.EventHandler(this.FrmTracking_Shown);
            this.panelTrackingNavigation.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTrackingNavigation;
        private System.Windows.Forms.Button btnInsack;
        private System.Windows.Forms.Button btnAirline;
        private System.Windows.Forms.Button btnInbound;
        private System.Windows.Forms.Button btnRetrieval;
        private System.Windows.Forms.Button btnDailyTrip;
        private System.Windows.Forms.Button btnDeliveryStatus;
        private System.Windows.Forms.Button btnDeliveryScoreCard;
        private System.Windows.Forms.Panel panelTrackingContent;
        private System.Windows.Forms.Panel panel1;
    }
}