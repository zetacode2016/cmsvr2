namespace CMS2.Client
{
    partial class FrmTrackingRetrieval
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
            this.gridRetrieval = new System.Windows.Forms.DataGridView();
            this.colDestinationBranch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOriginBranch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colScannedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrint = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.dateRetrievalDate = new System.Windows.Forms.DateTimePicker();
            this.lstDestinationBranch = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRetrieval)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.gridRetrieval);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Location = new System.Drawing.Point(39, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(393, 452);
            this.panel1.TabIndex = 0;
            // 
            // gridRetrieval
            // 
            this.gridRetrieval.AllowUserToAddRows = false;
            this.gridRetrieval.AllowUserToDeleteRows = false;
            this.gridRetrieval.AllowUserToResizeColumns = false;
            this.gridRetrieval.AllowUserToResizeRows = false;
            this.gridRetrieval.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridRetrieval.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridRetrieval.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridRetrieval.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDestinationBranch,
            this.colOriginBranch,
            this.colScannedDate,
            this.colPrint});
            this.gridRetrieval.Location = new System.Drawing.Point(1, 183);
            this.gridRetrieval.Name = "gridRetrieval";
            this.gridRetrieval.RowHeadersVisible = false;
            this.gridRetrieval.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridRetrieval.Size = new System.Drawing.Size(389, 266);
            this.gridRetrieval.TabIndex = 1;
            this.gridRetrieval.TabStop = false;
            this.gridRetrieval.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridRetrieval_CellContentClick);
            // 
            // colDestinationBranch
            // 
            this.colDestinationBranch.HeaderText = "Destination";
            this.colDestinationBranch.Name = "colDestinationBranch";
            this.colDestinationBranch.ReadOnly = true;
            // 
            // colOriginBranch
            // 
            this.colOriginBranch.HeaderText = "Origin";
            this.colOriginBranch.Name = "colOriginBranch";
            this.colOriginBranch.ReadOnly = true;
            // 
            // colScannedDate
            // 
            this.colScannedDate.HeaderText = "Scanned Date";
            this.colScannedDate.Name = "colScannedDate";
            this.colScannedDate.ReadOnly = true;
            // 
            // colPrint
            // 
            this.colPrint.HeaderText = "";
            this.colPrint.Name = "colPrint";
            this.colPrint.ReadOnly = true;
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
            this.tableLayoutPanel1.Controls.Add(this.lblMessage, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.dateRetrievalDate, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lstDestinationBranch, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnSearch, 1, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(56, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(267, 166);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 2);
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(261, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Retrieval Report";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Date";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Destination City";
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(89, 147);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 13);
            this.lblMessage.TabIndex = 3;
            // 
            // dateRetrievalDate
            // 
            this.dateRetrievalDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dateRetrievalDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateRetrievalDate.Location = new System.Drawing.Point(89, 53);
            this.dateRetrievalDate.Name = "dateRetrievalDate";
            this.dateRetrievalDate.Size = new System.Drawing.Size(102, 20);
            this.dateRetrievalDate.TabIndex = 0;
            // 
            // lstDestinationBranch
            // 
            this.lstDestinationBranch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lstDestinationBranch.FormattingEnabled = true;
            this.lstDestinationBranch.Location = new System.Drawing.Point(89, 79);
            this.lstDestinationBranch.Name = "lstDestinationBranch";
            this.lstDestinationBranch.Size = new System.Drawing.Size(156, 21);
            this.lstDestinationBranch.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSearch.Location = new System.Drawing.Point(89, 106);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 30);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // FrmTrackingRetrieval
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(1200, 500);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmTrackingRetrieval";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "FrmTrackingRetrieval";
            this.Load += new System.EventHandler(this.FrmTrackingRetrieval_Load);
            this.Shown += new System.EventHandler(this.FrmTrackingRetrieval_Shown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridRetrieval)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView gridRetrieval;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.DateTimePicker dateRetrievalDate;
        private System.Windows.Forms.ComboBox lstDestinationBranch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDestinationBranch;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOriginBranch;
        private System.Windows.Forms.DataGridViewTextBoxColumn colScannedDate;
        private System.Windows.Forms.DataGridViewButtonColumn colPrint;
    }
}