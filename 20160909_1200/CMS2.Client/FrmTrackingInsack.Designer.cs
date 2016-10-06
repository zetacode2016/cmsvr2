namespace CMS2.Client
{
    partial class FrmTrackingInsack
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTransactionDate = new System.Windows.Forms.DateTimePicker();
            this.lstOriginBranch = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gridInsack = new System.Windows.Forms.DataGridView();
            this.colSackNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItemCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDestinationBranch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSackWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSave = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colPrint = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridInsack)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.dateTransactionDate, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lstOriginBranch, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnSearch, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblMessage, 1, 5);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(136, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(257, 152);
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
            this.label1.Size = new System.Drawing.Size(251, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Insack Transmittal";
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
            // dateTransactionDate
            // 
            this.dateTransactionDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dateTransactionDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTransactionDate.Location = new System.Drawing.Point(68, 43);
            this.dateTransactionDate.Name = "dateTransactionDate";
            this.dateTransactionDate.Size = new System.Drawing.Size(93, 20);
            this.dateTransactionDate.TabIndex = 0;
            // 
            // lstOriginBranch
            // 
            this.lstOriginBranch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lstOriginBranch.FormattingEnabled = true;
            this.lstOriginBranch.Location = new System.Drawing.Point(68, 69);
            this.lstOriginBranch.Name = "lstOriginBranch";
            this.lstOriginBranch.Size = new System.Drawing.Size(174, 21);
            this.lstOriginBranch.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSearch.Location = new System.Drawing.Point(68, 96);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 30);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(68, 134);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(35, 13);
            this.lblMessage.TabIndex = 3;
            this.lblMessage.Text = "label4";
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.gridInsack);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Location = new System.Drawing.Point(68, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(553, 441);
            this.panel1.TabIndex = 1;
            // 
            // gridInsack
            // 
            this.gridInsack.AllowUserToAddRows = false;
            this.gridInsack.AllowUserToDeleteRows = false;
            this.gridInsack.AllowUserToResizeColumns = false;
            this.gridInsack.AllowUserToResizeRows = false;
            this.gridInsack.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridInsack.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridInsack.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridInsack.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSackNo,
            this.colItemCount,
            this.colDestinationBranch,
            this.colSackWeight,
            this.colSave,
            this.colPrint});
            this.gridInsack.Location = new System.Drawing.Point(3, 169);
            this.gridInsack.MultiSelect = false;
            this.gridInsack.Name = "gridInsack";
            this.gridInsack.RowHeadersVisible = false;
            this.gridInsack.RowTemplate.Height = 28;
            this.gridInsack.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridInsack.Size = new System.Drawing.Size(546, 269);
            this.gridInsack.TabIndex = 1;
            this.gridInsack.TabStop = false;
            this.gridInsack.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridInsack_CellContentClick);
            // 
            // colSackNo
            // 
            this.colSackNo.HeaderText = "Sack No";
            this.colSackNo.Name = "colSackNo";
            this.colSackNo.ReadOnly = true;
            // 
            // colItemCount
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colItemCount.DefaultCellStyle = dataGridViewCellStyle2;
            this.colItemCount.HeaderText = "Item Count";
            this.colItemCount.Name = "colItemCount";
            this.colItemCount.ReadOnly = true;
            // 
            // colDestinationBranch
            // 
            this.colDestinationBranch.HeaderText = "Destination";
            this.colDestinationBranch.Name = "colDestinationBranch";
            this.colDestinationBranch.ReadOnly = true;
            // 
            // colSackWeight
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = "0";
            this.colSackWeight.DefaultCellStyle = dataGridViewCellStyle3;
            this.colSackWeight.HeaderText = "Sack Weight";
            this.colSackWeight.Name = "colSackWeight";
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
            // FrmTrackingInsack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(1000, 500);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmTrackingInsack";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "FrmTrackingInsack";
            this.Load += new System.EventHandler(this.FrmTrackingInsack_Load);
            this.Shown += new System.EventHandler(this.FrmTrackingInsack_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridInsack)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTransactionDate;
        private System.Windows.Forms.ComboBox lstOriginBranch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView gridInsack;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSackNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDestinationBranch;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSackWeight;
        private System.Windows.Forms.DataGridViewButtonColumn colSave;
        private System.Windows.Forms.DataGridViewButtonColumn colPrint;
    }
}