using System;

namespace Microsoft.CSharp.Tools.Objects.HW
{
    /// <summary>
    /// Storage Object
    /// </summary>
    public class StorageObject
    {
        /// <summary>
        /// List of installed Drives
        /// </summary>
        public System.Collections.Generic.List<DriveObject> InstalledDrives { get; internal set; } = new System.Collections.Generic.List<DriveObject>();

        /// <summary>
        /// System Boot Drive
        /// </summary>
        public DriveObject SystemDrive { get; internal set; }
    }
}