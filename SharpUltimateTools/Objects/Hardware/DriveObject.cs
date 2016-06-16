using System;

namespace Microsoft.CSharp.Tools.Objects.HW
{
    /// <summary>
    /// Drive Object
    /// </summary>
    public class DriveObject
    {
        /// <summary>
        /// Drive Name
        /// </summary>
        public String Name { get; internal set; } = String.Empty;
        /// <summary>
        /// Drive Format
        /// </summary>
        public String Format { get; internal set; } = String.Empty;
        /// <summary>
        /// Drive Label
        /// </summary>
        public String Label { get; internal set; } = String.Empty;
        /// <summary>
        /// Drive Type
        /// </summary>
        public String DriveType { get; internal set; } = String.Empty;
        /// <summary>
        /// Drive Total Size
        /// </summary>
        public String TotalSize { get; internal set; } = String.Empty;
        /// <summary>
        /// Drive Total Free Space
        /// </summary>
        public String TotalFree { get; internal set; } = String.Empty;
    }
}