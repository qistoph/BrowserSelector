using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Forms;

namespace BrowserSelector
{
    public class UacHelper
    {

        public static bool ElevationRequired()
        {
            WindowsPrincipal pricipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            bool hasAdministrativeRight = pricipal.IsInRole(WindowsBuiltInRole.Administrator);
            return !hasAdministrativeRight;
        }


        public static void Elevate()
        {
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.Verb = "runas";
            processInfo.FileName = Application.ExecutablePath;

            try
            {
                Process p = Process.Start(processInfo);
                Application.Exit();
            }
            catch (Win32Exception)
            {
                //Do nothing. Probably the user canceled the UAC window
            }
        }
    }
}
