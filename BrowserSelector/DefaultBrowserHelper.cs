﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Reflection;
using System.Drawing;
using System.Diagnostics;

namespace BrowserSelector
{
    public enum ASSOCIATIONLEVEL
    {
        AL_MACHINE,
        AL_EFFECTIVE,
        AL_USER,
    };

    public enum ASSOCIATIONTYPE
    {
        AT_FILEEXTENSION,
        AT_URLPROTOCOL,
        AT_STARTMENUCLIENT,
        AT_MIMETYPE,
    };

    [Guid("4e530b0a-e611-4c77-a3ac-9031d022281b"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface IApplicationAssociationRegistration
    {
        void QueryCurrentDefault([In, MarshalAs(UnmanagedType.LPWStr)] string pszQuery,
        [In, MarshalAs(UnmanagedType.I4)] ASSOCIATIONTYPE atQueryType,
        [In, MarshalAs(UnmanagedType.I4)] ASSOCIATIONLEVEL alQueryLevel,
        [Out, MarshalAs(UnmanagedType.LPWStr)] out string ppszAssociation);

        void QueryAppIsDefault(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszQuery,
            [In] ASSOCIATIONTYPE atQueryType,
            [In] ASSOCIATIONLEVEL alQueryLevel,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszAppRegistryName,
            [Out] out bool pfDefault);

        void QueryAppIsDefaultAll(
            [In] ASSOCIATIONLEVEL alQueryLevel,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszAppRegistryName,
            [Out] out bool pfDefault);

        void SetAppAsDefault(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszAppRegistryName,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszSet,
            [In] ASSOCIATIONTYPE atSetType);

        void SetAppAsDefaultAll(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszAppRegistryName);

        void ClearUserAssociations();
    }

    [ComImport, Guid("591209c7-767b-42b2-9fba-44ee4615f2c7")]//
    class ApplicationAssociationRegistration
    {
    }

    public static class DefaultBrowserHelper
    {
        public const string BrowserRegistryId = "CVM.BrowserSelector";

        public static void Test()
        {
            IApplicationAssociationRegistration reg =
                (IApplicationAssociationRegistration)new ApplicationAssociationRegistration();

            string progID;

            reg.QueryCurrentDefault("http",
                ASSOCIATIONTYPE.AT_URLPROTOCOL,
                ASSOCIATIONLEVEL.AL_EFFECTIVE,
                out progID);
            Console.WriteLine(progID);
        }

        public static bool IsProgIdInRegistry()
        {
            string[] registeredClasses = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Classes").GetSubKeyNames();

            bool exists = registeredClasses.Contains(GetProgId());
            return exists;
        }

        public static bool IsProgIdDefaultHandler()
        {
            string progId = GetProgId();
            bool isHandler = true;

            RegistryKey regHttp = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice", RegistryKeyPermissionCheck.ReadSubTree, System.Security.AccessControl.RegistryRights.ReadKey);
            if ((regHttp.GetValue("Progid") as string) != progId)
                isHandler = false;

            RegistryKey regHttps = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\https\UserChoice", RegistryKeyPermissionCheck.ReadSubTree, System.Security.AccessControl.RegistryRights.ReadKey);
            if ((regHttps.GetValue("Progid") as string) != progId)
                isHandler = false;

            RegistryKey regFtp = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\ftp\UserChoice", RegistryKeyPermissionCheck.ReadSubTree, System.Security.AccessControl.RegistryRights.ReadKey);
            if ((regFtp.GetValue("Progid") as string) != progId)
                isHandler = false;

            return isHandler;
        }

        public static void CreateProgIdInRegistry()
        {
            // Create the prog id
            RegistryKey regClasses = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Classes", RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.CreateSubKey);
            RegistryKey regProgId = regClasses.CreateSubKey(GetProgId(), RegistryKeyPermissionCheck.ReadWriteSubTree);
            regProgId.SetValue(null, "HTML Document");
            regProgId.SetValue("URL Protocol", "", RegistryValueKind.String);

            RegistryKey regIcon = regProgId.CreateSubKey("DefaultIcon", RegistryKeyPermissionCheck.ReadWriteSubTree);
            regIcon.SetValue(null, GetProgramIconPath(), RegistryValueKind.String);

            RegistryKey regShellOpenCmd = regProgId.CreateSubKey(@"shell\open\command", RegistryKeyPermissionCheck.ReadWriteSubTree);
            regShellOpenCmd.SetValue(null, "\"" + GetProgramExecutablePath() + "\" \"%1\"", RegistryValueKind.String);
            //regShellOpenCmd.SetValue(null, "\"" + GetProgramExecutablePath() + "\" -d \"%1\"", RegistryValueKind.String);
        }

        public static void RegisterAsDefault()
        {
            RegistryKey regHttp = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice", RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.SetValue);
            regHttp.SetValue("Progid", GetProgId(), RegistryValueKind.String);

            RegistryKey regHttps = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\https\UserChoice", RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.SetValue);
            regHttps.SetValue("Progid", GetProgId(), RegistryValueKind.String);

            RegistryKey regFtp = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\ftp\UserChoice", RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.SetValue);
            regFtp.SetValue("Progid", GetProgId(), RegistryValueKind.String);
        }

        public static void CreateStartMenuInternet()
        {
            string progId = GetProgId();
            string smiName = progId;
            RegistryKey regStartMenuInternet = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Clients\StartMenuInternet", RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.CreateSubKey);
            RegistryKey regProgKey = regStartMenuInternet.CreateSubKey(smiName, RegistryKeyPermissionCheck.ReadWriteSubTree);
            regProgKey.SetValue(null, "Browser Selector", RegistryValueKind.String);

            RegistryKey regCapabilities = regProgKey.CreateSubKey("Capabilities", RegistryKeyPermissionCheck.ReadWriteSubTree);
            regCapabilities.SetValue("ApplicationDescription", "The description of DefaultBrowserHelper", RegistryValueKind.String);
            regCapabilities.SetValue("ApplicationIcon", GetProgramIconPath(), RegistryValueKind.String);
            regCapabilities.SetValue("ApplicationName", "Browser Selector", RegistryValueKind.String);

            RegistryKey regFileAssociations = regCapabilities.CreateSubKey("FileAssociations", RegistryKeyPermissionCheck.ReadWriteSubTree);
            regFileAssociations.SetValue(".htm", progId, RegistryValueKind.String);
            regFileAssociations.SetValue(".html", progId, RegistryValueKind.String);
            regFileAssociations.SetValue(".shtml", progId, RegistryValueKind.String);
            regFileAssociations.SetValue(".xht", progId, RegistryValueKind.String);
            regFileAssociations.SetValue(".xhtml", progId, RegistryValueKind.String);

            RegistryKey regStartMenu = regCapabilities.CreateSubKey("StartMenu", RegistryKeyPermissionCheck.ReadWriteSubTree);
            regStartMenu.SetValue("StartMenuInternet", smiName, RegistryValueKind.String);

            RegistryKey regURLAssociations = regCapabilities.CreateSubKey("URLAssociations", RegistryKeyPermissionCheck.ReadWriteSubTree);
            regURLAssociations.SetValue("ftp", progId, RegistryValueKind.String);
            regURLAssociations.SetValue("http", progId, RegistryValueKind.String);
            regURLAssociations.SetValue("https", progId, RegistryValueKind.String);

            RegistryKey regDefaultIcon = regProgKey.CreateSubKey("DefaultIcon", RegistryKeyPermissionCheck.ReadWriteSubTree);
            regDefaultIcon.SetValue(null, GetProgramIconPath(), RegistryValueKind.String);

            RegistryKey regInstallInfo = regProgKey.CreateSubKey("InstallInfo", RegistryKeyPermissionCheck.ReadWriteSubTree);
            regInstallInfo.SetValue("HideIconsCommand", "notepad.exe", RegistryValueKind.String);
            regInstallInfo.SetValue("ReinstallCommand", "notepad.exe", RegistryValueKind.String);
            regInstallInfo.SetValue("ShowIconsCommand", "notepad.exe", RegistryValueKind.String);
            regInstallInfo.SetValue("IconsVisible", 1, RegistryValueKind.DWord);

            RegistryKey regShellOpen = regProgKey.CreateSubKey(@"shell\open\command", RegistryKeyPermissionCheck.ReadWriteSubTree);
            regShellOpen.SetValue(null, GetProgramExecutablePath(), RegistryValueKind.String);

            RegistryKey regRegisteredApplications = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\RegisteredApplications", RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.SetValue);
            regRegisteredApplications.SetValue(GetProgId(), @"SOFTWARE\Clients\StartMenuInternet\" + smiName + @"\Capabilities", RegistryValueKind.String);

        }

        private static string GetProgId()
        {
            return BrowserRegistryId;
        }

        private static string GetProgramExecutablePath()
        {
            string exePath = Assembly.GetEntryAssembly().Location;
            return exePath;
        }

        private static string GetProgramIconPath()
        {
            return GetProgramExecutablePath() + ",0";
        }

        public static BrowserInfo[] GetAvailableBrowsers()
        {
            List<BrowserInfo> browsers = new List<BrowserInfo>();

            //HKEY_LOCAL_MACHINE\SOFTWARE\Clients\StartMenuInternet\CVM.DefaultBrowserHelper\Capabilities\URLAssociations
            
            //TODO: read Mail clients too
            RegistryKey regStartMenuInternet = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Clients\StartMenuInternet", RegistryKeyPermissionCheck.ReadSubTree, System.Security.AccessControl.RegistryRights.ReadKey);
            foreach (string subkey in regStartMenuInternet.GetSubKeyNames())
            {
                if (subkey == GetProgId()) continue;

                string browserName;
                string exePath = null;
                string iconLocation;
                List<string> appliesTo = new List<string>();

                RegistryKey regSubKey = regStartMenuInternet.OpenSubKey(subkey, RegistryKeyPermissionCheck.ReadSubTree, System.Security.AccessControl.RegistryRights.ReadKey);

                RegistryKey regCapabilities = regSubKey.OpenSubKey("Capabilities", RegistryKeyPermissionCheck.ReadSubTree, System.Security.AccessControl.RegistryRights.ReadKey);
                if (regCapabilities != null)
                {
                    browserName = regCapabilities.GetValue("ApplicationName") as string;

                    iconLocation = regCapabilities.GetValue("ApplicationIcon") as string;

                    RegistryKey regUrlAssociations = regCapabilities.OpenSubKey("URLAssociations", RegistryKeyPermissionCheck.ReadSubTree, System.Security.AccessControl.RegistryRights.ReadKey);
                    string lastAppSpecId = null;

                    foreach (string scheme in regUrlAssociations.GetValueNames())
                    {
                        string appSpecProgId = regUrlAssociations.GetValue(scheme) as string;
                        if (appSpecProgId == null)
                        {
                            throw new ApplicationException("appSpecProgId == null");
                        }

                        if (lastAppSpecId != null)
                        {
                            if (appSpecProgId != lastAppSpecId)
                            {
                                throw new ApplicationException("appSpecProgId != lastAppSpecId");
                            }
                        }
                        else
                        {
                            lastAppSpecId = appSpecProgId;
                            RegistryKey regShellOpenCommand = Registry.ClassesRoot.OpenSubKey(appSpecProgId + @"\shell\open\command");
                            exePath = regShellOpenCommand.GetValue(null) as string;
                        }

                        appliesTo.Add(scheme);
                    }

                }
                else
                {
                    browserName = regSubKey.GetValue(null) as string;

                    RegistryKey regDefaultIcon = regStartMenuInternet.OpenSubKey(subkey + @"\DefaultIcon", RegistryKeyPermissionCheck.ReadSubTree, System.Security.AccessControl.RegistryRights.ReadKey);
                    iconLocation = regDefaultIcon.GetValue(null) as string;

                    RegistryKey regShellOpenCommand = Registry.ClassesRoot.OpenSubKey(@"Applications\" + subkey + @"\shell\open\command", RegistryKeyPermissionCheck.ReadSubTree, System.Security.AccessControl.RegistryRights.ReadKey);
                    exePath = regShellOpenCommand.GetValue(null) as string;

                    //TODO: load this from the registry, somehow?
                    appliesTo.Add("http");
                    appliesTo.Add("https");
                    appliesTo.Add("ftp");
                }

                if (exePath == null)
                    throw new ApplicationException("exePath == null");

                browsers.Add(new BrowserInfo(browserName, BrowserCategory.Default, exePath, iconLocation) { AppliesTo = appliesTo });
            }

            return browsers.ToArray();
        }
    }

}
