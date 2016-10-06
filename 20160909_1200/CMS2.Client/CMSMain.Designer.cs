namespace CMS2.Client
{
    partial class CMSMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CMSMain));
            this.mnuMainMenu = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSyncCentral = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAppSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripVersionTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.panelHeading = new System.Windows.Forms.Panel();
            this.panelUser = new System.Windows.Forms.TableLayoutPanel();
            this.lblUserFullname = new System.Windows.Forms.Label();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnTracking = new System.Windows.Forms.Button();
            this.btnPaymentSummary = new System.Windows.Forms.Button();
            this.btnShipmentSummary = new System.Windows.Forms.Button();
            this.btnPayment = new System.Windows.Forms.Button();
            this.btnAcceptance = new System.Windows.Forms.Button();
            this.btnBooking = new System.Windows.Forms.Button();
            this.pictureLogo = new System.Windows.Forms.PictureBox();
            this.panelMainContent = new System.Windows.Forms.Panel();
            this.mnuMainMenu.SuspendLayout();
            this.panelHeading.SuspendLayout();
            this.panelUser.SuspendLayout();
            this.panelMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuMainMenu
            // 
            this.mnuMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuTools,
            this.toolStripVersionTextBox});
            this.mnuMainMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMainMenu.Name = "mnuMainMenu";
            this.mnuMainMenu.Size = new System.Drawing.Size(1284, 24);
            this.mnuMainMenu.TabIndex = 0;
            this.mnuMainMenu.Text = "MenuStrip";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printToolStripMenuItem,
            this.printPreviewToolStripMenuItem,
            this.printSetupToolStripMenuItem,
            this.toolStripSeparator5,
            this.mnuItemExit});
            this.mnuFile.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "&File";
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripMenuItem.Image")));
            this.printToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.printToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.printToolStripMenuItem.Text = "&Print";
            // 
            // printPreviewToolStripMenuItem
            // 
            this.printPreviewToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printPreviewToolStripMenuItem.Image")));
            this.printPreviewToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.printPreviewToolStripMenuItem.Text = "Print Pre&view";
            // 
            // printSetupToolStripMenuItem
            // 
            this.printSetupToolStripMenuItem.Name = "printSetupToolStripMenuItem";
            this.printSetupToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.printSetupToolStripMenuItem.Text = "Print Setup";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(140, 6);
            // 
            // mnuItemExit
            // 
            this.mnuItemExit.Name = "mnuItemExit";
            this.mnuItemExit.Size = new System.Drawing.Size(143, 22);
            this.mnuItemExit.Text = "E&xit";
            this.mnuItemExit.Click += new System.EventHandler(this.ExitToolsStripMenuItem_Click);
            // 
            // mnuTools
            // 
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSyncCentral,
            this.btnAppSetting});
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(47, 20);
            this.mnuTools.Text = "Tools";
            // 
            // mnuSyncCentral
            // 
            this.mnuSyncCentral.Name = "mnuSyncCentral";
            this.mnuSyncCentral.Size = new System.Drawing.Size(140, 22);
            this.mnuSyncCentral.Text = "Sync Central";
            this.mnuSyncCentral.Click += new System.EventHandler(this.mnuSyncCentral_Click);
            // 
            // btnAppSetting
            // 
            this.btnAppSetting.Enabled = false;
            this.btnAppSetting.Name = "btnAppSetting";
            this.btnAppSetting.Size = new System.Drawing.Size(140, 22);
            this.btnAppSetting.Text = "App Setting";
            this.btnAppSetting.Click += new System.EventHandler(this.btnAppSetting_Click);
            // 
            // toolStripVersionTextBox
            // 
            this.toolStripVersionTextBox.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripVersionTextBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.toolStripVersionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.toolStripVersionTextBox.Name = "toolStripVersionTextBox";
            this.toolStripVersionTextBox.Size = new System.Drawing.Size(100, 20);
            this.toolStripVersionTextBox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panelHeading
            // 
            this.panelHeading.BackColor = System.Drawing.Color.DimGray;
            this.panelHeading.Controls.Add(this.panelUser);
            this.panelHeading.Controls.Add(this.panelMenu);
            this.panelHeading.Controls.Add(this.pictureLogo);
            this.panelHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeading.Location = new System.Drawing.Point(0, 24);
            this.panelHeading.Name = "panelHeading";
            this.panelHeading.Size = new System.Drawing.Size(1284, 70);
            this.panelHeading.TabIndex = 9;
            // 
            // panelUser
            // 
            this.panelUser.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.panelUser.AutoSize = true;
            this.panelUser.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelUser.ColumnCount = 2;
            this.panelUser.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelUser.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelUser.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panelUser.Controls.Add(this.lblUserFullname, 0, 0);
            this.panelUser.Controls.Add(this.btnLogOut, 1, 0);
            this.panelUser.Location = new System.Drawing.Point(1189, 2);
            this.panelUser.Name = "panelUser";
            this.panelUser.RowCount = 1;
            this.panelUser.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelUser.Size = new System.Drawing.Size(71, 29);
            this.panelUser.TabIndex = 0;
            // 
            // lblUserFullname
            // 
            this.lblUserFullname.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblUserFullname.AutoSize = true;
            this.lblUserFullname.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserFullname.ForeColor = System.Drawing.Color.White;
            this.lblUserFullname.Location = new System.Drawing.Point(3, 4);
            this.lblUserFullname.Name = "lblUserFullname";
            this.lblUserFullname.Size = new System.Drawing.Size(0, 20);
            this.lblUserFullname.TabIndex = 1;
            this.lblUserFullname.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblUserFullname.Click += new System.EventHandler(this.lblUserFullname_Click);
            // 
            // btnLogOut
            // 
            this.btnLogOut.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnLogOut.Enabled = false;
            this.btnLogOut.Location = new System.Drawing.Point(9, 3);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(59, 23);
            this.btnLogOut.TabIndex = 2;
            this.btnLogOut.TabStop = false;
            this.btnLogOut.Text = "Log Out";
            this.btnLogOut.UseVisualStyleBackColor = true;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // panelMenu
            // 
            this.panelMenu.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.panelMenu.BackColor = System.Drawing.Color.Transparent;
            this.panelMenu.Controls.Add(this.btnTracking);
            this.panelMenu.Controls.Add(this.btnPaymentSummary);
            this.panelMenu.Controls.Add(this.btnShipmentSummary);
            this.panelMenu.Controls.Add(this.btnPayment);
            this.panelMenu.Controls.Add(this.btnAcceptance);
            this.panelMenu.Controls.Add(this.btnBooking);
            this.panelMenu.Location = new System.Drawing.Point(693, 35);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(588, 35);
            this.panelMenu.TabIndex = 15;
            // 
            // btnTracking
            // 
            this.btnTracking.Location = new System.Drawing.Point(484, 0);
            this.btnTracking.Name = "btnTracking";
            this.btnTracking.Size = new System.Drawing.Size(95, 35);
            this.btnTracking.TabIndex = 20;
            this.btnTracking.TabStop = false;
            this.btnTracking.Text = "Tracking";
            this.btnTracking.UseVisualStyleBackColor = true;
            this.btnTracking.Click += new System.EventHandler(this.btnTracking_Click);
            // 
            // btnPaymentSummary
            // 
            this.btnPaymentSummary.Location = new System.Drawing.Point(388, 0);
            this.btnPaymentSummary.Name = "btnPaymentSummary";
            this.btnPaymentSummary.Size = new System.Drawing.Size(95, 35);
            this.btnPaymentSummary.TabIndex = 19;
            this.btnPaymentSummary.TabStop = false;
            this.btnPaymentSummary.Text = "Payment Summary";
            this.btnPaymentSummary.UseVisualStyleBackColor = true;
            this.btnPaymentSummary.Click += new System.EventHandler(this.btnPaymentSummary_Click);
            // 
            // btnShipmentSummary
            // 
            this.btnShipmentSummary.Location = new System.Drawing.Point(292, 0);
            this.btnShipmentSummary.Name = "btnShipmentSummary";
            this.btnShipmentSummary.Size = new System.Drawing.Size(95, 35);
            this.btnShipmentSummary.TabIndex = 18;
            this.btnShipmentSummary.TabStop = false;
            this.btnShipmentSummary.Text = "Manifest";
            this.btnShipmentSummary.UseVisualStyleBackColor = true;
            this.btnShipmentSummary.Click += new System.EventHandler(this.btnShipmentSummary_Click);
            // 
            // btnPayment
            // 
            this.btnPayment.Location = new System.Drawing.Point(196, 0);
            this.btnPayment.Name = "btnPayment";
            this.btnPayment.Size = new System.Drawing.Size(95, 35);
            this.btnPayment.TabIndex = 17;
            this.btnPayment.TabStop = false;
            this.btnPayment.Text = "Payment";
            this.btnPayment.UseVisualStyleBackColor = true;
            this.btnPayment.Click += new System.EventHandler(this.btnPayment_Click);
            // 
            // btnAcceptance
            // 
            this.btnAcceptance.Location = new System.Drawing.Point(100, 0);
            this.btnAcceptance.Name = "btnAcceptance";
            this.btnAcceptance.Size = new System.Drawing.Size(95, 35);
            this.btnAcceptance.TabIndex = 16;
            this.btnAcceptance.TabStop = false;
            this.btnAcceptance.Text = "Acceptance";
            this.btnAcceptance.UseVisualStyleBackColor = true;
            this.btnAcceptance.Click += new System.EventHandler(this.btnAcceptance_Click);
            // 
            // btnBooking
            // 
            this.btnBooking.Location = new System.Drawing.Point(4, 0);
            this.btnBooking.Name = "btnBooking";
            this.btnBooking.Size = new System.Drawing.Size(95, 35);
            this.btnBooking.TabIndex = 15;
            this.btnBooking.TabStop = false;
            this.btnBooking.Text = "Booking";
            this.btnBooking.UseVisualStyleBackColor = true;
            this.btnBooking.Click += new System.EventHandler(this.btnBooking_Click);
            // 
            // pictureLogo
            // 
            this.pictureLogo.Image = global::CMS2.Client.Properties.Resources.logo;
            this.pictureLogo.Location = new System.Drawing.Point(48, 0);
            this.pictureLogo.Name = "pictureLogo";
            this.pictureLogo.Size = new System.Drawing.Size(221, 70);
            this.pictureLogo.TabIndex = 11;
            this.pictureLogo.TabStop = false;
            // 
            // panelMainContent
            // 
            this.panelMainContent.AutoScroll = true;
            this.panelMainContent.BackColor = System.Drawing.Color.Transparent;
            this.panelMainContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContent.Location = new System.Drawing.Point(0, 94);
            this.panelMainContent.Name = "panelMainContent";
            this.panelMainContent.Size = new System.Drawing.Size(1284, 567);
            this.panelMainContent.TabIndex = 11;
            // 
            // CMSMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 661);
            this.Controls.Add(this.panelMainContent);
            this.Controls.Add(this.panelHeading);
            this.Controls.Add(this.mnuMainMenu);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mnuMainMenu;
            this.Name = "CMSMain";
            this.Text = "APCargo CMS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.CMSMain_Load);
            this.Shown += new System.EventHandler(this.CMSMain_Shown);
            this.mnuMainMenu.ResumeLayout(false);
            this.mnuMainMenu.PerformLayout();
            this.panelHeading.ResumeLayout(false);
            this.panelHeading.PerformLayout();
            this.panelUser.ResumeLayout(false);
            this.panelUser.PerformLayout();
            this.panelMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip mnuMainMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem printSetupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuItemExit;
        private System.Windows.Forms.ToolStripMenuItem mnuTools;
        private System.Windows.Forms.ToolStripMenuItem mnuSyncCentral;
        private System.Windows.Forms.Panel panelHeading;
        private System.Windows.Forms.PictureBox pictureLogo;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Button btnPayment;
        private System.Windows.Forms.Button btnAcceptance;
        private System.Windows.Forms.Button btnBooking;
        private System.Windows.Forms.Panel panelMainContent;
        private System.Windows.Forms.Label lblUserFullname;
        private System.Windows.Forms.TableLayoutPanel panelUser;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.Button btnShipmentSummary;
        private System.Windows.Forms.Button btnPaymentSummary;
        private System.Windows.Forms.Button btnTracking;
        private System.Windows.Forms.ToolStripMenuItem btnAppSetting;
        private System.Windows.Forms.ToolStripTextBox toolStripVersionTextBox;
    }
}



