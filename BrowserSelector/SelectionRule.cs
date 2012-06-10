using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BrowserSelector
{
    public enum RuleType {
        Protocol,
        Regex
    }

    public class SelectionRule
    {
        public string TargetBrowserId { get; set; }
        public RuleType Type { get; set; }
        public string RuleText { get; set; }

        private Regex RuleRegex = null;

        public SelectionRule() { }

        public SelectionRule(string targetBrowserId, RuleType type, string ruleText)
        {
            TargetBrowserId = targetBrowserId;
            Type = type;
            RuleText = ruleText;

            if (Type == RuleType.Regex)
            {
                RuleRegex = new Regex(ruleText, RegexOptions.CultureInvariant);
            }
        }

        public bool Matches(Uri uri)
        {
            switch (Type)
            {
                case RuleType.Protocol:
                    return MatchesProtocol(uri);
                case RuleType.Regex:
                    return MatchesRegex(uri);
                default:
                    throw new ApplicationException("Unknown rule type: " + Type);
            }
        }

        private bool MatchesProtocol(Uri uri)
        {
            return uri.Scheme.ToLower() == RuleText.ToLower();
        }

        private bool MatchesRegex(Uri uri)
        {
            return RuleRegex.IsMatch(uri.ToString());
        }

    }
}
