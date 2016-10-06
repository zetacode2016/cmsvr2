namespace CMS2.Client
{
    partial class UcPayment
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSoaNo = new System.Windows.Forms.TextBox();
            this.txtAmountPaid = new System.Windows.Forms.TextBox();
            this.datePaymentDate = new System.Windows.Forms.DateTimePicker();
            this.txtOrNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtAwb = new System.Windows.Forms.TextBox();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.dateCheckDate = new System.Windows.Forms.DateTimePicker();
            this.txtCheckNo = new System.Windows.Forms.TextBox();
            this.txtCheckBank = new System.Windows.Forms.TextBox();
            this.lstPaymentType = new System.Windows.Forms.ComboBox();
            this.txtTaxWithheld = new System.Windows.Forms.TextBox();
            this.txtNetCollection = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtPrNo = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(19, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(263, 472);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Payment";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.btnAccept, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnCancel, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(48, 412);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(202, 36);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnAccept.Location = new System.Drawing.Point(3, 3);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(95, 30);
            this.btnAccept.TabIndex = 12;
            this.btnAccept.Text = "Save";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnCancel.Location = new System.Drawing.Point(104, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(95, 30);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "Reset/Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnCancel_KeyUp);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtSoaNo, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtOrNo, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtAwb, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtRemarks, 1, 12);
            this.tableLayoutPanel1.Controls.Add(this.dateCheckDate, 1, 11);
            this.tableLayoutPanel1.Controls.Add(this.txtCheckNo, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.txtCheckBank, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.lstPaymentType, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.txtNetCollection, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.txtTaxWithheld, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.txtAmountPaid, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 12);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.label12, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label13, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.datePaymentDate, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtPrNo, 1, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(16, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 15;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(234, 373);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "SOA";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "AWB";
            // 
            // txtSoaNo
            // 
            this.txtSoaNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSoaNo.Location = new System.Drawing.Point(84, 3);
            this.txtSoaNo.Name = "txtSoaNo";
            this.txtSoaNo.Size = new System.Drawing.Size(100, 20);
            this.txtSoaNo.TabIndex = 0;
            this.txtSoaNo.TextChanged += new System.EventHandler(this.txtSoaNo_TextChanged);
            this.txtSoaNo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSoaNo_KeyUp);
            // 
            // txtAmountPaid
            // 
            this.txtAmountPaid.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtAmountPaid.Location = new System.Drawing.Point(84, 133);
            this.txtAmountPaid.Name = "txtAmountPaid";
            this.txtAmountPaid.Size = new System.Drawing.Size(100, 20);
            this.txtAmountPaid.TabIndex = 4;
            this.txtAmountPaid.Text = "0.00";
            this.txtAmountPaid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmountPaid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtAmountPaid_KeyUp);
            this.txtAmountPaid.Leave += new System.EventHandler(this.txtAmountPaid_Leave);
            // 
            // datePaymentDate
            // 
            this.datePaymentDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.datePaymentDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datePaymentDate.Location = new System.Drawing.Point(84, 107);
            this.datePaymentDate.Name = "datePaymentDate";
            this.datePaymentDate.Size = new System.Drawing.Size(100, 20);
            this.datePaymentDate.TabIndex = 3;
            this.datePaymentDate.KeyUp += new System.Windows.Forms.KeyEventHandler(this.datePaymentDate_KeyUp);
            // 
            // txtOrNo
            // 
            this.txtOrNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtOrNo.Location = new System.Drawing.Point(84, 55);
            this.txtOrNo.Name = "txtOrNo";
            this.txtOrNo.Size = new System.Drawing.Size(100, 20);
            this.txtOrNo.TabIndex = 2;
            this.txtOrNo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtOrNo_KeyUp);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Amount Paid";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Payment Date";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 58);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "OR No";
            // 
            // txtAwb
            // 
            this.txtAwb.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtAwb.Location = new System.Drawing.Point(84, 29);
            this.txtAwb.Name = "txtAwb";
            this.txtAwb.Size = new System.Drawing.Size(100, 20);
            this.txtAwb.TabIndex = 1;
            this.txtAwb.TextChanged += new System.EventHandler(this.txtAwb_TextChanged);
            this.txtAwb.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtAwb_KeyUp);
            // 
            // txtRemarks
            // 
            this.txtRemarks.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtRemarks.Location = new System.Drawing.Point(84, 316);
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.tableLayoutPanel1.SetRowSpan(this.txtRemarks, 3);
            this.txtRemarks.Size = new System.Drawing.Size(147, 54);
            this.txtRemarks.TabIndex = 11;
            this.txtRemarks.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtRemarks_KeyUp);
            // 
            // dateCheckDate
            // 
            this.dateCheckDate.Enabled = false;
            this.dateCheckDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateCheckDate.Location = new System.Drawing.Point(84, 290);
            this.dateCheckDate.Name = "dateCheckDate";
            this.dateCheckDate.Size = new System.Drawing.Size(100, 20);
            this.dateCheckDate.TabIndex = 10;
            this.dateCheckDate.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dateCheckDate_KeyUp);
            // 
            // txtCheckNo
            // 
            this.txtCheckNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtCheckNo.Enabled = false;
            this.txtCheckNo.Location = new System.Drawing.Point(84, 264);
            this.txtCheckNo.Name = "txtCheckNo";
            this.txtCheckNo.Size = new System.Drawing.Size(100, 20);
            this.txtCheckNo.TabIndex = 9;
            this.txtCheckNo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCheckNo_KeyUp);
            // 
            // txtCheckBank
            // 
            this.txtCheckBank.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtCheckBank.Enabled = false;
            this.txtCheckBank.Location = new System.Drawing.Point(84, 238);
            this.txtCheckBank.Name = "txtCheckBank";
            this.txtCheckBank.Size = new System.Drawing.Size(147, 20);
            this.txtCheckBank.TabIndex = 8;
            this.txtCheckBank.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCheckBank_KeyUp);
            // 
            // lstPaymentType
            // 
            this.lstPaymentType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lstPaymentType.FormattingEnabled = true;
            this.lstPaymentType.Location = new System.Drawing.Point(84, 211);
            this.lstPaymentType.Name = "lstPaymentType";
            this.lstPaymentType.Size = new System.Drawing.Size(100, 21);
            this.lstPaymentType.TabIndex = 7;
            this.lstPaymentType.SelectionChangeCommitted += new System.EventHandler(this.lstPaymentType_SelectionChangeCommitted);
            // 
            // txtTaxWithheld
            // 
            this.txtTaxWithheld.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtTaxWithheld.Location = new System.Drawing.Point(84, 159);
            this.txtTaxWithheld.Name = "txtTaxWithheld";
            this.txtTaxWithheld.Size = new System.Drawing.Size(100, 20);
            this.txtTaxWithheld.TabIndex = 5;
            this.txtTaxWithheld.Text = "0.00";
            this.txtTaxWithheld.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTaxWithheld.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTaxWithheld_KeyUp);
            this.txtTaxWithheld.Leave += new System.EventHandler(this.txtTaxWithheld_Leave);
            // 
            // txtNetCollection
            // 
            this.txtNetCollection.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtNetCollection.Location = new System.Drawing.Point(84, 185);
            this.txtNetCollection.Name = "txtNetCollection";
            this.txtNetCollection.ReadOnly = true;
            this.txtNetCollection.Size = new System.Drawing.Size(100, 20);
            this.txtNetCollection.TabIndex = 6;
            this.txtNetCollection.Text = "0.00";
            this.txtNetCollection.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 316);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "Remarks";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 293);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Check Date";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 267);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Check No";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 241);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Check Bank";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 215);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Payment Type";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 162);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Tax Withheld";
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 188);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(73, 13);
            this.label12.TabIndex = 14;
            this.label12.Text = "Net Collection";
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 84);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(39, 13);
            this.label13.TabIndex = 16;
            this.label13.Text = "PR No";
            // 
            // txtPrNo
            // 
            this.txtPrNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtPrNo.Location = new System.Drawing.Point(84, 81);
            this.txtPrNo.Name = "txtPrNo";
            this.txtPrNo.Size = new System.Drawing.Size(100, 20);
            this.txtPrNo.TabIndex = 17;
            this.txtPrNo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPrNo_KeyUp);
            // 
            // UcPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.groupBox1);
            this.Name = "UcPayment";
            this.Size = new System.Drawing.Size(1252, 582);
            this.Load += new System.EventHandler(this.UcPayment_Load);
            this.VisibleChanged += new System.EventHandler(this.UcPayment_VisibleChanged);
            this.Enter += new System.EventHandler(this.UcPayment_Enter);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtAwb;
        private System.Windows.Forms.TextBox txtSoaNo;
        private System.Windows.Forms.TextBox txtOrNo;
        private System.Windows.Forms.DateTimePicker datePaymentDate;
        private System.Windows.Forms.TextBox txtAmountPaid;
        private System.Windows.Forms.ComboBox lstPaymentType;
        private System.Windows.Forms.TextBox txtCheckBank;
        private System.Windows.Forms.TextBox txtCheckNo;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DateTimePicker dateCheckDate;
        private System.Windows.Forms.TextBox txtTaxWithheld;
        private System.Windows.Forms.TextBox txtNetCollection;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtPrNo;
    }
}
