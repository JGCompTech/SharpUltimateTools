using System;
using static JGCompTech.CSharp.Tools.RegistryInfo;

namespace JGCompTech.CSharp.Tools.HWInfo
{
    /// <summary>
    /// Returns information about the system BIOS.
    /// </summary>
    public static class BIOS
    {
        /// <summary>
        /// Returns the system BIOS release date stored in the registry.
        /// </summary>
        public static String ReleaseDate
        {
            get
            {
                String key = "HARDWARE\\DESCRIPTION\\System\\BIOS";
                String value = "BIOSReleaseDate";
                return RegistryInfo.getStringValue(HKEY.LOCAL_MACHINE, key, value);
            }
        }

        /// <summary>
        /// Returns the system BIOS version stored in the registry.
        /// </summary>
        public static String Version
        {
            get
            {
                String key = "HARDWARE\\DESCRIPTION\\System\\BIOS";
                String value = "BIOSVersion";
                return RegistryInfo.getStringValue(HKEY.LOCAL_MACHINE, key, value);
            }
        }

        /// <summary>
        /// Returns the system BIOS vendor name stored in the registry.
        /// </summary>
        public static String Vendor
        {
            get
            {
                String key = "HARDWARE\\DESCRIPTION\\System\\BIOS";
                String value = "BIOSVendor";
                return RegistryInfo.getStringValue(HKEY.LOCAL_MACHINE, key, value);
            }
        }
    }
}
