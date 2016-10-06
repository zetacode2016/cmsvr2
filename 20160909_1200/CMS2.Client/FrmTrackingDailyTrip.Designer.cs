namespace CMS2.Client
{
    partial class FrmTrackingDailyTrip
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gridDailyTrip = new System.Windows.Forms.DataGridView();
            this.colPlateNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFieldRep = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDriver = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrint = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.dateTripDate = new System.Windows.Forms.DateTimePicker();
            this.lstBranch = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDailyTrip)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.gridDailyTrip);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Location = new System.Drawing.Point(73, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(390, 463);
            this.panel1.TabIndex = 0;
            // 
            // gridDailyTrip
            // 
            this.gridDailyTrip.AllowUserToAddRows = false;
            this.gridDailyTrip.AllowUserToDeleteRows = false;
            this.gridDailyTrip.AllowUserToResizeColumns = false;
            this.gridDailyTrip.AllowUserToResizeRows = false;
            this.gridDailyTrip.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridDailyTrip.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridDailyTrip.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridDailyTrip.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPlateNo,
            this.colFieldRep,
            this.colDriver,
            this.colPrint});
            this.gridDailyTrip.Location = new System.Drawing.Point(1, 179);
            this.gridDailyTrip.Name = "gridDailyTrip";
            this.gridDailyTrip.RowHeadersVisible = false;
            this.gridDailyTrip.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridDailyTrip.Size = new System.Drawing.Size(387, 281);
            this.gridDailyTrip.TabIndex = 1;
            this.gridDailyTrip.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridDailyTrip_CellContentClick);
            // 
            // colPlateNo
            // 
            this.colPlateNo.HeaderText = "Plate No";
            this.colPlateNo.Name = "colPlateNo";
            this.colPlateNo.ReadOnly = true;
            // 
            // colFieldRep
            // 
            this.colFieldRep.HeaderText = "Field Rep";
            this.colFieldRep.Name = "colFieldRep";
            this.colFieldRep.ReadOnly = true;
            // 
            // colDriver
            // 
            this.colDriver.HeaderText = "Driver";
            this.colDriver.Name = "colDriver";
            this.colDriver.ReadOnly = true;
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
            this.tableLayoutPanel1.Controls.Add(this.dateTripDate, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lstBranch, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnSearch, 1, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(94, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(215, 163);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 2);
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(209, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Daily Trip";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Date";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "BCO";
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(39, 142);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 13);
            this.lblMessage.TabIndex = 3;
            // 
            // dateTripDate
            // 
            this.dateTripDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dateTripDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTripDate.Location = new System.Drawing.Point(39, 48);
            this.dateTripDate.Name = "dateTripDate";
            this.dateTripDate.Size = new System.Drawing.Size(99, 20);
            this.dateTripDate.TabIndex = 4;
            // 
            // lstBranch
            // 
            this.lstBranch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lstBranch.FormattingEnabled = true;
            this.lstBranch.Location = new System.Drawing.Point(39, 74);
            this.lstBranch.Name = "lstBranch";
            this.lstBranch.Size = new System.Drawing.Size(156, 21);
            this.lstBranch.TabIndex = 5;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSearch.Location = new System.Drawing.Point(39, 101);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 30);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // FrmTrackingDailyTrip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(1200, 500);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmTrackingDailyTrip";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "FrmDailyTrip";
            this.Load += new System.EventHandler(this.FrmTrackingDailyTrip_Load);
            this.Shown += new System.EventHandler(this.FrmTrackingDailyTrip_Shown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDailyTrip)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView gridDailyTrip;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.DateTimePicker dateTripDate;
        private System.Windows.Forms.ComboBox lstBranch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlateNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFieldRep;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDriver;
        private System.Windows.Forms.DataGridViewButtonColumn colPrint;
    }
}