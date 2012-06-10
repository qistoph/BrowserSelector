using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrowserSelector
{
    public class CustomBrowserHelper
    {
        private AppConfig AppConfig;

        public CustomBrowserHelper(AppConfig appConfig)
        {
            AppConfig = appConfig;
        }

        public BrowserInfo[] GetAvailableBrowsers()
        {
            return AppConfig.CustomBrowsers.ToArray();
        }
    }
}
