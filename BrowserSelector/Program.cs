﻿using System;
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
                Uri uriToLaunch = new Uri(urlToLaunch);

                AppConfig appConfig = AppConfig.GetDefault();
                SelectionRule matchedRule = null;
                foreach (SelectionRule rule in appConfig.SelectionRules)
                {
                    if (rule.Matches(uriToLaunch))
                    {
                        matchedRule = rule;
                        break;
                    }
                }

                if (matchedRule == null)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Launcher form = new Launcher();
                    form.UrlToLaunch = urlToLaunch;
                    Application.Run(form);
                }
                else
                {
                    MessageBox.Show("Launch with " + matchedRule.TargetBrowserId);
                }
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