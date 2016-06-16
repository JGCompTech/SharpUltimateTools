using Microsoft.Win32;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CultureInfo = System.Globalization.CultureInfo;

// Returns information about the computer's operating system.
namespace Microsoft.CSharp.Tools.OSInfo
{
    /// <summary>
    /// General constants
    /// </summary>
    /// <summary>
    /// A list of Product Editions according to ( http://msdn.microsoft.com/en-us/library/ms724358(VS.85).aspx )
    /// </summary>
    [Flags]
    public enum ProductEdition
    {
        /// <summary>
        /// Business
        /// </summary>
        Business = 6,
        /// <summary>
        /// BusinessN
        /// </summary>
        BusinessN = 16,
        /// <summary>
        /// ClusterServer
        /// </summary>
        ClusterServer = 18,
        /// <summary>
        /// DatacenterServer
        /// </summary>
        DatacenterServer = 8,
        /// <summary>
        /// DatacenterServerCore
        /// </summary>
        DatacenterServerCore = 12,
        /// <summary>
        /// DatacenterServerCoreV
        /// </summary>
        DatacenterServerCoreV = 39,
        /// <summary>
        /// DatacenterServerV
        /// </summary>
        DatacenterServerV = 37,

        //DeveloperPreview = 74,
        /// <summary>
        /// Enterprise
        /// </summary>
        Enterprise = 4,

        /// <summary>
        /// EnterpriseE
        /// </summary>
        EnterpriseE = 70,
        /// <summary>
        /// EnterpriseN
        /// </summary>
        EnterpriseN = 27,
        /// <summary>
        /// EnterpriseServer
        /// </summary>
        EnterpriseServer = 10,
        /// <summary>
        /// EnterpriseServerCore
        /// </summary>
        EnterpriseServerCore = 14,
        /// <summary>
        /// EnterpriseServerCoreV
        /// </summary>
        EnterpriseServerCoreV = 41,
        /// <summary>
        /// EnterpriseServerIA64
        /// </summary>
        EnterpriseServerIA64 = 15,
        /// <summary>
        /// EnterpriseServerV
        /// </summary>
        EnterpriseServerV = 38,
        /// <summary>
        /// HomeBasic
        /// </summary>
        HomeBasic = 2,
        /// <summary>
        /// HomeBasicE
        /// </summary>
        HomeBasicE = 67,
        /// <summary>
        /// HomeBasicN
        /// </summary>
        HomeBasicN = 5,
        /// <summary>
        /// HomePremium
        /// </summary>
        HomePremium = 3,
        /// <summary>
        /// HomePremiumE
        /// </summary>
        HomePremiumE = 68,
        /// <summary>
        /// HomePremiumN
        /// </summary>
        HomePremiumN = 26,

        //HomePremiumServer = 34,
        //HyperV = 42,
        /// <summary>
        /// MediumBusinessServerManagement
        /// </summary>
        MediumBusinessServerManagement = 30,

        /// <summary>
        /// MediumBusinessServerMessaging
        /// </summary>
        MediumBusinessServerMessaging = 32,
        /// <summary>
        /// MediumBusinessServerSecurity
        /// </summary>
        MediumBusinessServerSecurity = 31,
        /// <summary>
        /// Professional
        /// </summary>
        Professional = 48,
        /// <summary>
        /// ProfessionalE
        /// </summary>
        ProfessionalE = 69,
        /// <summary>
        /// ProfessionalN
        /// </summary>
        ProfessionalN = 49,

        //SBSolutionServer = 50,
        /// <summary>
        /// ServerForSmallBusiness
        /// </summary>
        ServerForSmallBusiness = 24,

        /// <summary>
        /// ServerForSmallBusinessV
        /// </summary>
        ServerForSmallBusinessV = 35,

        //ServerFoundation = 33,
        /// <summary>
        /// SmallBusinessServer
        /// </summary>
        SmallBusinessServer = 9,

        //SmallBusinessServerPremium = 25,
        //SolutionEmbeddedServer = 56,
        /// <summary>
        /// StandardServer
        /// </summary>
        StandardServer = 7,

        /// <summary>
        /// StandardServerCore
        /// </summary>
        StandardServerCore = 13,
        /// <summary>
        /// StandardServerCoreV
        /// </summary>
        StandardServerCoreV = 40,
        /// <summary>
        /// StandardServerV
        /// </summary>
        StandardServerV = 36,
        /// <summary>
        /// Starter
        /// </summary>
        Starter = 11,
        /// <summary>
        /// StarterE
        /// </summary>
        StarterE = 66,
        /// <summary>
        /// StarterN
        /// </summary>
        StarterN = 47,
        /// <summary>
        /// StorageEnterpriseServer
        /// </summary>
        StorageEnterpriseServer = 23,
        /// <summary>
        /// StorageExpressServer
        /// </summary>
        StorageExpressServer = 20,
        /// <summary>
        /// StorageStandardServer
        /// </summary>
        StorageStandardServer = 21,
        /// <summary>
        /// StorageWorkgroupServer
        /// </summary>
        StorageWorkgroupServer = 22,
        /// <summary>
        /// Undefined
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// Ultimate
        /// </summary>
        Ultimate = 1,
        /// <summary>
        /// UltimateE
        /// </summary>
        UltimateE = 71,
        /// <summary>
        /// UltimateN
        /// </summary>
        UltimateN = 28,
        /// <summary>
        /// WebServer
        /// </summary>
        WebServer = 17,
        /// <summary>
        /// WebServerCore
        /// </summary>
        WebServerCore = 29
    }

    /// <summary>
    /// A list of Product Types according to ( http://msdn.microsoft.com/en-us/library/ms724833(VS.85).aspx )
    /// </summary>
    [Flags]
    public enum ProductType
    {
        /// <summary>
        /// Workstation
        /// </summary>
        NTWorkstation = 1,
        /// <summary>
        /// Domain Controller
        /// </summary>
        NTDomainController = 2,
        /// <summary>
        /// Server
        /// </summary>
        NTServer = 3
    }

    /// <summary>
    /// A list of Version Suite Masks according to ( http://msdn.microsoft.com/en-us/library/ms724833(VS.85).aspx )
    /// </summary>
    [Flags]
    public enum VERSuite
    {
        //SmallBusiness = 1,
        /// <summary>
        /// Enterprise
        /// </summary>
        Enterprise = 2,

        //BackOffice = 4,
        //Terminal = 16,
        //SmallBusinessRestricted = 32,
        /// <summary>
        /// EmbeddedNT
        /// </summary>
        EmbeddedNT = 64,

        /// <summary>
        /// Datacenter
        /// </summary>
        Datacenter = 128,

        //SingleUserTS = 256,
        /// <summary>
        /// Personal
        /// </summary>
        Personal = 512,

        /// <summary>
        /// Blade
        /// </summary>
        Blade = 1024,
        /// <summary>
        /// StorageServer
        /// </summary>
        StorageServer = 8192,
        /// <summary>
        /// ComputeServer
        /// </summary>
        ComputeServer = 16384
        //WHServer = 32768
    }
    [Flags]
    internal enum OtherConsts
    {
        //Type bitmask ( http://msdn.microsoft.com/en-gb/library/ms725494(vs.85).aspx )
        //VERMinorVersion = 1,
        //VERMajorVersion = 2,
        //VERBuildNumber = 4,
        //VERPlatformID = 8,
        //VERServicePackMinor = 16,
        //VERServicePackMajor = 32,
        //VERSuiteName = 64,
        //VERProductType = 128,

        //Condition bitmask ( http://msdn.microsoft.com/en-gb/library/ms725494(vs.85).aspx )
        //VEREqual = 1,
        //VERGreater = 2,
        //VERGreaterEqual = 3,
        //VERLess = 4,
        //VERLessEqual = 5,
        //VERAnd = 6, // only For wSuiteMask
        //VEROr = 7, // only For wSuiteMask

        //sysMetrics ( http://msdn.microsoft.com/en-us/library/ms724385(VS.85).aspx )
        SMTabletPC = 86,
        SMMediaCenter = 87,
        //SMStarter = 88,
        SMServerR2 = 89
    }

    /// <summary>
    /// Gets And Decrypts The Current Product Key From The Registry
    /// </summary>
    public static class ProductKey
    {
        /// <summary>
        /// Gets And Decrypts The Current Product Key From The Registry
        /// </summary>
        /// <returns>Returns Product Key As A String</returns>
        ///
        public static String Key
        {
            get
            {
                byte[] digitalProductId = null;
                RegistryKey HKLM = null;
                HKLM = CheckIf.Is64BitOS ? RegistryHives.LocalMachine64 : RegistryHives.LocalMachine;
                HKLM = HKLM.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion");
                digitalProductId = HKLM.GetValue("DigitalProductId") as byte[];

                if (digitalProductId.IsNull()) { return "Cannot Retrieve Product Key."; }

                return CheckIf.IsWin8OrLater ? DecodeKeyWin8AndUp(digitalProductId) : DecodeKey(digitalProductId);
            }
        }

        /// <summary>
        /// Returns the decoded product key from the provided byte array. Works with Windows 7 and below.
        /// </summary>
        /// <param name="digitalProductId"></param>
        public static String DecodeKey(byte[] digitalProductId)
        {
            digitalProductId.ExceptionIfNull("The specified " + nameof(digitalProductId) + " cannot be null!", nameof(digitalProductId));
            // Offset of first byte of encoded product key in
            //  'DigitalProductIdxxx" REGBINARY value. Offset = 34H.
            const int keyStartIndex = 52;
            // Offset of last byte of encoded product key in
            //  'DigitalProductIdxxx" REGBINARY value. Offset = 43H.
            const int keyEndIndex = keyStartIndex + 15;
            // Possible alpha-numeric characters in product key.
            const string digits = "BCDFGHJKMPQRTVWXY2346789";
            // Length of decoded product key
            const int decodeLength = 29;
            // Length of decoded product key in byte-Form.
            // Each byte represents 2 chars.
            const int decodeStringLength = 15;
            // Array of containing the decoded product key.
            var decodedChars = new char[decodeLength];
            // Extract byte 52 to 67 inclusive.
            var hexPid = new System.Collections.ArrayList();
            for (int i = keyStartIndex; i <= keyEndIndex; i++) hexPid.Add(digitalProductId[i]);
            for (int i = decodeLength - 1; i >= 0; i--)
            {
                // Every sixth char is a separator.
                if ((i + 1) % 6 == 0) decodedChars[i] = '-';
                else
                {
                    // Do the actual decoding.
                    var digitMapIndex = 0;
                    for (int j = decodeStringLength - 1; j >= 0; j--)
                    {
                        var byteValue = (digitMapIndex << 8) | (byte)hexPid[j];
                        hexPid[j] = (byte)(byteValue / 24);
                        digitMapIndex = byteValue % 24;
                        decodedChars[i] = digits[digitMapIndex];
                    }
                }
            }
            return new string(decodedChars);
        }

        /// <summary>
        /// Returns the decoded product key from the provided byte array. Works with Windows 8 and up.
        /// </summary>
        /// <param name="digitalProductId"></param>
        public static String DecodeKeyWin8AndUp(byte[] digitalProductId)
        {
            digitalProductId.ExceptionIfNull("The specified " + nameof(digitalProductId) + " cannot be null!", nameof(digitalProductId));
            var key = String.Empty;
            const int keyOffset = 52;
            var isWin8 = (byte)((digitalProductId[66] / 6) & 1);
            digitalProductId[66] = (byte)((digitalProductId[66] & 0xf7) | (isWin8 & 2) * 4);

            // Possible alpha-numeric characters in product key.
            const string digits = "BCDFGHJKMPQRTVWXY2346789";
            var last = 0;
            for (var i = 24; i >= 0; i--)
            {
                var current = 0;
                for (var j = 14; j >= 0; j--)
                {
                    current = current * 256;
                    current = digitalProductId[j + keyOffset] + current;
                    digitalProductId[j + keyOffset] = (byte)(current / 24);
                    current = current % 24;
                    last = current;
                }
                key = digits[current] + key;
            }
            var keypart1 = key.Substring(1, last);
            const string insert = "N";
            key = key.Substring(1).Replace(keypart1, keypart1 + insert);
            if (last == 0) key = insert + key;
            for (var i = 5; i < key.Length; i += 6) key = key.Insert(i, "-");
            return key;
        }
    }

    /// <summary>
    /// Gets the product type of the operating system running on this Computer.
    /// </summary>
    public static class Edition
    {
        /// <summary>
        /// Returns the product type of the operating system running on this Computer.
        /// </summary>
        /// <returns>A String containing the the operating system product type.</returns>
        public static String String
        {
            get
            {
                var osVersionInfo = new NativeMethods.OSVERSIONINFOEX
                {
                    dwOSVersionInfoSize = Marshal.SizeOf(typeof(NativeMethods.OSVERSIONINFOEX))
                };
                if (!NativeMethods.GetVersionEx(ref osVersionInfo)) return String.Empty;

                switch (Version.Major)
                {
                    case 4:
                        return GetVersion4(osVersionInfo);

                    case 5:
                        return GetVersion5(osVersionInfo);

                    case 6:
                    case 10:
                        return GetVersion6AndUp(osVersionInfo);
                }
                return String.Empty;
            }
        }

        /// <summary>
        /// Returns the product type from Windows NT
        /// </summary>
        /// <param name="osVersionInfo"></param>
        /// <returns></returns>
        static String GetVersion4(NativeMethods.OSVERSIONINFOEX osVersionInfo)
        {
            osVersionInfo.ExceptionIfNull(nameof(osVersionInfo) + " Cannot Be Null!", nameof(osVersionInfo));
            if (CheckIf.IsServer)
            {
                if ((osVersionInfo.wSuiteMask & (Int32)VERSuite.Enterprise) != 0)
                {
                    // Windows NT 4.0 Server Enterprise
                    return "Enterprise Server";
                }
                // Windows NT 4.0 Server
                return "Standard Server";
            }
            // Windows NT 4.0 Workstation
            return " Workstation";
        }

        /// <summary>
        /// Returns the product type from Windows 2000 to XP and Server 2000 to 2003
        /// </summary>
        /// <param name="osVersionInfo"></param>
        /// <returns></returns>
        static String GetVersion5(NativeMethods.OSVERSIONINFOEX osVersionInfo)
        {
            osVersionInfo.ExceptionIfNull(nameof(osVersionInfo) + " Cannot Be Null!", nameof(osVersionInfo));
            if (NativeMethods.GetSystemMetrics((Int32)OtherConsts.SMMediaCenter)) return " Media Center";
            if (NativeMethods.GetSystemMetrics((Int32)OtherConsts.SMTabletPC)) return " Tablet PC";
            if (CheckIf.IsServer)
            {
                if (Version.Minor == 0)
                {
                    if (((VERSuite)osVersionInfo.wSuiteMask & VERSuite.Datacenter) == VERSuite.Datacenter)
                    {
                        // Windows 2000 Datacenter Server
                        return " Datacenter Server";
                    }
                    if (((VERSuite)osVersionInfo.wSuiteMask & VERSuite.Enterprise) == VERSuite.Enterprise)
                    {
                        // Windows 2000 Advanced Server
                        return " Advanced Server";
                    }
                    // Windows 2000 Server
                    return " Server";
                }
                if (Version.Minor == 2)
                {
                    if (((VERSuite)osVersionInfo.wSuiteMask & VERSuite.Datacenter) == VERSuite.Datacenter)
                    {
                        // Windows Server 2003 Datacenter Edition
                        return " Datacenter Edition";
                    }
                    if (((VERSuite)osVersionInfo.wSuiteMask & VERSuite.Enterprise) == VERSuite.Enterprise)
                    {
                        // Windows Server 2003 Enterprise Edition
                        return " Enterprise Edition";
                    }
                    if (((VERSuite)osVersionInfo.wSuiteMask & VERSuite.StorageServer) == VERSuite.StorageServer)
                    {
                        // Windows Server 2003 Storage Edition
                        return " Storage Edition";
                    }
                    if (((VERSuite)osVersionInfo.wSuiteMask & VERSuite.ComputeServer) == VERSuite.ComputeServer)
                    {
                        // Windows Server 2003 Compute Cluster Edition
                        return " Compute Cluster Edition";
                    }
                    if (((VERSuite)osVersionInfo.wSuiteMask & VERSuite.Blade) == VERSuite.Blade)
                    {
                        // Windows Server 2003 Web Edition
                        return " Web Edition";
                    }
                    // Windows Server 2003 Standard Edition
                    return " Standard Edition";
                }
            }
            else
            {
                if (((VERSuite)osVersionInfo.wSuiteMask & VERSuite.EmbeddedNT) == VERSuite.EmbeddedNT)
                {
                    //Windows XP Embedded
                    return " Embedded";
                }
                // Windows XP / Windows 2000 Professional
                return ((VERSuite)osVersionInfo.wSuiteMask & VERSuite.Personal) == VERSuite.Personal ? " Home" : " Professional";
            }
            return String.Empty;
        }

        /// <summary>
        /// Returns the product type from Windows Vista to 10 and Server 2008 to 2016
        /// </summary>
        /// <param name="osVersionInfo"></param>
        /// <returns></returns>
        static String GetVersion6AndUp(NativeMethods.OSVERSIONINFOEX osVersionInfo)
        {
            osVersionInfo.ExceptionIfNull(nameof(osVersionInfo) + " Cannot Be Null!", nameof(osVersionInfo));
            Int32 strProductType;
            NativeMethods.GetProductInfo(osVersionInfo.dwMajorVersion, osVersionInfo.dwMinorVersion, 0, 0, out strProductType);
            switch ((ProductEdition)strProductType)
            {
                case ProductEdition.Ultimate:
                case ProductEdition.UltimateE:
                case ProductEdition.UltimateN:
                    return "Ultimate Edition";

                case ProductEdition.Professional:
                case ProductEdition.ProfessionalE:
                case ProductEdition.ProfessionalN:
                    return "Professional";

                case ProductEdition.HomePremium:
                case ProductEdition.HomePremiumE:
                case ProductEdition.HomePremiumN:
                    return "Home Premium Edition";

                case ProductEdition.HomeBasic:
                case ProductEdition.HomeBasicE:
                case ProductEdition.HomeBasicN:
                    return "Home Basic Edition";

                case ProductEdition.Enterprise:
                case ProductEdition.EnterpriseE:
                case ProductEdition.EnterpriseN:
                case ProductEdition.EnterpriseServerV:
                    return "Enterprise Edition";

                case ProductEdition.Business:
                case ProductEdition.BusinessN:
                    return "Business Edition";

                case ProductEdition.Starter:
                case ProductEdition.StarterE:
                case ProductEdition.StarterN:
                    return "Starter Edition";

                case ProductEdition.ClusterServer:
                    return "Cluster Server Edition";

                case ProductEdition.DatacenterServer:
                case ProductEdition.DatacenterServerV:
                    return "Datacenter Edition";

                case ProductEdition.DatacenterServerCore:
                case ProductEdition.DatacenterServerCoreV:
                    return "Datacenter Edition (Core installation)";

                case ProductEdition.EnterpriseServer:
                    return "Enterprise Edition";

                case ProductEdition.EnterpriseServerCore:
                case ProductEdition.EnterpriseServerCoreV:
                    return "Enterprise Edition (Core installation)";

                case ProductEdition.EnterpriseServerIA64:
                    return "Enterprise Edition For Itanium-based Systems";

                case ProductEdition.SmallBusinessServer:
                    return "Small Business Server";
                //case ProductType.SmallBusinessServerPremium:
                //  return "Small Business Server Premium Edition";
                case ProductEdition.ServerForSmallBusiness:
                case ProductEdition.ServerForSmallBusinessV:
                    return "Windows Essential Server Solutions";

                case ProductEdition.StandardServer:
                case ProductEdition.StandardServerV:
                    return "Standard Edition";

                case ProductEdition.StandardServerCore:
                case ProductEdition.StandardServerCoreV:
                    return "Standard Edition (Core installation)";

                case ProductEdition.WebServer:
                case ProductEdition.WebServerCore:
                    return "Web Server Edition";

                case ProductEdition.MediumBusinessServerManagement:
                case ProductEdition.MediumBusinessServerMessaging:
                case ProductEdition.MediumBusinessServerSecurity:
                    return "Windows Essential Business Server";

                case ProductEdition.StorageEnterpriseServer:
                case ProductEdition.StorageExpressServer:
                case ProductEdition.StorageStandardServer:
                case ProductEdition.StorageWorkgroupServer:
                    return "Storage Server";
            }
            return String.Empty;
        }

        /// <summary>
        /// Gets the product type of the operating system running on this Computer.
        /// </summary>
        public static Byte ProductType
        {
            get
            {
                var osVersionInfo = new NativeMethods.OSVERSIONINFOEX
                {
                    dwOSVersionInfoSize = Marshal.SizeOf(typeof(NativeMethods.OSVERSIONINFOEX))
                };
                if (!NativeMethods.GetVersionEx(ref osVersionInfo)) return (Int32)ProductEdition.Undefined;
                return osVersionInfo.wProductType;
            }
        }
    }

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
        /// <returns>A Int32 containing the operating system service pack number.</returns>
        ///
        public static Int32 Number
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

    /// <summary>
    /// Gets the different names provides by the operating system.
    /// </summary>
    public static class NameStrings
    {
        /// <summary>
        /// List of all operating systems
        /// </summary>
        public enum OSList
        {
            ///<summary>
            /// Windows 95/98, NT4.0, 2000
            ///</summary>
            Windows2000AndPrevious,
            ///<summary>
            /// Windows XP x86
            ///</summary>
            WindowsXP,
            ///<summary>
            /// Windows XP x64
            ///</summary>
            WindowsXP64,
            ///<summary>
            /// Windows Vista
            ///</summary>
            WindowsVista,
            ///<summary>
            /// Windows 7
            ///</summary>
            Windows7,
            ///<summary>
            /// Windows 8
            ///</summary>
            Windows8,
            ///<summary>
            /// Windows 8
            ///</summary>
            Windows81,
            ///<summary>
            /// Windows 10
            ///</summary>
            Windows10,
            ///<summary>
            /// Windows 2003 Server
            ///</summary>
            Windows2003,
            ///<summary>
            /// Windows 2003 R2 Server
            ///</summary>
            Windows2003R2,
            ///<summary>
            /// Windows 2008 Server
            ///</summary>
            Windows2008,
            ///<summary>
            /// Windows 2008 R2 Server
            ///</summary>
            Windows2008R2,
            ///<summary>
            /// Windows 2012 Server
            ///</summary>
            Windows2012,
            ///<summary>
            /// Windows 2012 R2 Server
            ///</summary>
            Windows2012R2,
            ///<summary>
            /// Windows 2016 Server
            ///</summary>
            Windows2016
        }

        /// <summary>
        /// Return a full version String, es.: "Windows XP Service Pack 2 (32 Bit)"
        /// </summary>
        /// <returns>A String representing a fully displayable version</returns>
        public static String DisplayVersion
        {
            get
            {
                var ServicePack = String.Empty;
                if (CheckIf.IsWin8OrLater)
                {
                    ServicePack = " - " + Version.Build.ToString(CultureInfo.CurrentCulture);
                    return NameString + " " + Edition.String + ServicePack + " (" + Architecture.Number + " Bit)";
                }
                var SPString = ServicePack;
                ServicePack = " SP" + SPString.Substring(SPString.Length - 1);
                var key = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion");
                return key.GetValue("ProductName") + ServicePack + " (" + Architecture.Number + " Bit)";
            }
        }

        /// <summary>
        /// Returns the name of the operating system running on this Computer.
        /// </summary>
        public static OSList Name
        {
            get
            {
                switch (Version.Number)
                {
                    case 51:
                        return OSList.WindowsXP;

                    case 52:
                        return CheckIf.IsServer ? (NativeMethods.GetSystemMetrics((Int32)OtherConsts.SMServerR2) ? OSList.Windows2003R2 : OSList.Windows2003) : OSList.WindowsXP64;
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
        public static String NameString
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
                                                return CheckIf.IsServer ? (NativeMethods.GetSystemMetrics((Int32)OtherConsts.SMServerR2) ? "Windows Server 2003 R2" : "Windows Server 2003") : "WindowsXP x64";
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
                var key = Registry.LocalMachine.OpenSubKey("System\\ControlSet001\\Control\\ComputerName\\ActiveComputerName");
                return key.GetValue("ComputerName").ToString();
            }
        }

        /// <summary>
        /// Gets the pending Computer name that it will update to on reboot.
        /// </summary>
        public static String ComputerNamePending
        {
            get
            {
                var key = Registry.LocalMachine.OpenSubKey("System\\ControlSet001\\Control\\ComputerName\\ComputerName");
                return key.GetValue("ComputerName").ToString() == ComputerNameActive ? "N/A" : key.GetValue("ComputerName").ToString();
            }
        }
    }

    /// <summary>
    /// Gets the full version of the operating system running on this Computer.
    /// </summary>
    ///
    public static class Version
    {
        /// <summary>
        /// Gets the full version of the operating system running on this Computer. Uses the deprecated OSVersion.
        /// </summary>
        ///
        [Obsolete("MainOSV is deprecated, please use Main instead.")]
        public static String MainOSV => Environment.OSVersion.Version.ToString();

        /// <summary>
        /// Gets the full version of the operating system running on this Computer. Uses the newer WMI.
        /// </summary>
        ///
        public static String Main => GetVersionInfo(VersionType.Main);

        /// <summary>
        /// Gets the major version of the operating system running on this Computer. Uses the deprecated OSVersion.
        /// </summary>
        [Obsolete("MajorOSV is deprecated, please use Major instead.")]
        public static Int32 MajorOSV => Environment.OSVersion.Version.Major;

        /// <summary>
        /// Gets the major version of the operating system running on this Computer. Uses the newer WMI.
        /// </summary>
        public static Int32 Major => Convert.ToInt32(GetVersionInfo(VersionType.Major), CultureInfo.CurrentCulture);

        /// <summary>
        /// Gets the minor version of the operating system running on this Computer. Uses the deprecated OSVersion.
        /// </summary>
        [Obsolete("MinorOSV is deprecated, please use Minor instead.")]
        public static Int32 MinorOSV => Environment.OSVersion.Version.Minor;

        /// <summary>
        /// Gets the minor version of the operating system running on this Computer. Uses the newer WMI.
        /// </summary>
        public static Int32 Minor => Convert.ToInt32(GetVersionInfo(VersionType.Minor), CultureInfo.CurrentCulture);

        /// <summary>
        /// Gets the build version of the operating system running on this Computer. Uses the deprecated OSVersion.
        /// </summary>
        [Obsolete("BuildOSV is deprecated, please use Build instead.")]
        public static Int32 BuildOSV => Environment.OSVersion.Version.Build;

        /// <summary>
        /// Gets the build version of the operating system running on this Computer. Uses the newer WMI.
        /// </summary>
        public static Int32 Build => Convert.ToInt32(GetVersionInfo(VersionType.Build), CultureInfo.CurrentCulture);

        /// <summary>
        /// Gets the revision version of the operating system running on this Computer. Uses the deprecated OSVersion.
        /// </summary>
        [Obsolete("BuildOSV is deprecated, please use Build instead.")]
        public static Int32 RevisionOSV => Environment.OSVersion.Version.Revision;

        /// <summary>
        /// Gets the revision version of the operating system running on this Computer. Uses the newer WMI.
        /// </summary>
        public static Int32 Revision => Convert.ToInt32(GetVersionInfo(VersionType.Revision), CultureInfo.CurrentCulture);

        /// <summary>
        /// Return a numeric value representing OS version. Uses the deprecated OSVersion.
        /// </summary>
        /// <returns>(OSMajorVersion * 10 + OSMinorVersion)</returns>
        [Obsolete("IntNumOSV is deprecated, please use IntNum instead.")]
        public static Int32 NumberOSV => (MajorOSV * 10 + MinorOSV);

        /// <summary>
        /// Return a numeric value representing OS version. Uses the newer WMI.
        /// </summary>
        /// <returns>(OSMajorVersion * 10 + OSMinorVersion)</returns>
        public static Int32 Number => (Major * 10 + Minor);

        internal static String GetVersionInfo(VersionType type)
        {
            try
            {
                var VersionString = String.Empty;
                using (System.Management.ManagementObjectSearcher objMOS = new System.Management.ManagementObjectSearcher("SELECT * FROM  Win32_OperatingSystem"))
                {
                    foreach (System.Management.ManagementObject o in objMOS.Get()) { VersionString = o[nameof(Version)].ToString(); }
                }

                var Temp = String.Empty;
                var Major = VersionString.Substring(0, VersionString.IndexOf(".", StringComparison.CurrentCulture));
                Temp = VersionString.Substring(Major.Length + 1);
                var Minor = Temp.Substring(0, VersionString.IndexOf(".", StringComparison.CurrentCulture) - 1);
                Temp = VersionString.Substring(Major.Length + 1 + Minor.Length + 1);
                String Build;
                if (Temp.Contains("."))
                {
                    Build = Temp.Substring(0, VersionString.IndexOf(".", StringComparison.CurrentCulture) - 1);
                    Temp = VersionString.Substring(Major.Length + 1 + Minor.Length + 1 + Build.Length + 1);
                }
                else
                {
                    Build = Temp;
                    Temp = "0";
                }
                var Revision = Temp;

                var ReturnString = "0";
                switch (type)
                {
                    case VersionType.Main:
                        ReturnString = VersionString;
                        break;

                    case VersionType.Major:
                        ReturnString = Major;
                        break;

                    case VersionType.Minor:
                        ReturnString = Minor;
                        break;

                    case VersionType.Build:
                        ReturnString = Build;
                        break;

                    case VersionType.Revision:
                        ReturnString = Revision;
                        break;
                }

                if (ReturnString.IsNullOrEmpty()) return "0";
                return ReturnString;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "0";
            }
        }

        internal enum VersionType
        {
            Main,
            Major,
            Minor,
            Build,
            Revision
        }
    }

    /// <summary>
    /// Determines if the current application is 32 or 64-bit.
    /// </summary>
    public static class Architecture
    {
        /// <summary>
        /// Determines if the current application is 32 or 64-bit.
        /// </summary>
        public static String String => CheckIf.Is64BitOS ? "64 bit" : "32 bit";

        /// <summary>
        /// Determines if the current application is 32 or 64-bit.
        /// </summary>
        public static Int32 Number => CheckIf.Is64BitOS ? 64 : 32;
    }

    /// <summary>
    /// Gets info about the currently logged in user account.
    /// </summary>
    public static class UserInfo
    {
        /// <summary>
        /// Gets the current Registered Organization.
        /// </summary>
        public static String RegisteredOrganization
        {
            get
            {
                try
                {
                    return (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows NT\CurrentVersion", nameof(RegisteredOrganization), "");
                }
                catch (NullReferenceException)
                {
                    return String.Empty;
                }
            }
        }

        /// <summary>
        /// Gets the current Registered Owner.
        /// </summary>
        public static String RegisteredOwner
        {
            get
            {
                try
                {
                    return (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows NT\CurrentVersion", nameof(RegisteredOwner), "");
                }
                catch (NullReferenceException)
                {
                    return String.Empty;
                }
            }
        }

        /// <summary>
        /// Gets the user name of the person who is currently logged on to the Windows operating system.
        /// </summary>
        public static String LoggedInUserName => Environment.UserName;

        /// <summary>
        /// Gets the network domain name associated with the current user.
        /// </summary>
        /// <exception cref="PlatformNotSupportedException">The operating system does not support retrieving the network domain name.</exception>
        /// <exception cref="InvalidOperationException">The network domain name cannot be retrieved.</exception>
        public static String CurrentDomainName => Environment.UserDomainName;
    }

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
                    System.Management.ManagementScope Scope;
                    Scope = new System.Management.ManagementScope(String.Format(CultureInfo.CurrentCulture, "\\\\{0}\\root\\CIMV2", ComputerName), null);

                    Scope.Connect();
                    var Query = new System.Management.ObjectQuery("SELECT * FROM SoftwareLicensingProduct Where PartialProductKey <> null AND ApplicationId='55c92734-d682-4d71-983e-d6ec3f16059f' AND LicenseIsAddon=False");
                    using (System.Management.ManagementObjectSearcher Searcher = new System.Management.ManagementObjectSearcher(Scope, Query))
                    {
                        foreach (System.Management.ManagementObject WmiObject in Searcher.Get())
                        {
                            switch ((UInt32)WmiObject["LicenseStatus"])
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
                var ActivationTextFile = @"C:\Windows\Temp\Activation-Status.txt";
                var strActivationStatus = String.Empty;
                CommandInfo.Run(@"cscript C:\Windows\System32\Slmgr.vbs /dli > C:\Windows\Temp\Activation-Status.txt", true);
                if (File.Exists(ActivationTextFile))
                {
                    using (StreamReader objReader = new StreamReader(ActivationTextFile))
                    {
                        for (Int32 i = 0; i < 8; i++)
                        {
                            objReader.ReadLine();
                        }
                        strActivationStatus = objReader.ReadLine();
                        if (strActivationStatus.Contains("License Status: "))
                        {
                            strActivationStatus = strActivationStatus.Remove(0, 16);
                        }
                    }
                }
                if (File.Exists(ActivationTextFile)) { File.Delete(ActivationTextFile); }
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
                return ((Win32MethodExists && NativeMethods.IsWow64Process(NativeMethods.GetCurrentProcess(), out flag)) && flag);
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
                return (NativeMethods.GetProcAddress(moduleHandle, "IsWow64Process") != IntPtr.Zero);
            }
        }
    }

    /// <summary>
    /// Provides objects that represent the root keys in the Windows registry.
    /// </summary>
    public static class RegistryHives
    {
        /// <summary>
        /// This field reads the Windows registry base key HKEYLOCALMACHINE 32-bit Version.
        /// </summary>
        public static RegistryKey LocalMachine => RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);

        /// <summary>
        /// This field reads the Windows registry base key HKEYLOCALMACHINE 64-bit Version.
        /// </summary>
        public static RegistryKey LocalMachine64 => RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);

        /// <summary>
        /// This field reads the Windows registry base key HKEYCURRENTUSER 32-bit Version.
        /// </summary>
        public static RegistryKey CurrentUsers => RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);

        /// <summary>
        /// This field reads the Windows registry base key HKEYCURRENTUSER 64-bit Version.
        /// </summary>
        public static RegistryKey CurrentUsers64 => RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);

        /// <summary>
        /// This field reads the Windows registry base key HKEYCURRENTCONFIG 32-bit Version.
        /// </summary>
        public static RegistryKey CurrentConfig => RegistryKey.OpenBaseKey(RegistryHive.CurrentConfig, RegistryView.Registry32);

        /// <summary>
        /// This field reads the Windows registry base key HKEYCURRENTCONFIG 64-bit Version.
        /// </summary>
        public static RegistryKey CurrentConfig64 => RegistryKey.OpenBaseKey(RegistryHive.CurrentConfig, RegistryView.Registry64);

        /// <summary>
        /// This field reads the Windows registry base key HKEYCLASSESROOT 32-bit Version.
        /// </summary>
        public static RegistryKey ClassesRoot => RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry32);

        /// <summary>
        /// This field reads the Windows registry base key HKEYCLASSESROOT 64-bit Version.
        /// </summary>
        public static RegistryKey ClassesRoot64 => RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry64);

        /// <summary>
        /// This field reads the Windows registry base key HKEYUSERS 32-bit Version.
        /// </summary>
        public static RegistryKey Users => RegistryKey.OpenBaseKey(RegistryHive.Users, RegistryView.Registry32);

        /// <summary>
        /// This field reads the Windows registry base key HKEYUSERS 64-bit Version.
        /// </summary>
        public static RegistryKey Users64 => RegistryKey.OpenBaseKey(RegistryHive.Users, RegistryView.Registry64);
    }
}
