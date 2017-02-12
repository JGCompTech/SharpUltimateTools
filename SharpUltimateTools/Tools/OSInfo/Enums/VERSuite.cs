using System;

namespace JGCompTech.CSharp.Tools.OSInfo.Enums
{
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
}
