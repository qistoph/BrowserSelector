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

            ShowAvailableBrowsers();
        }

        private void ShowAvailableBrowsers()
        {
            listView1.LargeImageList = new ImageList();
            listView1.LargeImageList.ImageSize = new System.Drawing.Size(32, 32);
            listView1.SmallImageList = new ImageList();

            ListViewBrowsers = new Dictionary<ListViewItem, BrowserInfo>();

            //TODO: use actual protocol from argument
            int n = 0;
            foreach (BrowserInfo browserInfo in DefaultBrowserHelper.GetAvailableBrowsers("http"))
            {
                listView1.LargeImageList.Images.Add(browserInfo.Icon);
                listView1.SmallImageList.Images.Add(browserInfo.Icon);
                ListViewItem lvi = new ListViewItem(browserInfo.Name, n);
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
                string launchPath = ListViewBrowsers[listView1.SelectedItems[0]].ExePath;
                launchPath = launchPath.Replace("%1", UrlToLaunch);
                //TODO: fix %1 after parsing the tokens

                string exePath;
                string arguments;
                if (launchPath[0] == '"')
                {
                    int nextQuote = launchPath.IndexOf('"', 1);
                    exePath = launchPath.Substring(1, nextQuote - 1);
                    arguments = launchPath.Substring(nextQuote + 1);
                }
                else
                {
                    int spaceAt = launchPath.IndexOf(' ');
                    if (spaceAt < 0)
                    {
                        exePath = launchPath;
                        arguments = "";
                    }
                    else
                    {
                        exePath = launchPath.Substring(0, spaceAt);
                        arguments = launchPath.Substring(spaceAt + 1);
                    }
                }

                //List<string> pathParts = new List<string>();
                //StringBuilder str = new StringBuilder();
                //bool inString = false;
                //for (int i = 0; i < launchPath.Length; ++i)
                //{
                //    char c = launchPath[i];

                //    if (c == '"')
                //    {
                //        inString = !inString;
                //        continue;
                //    }

                //    if (inString)
                //    {
                //        str.Append(c);
                //    }
                //    else
                //    {
                //        if (c == ' ')
                //        {
                //            pathParts.Add(str.ToString());
                //            str = new StringBuilder();
                //        }
                //        else
                //        {
                //            str.Append(c);
                //        }
                //    }
                //}

                //if (str.Length > 0)
                //    pathParts.Add(str.ToString());

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = exePath;
                psi.Arguments = arguments;

                //MessageBox.Show("Exe: " + exePath + Environment.NewLine + "Args: " + arguments);

                Process.Start(psi);
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
    }
}
