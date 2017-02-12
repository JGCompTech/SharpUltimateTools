using System;

namespace JGCompTech.CSharp.Tools.OSInfo.Objects
{
    /// <summary>
    /// Install Info Object
    /// </summary>
    public class InstallInfoObject
    {
        /// <summary>
        /// Activation Status
        /// </summary>
        public String ActivationStatus { get; internal set; } = String.Empty;
        /// <summary>
        /// Architecture
        /// </summary>
        public String Architecture { get; internal set; } = String.Empty;
        /// <summary>
        /// Display Version
        /// </summary>
        public String NameExpanded { get; internal set; } = String.Empty;
        /// <summary>
        /// Name
        /// </summary>
        public String Name { get; internal set; } = String.Empty;
        /// <summary>
        /// Product Key
        /// </summary>
        public String ProductKey { get; internal set; } = String.Empty;
        /// <summary>
        /// Service Pack
        /// </summary>
        public String ServicePack { get; internal set; } = String.Empty;
        /// <summary>
        /// Service Pack Number
        /// </summary>
        public int ServicePackNumber { get; internal set; } = 0;
        /// <summary>
        /// Version Object
        /// </summary>
        public VersionObject Version { get; internal set; } = new VersionObject();
    }
}