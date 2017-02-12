using System;

namespace JGCompTech.CSharp.Tools.OSInfo.Objects
{
    /// <summary>
    /// Version Object
    /// </summary>
    public class VersionObject
    {
        /// <summary>
        /// Version Main String
        /// </summary>
        public String Main { get; internal set; }
        /// <summary>
        /// Version Major
        /// </summary>
        public int Major { get; internal set; }
        /// <summary>
        /// Version Minor
        /// </summary>
        public int Minor { get; internal set; }
        /// <summary>
        /// Version Build
        /// </summary>
        public int Build { get; internal set; }
        /// <summary>
        /// Version Revision
        /// </summary>
        public int Revision { get; internal set; }
        /// <summary>
        /// Version Number
        /// </summary>
        public int Number { get; internal set; }
    }
}