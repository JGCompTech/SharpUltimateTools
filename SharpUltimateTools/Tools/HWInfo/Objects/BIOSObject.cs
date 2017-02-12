using System;

namespace JGCompTech.CSharp.Tools.HWInfo.Objects
{
    /// <summary>
    /// BIOS Object
    /// </summary>
    public class BIOSObject
    {
        /// <summary>
        /// BIOS Name
        /// </summary>
        public String Name { get; internal set; } = String.Empty;
        /// <summary>
        /// BIOS Release Date
        /// </summary>
        public String ReleaseDate { get; internal set; } = String.Empty;
        /// <summary>
        /// BIOS Vendor
        /// </summary>
        public String Vendor { get; internal set; } = String.Empty;
        /// <summary>
        /// BIOS Version
        /// </summary>
        public String Version { get; internal set; } = String.Empty;
    }

}