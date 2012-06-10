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
        private BrowserInfo[] Browsers;

        public Rules()
        {
            InitializeComponent();
        }

        public Rules(AppConfig config, BrowserInfo[] browsers)
        {
            Browsers = browsers;

            InitializeComponent();

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
                listView1.SmallImageList.Images.Add(browser.Name, browser.Icon);
                n++;
            }

            foreach (SelectionRule rule in rules)
            {
                RuleListViewItem lvi = new RuleListViewItem(rule);
                listView1.Items.Add(lvi);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            EditRule(new SelectionRule(), true);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                EditRule(((RuleListViewItem)listView1.SelectedItems[0]).Rule, false);
            }
        }

        private void EditRule(SelectionRule rule, bool addIfOk)
        {
            RuleEditor editor = new RuleEditor(Browsers);
            editor.LoadRule(rule);
            if (DialogResult.OK == editor.ShowDialog(this))
            {
                editor.UpdateRule(rule);
                if (addIfOk)
                {
                    AddNewRule(rule);
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                // TODO: Store the removal

                listView1.Items.RemoveAt(listView1.SelectedIndices[0]);
            }
        }

        private void AddNewRule(SelectionRule rule)
        {
            // TODO: Store the new rule

            // Display the new rule
            RuleListViewItem lvi = new RuleListViewItem(rule);
            listView1.Items.Add(lvi);
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            //TODO: Store the change
            if (listView1.SelectedIndices.Count > 0)
            {
                int orgIndex = listView1.SelectedIndices[0];
                MoveListViewItem(orgIndex, orgIndex - 1, true);
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            //TODO: Store the change
            if (listView1.SelectedIndices.Count > 0)
            {
                int orgIndex = listView1.SelectedIndices[0];
                MoveListViewItem(orgIndex, orgIndex + 1, true);
            }
        }

        private void MoveListViewItem(int oldIndex, int newIndex, bool selectAfterwards)
        {
            int maxIndex = listView1.Items.Count - 1;
            if (oldIndex >= 0 && newIndex >= 0 && oldIndex <= maxIndex && newIndex <= maxIndex)
            {
                ListViewItem lvi = listView1.Items[oldIndex];
                listView1.Items.RemoveAt(oldIndex);
                listView1.Items.Insert(newIndex, lvi);

                if(selectAfterwards)
                    lvi.Selected = true;
            }
        }
    }
}
