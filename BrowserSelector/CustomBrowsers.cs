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
    public partial class CustomBrowsers : Form
    {
        private AppConfig AppConfig;

        public CustomBrowsers()
        {
            InitializeComponent();
        }

        public CustomBrowsers(AppConfig config)
            : this()
        {
            AppConfig = config;

            DisplayCustomBrowsers(AppConfig.CustomBrowsers);

            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        public void DisplayCustomBrowsers(IEnumerable<BrowserInfo> browsers)
        {
            listView1.SmallImageList = new ImageList();
            listView1.SmallImageList.ColorDepth = ColorDepth.Depth32Bit;
            listView1.SmallImageList.ImageSize = new System.Drawing.Size(16, 16);

            listView1.Items.Clear();

            int n = 0;
            foreach (BrowserInfo browser in browsers)
            {
                listView1.SmallImageList.Images.Add(browser.Name, browser.Icon);

                BrowserInfoListViewItem lvi = new BrowserInfoListViewItem(browser);
                listView1.Items.Add(lvi);

                n++;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            BrowserInfo bi = new BrowserInfo();
            if (DialogResult.OK == EditCustomBrowser(bi))
            {
                AddNewCustomBrowser(bi);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditSelected();
        }

        private void EditSelected()
        {
            if (listView1.SelectedItems.Count > 0)
            {
                BrowserInfoListViewItem bilvi = (BrowserInfoListViewItem)listView1.SelectedItems[0];
                if (DialogResult.OK == EditCustomBrowser(bilvi.Browser))
                {
                    bilvi.Update();
                }
            }
        }

        private DialogResult EditCustomBrowser(BrowserInfo customBrowser)
        {
            CustomBrowserEditor editor = new CustomBrowserEditor();
            editor.LoadCustomBrowser(customBrowser);
            DialogResult answer = editor.ShowDialog(this);
            if (DialogResult.OK == answer)
            {
                AppConfig.UnsavedChanges = true;
                editor.UpdateCustomBrowser(customBrowser);

                listView1.SmallImageList.Images.RemoveByKey(customBrowser.Name);
                listView1.SmallImageList.Images.Add(customBrowser.Name, customBrowser.Icon);
            }

            return answer;
        }
        
        private void btnRemove_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.Assert(listView1.Items.Count == AppConfig.CustomBrowsers.Count);

            if (listView1.SelectedItems.Count > 0)
            {
                AppConfig.UnsavedChanges = true;
                AppConfig.CustomBrowsers.Remove(((BrowserInfoListViewItem)listView1.SelectedItems[0]).Browser);

                listView1.Items.RemoveAt(listView1.SelectedIndices[0]);
            }

            System.Diagnostics.Debug.Assert(listView1.Items.Count == AppConfig.CustomBrowsers.Count);
        }

        private void AddNewCustomBrowser(BrowserInfo customBrowser)
        {
            System.Diagnostics.Debug.Assert(listView1.Items.Count == AppConfig.CustomBrowsers.Count);

            // Store the new rule
            AppConfig.UnsavedChanges = true;
            AppConfig.CustomBrowsers.Add(customBrowser);

            // Display the new rule
            BrowserInfoListViewItem lvi = new BrowserInfoListViewItem(customBrowser);
            listView1.Items.Add(lvi);

            System.Diagnostics.Debug.Assert(listView1.Items.Count == AppConfig.CustomBrowsers.Count);
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            EditSelected();
        }
    }
}
