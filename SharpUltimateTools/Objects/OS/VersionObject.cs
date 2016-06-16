using System;

namespace Microsoft.CSharp.Tools.Objects.OS
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
        public Int32 Major { get; internal set; }
        /// <summary>
        /// Version Minor
        /// </summary>
        public Int32 Minor { get; internal set; }
        /// <summary>
        /// Version Build
        /// </summary>
        public Int32 Build { get; internal set; }
        /// <summary>
        /// Version Revision
        /// </summary>
        public Int32 Revision { get; internal set; }
        /// <summary>
        /// Version Number
        /// </summary>
        public Int32 Number { get; internal set; }
    }
}