namespace CMS2.Client
{
    partial class FrmTrackingInbound
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gridInbound = new System.Windows.Forms.DataGridView();
            this.colAirwayBill = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDestinationBranch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCountOutbound = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCountInbound = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDifference = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActualWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.dateInboundDate = new System.Windows.Forms.DateTimePicker();
            this.lstOriginBranch = new System.Windows.Forms.ComboBox();
            this.lstAirline = new System.Windows.Forms.ComboBox();
            this.txtMasterAwb = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridInbound)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.gridInbound);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Location = new System.Drawing.Point(57, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(633, 463);
            this.panel1.TabIndex = 0;
            // 
            // gridInbound
            // 
            this.gridInbound.AllowUserToAddRows = false;
            this.gridInbound.AllowUserToDeleteRows = false;
            this.gridInbound.AllowUserToResizeColumns = false;
            this.gridInbound.AllowUserToResizeRows = false;
            this.gridInbound.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridInbound.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridInbound.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridInbound.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAirwayBill,
            this.colDestinationBranch,
            this.colStatus,
            this.colCountOutbound,
            this.colCountInbound,
            this.colDifference,
            this.colActualWeight,
            this.colTotalAmount});
            this.gridInbound.Location = new System.Drawing.Point(1, 220);
            this.gridInbound.Name = "gridInbound";
            this.gridInbound.RowHeadersVisible = false;
            this.gridInbound.RowTemplate.Height = 28;
            this.gridInbound.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridInbound.Size = new System.Drawing.Size(628, 240);
            this.gridInbound.TabIndex = 1;
            // 
            // colAirwayBill
            // 
            this.colAirwayBill.HeaderText = "AWB";
            this.colAirwayBill.Name = "colAirwayBill";
            this.colAirwayBill.ReadOnly = true;
            this.colAirwayBill.Width = 80;
            // 
            // colDestinationBranch
            // 
            this.colDestinationBranch.HeaderText = "Destination";
            this.colDestinationBranch.Name = "colDestinationBranch";
            this.colDestinationBranch.ReadOnly = true;
            // 
            // colStatus
            // 
            this.colStatus.HeaderText = "Category";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.Width = 80;
            // 
            // colCountOutbound
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colCountOutbound.DefaultCellStyle = dataGridViewCellStyle2;
            this.colCountOutbound.HeaderText = "Outbound Count";
            this.colCountOutbound.Name = "colCountOutbound";
            this.colCountOutbound.ReadOnly = true;
            this.colCountOutbound.Width = 60;
            // 
            // colCountInbound
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colCountInbound.DefaultCellStyle = dataGridViewCellStyle3;
            this.colCountInbound.HeaderText = "Inbound Count";
            this.colCountInbound.Name = "colCountInbound";
            this.colCountInbound.ReadOnly = true;
            this.colCountInbound.Width = 60;
            // 
            // colDifference
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colDifference.DefaultCellStyle = dataGridViewCellStyle4;
            this.colDifference.HeaderText = "Difference";
            this.colDifference.Name = "colDifference";
            this.colDifference.ReadOnly = true;
            this.colDifference.Width = 60;
            // 
            // colActualWeight
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colActualWeight.DefaultCellStyle = dataGridViewCellStyle5;
            this.colActualWeight.HeaderText = "Actual Weight";
            this.colActualWeight.Name = "colActualWeight";
            this.colActualWeight.ReadOnly = true;
            this.colActualWeight.Width = 80;
            // 
            // colTotalAmount
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colTotalAmount.DefaultCellStyle = dataGridViewCellStyle6;
            this.colTotalAmount.HeaderText = "Total Amount";
            this.colTotalAmount.Name = "colTotalAmount";
            this.colTotalAmount.ReadOnly = true;
            this.colTotalAmount.Width = 80;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.lblMessage, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.dateInboundDate, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lstOriginBranch, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lstAirline, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtMasterAwb, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.btnSearch, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.btnSave, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.btnPrint, 3, 6);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(163, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(336, 210);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 3);
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(239, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Inbound Cargo";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Date";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Origin BCO";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Airline";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Master AWB";
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMessage.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblMessage, 3);
            this.lblMessage.Location = new System.Drawing.Point(76, 189);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 13);
            this.lblMessage.TabIndex = 5;
            // 
            // dateInboundDate
            // 
            this.dateInboundDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel1.SetColumnSpan(this.dateInboundDate, 2);
            this.dateInboundDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateInboundDate.Location = new System.Drawing.Point(76, 43);
            this.dateInboundDate.Name = "dateInboundDate";
            this.dateInboundDate.Size = new System.Drawing.Size(100, 20);
            this.dateInboundDate.TabIndex = 0;
            // 
            // lstOriginBranch
            // 
            this.lstOriginBranch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel1.SetColumnSpan(this.lstOriginBranch, 2);
            this.lstOriginBranch.FormattingEnabled = true;
            this.lstOriginBranch.Location = new System.Drawing.Point(76, 69);
            this.lstOriginBranch.Name = "lstOriginBranch";
            this.lstOriginBranch.Size = new System.Drawing.Size(166, 21);
            this.lstOriginBranch.TabIndex = 1;
            // 
            // lstAirline
            // 
            this.lstAirline.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel1.SetColumnSpan(this.lstAirline, 2);
            this.lstAirline.FormattingEnabled = true;
            this.lstAirline.Location = new System.Drawing.Point(76, 96);
            this.lstAirline.Name = "lstAirline";
            this.lstAirline.Size = new System.Drawing.Size(166, 21);
            this.lstAirline.TabIndex = 2;
            // 
            // txtMasterAwb
            // 
            this.txtMasterAwb.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel1.SetColumnSpan(this.txtMasterAwb, 2);
            this.txtMasterAwb.Location = new System.Drawing.Point(76, 123);
            this.txtMasterAwb.Name = "txtMasterAwb";
            this.txtMasterAwb.Size = new System.Drawing.Size(100, 20);
            this.txtMasterAwb.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSearch.Location = new System.Drawing.Point(76, 149);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 30);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSave.Location = new System.Drawing.Point(162, 149);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save MAWB";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnPrint.Location = new System.Drawing.Point(248, 149);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(80, 30);
            this.btnPrint.TabIndex = 6;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // FrmTrackingInbound
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(1200, 500);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmTrackingInbound";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "FrmTrackingInbound";
            this.Load += new System.EventHandler(this.FrmTrackingInbound_Load);
            this.Shown += new System.EventHandler(this.FrmTrackingInbound_Shown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridInbound)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView gridInbound;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.DateTimePicker dateInboundDate;
        private System.Windows.Forms.ComboBox lstOriginBranch;
        private System.Windows.Forms.ComboBox lstAirline;
        private System.Windows.Forms.TextBox txtMasterAwb;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAirwayBill;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDestinationBranch;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCountOutbound;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCountInbound;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDifference;
        private System.Windows.Forms.DataGridViewTextBoxColumn colActualWeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalAmount;
    }
}