using JGCompTech.CSharp.Tools.OSInfo.Enums;
using System;
using System.Globalization;

namespace JGCompTech.CSharp.Tools.OSInfo
{
    /// <summary>
    /// Runs different checks against the OS and returns a Boolean value
    /// </summary>
    public static class CheckIf
    {
        /// <summary>
        /// Checks If Windows Is Activated. Uses the newer WMI.
        /// </summary>
        /// <returns>Licensed If Genuinely Activated</returns>
        ///
        public static String IsActivatedWMI
        {
            get
            {
                var str = String.Empty;
                try
                {
                    const String ComputerName = "localhost";
                    var Scope = new System.Management.ManagementScope(String.Format(CultureInfo.CurrentCulture, @"\\{0}\root\CIMV2", ComputerName), null);

                    Scope.Connect();
                    var Query = new System.Management.ObjectQuery("SELECT * FROM SoftwareLicensingProduct Where PartialProductKey <> null AND ApplicationId='55c92734-d682-4d71-983e-d6ec3f16059f' AND LicenseIsAddon=False");
                    using (var Searcher = new System.Management.ManagementObjectSearcher(Scope, Query))
                    {
                        foreach (var WmiObject in Searcher.Get())
                        {
                            switch ((uint)WmiObject["LicenseStatus"])
                            {
                                case 0:
                                    str = "Unlicensed";
                                    break;

                                case 1:
                                    str = "Licensed";
                                    break;

                                case 2:
                                    str = "Out-Of-Box Grace";
                                    break;

                                case 3:
                                    str = "Out-Of-Tolerance Grace";
                                    break;

                                case 4:
                                    str = "Non Genuine Grace";
                                    break;

                                case 5:
                                    str = "Notification";
                                    break;

                                case 6:
                                    str = "Extended Grace";
                                    break;

                                default:
                                    str = "Unknown License Status";
                                    break;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    str = "Unknown License Status";
                }
                return str;
            }
        }

        /// <summary>
        /// Checks If Windows Is Activated. Uses the older Software Licensing Manager Script.
        /// </summary>
        /// <returns>Licensed If Genuinely Activated</returns>
        ///
        public static String IsActivatedSLMGR
        {
            get
            {
                var strActivationStatus = String.Empty;
                var results = CommandInfo.Run(@"cscript C:\Windows\System32\Slmgr.vbs /dli", true);

                foreach (var line in results.Result)
                {
                    if (line.Contains("License Status: ")) strActivationStatus = line.Remove(0, 16);
                }

                return strActivationStatus;
            }
        }

        #region Later Checks

        /// <summary>
        /// Return if running on XP or later
        /// </summary>
        /// <returns>true means XP or later</returns>
        /// <returns>false means 2000 or previous</returns>
        public static Boolean IsXPOrLater => Version.Number >= 51;

        /// <summary>
        /// Return if running on XP 64 or later
        /// </summary>
        /// <returns>true means XP 64 or later</returns>
        /// <returns>false means XP or previous</returns>
        public static Boolean IsXP64OrLater => Version.Number >= 52;

        /// <summary>
        /// Return if running on Vista or later
        /// </summary>
        /// <returns>true means Vista or later</returns>
        /// <returns>false means Xp or previous</returns>
        public static Boolean IsVistaOrLater => Version.Number >= 60;

        /// <summary>
        /// Return if running on Windows7 or later
        /// </summary>
        /// <returns>true means Windows7 or later</returns>
        /// <returns>false means Vista or previous</returns>
        public static Boolean IsWin7OrLater => Version.Number >= 61;

        /// <summary>
        /// Return if running on Windows8 or later
        /// </summary>
        /// <returns>true means Windows8 or later</returns>
        /// <returns>false means Win7 or previous</returns>
        public static Boolean IsWin8OrLater => Version.Number >= 62;

        /// <summary>
        /// Return if running on Windows8.1 or later
        /// </summary>
        /// <returns>true means Windows8.1 or later</returns>
        /// <returns>false means Win8 or previous</returns>
        public static Boolean IsWin81OrLater => Version.Number >= 63;

        /// <summary>
        /// Return if running on Windows10 or later
        /// </summary>
        /// <returns>true means Windows10 or later</returns>
        /// <returns>false means Win10 or previous</returns>
        public static Boolean IsWin10OrLater => Version.Number >= 100;

        #endregion Later Checks

        /// <summary>
        /// Identifies if OS is a 64 Bit OS
        /// </summary>
        /// <returns>True if OS is a 64 Bit OS</returns>
        ///
        public static Boolean Is64BitOS => (IntPtr.Size == 8 || (IntPtr.Size == 4 && Is32BitProcessOn64BitProcessor));

        /// <summary>
        /// Identifies if OS is a Windows Server OS
        /// </summary>
        /// <returns>True if OS is a Windows Server OS</returns>
        ///
        public static Boolean IsServer => (ProductType)Edition.ProductType != ProductType.NTWorkstation;

        /// <summary>
        /// Identifies if OS is a Windows Domain Controller
        /// </summary>
        /// <returns>True if OS is a Windows Server OS</returns>
        ///
        public static Boolean IsDomainController => (ProductType)Edition.ProductType == ProductType.NTDomainController;

        /// <summary>
        /// Identifies Arch of running process
        /// </summary>
        /// <returns>True if process is 32bit running on a 64bit machine</returns>
        ///
        public static Boolean Is32BitProcessOn64BitProcessor
        {
            get
            {
                if (IntPtr.Size == 8) return true; // 64-bit programs run only on Win64
                                                   // 32-bit programs run on both 32-bit and 64-bit Windows
                                                   // Detect whether the current process is a 32-bit process running on a 64-bit system.
                Boolean flag;
                return (Win32MethodExists && NativeMethods.IsWow64Process(NativeMethods.GetCurrentProcess(), out flag)) && flag;
            }
        }

        /// <summary>
        /// The function determines whether a method exists in the export
        /// table of a certain module.
        /// </summary>
        internal static Boolean Win32MethodExists
        {
            get
            {
                var moduleHandle = NativeMethods.GetModuleHandle("kernel32.dll");
                if (moduleHandle == IntPtr.Zero) return false;
                return NativeMethods.GetProcAddress(moduleHandle, "IsWow64Process") != IntPtr.Zero;
            }
        }
    }
}
