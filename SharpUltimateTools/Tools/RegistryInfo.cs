using JGCompTech.CSharp.Tools.OSInfo;
using Microsoft.Win32;
using System;

namespace JGCompTech.CSharp.Tools
{
    /// <summary>
    /// Returns data from registry keys and values.
    /// </summary>
    public static class RegistryInfo
    {
        /// <summary>
        /// HKEY
        /// </summary>
        public enum HKEY
        {
            /// <summary>
            /// CLASSES_ROOT
            /// </summary>
            CLASSES_ROOT,
            /// <summary>
            /// CLASSES_USER
            /// </summary>
            CURRENT_USER,
            /// <summary>
            /// LOCAL_MACHINE
            /// </summary>
            LOCAL_MACHINE,
            /// <summary>
            /// USERS
            /// </summary>
            USERS,
            /// <summary>
            /// PERFORMANCE_DATA
            /// </summary>
            PERFORMANCE_DATA,
            /// <summary>
            /// CURRENT_CONFIG
            /// </summary>
            CURRENT_CONFIG
        }

        /// <summary>
        /// Gets string value of a value in the registry.
        /// </summary>
        /// <param name="hkey"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String getStringValue(HKEY hkey, String key, String value)
        {
            String text;
            if (CheckIf.Is64BitOS)
            {
                text = getKeyValue(getBaseKey(hkey, true), key, value).ToString();
                if (text.IsNullOrEmpty())
                {
                    text = getKeyValue(getBaseKey(hkey, false), key, value).ToString();
                }
            }
            else
            {
                text = getKeyValue(getBaseKey(hkey, false), key, value).ToString();
            }
            return text;
        }

        /// <summary>
        /// Gets byte value of a value in the registry.
        /// </summary>
        /// <param name="hkey"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] getByteValue(HKEY hkey, String key, String value)
        {
            var byteobj = getKeyValue(getBaseKey(hkey, false), key, value) as byte[];
            if (byteobj.IsNullOrEmpty())
            {
                byteobj = getKeyValue(getBaseKey(hkey, true), key, value) as byte[];
            }
            return byteobj;
        }

        static object getKeyValue(RegistryKey hkey, String key, String value)
        {
            if (key.IsNull()) return null;
            return hkey.OpenSubKey(key).GetValue(value);
        }

        static RegistryKey getBaseKey(HKEY hkey, Boolean get64)
        {
            switch (hkey)
            {
                case HKEY.CLASSES_ROOT:
                    if (get64) return getBaseKey64(RegistryHive.ClassesRoot);
                    return getBaseKey32(RegistryHive.ClassesRoot);
                case HKEY.CURRENT_USER:
                    if (get64) return getBaseKey64(RegistryHive.CurrentUser);
                    return getBaseKey32(RegistryHive.CurrentUser);
                case HKEY.LOCAL_MACHINE:
                    if (get64) return getBaseKey64(RegistryHive.LocalMachine);
                    return getBaseKey32(RegistryHive.LocalMachine);
                case HKEY.USERS:
                    if (get64) return getBaseKey64(RegistryHive.Users);
                    return getBaseKey32(RegistryHive.Users);
                case HKEY.PERFORMANCE_DATA:
                    if (get64) return getBaseKey64(RegistryHive.PerformanceData);
                    return getBaseKey32(RegistryHive.PerformanceData);
                case HKEY.CURRENT_CONFIG:
                    if (get64) return getBaseKey64(RegistryHive.CurrentConfig);
                    return getBaseKey32(RegistryHive.CurrentConfig);
            }

            return null;
        }

        static RegistryKey getBaseKey32(RegistryHive hive) => RegistryKey.OpenBaseKey(hive, RegistryView.Registry32);


        static RegistryKey getBaseKey64(RegistryHive hive) => RegistryKey.OpenBaseKey(hive, RegistryView.Registry64);
    }
}
