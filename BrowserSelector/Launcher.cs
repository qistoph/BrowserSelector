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

        private AppConfig AppConfig;
        private BrowserInfo[] Browsers;

        public Launcher()
        {
            InitializeComponent();
        }

        public Launcher(AppConfig config, BrowserInfo[] browsers)
        {
            AppConfig = config;
            Browsers = browsers;
            InitializeComponent();

            Text = Properties.Resources.AppName;
            Icon = Properties.Resources.AppIcon;

            pbIcon.Image = Icon.ToBitmap();
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
            if(AppConfig.UnsavedChanges)
                AppConfig.Save(Program.GetConfigName());

            LaunchUrlInSelectedBrowser();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRules_Click(object sender, EventArgs e)
        {
            new Rules(AppConfig, Browsers).ShowDialog();
        }

        private void Launcher_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && AppConfig.UnsavedChanges)
            {
                DialogResult answer = MessageBox.Show(this, "There are unsaved changed in the configuration. Save these changes?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (answer == DialogResult.Yes)
                {
                    AppConfig.Save(Program.GetConfigName());
                }
                else if (answer == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
