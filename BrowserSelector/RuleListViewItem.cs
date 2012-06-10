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

            Update();
        }

        internal void Update()
        {
            Text = Rule.RuleText;
            SubItems.Add(Rule.TargetBrowserId);
            SubItems.Add(Rule.Type.ToString());
            ImageKey = Rule.TargetBrowserId;
        }
    }
}
