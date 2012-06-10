using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace BrowserSelector
{
    public partial class Launcher : Form
    {
        private Dictionary<ListViewItem, BrowserInfo> ListViewBrowsers = null;

        protected internal string UrlToLaunch { get; set; }

        public Launcher()
        {
            InitializeComponent();
        }

        private void Launcher_Load(object sender, EventArgs e)
        {
            lblUrl.Text = "Link: " + UrlToLaunch;
            ShowAvailableBrowsers();
        }

        private void ShowAvailableBrowsers()
        {
            listView1.LargeImageList = new ImageList();
            listView1.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
            listView1.LargeImageList.ImageSize = new System.Drawing.Size(32, 32);

            listView1.Items.Clear();

            ListViewBrowsers = new Dictionary<ListViewItem, BrowserInfo>();

            Uri uri = new Uri(UrlToLaunch);

            int n = 0;
            foreach (BrowserInfo browserInfo in DefaultBrowserHelper.GetAvailableBrowsers(uri.Scheme))
            {
                listView1.LargeImageList.Images.Add(browserInfo.Icon);
                ListViewItem lvi = new ListViewItem(browserInfo.Name, n);
                lvi.Group = listView1.Groups["lvgDefault"];
                lvi.SubItems.Add(browserInfo.Company);
                ListViewBrowsers.Add(lvi, browserInfo);
                listView1.Items.Add(lvi);

                n++;
            }

            //TODO: base listView1 width on number of icons (or required size)
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LaunchUrlInSelectedBrowser();
        }

        private void LaunchUrlInSelectedBrowser()
        {
            if (listView1.SelectedItems.Count == 1)
            {
                BrowserInfo browser = ListViewBrowsers[listView1.SelectedItems[0]];
                browser.Launch(UrlToLaunch);
                this.Close();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            LaunchUrlInSelectedBrowser();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRules_Click(object sender, EventArgs e)
        {
            new Rules().ShowDialog();
        }

    }
}
