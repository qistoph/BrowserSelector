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

        [XmlIgnore]
        public Icon Icon { get; protected internal set; }

        [XmlIgnore]
        public string Company { get; protected set; }

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
            SetValuesFromExe();
        }

        public BrowserInfo(string name, BrowserCategory category, string exePath, string arguments)
        {
            Name = name;
            Category = category;
            Executable = exePath;
            Arguments = arguments;
            SetValuesFromExe();
        }

        public BrowserInfo(string name, BrowserCategory category, string exePathWithArguments, Icon icon)
        {
            string exe, arguments;
            SplitExeAndArgs(exePathWithArguments, out exe, out arguments);
            Name = name;
            Category = category;
            Executable = exe;
            Arguments = arguments;
            Icon = icon;
            SetCompanyFromExe();
        }

        public void SetCompanyFromExe()
        {
            Company = FileVersionInfo.GetVersionInfo(Executable).CompanyName;
        }

        public void SetIconFromExe()
        {
            Icon = IconExtracter.ExtractIconFromExe(Executable, true);
        }

        public void SetValuesFromExe()
        {
            SetCompanyFromExe();
            SetIconFromExe();
        }

        public void Launch(Uri uri)
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = Executable;
            psi.Arguments = Arguments.Replace("%1", uri.ToString());

            //MessageBox.Show("Exe: " + exePath + Environment.NewLine + "Args: " + arguments);

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


    }
}
