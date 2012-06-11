using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Xml.Serialization;

namespace BrowserSelector
{
    public enum BrowserCategory
    {
        Default,
        Custom
    }

    public class BrowserInfo
    {
        public string Name { get; set; }
        public string Executable { get; set; }
        public string Arguments { get; set; }

        [XmlElement("Apply")]
        public List<string> AppliesTo { get; set; }

        [XmlIgnore]
        public BrowserCategory Category { get; protected internal set; }

        private Icon _Icon = null;
        private string IconLocation = null;

        [XmlIgnore]
        public Icon Icon
        {
            get
            {
                if (_Icon == null)
                {
                    _Icon = GetIconFromPath(IconLocation ?? Executable, true);
                }
                return _Icon;
            }
            protected internal set
            {
                _Icon = value;
            }
        }

        private string _Company = null;

        [XmlIgnore]
        public string Company
        {
            get
            {
                if (_Company == null)
                {
                    _Company = FileVersionInfo.GetVersionInfo(Executable).CompanyName;
                }
                return _Company;
            }
            protected set
            {
                _Company = value;
            }
        }

        public BrowserInfo()
        {
        }

        public BrowserInfo(string name, BrowserCategory category, string exePathWithArguments)
        {
            string exe, arguments;
            SplitExeAndArgs(exePathWithArguments, out exe, out arguments);
            Name = name;
            Category = category;
            Executable = exe;
            Arguments = arguments;
            IconLocation = Executable;
        }

        public BrowserInfo(string name, BrowserCategory category, string exePathWithArguments, string iconLocation)
        {
            string exe, arguments;
            SplitExeAndArgs(exePathWithArguments, out exe, out arguments);
            Name = name;
            Category = category;
            Executable = exe;
            Arguments = arguments;
            IconLocation = iconLocation;
        }

        public void Launch(Uri uri)
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = Executable;
            psi.Arguments = Arguments.Replace("%1", uri.ToString());

            Process.Start(psi);
        }

        protected internal static void SplitExeAndArgs(string launchPath, out string exePath, out string arguments)
        {
            if (launchPath[0] == '"')
            {
                int nextQuote = launchPath.IndexOf('"', 1);
                exePath = launchPath.Substring(1, nextQuote - 1);
                arguments = launchPath.Substring(nextQuote + 1);
            }
            else
            {
                int spaceAt = launchPath.IndexOf(' ');
                if (spaceAt < 0)
                {
                    exePath = launchPath;
                    arguments = "";
                }
                else
                {
                    exePath = launchPath.Substring(0, spaceAt);
                    arguments = launchPath.Substring(spaceAt + 1);
                }
            }

            //List<string> pathParts = new List<string>();
            //StringBuilder str = new StringBuilder();
            //bool inString = false;
            //for (int i = 0; i < launchPath.Length; ++i)
            //{
            //    char c = launchPath[i];

            //    if (c == '"')
            //    {
            //        inString = !inString;
            //        continue;
            //    }

            //    if (inString)
            //    {
            //        str.Append(c);
            //    }
            //    else
            //    {
            //        if (c == ' ')
            //        {
            //            pathParts.Add(str.ToString());
            //            str = new StringBuilder();
            //        }
            //        else
            //        {
            //            str.Append(c);
            //        }
            //    }
            //}

            //if (str.Length > 0)
            //    pathParts.Add(str.ToString());
        }

        private static System.Drawing.Icon GetIconFromPath(string iconLocation, bool large)
        {
            int iconIndex;
            string iconFile;

            iconLocation = iconLocation.Replace("\"", "");

            int commaAt = iconLocation.IndexOf(',');
            if (commaAt < 0)
            {
                iconIndex = 0;
                iconFile = iconLocation;
            }
            else
            {
                iconIndex = int.Parse(iconLocation.Substring(commaAt + 1));
                iconFile = iconLocation.Substring(0, commaAt);
            }

            return IconExtracter.ExtractIconFromExe(iconFile, iconIndex, large);
        }
    }
}
