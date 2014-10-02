namespace BrowserSelector
{
    partial class CustomBrowserEditor
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblExeLabel = new System.Windows.Forms.Label();
            this.lblIconLabel = new System.Windows.Forms.Label();
            this.pbBrowserIcon = new System.Windows.Forms.PictureBox();
            this.lblExe = new System.Windows.Forms.Label();
            this.btnExeBrowse = new System.Windows.Forms.Button();
            this.btnIconChoose = new System.Windows.Forms.Button();
            this.lblIcon = new System.Windows.Forms.Label();
            this.ofdExe = new System.Windows.Forms.OpenFileDialog();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbBrowserIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(155, 94);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(74, 94);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // lblExeLabel
            // 
            this.lblExeLabel.AutoSize = true;
            this.lblExeLabel.Location = new System.Drawing.Point(50, 13);
            this.lblExeLabel.Name = "lblExeLabel";
            this.lblExeLabel.Size = new System.Drawing.Size(28, 13);
            this.lblExeLabel.TabIndex = 0;
            this.lblExeLabel.Text = "Exe:";
            // 
            // lblIconLabel
            // 
            this.lblIconLabel.AutoSize = true;
            this.lblIconLabel.Location = new System.Drawing.Point(50, 40);
            this.lblIconLabel.Name = "lblIconLabel";
            this.lblIconLabel.Size = new System.Drawing.Size(31, 13);
            this.lblIconLabel.TabIndex = 2;
            this.lblIconLabel.Text = "Icon:";
            // 
            // pbBrowserIcon
            // 
            this.pbBrowserIcon.Location = new System.Drawing.Point(12, 10);
            this.pbBrowserIcon.Name = "pbBrowserIcon";
            this.pbBrowserIcon.Size = new System.Drawing.Size(32, 32);
            this.pbBrowserIcon.TabIndex = 13;
            this.pbBrowserIcon.TabStop = false;
            // 
            // lblExe
            // 
            this.lblExe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblExe.Location = new System.Drawing.Point(84, 13);
            this.lblExe.Name = "lblExe";
            this.lblExe.Size = new System.Drawing.Size(65, 13);
            this.lblExe.TabIndex = 14;
            this.lblExe.Text = "lblExe";
            // 
            // btnExeBrowse
            // 
            this.btnExeBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExeBrowse.Location = new System.Drawing.Point(155, 8);
            this.btnExeBrowse.Name = "btnExeBrowse";
            this.btnExeBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnExeBrowse.TabIndex = 15;
            this.btnExeBrowse.Text = "Browse...";
            this.btnExeBrowse.UseVisualStyleBackColor = true;
            this.btnExeBrowse.Click += new System.EventHandler(this.btnExeBrowse_Click);
            // 
            // btnIconChoose
            // 
            this.btnIconChoose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIconChoose.Location = new System.Drawing.Point(155, 35);
            this.btnIconChoose.Name = "btnIconChoose";
            this.btnIconChoose.Size = new System.Drawing.Size(75, 23);
            this.btnIconChoose.TabIndex = 17;
            this.btnIconChoose.Text = "Choose...";
            this.btnIconChoose.UseVisualStyleBackColor = true;
            this.btnIconChoose.Click += new System.EventHandler(this.btnIconChoose_Click);
            // 
            // lblIcon
            // 
            this.lblIcon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIcon.Location = new System.Drawing.Point(84, 40);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(65, 13);
            this.lblIcon.TabIndex = 16;
            this.lblIcon.Text = "lblIcon";
            // 
            // ofdExe
            // 
            this.ofdExe.Filter = "Executables (*.exe)|*.exe|All files|*.*";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 69);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 18;
            this.lblName.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(53, 66);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(177, 20);
            this.txtName.TabIndex = 19;
            // 
            // CustomBrowserEditor
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(242, 129);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btnIconChoose);
            this.Controls.Add(this.lblIcon);
            this.Controls.Add(this.btnExeBrowse);
            this.Controls.Add(this.lblExe);
            this.Controls.Add(this.pbBrowserIcon);
            this.Controls.Add(this.lblIconLabel);
            this.Controls.Add(this.lblExeLabel);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(2000, 167);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(258, 167);
            this.Name = "CustomBrowserEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Browser Editor";
            ((System.ComponentModel.ISupportInitialize)(this.pbBrowserIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblExeLabel;
        private System.Windows.Forms.Label lblIconLabel;
        private System.Windows.Forms.PictureBox pbBrowserIcon;
        private System.Windows.Forms.Label lblExe;
        private System.Windows.Forms.Button btnExeBrowse;
        private System.Windows.Forms.Button btnIconChoose;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.OpenFileDialog ofdExe;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
    }
}