namespace CMS2.Client
{
    partial class FrmTrackingDeliveryStatus
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
            this.griddeliveryStatus = new System.Windows.Forms.DataGridView();
            this.colStatusDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAirline = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAirwayBill = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.griddeliveryStatus)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.griddeliveryStatus);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Location = new System.Drawing.Point(44, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(735, 421);
            this.panel1.TabIndex = 0;
            // 
            // griddeliveryStatus
            // 
            this.griddeliveryStatus.AllowUserToAddRows = false;
            this.griddeliveryStatus.AllowUserToDeleteRows = false;
            this.griddeliveryStatus.AllowUserToResizeColumns = false;
            this.griddeliveryStatus.AllowUserToResizeRows = false;
            this.griddeliveryStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.griddeliveryStatus.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.griddeliveryStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.griddeliveryStatus.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStatusDate,
            this.colLocation,
            this.colStatus,
            this.colUser,
            this.colAirline,
            this.colRemark,
            this.colNote});
            this.griddeliveryStatus.Location = new System.Drawing.Point(1, 149);
            this.griddeliveryStatus.Name = "griddeliveryStatus";
            this.griddeliveryStatus.RowHeadersVisible = false;
            this.griddeliveryStatus.Size = new System.Drawing.Size(731, 269);
            this.griddeliveryStatus.TabIndex = 1;
            // 
            // colStatusDate
            // 
            this.colStatusDate.HeaderText = "Date Time";
            this.colStatusDate.Name = "colStatusDate";
            this.colStatusDate.ReadOnly = true;
            this.colStatusDate.Width = 130;
            // 
            // colLocation
            // 
            this.colLocation.HeaderText = "Location";
            this.colLocation.Name = "colLocation";
            this.colLocation.ReadOnly = true;
            this.colLocation.Width = 90;
            // 
            // colStatus
            // 
            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.Width = 90;
            // 
            // colUser
            // 
            this.colUser.HeaderText = "User";
            this.colUser.Name = "colUser";
            this.colUser.ReadOnly = true;
            this.colUser.Width = 90;
            // 
            // colAirline
            // 
            this.colAirline.HeaderText = "Airlines";
            this.colAirline.Name = "colAirline";
            this.colAirline.ReadOnly = true;
            // 
            // colRemark
            // 
            this.colRemark.HeaderText = "Remarks/Delivered By";
            this.colRemark.Name = "colRemark";
            this.colRemark.ReadOnly = true;
            // 
            // colNote
            // 
            this.colNote.HeaderText = "MAWB/Received By";
            this.colNote.Name = "colNote";
            this.colNote.ReadOnly = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtAirwayBill, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnSearch, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblMessage, 1, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(241, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(252, 109);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 3);
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(246, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Delivery Status";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtAirwayBill
            // 
            this.txtAirwayBill.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtAirwayBill.Location = new System.Drawing.Point(60, 51);
            this.txtAirwayBill.Name = "txtAirwayBill";
            this.txtAirwayBill.Size = new System.Drawing.Size(100, 20);
            this.txtAirwayBill.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSearch.Location = new System.Drawing.Point(166, 46);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 30);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "AirwayBill";
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMessage.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblMessage, 2);
            this.lblMessage.Location = new System.Drawing.Point(60, 87);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 13);
            this.lblMessage.TabIndex = 2;
            // 
            // FrmTrackingDeliveryStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(1200, 500);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmTrackingDeliveryStatus";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "FrmTrackingDeliveryStatus";
            this.Load += new System.EventHandler(this.FrmTrackingDeliveryStatus_Load);
            this.Shown += new System.EventHandler(this.FrmTrackingDeliveryStatus_Shown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.griddeliveryStatus)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView griddeliveryStatus;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtAirwayBill;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatusDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAirline;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRemark;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNote;
    }
}