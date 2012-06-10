using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BrowserSelector
{
    public enum RuleType
    {
        Protocol,
        Regex
    }

    public class SelectionRule
    {
        private RuleType _Type;
        private string _RuleText;

        public string TargetBrowserId { get; set; }
        public RuleType Type
        {
            get { return _Type; }
            set
            {
                _Type = value;
                RuleRegex = null;
            }
        }
        public string RuleText
        {
            get { return _RuleText; }
            set
            {
                _RuleText = value;
                RuleRegex = new Regex(_RuleText);
            }
        }

        private Regex RuleRegex = null;

        public SelectionRule() { }

        public SelectionRule(string targetBrowserId, RuleType type, string ruleText)
        {
            TargetBrowserId = targetBrowserId;
            _Type = type;
            _RuleText = ruleText;

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

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();

            str.Append("SelectionRule { ");
            str.Append(Type.ToString()).Append(", ");
            str.Append(TargetBrowserId).Append(", ");
            str.Append(RuleText).Append(" }");

            return str.ToString();
        }
    }
}
