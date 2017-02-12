using JGCompTech.CSharp.Tools.OSInfo.Enums;
using System;
using System.Runtime.InteropServices;

namespace JGCompTech.CSharp.Tools.OSInfo
{
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
                switch (Version.Major)
                {
                    case 5:
                        return GetVersion5();

                    case 6:
                    case 10:
                        return GetVersion6AndUp();
                }
                return String.Empty;
            }
        }

        /// <summary>
        /// Returns the product type from Windows 2000 to XP and Server 2000 to 2003
        /// </summary>
        /// <returns></returns>
        static String GetVersion5()
        {
            var osVersionInfo = new NativeMethods.OSVERSIONINFOEX
            {
                dwOSVersionInfoSize = Marshal.SizeOf(typeof(NativeMethods.OSVERSIONINFOEX))
            };
            if (!NativeMethods.GetVersionEx(ref osVersionInfo)) return String.Empty;

            osVersionInfo.ExceptionIfNull(nameof(osVersionInfo) + " Cannot Be Null!", nameof(osVersionInfo));
            if (NativeMethods.GetSystemMetrics((int)OtherConsts.SMMediaCenter)) return " Media Center";
            if (NativeMethods.GetSystemMetrics((int)OtherConsts.SMTabletPC)) return " Tablet PC";
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
        /// <returns></returns>
        static String GetVersion6AndUp()
        {
            switch ((ProductEdition)getProductInfo())
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
                if (!NativeMethods.GetVersionEx(ref osVersionInfo)) return (int)ProductEdition.Undefined;
                return osVersionInfo.wProductType;
            }
        }

        private static int getProductInfo()
        {
            return NativeMethods.getProductInfo(Version.Major, Version.Minor);
        }
    }
}
