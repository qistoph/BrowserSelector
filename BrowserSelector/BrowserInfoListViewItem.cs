using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BrowserSelector
{
    public class BrowserInfoListViewItem : ListViewItem
    {
        public BrowserInfo Browser { get; private set; }

        public BrowserInfoListViewItem(BrowserInfo browserInfo)
        {
            Browser = browserInfo;
            Update();
        }

        internal void Update()
        {
            Text = Browser.Name;
            SubItems.Add(Browser.Executable);
            SubItems.Add(Browser.Arguments);
            SubItems.Add(Browser.Company);
            ImageKey = Browser.Name;
        }
    }
}
