using System;

namespace JGCompTech.CSharp.Tools.OSInfo.Enums
{
    /// <summary>
    /// A list of Product Types according to ( http://msdn.microsoft.com/en-us/library/ms724833(VS.85).aspx )
    /// </summary>
    [Flags]
    public enum ProductType
    {
        /// <summary>
        /// Workstation
        /// </summary>
        NTWorkstation = 1,
        /// <summary>
        /// Domain Controller
        /// </summary>
        NTDomainController = 2,
        /// <summary>
        /// Server
        /// </summary>
        NTServer = 3
    }
}
