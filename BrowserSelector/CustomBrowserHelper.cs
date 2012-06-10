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

        public BrowserInfo[] GetAvailableBrowsers(string protocol)
        {
            List<BrowserInfo> browsers = new List<BrowserInfo>();

            foreach (BrowserInfo bi in AppConfig.CustomBrowsers)
            {
                if (bi.AppliesTo.Contains(protocol))
                {
                    browsers.Add(bi);
                }
            }

            return browsers.ToArray();
        }
    }
}
