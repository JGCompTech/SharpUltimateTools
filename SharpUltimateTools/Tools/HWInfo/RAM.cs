using System;

namespace JGCompTech.CSharp.Tools.HWInfo
{
    /// <summary>
    /// RAM Information
    /// </summary>
    public static class RAM
    {
        /// <summary>
        /// Returns the total ram installed on the Computer.
        /// </summary>
        public static String GetTotalRam
        {
            get
            {
                try
                {
                    long installedMemory = 0;
                    //NativeMethods.MEMORYSTATUSEX memStatus = new NativeMethods.MEMORYSTATUSEX();
                    NativeMethods.GetPhysicallyInstalledSystemMemory(out installedMemory);
                    return ((double)installedMemory).ConvertKilobytes();
                }
                catch (NullReferenceException)
                {
                    return String.Empty;
                }
            }
        }
    }
}
