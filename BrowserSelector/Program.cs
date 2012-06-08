using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace BrowserSelector
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] arguments)
        {
            string urlToLaunch = null;
            int n = 0;
            for (; n < arguments.Length; ++n)
            {
                if (arguments[n] == "-b")
                {
                    Debugger.Launch();
                }
                else if (arguments[n] == "--")
                {
                    ++n;
                    break;
                }
            }

            if (n < arguments.Length)
                urlToLaunch = arguments[n];

            if (urlToLaunch != null)
            {
                //TODO: determine if it's a known URL, otherwise launch the Launcher

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Launcher form = new Launcher();
                form.UrlToLaunch = urlToLaunch;
                Application.Run(form);
            }
            else
            {
                if (UacHelper.ElevationRequired())
                {
                    UacHelper.Elevate();
                }
                else
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Configurator());
                }
            }
        }
    }
}
