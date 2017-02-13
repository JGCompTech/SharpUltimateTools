using JGCompTech.CSharp.Tools.OSInfo.Enums;
using System;
using System.Runtime.InteropServices;
using static JGCompTech.CSharp.Tools.NativeMethods;
using static JGCompTech.CSharp.Tools.OSInfo.Enums.ProductEdition;

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
            
            var osVersionInfo = new OSVERSIONINFOEX
            {
                dwOSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX))
            };
            if (!GetVersionEx(ref osVersionInfo)) return String.Empty;

            osVersionInfo.ExceptionIfNull(nameof(osVersionInfo) + " Cannot Be Null!", nameof(osVersionInfo));

            var Mask = (VERSuite)osVersionInfo.wSuiteMask;

            if (GetSystemMetrics((int)OtherConsts.SMMediaCenter)) return " Media Center";
            if (GetSystemMetrics((int)OtherConsts.SMTabletPC)) return " Tablet PC";
            if (CheckIf.IsServer)
            {
                if (Version.Minor == 0)
                {
                    if ((Mask & VERSuite.Datacenter) == VERSuite.Datacenter)
                    {
                        // Windows 2000 Datacenter Server
                        return " Datacenter Server";
                    }
                    if ((Mask & VERSuite.Enterprise) == VERSuite.Enterprise)
                    {
                        // Windows 2000 Advanced Server
                        return " Advanced Server";
                    }
                    // Windows 2000 Server
                    return " Server";
                }
                if (Version.Minor == 2)
                {
                    if ((Mask & VERSuite.Datacenter) == VERSuite.Datacenter)
                    {
                        // Windows Server 2003 Datacenter Edition
                        return " Datacenter Edition";
                    }
                    if ((Mask & VERSuite.Enterprise) == VERSuite.Enterprise)
                    {
                        // Windows Server 2003 Enterprise Edition
                        return " Enterprise Edition";
                    }
                    if ((Mask & VERSuite.StorageServer) == VERSuite.StorageServer)
                    {
                        // Windows Server 2003 Storage Edition
                        return " Storage Edition";
                    }
                    if ((Mask & VERSuite.ComputeServer) == VERSuite.ComputeServer)
                    {
                        // Windows Server 2003 Compute Cluster Edition
                        return " Compute Cluster Edition";
                    }
                    if ((Mask & VERSuite.Blade) == VERSuite.Blade)
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
                if ((Mask & VERSuite.EmbeddedNT) == VERSuite.EmbeddedNT)
                {
                    //Windows XP Embedded
                    return " Embedded";
                }
                // Windows XP / Windows 2000 Professional
                return (Mask & VERSuite.Personal) == VERSuite.Personal ? " Home" : " Professional";
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
                case Ultimate:
                case UltimateE:
                case UltimateN:
                    return nameof(Ultimate);

                case Professional:
                case ProfessionalE:
                case ProfessionalN:
                    return nameof(Professional);

                case HomePremium:
                case HomePremiumE:
                case HomePremiumN:
                    return "Home Premium";

                case HomeBasic:
                case HomeBasicE:
                case HomeBasicN:
                    return "Home Basic";

                case Enterprise:
                case EnterpriseE:
                case EnterpriseN:
                case EnterpriseServerV:
                    return nameof(Enterprise);

                case Business:
                case BusinessN:
                    return nameof(Business);

                case Starter:
                case StarterE:
                case StarterN:
                    return nameof(Starter);

                case ClusterServer:
                    return "Cluster Server";

                case DatacenterServer:
                case DatacenterServerV:
                    return "Datacenter";

                case DatacenterServerCore:
                case DatacenterServerCoreV:
                    return "Datacenter (Core installation)";

                case EnterpriseServer:
                    return nameof(Enterprise);

                case EnterpriseServerCore:
                case EnterpriseServerCoreV:
                    return "Enterprise (Core installation)";

                case EnterpriseServerIA64:
                    return "Enterprise For Itanium-based Systems";

                case SmallBusinessServer:
                    return "Small Business Server";

                //case SmallBusinessServerPremium:
                //  return "Small Business Server Premium Edition";

                case ServerForSmallBusiness:
                case ServerForSmallBusinessV:
                    return "Windows Essential Server Solutions";

                case StandardServer:
                case StandardServerV:
                    return "Standard";

                case StandardServerCore:
                case StandardServerCoreV:
                    return "Standard (Core installation)";

                case WebServer:
                case WebServerCore:
                    return "Web Server";

                case MediumBusinessServerManagement:
                case MediumBusinessServerMessaging:
                case MediumBusinessServerSecurity:
                    return "Windows Essential Business Server";

                case StorageEnterpriseServer:
                case StorageExpressServer:
                case StorageStandardServer:
                case StorageWorkgroupServer:
                    return "Storage Server";
            }
            return String.Empty;
        }

        /// <summary>
        /// Gets the product type of the operating system running on this Computer.
        /// </summary>
        public static byte ProductType
        {
            get
            {
                var osVersionInfo = new OSVERSIONINFOEX
                {
                    dwOSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX))
                };
                if (!GetVersionEx(ref osVersionInfo)) return (int)Undefined;
                return osVersionInfo.wProductType;
            }
        }

        static int getProductInfo() => NativeMethods.getProductInfo(Version.Major, Version.Minor);
    }
}
