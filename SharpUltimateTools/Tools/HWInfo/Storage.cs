using System;
using System.IO;

namespace JGCompTech.CSharp.Tools.HWInfo
{
    /// <summary>
    /// Storage Information
    /// </summary>
    public static class Storage
    {
        internal static String SystemDrivePath = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));
        /// <summary>
        /// Returns the drive size of the drive Windows is installed on.
        /// </summary>
        public static String GetSystemDriveSize
        {
            get
            {
                try
                {
                    foreach (DriveInfo drive in DriveInfo.GetDrives())
                    {
                        return drive.IsReady && drive.Name == SystemDrivePath ? Convert.ToDouble(drive.TotalSize).ConvertBytes() : String.Empty;
                    }
                    return String.Empty;
                }
                catch (NullReferenceException)
                {
                    return String.Empty;
                }
            }
        }

        /// <summary>
        /// Returns the free space of drive of the drive Windows is installed on.
        /// </summary>
        public static String GetSystemDriveFreeSpace
        {
            get
            {
                try
                {
                    foreach (DriveInfo drive in DriveInfo.GetDrives())
                    {
                        return drive.IsReady && drive.Name == SystemDrivePath ? Convert.ToDouble(drive.TotalFreeSpace).ConvertBytes() : String.Empty;
                    }
                    return String.Empty;
                }
                catch (NullReferenceException)
                {
                    return String.Empty;
                }
            }
        }
    }
}
