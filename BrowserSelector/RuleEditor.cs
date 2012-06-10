using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BrowserSelector
{
    public partial class RuleEditor : Form
    {
        private EnumComboBox<RuleType> ecbRuleType;

        public RuleEditor()
        {
            InitializeComponent();
        }

        public RuleEditor(BrowserInfo[] browsers)
        {
            InitializeComponent();

            ecbRuleType = new EnumComboBox<RuleType>(cmbRuleType);
            ecbRuleType.SelectedItem = RuleType.Regex;
            cmbRuleType = ecbRuleType;

            ShowBrowsers(browsers);
        }

        public void LoadRule(SelectionRule rule)
        {
            ecbRuleType.SelectedItem = rule.Type;
            txtRule.Text = rule.RuleText;

            for (int i = 0, c = cmbBrowser.Items.Count; i < c; ++i)
            {
                if (((BrowserInfo)cmbBrowser.Items[i]).Name == rule.TargetBrowserId)
                {
                    cmbBrowser.SelectedIndex = i;
                    break;
                }
            }
        }

        private void ShowBrowsers(BrowserInfo[] browsers)
        {
            cmbBrowser.Items.Clear();

            foreach (BrowserInfo browser in browsers)
            {
                cmbBrowser.Items.Add(browser);
            }
            if(cmbBrowser.Items.Count > 0)
                cmbBrowser.SelectedIndex = 0;
        }

        private void cmbBrowser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pbBrowserIcon.Image != null)
            {
                pbBrowserIcon.Image.Dispose();
            }
            pbBrowserIcon.Image = ((BrowserInfo)cmbBrowser.SelectedItem).Icon.ToBitmap();
        }

        internal void UpdateRule(SelectionRule rule)
        {
            rule.TargetBrowserId = ((BrowserInfo)cmbBrowser.SelectedItem).Name;
            rule.Type = ecbRuleType.SelectedItem;
            rule.RuleText = txtRule.Text;
        }
    }
}
