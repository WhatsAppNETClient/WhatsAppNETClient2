namespace DemoWhatsAppNETAPICSharp
{
    partial class FrmSetStatus
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSetStatus = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkUrl = new System.Windows.Forms.CheckBox();
            this.chkGambar = new System.Windows.Forms.CheckBox();
            this.btnCariGambar = new System.Windows.Forms.Button();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.txtFileGambar = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btnSetStatus, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(411, 146);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnSetStatus
            // 
            this.btnSetStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSetStatus.Location = new System.Drawing.Point(3, 119);
            this.btnSetStatus.Name = "btnSetStatus";
            this.btnSetStatus.Size = new System.Drawing.Size(405, 24);
            this.btnSetStatus.TabIndex = 1;
            this.btnSetStatus.Text = "Set Status";
            this.btnSetStatus.UseVisualStyleBackColor = true;
            this.btnSetStatus.Click += new System.EventHandler(this.btnSetStatus_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(405, 110);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.txtStatus, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(399, 29);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Status";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtStatus
            // 
            this.txtStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStatus.Location = new System.Drawing.Point(46, 3);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(350, 20);
            this.txtStatus.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkUrl);
            this.groupBox1.Controls.Add(this.chkGambar);
            this.groupBox1.Controls.Add(this.btnCariGambar);
            this.groupBox1.Controls.Add(this.txtUrl);
            this.groupBox1.Controls.Add(this.txtFileGambar);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(399, 69);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " [ Status dengan gambar ] ";
            // 
            // chkUrl
            // 
            this.chkUrl.AutoSize = true;
            this.chkUrl.Location = new System.Drawing.Point(6, 44);
            this.chkUrl.Name = "chkUrl";
            this.chkUrl.Size = new System.Drawing.Size(97, 17);
            this.chkUrl.TabIndex = 3;
            this.chkUrl.Text = "Gambar dari url";
            this.chkUrl.UseVisualStyleBackColor = true;
            this.chkUrl.CheckedChanged += new System.EventHandler(this.chkUrl_CheckedChanged);
            // 
            // chkGambar
            // 
            this.chkGambar.AutoSize = true;
            this.chkGambar.Location = new System.Drawing.Point(6, 22);
            this.chkGambar.Name = "chkGambar";
            this.chkGambar.Size = new System.Drawing.Size(63, 17);
            this.chkGambar.TabIndex = 3;
            this.chkGambar.Text = "Gambar";
            this.chkGambar.UseVisualStyleBackColor = true;
            this.chkGambar.CheckedChanged += new System.EventHandler(this.chkGambar_CheckedChanged);
            // 
            // btnCariGambar
            // 
            this.btnCariGambar.Enabled = false;
            this.btnCariGambar.Location = new System.Drawing.Point(356, 18);
            this.btnCariGambar.Name = "btnCariGambar";
            this.btnCariGambar.Size = new System.Drawing.Size(34, 23);
            this.btnCariGambar.TabIndex = 1;
            this.btnCariGambar.Text = "...";
            this.btnCariGambar.UseVisualStyleBackColor = true;
            this.btnCariGambar.Click += new System.EventHandler(this.btnCariGambar_Click);
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(109, 42);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(281, 20);
            this.txtUrl.TabIndex = 2;
            this.txtUrl.Text = "http://coding4ever.net/assets/images/avatar.png";
            // 
            // txtFileGambar
            // 
            this.txtFileGambar.Location = new System.Drawing.Point(109, 20);
            this.txtFileGambar.Name = "txtFileGambar";
            this.txtFileGambar.Size = new System.Drawing.Size(241, 20);
            this.txtFileGambar.TabIndex = 0;
            // 
            // FrmSetStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 146);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSetStatus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Status";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnSetStatus;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.TextBox txtFileGambar;
        private System.Windows.Forms.Button btnCariGambar;
        private System.Windows.Forms.CheckBox chkUrl;
        private System.Windows.Forms.CheckBox chkGambar;
    }
}