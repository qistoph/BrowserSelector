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
    public partial class Configurator : Form
    {
        private AppConfig AppConfig;
        private BrowserInfo[] Browsers;

        public Configurator()
        {
            InitializeComponent();
        }

        public Configurator(AppConfig config, BrowserInfo[] browser)
        {
            InitializeComponent();
            AppConfig = config;
            Browsers = browser;
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            Check();
        }

        private void Check()
        {
            string message;
            if (DefaultBrowserHelper.IsProgIdInRegistry() &&
                DefaultBrowserHelper.IsProgIdDefaultHandler())
            {
                message = "Browser Selector is the default handler.";
            }
            else
            {
                message = "Browser Selector is not the default handler.";
            }

            MessageBox.Show(this, message, this.Text, MessageBoxButtons.OK);
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            DefaultBrowserHelper.CreateProgIdInRegistry();
            DefaultBrowserHelper.CreateStartMenuInternet();
            DefaultBrowserHelper.RegisterAsDefault();

            Check();
        }

        private void btnRules_Click(object sender, EventArgs e)
        {
            Rules rules = new Rules(AppConfig, Browsers);
            rules.ShowDialog(this);
        }
    }
}
