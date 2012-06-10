namespace BrowserSelector
{
    partial class RuleEditor
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
            this.cmbRuleType = new System.Windows.Forms.ComboBox();
            this.lblRuleType = new System.Windows.Forms.Label();
            this.lblBrowser = new System.Windows.Forms.Label();
            this.cmbBrowser = new System.Windows.Forms.ComboBox();
            this.lblRule = new System.Windows.Forms.Label();
            this.txtRule = new System.Windows.Forms.TextBox();
            this.pbBrowserIcon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbBrowserIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(155, 111);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(74, 111);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // cmbRuleType
            // 
            this.cmbRuleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRuleType.FormattingEnabled = true;
            this.cmbRuleType.Location = new System.Drawing.Point(108, 10);
            this.cmbRuleType.Name = "cmbRuleType";
            this.cmbRuleType.Size = new System.Drawing.Size(121, 21);
            this.cmbRuleType.TabIndex = 7;
            // 
            // lblRuleType
            // 
            this.lblRuleType.AutoSize = true;
            this.lblRuleType.Location = new System.Drawing.Point(50, 13);
            this.lblRuleType.Name = "lblRuleType";
            this.lblRuleType.Size = new System.Drawing.Size(34, 13);
            this.lblRuleType.TabIndex = 8;
            this.lblRuleType.Text = "Type:";
            // 
            // lblBrowser
            // 
            this.lblBrowser.AutoSize = true;
            this.lblBrowser.Location = new System.Drawing.Point(50, 40);
            this.lblBrowser.Name = "lblBrowser";
            this.lblBrowser.Size = new System.Drawing.Size(48, 13);
            this.lblBrowser.TabIndex = 9;
            this.lblBrowser.Text = "Browser:";
            // 
            // cmbBrowser
            // 
            this.cmbBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbBrowser.DisplayMember = "Name";
            this.cmbBrowser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBrowser.FormattingEnabled = true;
            this.cmbBrowser.Location = new System.Drawing.Point(108, 37);
            this.cmbBrowser.Name = "cmbBrowser";
            this.cmbBrowser.Size = new System.Drawing.Size(122, 21);
            this.cmbBrowser.TabIndex = 10;
            this.cmbBrowser.SelectedIndexChanged += new System.EventHandler(this.cmbBrowser_SelectedIndexChanged);
            // 
            // lblRule
            // 
            this.lblRule.AutoSize = true;
            this.lblRule.Location = new System.Drawing.Point(12, 69);
            this.lblRule.Name = "lblRule";
            this.lblRule.Size = new System.Drawing.Size(32, 13);
            this.lblRule.TabIndex = 11;
            this.lblRule.Text = "Rule:";
            // 
            // txtRule
            // 
            this.txtRule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRule.Location = new System.Drawing.Point(12, 85);
            this.txtRule.Name = "txtRule";
            this.txtRule.Size = new System.Drawing.Size(218, 20);
            this.txtRule.TabIndex = 12;
            // 
            // pbBrowserIcon
            // 
            this.pbBrowserIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbBrowserIcon.Location = new System.Drawing.Point(12, 10);
            this.pbBrowserIcon.Name = "pbBrowserIcon";
            this.pbBrowserIcon.Size = new System.Drawing.Size(32, 32);
            this.pbBrowserIcon.TabIndex = 13;
            this.pbBrowserIcon.TabStop = false;
            // 
            // RuleEditor
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(242, 146);
            this.Controls.Add(this.pbBrowserIcon);
            this.Controls.Add(this.txtRule);
            this.Controls.Add(this.lblRule);
            this.Controls.Add(this.cmbBrowser);
            this.Controls.Add(this.lblBrowser);
            this.Controls.Add(this.lblRuleType);
            this.Controls.Add(this.cmbRuleType);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(220, 184);
            this.Name = "RuleEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "RuleEditor";
            ((System.ComponentModel.ISupportInitialize)(this.pbBrowserIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ComboBox cmbRuleType;
        private System.Windows.Forms.Label lblRuleType;
        private System.Windows.Forms.Label lblBrowser;
        private System.Windows.Forms.ComboBox cmbBrowser;
        private System.Windows.Forms.Label lblRule;
        private System.Windows.Forms.TextBox txtRule;
        private System.Windows.Forms.PictureBox pbBrowserIcon;
    }
}