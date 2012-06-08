using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace BrowserSelector
{
    /* 
     * Example using ExtractIconEx
     * Created by Martin Hyldahl (alanadin@post8.tele.dk)
     * http://www.hyldahlnet.dk
     */

    /// <summary>
    /// Example using ExtractIconEx
    /// </summary>
    public class IconExtracter
    {
        /* CONSTRUCTORS */
        static IconExtracter()
        {
        }

        // HIDE INSTANCE CONSTRUCTOR
        private IconExtracter()
        {
        }

        [DllImport("Shell32", CharSet = CharSet.Auto)]
        private static extern int ExtractIconEx(
            string lpszFile,
            int nIconIndex,
            IntPtr[] phIconLarge,
            IntPtr[] phIconSmall,
            int nIcons);

        [DllImport("user32.dll", EntryPoint = "DestroyIcon", SetLastError = true)]
        private static extern int DestroyIcon(IntPtr hIcon);

        public static Icon ExtractIconFromExe(string file, bool large)
        {
            return ExtractIconFromExe(file, 0, large);
        }

        public static Icon ExtractIconFromExe(string file, int index, bool large)
        {
            int readIconCount = 0;
            IntPtr[] hDummy = new IntPtr[10] { IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero };
            IntPtr[] hIconEx = new IntPtr[10] { IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero };

            try
            {
                if (large)
                    readIconCount = ExtractIconEx(file, index, hIconEx, hDummy, hIconEx.Length);
                else
                    readIconCount = ExtractIconEx(file, index, hDummy, hIconEx, hIconEx.Length);

                if (readIconCount > 0 && hIconEx[0] != IntPtr.Zero)
                {
                    // GET FIRST EXTRACTED ICON
                    Icon extractedIcon = (Icon)Icon.FromHandle(hIconEx[0]).Clone();

                    return extractedIcon;
                }
                else // NO ICONS READ
                    return null;
            }
            catch (Exception ex)
            {
                /* EXTRACT ICON ERROR */

                // BUBBLE UP
                throw new ApplicationException("Could not extract icon", ex);
            }
            finally
            {
                // RELEASE RESOURCES
                foreach (IntPtr ptr in hIconEx)
                    if (ptr != IntPtr.Zero)
                        DestroyIcon(ptr);

                foreach (IntPtr ptr in hDummy)
                    if (ptr != IntPtr.Zero)
                        DestroyIcon(ptr);
            }

        }
    }
}
