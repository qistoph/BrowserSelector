using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace BrowserSelector
{
    public class AppConfig
    {
        private static XmlSerializer ConfigSerializer = new XmlSerializer(typeof(AppConfig));

        public List<SelectionRule> SelectionRules { get; set; }

        public static AppConfig GetDefault()
        {
            List<SelectionRule> selectionRules = new List<SelectionRule>();
            selectionRules.Add(new SelectionRule("Internet Explorer", RuleType.Regex, @"https?://.*\.microsoft\.com.*"));
            //selectionRules.Add(new SelectionRule("Google Chrome", RuleType.Regex, "(ftp)|(https?)"));

            AppConfig config = new AppConfig()
            {
                SelectionRules = selectionRules
            };
            return config;
        }

        public static void Save(AppConfig config, string filename)
        {
            config.Save(filename);
        }

        public void Save(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                ConfigSerializer.Serialize(fs, this);
            }
        }

        public override string ToString()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ConfigSerializer.Serialize(ms, this);
                return UTF8Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        public static AppConfig FromString(string xmlString)
        {
            using (MemoryStream ms = new MemoryStream(UTF8Encoding.UTF8.GetBytes(xmlString)))
            {
                return Load(ms);
            }
        }

        public static AppConfig Load(Stream s)
        {
            AppConfig config = (AppConfig)ConfigSerializer.Deserialize(s);

            return config;
        }

        public static AppConfig Load(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                return Load(fs);
            }
        }

        public static AppConfig LoadOrDefault(string filename)
        {
            if (File.Exists(filename))
            {
                return Load(filename);
            }
            else
            {
                return GetDefault();
            }
        }
    }

}
