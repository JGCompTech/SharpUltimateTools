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
        /// Returns the full name of the system BIOS stored in the registry.
        /// </summary>
        public static String Name => $"{Vendor} {Version}";

        /// <summary>
        /// Returns the system BIOS release date stored in the registry.
        /// </summary>
        public static String ReleaseDate
        {
            get
            {
                const String key = @"HARDWARE\DESCRIPTION\System\BIOS";
                const String value = "BIOSReleaseDate";
                return getStringValue(HKEY.LOCAL_MACHINE, key, value);
            }
        }

        /// <summary>
        /// Returns the system BIOS version stored in the registry.
        /// </summary>
        public static String Version
        {
            get
            {
                const String key = "HARDWARE\\DESCRIPTION\\System\\BIOS";
                const String value = "BIOSVersion";
                return getStringValue(HKEY.LOCAL_MACHINE, key, value);
            }
        }

        /// <summary>
        /// Returns the system BIOS vendor name stored in the registry.
        /// </summary>
        public static String Vendor
        {
            get
            {
                const String key = "HARDWARE\\DESCRIPTION\\System\\BIOS";
                const String value = "BIOSVendor";
                return getStringValue(HKEY.LOCAL_MACHINE, key, value);
            }
        }
    }
}
