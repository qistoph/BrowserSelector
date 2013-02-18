using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace BrowserSelector
{
    static class Program
    {
        //TODO: HKEY_CLASSES_ROOT\http\shell\open\command

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] arguments)
        {
            //Debugger.Launch();
            string configFileName = GetConfigName();

            string urlToLaunch = null;
            int n = 0;
            for (; n < arguments.Length; ++n)
            {
                if (arguments[n] == "-d")
                {
                    Debugger.Launch();
                }
                else if(urlToLaunch == null)
                {
                    urlToLaunch = arguments[n];
                }
            }

            AppConfig appConfig = AppConfig.LoadOrDefault(configFileName);

            //TODO: find all browsers, not just handlers of http
            CustomBrowserHelper customBrowserHelper = new CustomBrowserHelper(appConfig);
            BrowserInfo[] defaultBrowsers = DefaultBrowserHelper.GetAvailableBrowsers();
            BrowserInfo[] customBrowsers = customBrowserHelper.GetAvailableBrowsers();

            BrowserInfo[] browsers = new BrowserInfo[defaultBrowsers.Length + customBrowsers.Length];
            defaultBrowsers.CopyTo(browsers, 0);
            customBrowsers.CopyTo(browsers, defaultBrowsers.Length);

            if (urlToLaunch != null)
            {
                LaunchUrl(urlToLaunch, browsers);
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
                    Application.Run(new Configurator(appConfig, browsers));
                }
            }
        }

        private static void LaunchUrl(string urlToLaunch, BrowserInfo[] browsers)
        {
            AppConfig appConfig = AppConfig.LoadOrDefault(GetConfigName());
            SelectionRule matchedRule = null;

            if (Uri.IsWellFormedUriString(urlToLaunch, UriKind.Absolute))
            {
                Uri uriToLaunch = new Uri(urlToLaunch);

                foreach (SelectionRule rule in appConfig.SelectionRules)
                {
                    if (rule.Matches(uriToLaunch))
                    {
                        matchedRule = rule;
                        break;
                    }
                }
            }

            if (matchedRule == null)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Launcher form = new Launcher(appConfig, browsers);
                form.UrlToLaunch = urlToLaunch;
                form.BringToFront();
                Application.Run(form);

                if (form.DialogResult == DialogResult.OK)
                {
                    appConfig.Save(GetConfigName());
                }
            }
            else
            {
                BrowserInfo browser = browsers.First(bi => bi.Name == matchedRule.TargetBrowserId);
                browser.Launch(urlToLaunch);
            }
        }

        internal static string GetConfigName()
        {
            string exePath = Path.GetDirectoryName(Application.ExecutablePath);
            string exeNameWithoutExt = Path.GetFileNameWithoutExtension(Application.ExecutablePath);
            string configFileName = Path.Combine(exePath, exeNameWithoutExt + ".config");
            return configFileName;
        }
    }
}
