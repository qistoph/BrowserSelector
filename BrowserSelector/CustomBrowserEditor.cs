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
            ShowBrowser(browser);
        }

        private void ShowBrowser(BrowserInfo browser)
        {
            throw new NotImplementedException();
        }

        internal BrowserInfo GetBrowserInfo()
        {
            string name = txtName.Text;
            BrowserCategory category = BrowserCategory.Custom;
            string exePathWithArguments = lblExe.Text;
            string iconLocation = lblIcon.Text;

            BrowserInfo bi = new BrowserInfo(name, category, exePathWithArguments, iconLocation);
            return bi;
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

        private void UpdateIcon(string iconFile, int index)
        {
            lblIcon.Text = "\"" + iconFile + "\"," + index;

            //Icon icon = IconExtracter.ExtractIconFromExe(iconFile, index, true);
            IntPtr pIcon = IconExtracter.ExtractIcon(IntPtr.Zero, iconFile, (uint)index);
            Icon icon = Icon.FromHandle(pIcon);
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
