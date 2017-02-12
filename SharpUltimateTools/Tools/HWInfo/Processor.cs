using System;
using static JGCompTech.CSharp.Tools.RegistryInfo;

namespace JGCompTech.CSharp.Tools.HWInfo
{
    /// <summary>
    /// Processor Information
    /// </summary>
    public static class Processor
    {
        /// <summary>
        /// Returns the system processor name that is stored in the registry.
        /// </summary>
        public static String Name
        {
            get
            {
                String key = "HARDWARE\\DESCRIPTION\\System\\CentralProcessor\\0";
                String value = "ProcessorNameString";
                return RegistryInfo.getStringValue(HKEY.LOCAL_MACHINE, key, value);
            }
        }

        /// <summary>
        /// Returns the number of cores available on the system processor.
        /// </summary>
        public static int Cores
        {
            get
            {
                return Environment.ProcessorCount;
            }
        }
    }
}
