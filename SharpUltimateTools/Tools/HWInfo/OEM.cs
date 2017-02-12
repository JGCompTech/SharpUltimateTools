using System;
using static JGCompTech.CSharp.Tools.RegistryInfo;

namespace JGCompTech.CSharp.Tools.HWInfo
{
    /// <summary>
    /// Manufaturer Information
    /// </summary>
    public static class OEM
    {
        /// <summary>
        /// Returns the system manufacturer name that is stored in the registry.
        /// </summary>
        public static String Name
        {
            get
            {
                String key = "HARDWARE\\DESCRIPTION\\System\\BIOS";
                String value = "SystemManufacturer";
                String text = RegistryInfo.getStringValue(HKEY.LOCAL_MACHINE, key, value);
                if (text.IsNullOrEmpty())
                {
                    key = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\OEMInFormation";
                    value = "Manufacturer";
                    return RegistryInfo.getStringValue(HKEY.LOCAL_MACHINE, key, value);
                }
                return text;
            }
        }

        /// <summary>
        /// Returns the system product name that is stored in the registry.
        /// </summary>
        public static String ProductName
        {
            get
            {
                String key = "HARDWARE\\DESCRIPTION\\System\\BIOS";
                String value = "SystemProductName";
                String text = RegistryInfo.getStringValue(HKEY.LOCAL_MACHINE, key, value);
                if (text.IsNullOrEmpty())
                {
                    key = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\OEMInFormation";
                    value = "Model";
                    return RegistryInfo.getStringValue(HKEY.LOCAL_MACHINE, key, value);
                }
                return text;
            }
        }
    }
}
