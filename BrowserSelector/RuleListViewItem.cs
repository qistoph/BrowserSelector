using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BrowserSelector
{
    public class RuleListViewItem : ListViewItem
    {
        public SelectionRule Rule { get; private set; }

        public RuleListViewItem(SelectionRule rule)
        {
            Rule = rule;

            Text = rule.RuleText;

            SubItems.Add(rule.TargetBrowserId);
            SubItems.Add(rule.Type.ToString());
            ImageKey = rule.TargetBrowserId;
        }
    }
}
