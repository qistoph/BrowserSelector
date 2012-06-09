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
    public partial class Rules : Form
    {
        public Rules()
        {
            InitializeComponent();

            //TODO: find all browsers, not just handlers
            BrowserInfo[] browsers = DefaultBrowserHelper.GetAvailableBrowsers("http");

            AppConfig config = AppConfig.GetDefault();
            DisplayRules(config.SelectionRules, browsers);

            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        public void DisplayRules(IEnumerable<SelectionRule> rules, BrowserInfo[] browsers)
        {
            listView1.SmallImageList = new ImageList();
            listView1.SmallImageList.ColorDepth = ColorDepth.Depth32Bit;
            listView1.SmallImageList.ImageSize = new System.Drawing.Size(16, 16);

            listView1.Items.Clear();

            Dictionary<string, int> browserNameToIndex = new Dictionary<string, int>();

            int n = 0;
            foreach (BrowserInfo browser in browsers)
            {
                browserNameToIndex.Add(browser.Name, n);
                listView1.SmallImageList.Images.Add(browser.Icon);
                n++;
            }

            foreach (SelectionRule rule in rules)
            {
                ListViewItem lvi = new ListViewItem(rule.RuleText);
                lvi.SubItems.Add(rule.TargetBrowserId);
                lvi.SubItems.Add(rule.Type.ToString());
                lvi.ImageIndex = browserNameToIndex[rule.TargetBrowserId];

                listView1.Items.Add(lvi);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
