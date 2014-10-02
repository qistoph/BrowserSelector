using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace BrowserSelector
{
    public partial class CustomBrowserEditor : Form
    {
        public CustomBrowserEditor()
        {
            InitializeComponent();

            lblExe.Text = "";
            lblIcon.Text = "";
        }

        public CustomBrowserEditor(BrowserInfo browser)
            : this()
        {
            LoadCustomBrowser(browser);
        }

        public void LoadCustomBrowser(BrowserInfo browser)
        {
            txtName.Text = browser.Name;
            lblExe.Text = browser.Executable;
            lblIcon.Text = browser.IconLocation;
            if (browser.IconLocation != null)
            {
                UpdateIcon(browser);
            }
        }

        internal BrowserInfo GetBrowserInfo()
        {
            BrowserInfo bi = new BrowserInfo();
            UpdateCustomBrowser(bi);
            return bi;
        }

        internal void UpdateCustomBrowser(BrowserInfo customBrowser)
        {
            customBrowser.Name = txtName.Text;
            customBrowser.Category = BrowserCategory.Custom;
            customBrowser.Executable = lblExe.Text;
            customBrowser.Arguments = "\"%1\"";
            customBrowser.IconLocation = lblIcon.Text;
        }

        private void btnExeBrowse_Click(object sender, EventArgs e)
        {
            if (ofdExe.ShowDialog() == DialogResult.OK)
            {
                string exePath = ofdExe.FileName;
                lblExe.Text = exePath;
                UpdateIcon(exePath, 0);

                txtName.Text = System.Diagnostics.FileVersionInfo.GetVersionInfo(exePath).ProductName;
            }
        }

        private void UpdateIcon(BrowserInfo browserInfo)
        {
            lblIcon.Text = browserInfo.IconLocation;
            pbBrowserIcon.Image = browserInfo.Icon.ToBitmap();
        }

        private void UpdateIcon(string iconFile, int index)
        {
            lblIcon.Text = "\"" + iconFile + "\"," + index;

            Icon icon = IconExtracter.ExtractIconFromExe(iconFile, index, true);
            pbBrowserIcon.Image = icon.ToBitmap();
        }

        [DllImport("shell32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        private static extern int PickIconDlg(IntPtr hwndOwner, System.Text.StringBuilder lpstrFile, int nMaxFile, ref int lpdwIconIndex);

        private void btnIconChoose_Click(object sender, EventArgs e)
        {
            string iconfile = lblExe.Text != "" ? lblExe.Text : (Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\shell32.dll");
            int iconindex = 0;
            int retval;
            StringBuilder sb = new StringBuilder(iconfile, 500);
            retval = PickIconDlg(this.Handle, sb, sb.Capacity, ref iconindex);
            iconfile = sb.ToString();

            UpdateIcon(iconfile, iconindex);
        }
    }
}
