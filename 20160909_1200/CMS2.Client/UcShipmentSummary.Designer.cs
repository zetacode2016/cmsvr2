namespace CMS2.Client
{
    partial class UcShipmentSummary
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gridShipmentSummary = new System.Windows.Forms.DataGridView();
            this.btnRefreshGrid = new System.Windows.Forms.Button();
            this.colAwbNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDateAccepted = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAccountNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colShipper = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOriginCity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colConsignee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDestinationCity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChargeableWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVatAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colServiceMode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaymentMode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAcceptedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridShipmentSummary)).BeginInit();
            this.SuspendLayout();
            // 
            // gridShipmentSummary
            // 
            this.gridShipmentSummary.AllowUserToAddRows = false;
            this.gridShipmentSummary.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridShipmentSummary.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridShipmentSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridShipmentSummary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAwbNo,
            this.colDateAccepted,
            this.colAccountNo,
            this.colShipper,
            this.colOriginCity,
            this.colConsignee,
            this.colDestinationCity,
            this.colQuantity,
            this.colChargeableWeight,
            this.colSubTotal,
            this.colVatAmount,
            this.colTotal,
            this.colServiceMode,
            this.colPaymentMode,
            this.colAcceptedBy});
            this.gridShipmentSummary.Location = new System.Drawing.Point(20, 130);
            this.gridShipmentSummary.MultiSelect = false;
            this.gridShipmentSummary.Name = "gridShipmentSummary";
            this.gridShipmentSummary.ReadOnly = true;
            this.gridShipmentSummary.RowHeadersVisible = false;
            this.gridShipmentSummary.Size = new System.Drawing.Size(1196, 378);
            this.gridShipmentSummary.TabIndex = 0;
            this.gridShipmentSummary.TabStop = false;
            // 
            // btnRefreshGrid
            // 
            this.btnRefreshGrid.Location = new System.Drawing.Point(20, 88);
            this.btnRefreshGrid.Name = "btnRefreshGrid";
            this.btnRefreshGrid.Size = new System.Drawing.Size(95, 30);
            this.btnRefreshGrid.TabIndex = 1;
            this.btnRefreshGrid.TabStop = false;
            this.btnRefreshGrid.Text = "Refresh Grid";
            this.btnRefreshGrid.UseVisualStyleBackColor = true;
            this.btnRefreshGrid.Click += new System.EventHandler(this.btnRefreshGrid_Click);
            // 
            // colAwbNo
            // 
            this.colAwbNo.HeaderText = "AWB No";
            this.colAwbNo.Name = "colAwbNo";
            this.colAwbNo.ReadOnly = true;
            this.colAwbNo.Width = 80;
            // 
            // colDateAccepted
            // 
            this.colDateAccepted.HeaderText = "Date Accepted";
            this.colDateAccepted.Name = "colDateAccepted";
            this.colDateAccepted.ReadOnly = true;
            this.colDateAccepted.Width = 80;
            // 
            // colAccountNo
            // 
            this.colAccountNo.HeaderText = "Account No";
            this.colAccountNo.Name = "colAccountNo";
            this.colAccountNo.ReadOnly = true;
            this.colAccountNo.Width = 90;
            // 
            // colShipper
            // 
            this.colShipper.HeaderText = "Shipper";
            this.colShipper.Name = "colShipper";
            this.colShipper.ReadOnly = true;
            this.colShipper.Width = 120;
            // 
            // colOriginCity
            // 
            this.colOriginCity.HeaderText = "Origin City";
            this.colOriginCity.Name = "colOriginCity";
            this.colOriginCity.ReadOnly = true;
            this.colOriginCity.Width = 120;
            // 
            // colConsignee
            // 
            this.colConsignee.HeaderText = "Consignee";
            this.colConsignee.Name = "colConsignee";
            this.colConsignee.ReadOnly = true;
            this.colConsignee.Width = 120;
            // 
            // colDestinationCity
            // 
            this.colDestinationCity.HeaderText = "Destination City";
            this.colDestinationCity.Name = "colDestinationCity";
            this.colDestinationCity.ReadOnly = true;
            this.colDestinationCity.Width = 120;
            // 
            // colQuantity
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colQuantity.DefaultCellStyle = dataGridViewCellStyle2;
            this.colQuantity.HeaderText = "Qty";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.ReadOnly = true;
            this.colQuantity.Width = 40;
            // 
            // colChargeableWeight
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colChargeableWeight.DefaultCellStyle = dataGridViewCellStyle3;
            this.colChargeableWeight.HeaderText = "Chargeable Wt";
            this.colChargeableWeight.Name = "colChargeableWeight";
            this.colChargeableWeight.ReadOnly = true;
            this.colChargeableWeight.Width = 75;
            // 
            // colSubTotal
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colSubTotal.DefaultCellStyle = dataGridViewCellStyle4;
            this.colSubTotal.HeaderText = "Sub-Total";
            this.colSubTotal.Name = "colSubTotal";
            this.colSubTotal.ReadOnly = true;
            this.colSubTotal.Width = 75;
            // 
            // colVatAmount
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colVatAmount.DefaultCellStyle = dataGridViewCellStyle5;
            this.colVatAmount.HeaderText = "Vat Amount";
            this.colVatAmount.Name = "colVatAmount";
            this.colVatAmount.ReadOnly = true;
            this.colVatAmount.Width = 70;
            // 
            // colTotal
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colTotal.DefaultCellStyle = dataGridViewCellStyle6;
            this.colTotal.HeaderText = "Total";
            this.colTotal.Name = "colTotal";
            this.colTotal.ReadOnly = true;
            this.colTotal.Width = 75;
            // 
            // colServiceMode
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colServiceMode.DefaultCellStyle = dataGridViewCellStyle7;
            this.colServiceMode.HeaderText = "Service Mode";
            this.colServiceMode.Name = "colServiceMode";
            this.colServiceMode.ReadOnly = true;
            this.colServiceMode.Width = 80;
            // 
            // colPaymentMode
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colPaymentMode.DefaultCellStyle = dataGridViewCellStyle8;
            this.colPaymentMode.HeaderText = "Pay Mode";
            this.colPaymentMode.Name = "colPaymentMode";
            this.colPaymentMode.ReadOnly = true;
            this.colPaymentMode.Width = 70;
            // 
            // colAcceptedBy
            // 
            this.colAcceptedBy.HeaderText = "AcceptedBy";
            this.colAcceptedBy.Name = "colAcceptedBy";
            this.colAcceptedBy.ReadOnly = true;
            this.colAcceptedBy.Width = 120;
            // 
            // UcShipmentSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.btnRefreshGrid);
            this.Controls.Add(this.gridShipmentSummary);
            this.Name = "UcShipmentSummary";
            this.Size = new System.Drawing.Size(1250, 580);
            this.Load += new System.EventHandler(this.UcShipmentSummary_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridShipmentSummary)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridShipmentSummary;
        private System.Windows.Forms.Button btnRefreshGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAwbNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDateAccepted;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAccountNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colShipper;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOriginCity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colConsignee;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDestinationCity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChargeableWeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVatAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServiceMode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaymentMode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAcceptedBy;
    }
}
