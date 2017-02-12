using System;
using System.Runtime.InteropServices;

namespace JGCompTech.CSharp.Tools.OSInfo
{
    /// <summary>
    /// Gets the service pack information of the operating system running on this Computer.
    /// </summary>
    public static class ServicePack
    {
        /// <summary>
        /// Returns the service pack information of the operating system running on this Computer.
        /// </summary>
        /// <returns>A String containing the operating system service pack inFormation.</returns>
        ///
        public static String String
        {
            get
            {
                var sp = Environment.OSVersion.ServicePack;
                return CheckIf.IsWin8OrLater ? String.Empty : (sp.IsNullOrEmpty() ? "Service Pack 0" : sp);
            }
        }

        /// <summary>
        /// Returns the service pack information of the operating system running on this Computer.
        /// </summary>
        /// <returns>A int containing the operating system service pack number.</returns>
        ///
        public static int Number
        {
            get
            {
                var osVersionInfo = new NativeMethods.OSVERSIONINFOEX
                {
                    dwOSVersionInfoSize = Marshal.SizeOf(typeof(NativeMethods.OSVERSIONINFOEX))
                };
                return !NativeMethods.GetVersionEx(ref osVersionInfo) ? -1 : osVersionInfo.wServicePackMajor;
            }
        }
    }
}
