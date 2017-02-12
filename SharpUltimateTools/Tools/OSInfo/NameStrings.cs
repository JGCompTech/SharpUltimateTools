using JGCompTech.CSharp.Tools.OSInfo.Enums;
using System;
using System.Globalization;
using static JGCompTech.CSharp.Tools.RegistryInfo;

namespace JGCompTech.CSharp.Tools.OSInfo
{
    /// <summary>
    /// Gets the different names provides by the operating system.
    /// </summary>
    public static class Name
    {
        /// <summary>
        /// Return a full version String, es.: "Windows XP Service Pack 2 (32 Bit)"
        /// </summary>
        /// <returns>A String representing a fully displayable version</returns>
        public static String StringExpanded
        {
            get
            {
                var ServicePack = String.Empty;
                if (CheckIf.IsWin8OrLater)
                {
                    ServicePack = " - " + Version.Build.ToString(CultureInfo.CurrentCulture);
                    return String + " " + Edition.String + ServicePack + " (" + Architecture.Number + " Bit)";
                }
                var SPString = ServicePack;
                ServicePack = " SP" + SPString.Substring(SPString.Length - 1);
                return String + " " + Edition.String + ServicePack + " (" + Architecture.Number + " Bit)";
            }
        }

        public static String StringExpanded2
        {
            get
            {
                String key = "Software\\\\Microsoft\\\\Windows NT\\\\CurrentVersion";
                String value = "ProductName";
                String name = RegistryInfo.getStringValue(HKEY.LOCAL_MACHINE, key, value);

                var ServicePack = String.Empty;
                if (CheckIf.IsWin8OrLater)
                {
                    ServicePack = " - " + Version.Build.ToString(CultureInfo.CurrentCulture);
                    return name + " " + ServicePack + " (" + Architecture.Number + " Bit)";
                }
                var SPString = ServicePack;
                ServicePack = " SP" + SPString.Substring(SPString.Length - 1);
                return name + ServicePack + " (" + Architecture.Number + " Bit)";
            }
        }

        /// <summary>
        /// Returns the name of the operating system running on this Computer.
        /// </summary>
        public static OSList Enum
        {
            get
            {
                switch (Version.Number)
                {
                    case 51:
                        return OSList.WindowsXP;

                    case 52:
                        return CheckIf.IsServer ? (NativeMethods.GetSystemMetrics((int)OtherConsts.SMServerR2) ? OSList.Windows2003R2 : OSList.Windows2003) : OSList.WindowsXP64;
                    case 60:
                        return CheckIf.IsServer ? OSList.Windows2008 : OSList.WindowsVista;

                    case 61:
                        return CheckIf.IsServer ? OSList.Windows2008R2 : OSList.Windows7;

                    case 62:
                        return CheckIf.IsServer ? OSList.Windows2012 : OSList.Windows8;

                    case 63:
                        return CheckIf.IsServer ? OSList.Windows2012R2 : OSList.Windows81;

                    case 64:
                        return CheckIf.IsServer ? OSList.Windows2016 : OSList.Windows10;
                }
                return OSList.Windows2000AndPrevious;
            }
        }

        /// <summary>
        /// Returns the name of the operating system running on this Computer.
        /// </summary>
        /// <returns>A String containing the the operating system name.</returns>
        public static String String
        {
            get
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32Windows:
                        {
                            switch (Version.Minor)
                            {
                                case 0: return "Windows 95";
                                case 10: return Environment.OSVersion.Version.Revision.ToString(CultureInfo.CurrentCulture) == "2222A" ? "Windows 98 Second Edition" : "Windows 98";
                                case 90: return "Windows Me";
                            }
                            break;
                        }

                    case PlatformID.Win32NT:
                        {
                            switch (Version.Major)
                            {
                                case 3: return "Windows NT 3.51";
                                case 4: return "Windows NT 4.0";
                                case 5:
                                    {
                                        switch (Version.Minor)
                                        {
                                            case 0: return "Windows 2000";
                                            case 1: return "Windows XP";
                                            case 2:
                                                return CheckIf.IsServer ? (NativeMethods.GetSystemMetrics((int)OtherConsts.SMServerR2) ? "Windows Server 2003 R2" : "Windows Server 2003") : "WindowsXP x64";
                                        }
                                        break;
                                    }
                                case 6:
                                    {
                                        switch (Version.Minor)
                                        {
                                            case 0: return CheckIf.IsServer ? "Windows 2008" : "Windows Vista";
                                            case 1: return CheckIf.IsServer ? "Windows 2008 R2" : "Windows 7";
                                            case 2: return CheckIf.IsServer ? "Windows 2012" : "Windows 8";
                                            case 3: return CheckIf.IsServer ? "Windows 2012 R2" : "Windows 8.1";
                                        }
                                        break;
                                    }
                                case 10:
                                    {
                                        switch (Version.Minor)
                                        {
                                            case 0: return CheckIf.IsServer ? "Windows 2016" : "Windows 10";
                                        }
                                        break;
                                    }
                            }
                            break;
                        }
                }
                return "UNKNOWN";
            }
        }

        /// <summary>
        /// Gets the current Computer name.
        /// </summary>
        public static String ComputerNameActive
        {
            get
            {
                String key = "System\\ControlSet001\\Control\\ComputerName\\ActiveComputerName";
                String value = "ComputerNameActive";
                return RegistryInfo.getStringValue(HKEY.LOCAL_MACHINE, key, value);
            }
        }

        /// <summary>
        /// Gets the pending Computer name that it will update to on reboot.
        /// </summary>
        public static String ComputerNamePending
        {
            get
            {
                String key = "System\\ControlSet001\\Control\\ComputerName\\ActiveComputerName";
                String value = "ComputerNameActive";
                String text = RegistryInfo.getStringValue(HKEY.LOCAL_MACHINE, key, value);
                return text.Equals(ComputerNameActive) ? "N/A" : text;
            }
        }
    }
}
