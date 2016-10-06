namespace CMS2.Client
{
    partial class FrmTrackingAirline
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.gridAirline = new System.Windows.Forms.DataGridView();
            this.colTransactionDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAirline = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOriginBranch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDestinationBranch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMasterAwb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSave = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colPrint = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.dateTransactionDate = new System.Windows.Forms.DateTimePicker();
            this.lstAirline = new System.Windows.Forms.ComboBox();
            this.lstOriginBranch = new System.Windows.Forms.ComboBox();
            this.lstStatus = new System.Windows.Forms.ComboBox();
            this.txtMasterAwb = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridAirline)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.gridAirline);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Location = new System.Drawing.Point(188, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(755, 530);
            this.panel1.TabIndex = 0;
            // 
            // gridAirline
            // 
            this.gridAirline.AllowUserToAddRows = false;
            this.gridAirline.AllowUserToDeleteRows = false;
            this.gridAirline.AllowUserToResizeColumns = false;
            this.gridAirline.AllowUserToResizeRows = false;
            this.gridAirline.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridAirline.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridAirline.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridAirline.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTransactionDate,
            this.colAirline,
            this.colOriginBranch,
            this.colDestinationBranch,
            this.colStatus,
            this.colMasterAwb,
            this.colSave,
            this.colPrint});
            this.gridAirline.Location = new System.Drawing.Point(1, 245);
            this.gridAirline.MultiSelect = false;
            this.gridAirline.Name = "gridAirline";
            this.gridAirline.RowHeadersVisible = false;
            this.gridAirline.RowTemplate.Height = 28;
            this.gridAirline.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridAirline.Size = new System.Drawing.Size(752, 282);
            this.gridAirline.TabIndex = 1;
            this.gridAirline.TabStop = false;
            this.gridAirline.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridAirline_CellContentClick);
            // 
            // colTransactionDate
            // 
            this.colTransactionDate.HeaderText = "Date";
            this.colTransactionDate.Name = "colTransactionDate";
            this.colTransactionDate.ReadOnly = true;
            // 
            // colAirline
            // 
            this.colAirline.HeaderText = "Airline";
            this.colAirline.Name = "colAirline";
            this.colAirline.ReadOnly = true;
            // 
            // colOriginBranch
            // 
            this.colOriginBranch.HeaderText = "Origin";
            this.colOriginBranch.Name = "colOriginBranch";
            this.colOriginBranch.ReadOnly = true;
            // 
            // colDestinationBranch
            // 
            this.colDestinationBranch.HeaderText = "Destination";
            this.colDestinationBranch.Name = "colDestinationBranch";
            this.colDestinationBranch.ReadOnly = true;
            // 
            // colStatus
            // 
            this.colStatus.HeaderText = "Status/Type";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            // 
            // colMasterAwb
            // 
            this.colMasterAwb.HeaderText = "Master AWB";
            this.colMasterAwb.Name = "colMasterAwb";
            // 
            // colSave
            // 
            this.colSave.HeaderText = "";
            this.colSave.Name = "colSave";
            this.colSave.Text = "Save";
            this.colSave.UseColumnTextForButtonValue = true;
            this.colSave.Width = 60;
            // 
            // colPrint
            // 
            this.colPrint.HeaderText = "";
            this.colPrint.Name = "colPrint";
            this.colPrint.Text = "Print";
            this.colPrint.UseColumnTextForButtonValue = true;
            this.colPrint.Width = 60;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.lblMessage, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.dateTransactionDate, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lstAirline, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lstOriginBranch, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lstStatus, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtMasterAwb, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.btnSearch, 1, 7);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(232, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 9;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(242, 234);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 2);
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Airline Transmittal";
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
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Airline";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Origin BCO";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Status/Type";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 153);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Master AWB";
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(76, 215);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 13);
            this.lblMessage.TabIndex = 6;
            // 
            // dateTransactionDate
            // 
            this.dateTransactionDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dateTransactionDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTransactionDate.Location = new System.Drawing.Point(76, 43);
            this.dateTransactionDate.Name = "dateTransactionDate";
            this.dateTransactionDate.Size = new System.Drawing.Size(97, 20);
            this.dateTransactionDate.TabIndex = 0;
            // 
            // lstAirline
            // 
            this.lstAirline.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lstAirline.FormattingEnabled = true;
            this.lstAirline.Location = new System.Drawing.Point(76, 69);
            this.lstAirline.Name = "lstAirline";
            this.lstAirline.Size = new System.Drawing.Size(162, 21);
            this.lstAirline.TabIndex = 1;
            // 
            // lstOriginBranch
            // 
            this.lstOriginBranch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lstOriginBranch.FormattingEnabled = true;
            this.lstOriginBranch.Location = new System.Drawing.Point(76, 96);
            this.lstOriginBranch.Name = "lstOriginBranch";
            this.lstOriginBranch.Size = new System.Drawing.Size(162, 21);
            this.lstOriginBranch.TabIndex = 2;
            // 
            // lstStatus
            // 
            this.lstStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lstStatus.FormattingEnabled = true;
            this.lstStatus.Location = new System.Drawing.Point(76, 123);
            this.lstStatus.Name = "lstStatus";
            this.lstStatus.Size = new System.Drawing.Size(162, 21);
            this.lstStatus.TabIndex = 3;
            // 
            // txtMasterAwb
            // 
            this.txtMasterAwb.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtMasterAwb.Location = new System.Drawing.Point(76, 150);
            this.txtMasterAwb.Name = "txtMasterAwb";
            this.txtMasterAwb.Size = new System.Drawing.Size(100, 20);
            this.txtMasterAwb.TabIndex = 4;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSearch.Location = new System.Drawing.Point(76, 176);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 30);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // FrmTrackingAirline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(1200, 579);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmTrackingAirline";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "FrmTrackingAirline";
            this.Load += new System.EventHandler(this.FrmTrackingAirline_Load);
            this.Shown += new System.EventHandler(this.FrmTrackingAirline_Shown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridAirline)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView gridAirline;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.DateTimePicker dateTransactionDate;
        private System.Windows.Forms.ComboBox lstAirline;
        private System.Windows.Forms.ComboBox lstOriginBranch;
        private System.Windows.Forms.ComboBox lstStatus;
        private System.Windows.Forms.TextBox txtMasterAwb;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTransactionDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAirline;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOriginBranch;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDestinationBranch;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMasterAwb;
        private System.Windows.Forms.DataGridViewButtonColumn colSave;
        private System.Windows.Forms.DataGridViewButtonColumn colPrint;
    }
}