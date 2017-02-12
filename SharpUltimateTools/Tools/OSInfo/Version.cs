using System;
using System.Globalization;
using System.Windows.Forms;

namespace JGCompTech.CSharp.Tools.OSInfo
{
    /// <summary>
    /// Gets the full version of the operating system running on this Computer.
    /// </summary>
    ///
    public static class Version
    {
        /// <summary>
        /// Gets the full version of the operating system running on this Computer. Uses the deprecated OSVersion.
        /// </summary>
        ///
        [Obsolete("MainOSV is deprecated, please use Main instead.")]
        public static String MainOSV => Environment.OSVersion.Version.ToString();

        /// <summary>
        /// Gets the full version of the operating system running on this Computer. Uses the newer WMI.
        /// </summary>
        ///
        public static String Main => GetVersionInfo(VersionType.Main);

        /// <summary>
        /// Gets the major version of the operating system running on this Computer. Uses the deprecated OSVersion.
        /// </summary>
        [Obsolete("MajorOSV is deprecated, please use Major instead.")]
        public static int MajorOSV => Environment.OSVersion.Version.Major;

        /// <summary>
        /// Gets the major version of the operating system running on this Computer. Uses the newer WMI.
        /// </summary>
        public static int Major => Convert.ToInt32(GetVersionInfo(VersionType.Major), CultureInfo.CurrentCulture);

        /// <summary>
        /// Gets the minor version of the operating system running on this Computer. Uses the deprecated OSVersion.
        /// </summary>
        [Obsolete("MinorOSV is deprecated, please use Minor instead.")]
        public static int MinorOSV => Environment.OSVersion.Version.Minor;

        /// <summary>
        /// Gets the minor version of the operating system running on this Computer. Uses the newer WMI.
        /// </summary>
        public static int Minor => Convert.ToInt32(GetVersionInfo(VersionType.Minor), CultureInfo.CurrentCulture);

        /// <summary>
        /// Gets the build version of the operating system running on this Computer. Uses the deprecated OSVersion.
        /// </summary>
        [Obsolete("BuildOSV is deprecated, please use Build instead.")]
        public static int BuildOSV => Environment.OSVersion.Version.Build;

        /// <summary>
        /// Gets the build version of the operating system running on this Computer. Uses the newer WMI.
        /// </summary>
        public static int Build => Convert.ToInt32(GetVersionInfo(VersionType.Build), CultureInfo.CurrentCulture);

        /// <summary>
        /// Gets the revision version of the operating system running on this Computer. Uses the deprecated OSVersion.
        /// </summary>
        [Obsolete("BuildOSV is deprecated, please use Build instead.")]
        public static int RevisionOSV => Environment.OSVersion.Version.Revision;

        /// <summary>
        /// Gets the revision version of the operating system running on this Computer. Uses the newer WMI.
        /// </summary>
        public static int Revision => Convert.ToInt32(GetVersionInfo(VersionType.Revision), CultureInfo.CurrentCulture);

        /// <summary>
        /// Return a numeric value representing OS version. Uses the deprecated OSVersion.
        /// </summary>
        /// <returns>(OSMajorVersion * 10 + OSMinorVersion)</returns>
        [Obsolete("IntNumOSV is deprecated, please use IntNum instead.")]
        public static int NumberOSV => (MajorOSV * 10 + MinorOSV);

        /// <summary>
        /// Return a numeric value representing OS version. Uses the newer WMI.
        /// </summary>
        /// <returns>(OSMajorVersion * 10 + OSMinorVersion)</returns>
        public static int Number => (Major * 10 + Minor);

        internal static String GetVersionInfo(VersionType type)
        {
            try
            {
                var VersionString = String.Empty;
                using (System.Management.ManagementObjectSearcher objMOS = new System.Management.ManagementObjectSearcher("SELECT * FROM  Win32_OperatingSystem"))
                {
                    foreach (System.Management.ManagementObject o in objMOS.Get()) { VersionString = o[nameof(Version)].ToString(); }
                }

                var Temp = String.Empty;
                var Major = VersionString.Substring(0, VersionString.IndexOf(".", StringComparison.CurrentCulture));
                Temp = VersionString.Substring(Major.Length + 1);
                var Minor = Temp.Substring(0, VersionString.IndexOf(".", StringComparison.CurrentCulture) - 1);
                Temp = VersionString.Substring(Major.Length + 1 + Minor.Length + 1);
                String Build;
                if (Temp.Contains("."))
                {
                    Build = Temp.Substring(0, VersionString.IndexOf(".", StringComparison.CurrentCulture) - 1);
                    Temp = VersionString.Substring(Major.Length + 1 + Minor.Length + 1 + Build.Length + 1);
                }
                else
                {
                    Build = Temp;
                    Temp = "0";
                }
                var Revision = Temp;

                var ReturnString = "0";
                switch (type)
                {
                    case VersionType.Main:
                        ReturnString = VersionString;
                        break;

                    case VersionType.Major:
                        ReturnString = Major;
                        break;

                    case VersionType.Minor:
                        ReturnString = Minor;
                        break;

                    case VersionType.Build:
                        ReturnString = Build;
                        break;

                    case VersionType.Revision:
                        ReturnString = Revision;
                        break;
                }

                if (ReturnString.IsNullOrEmpty()) return "0";
                return ReturnString;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "0";
            }
        }

        internal enum VersionType
        {
            Main,
            Major,
            Minor,
            Build,
            Revision
        }
    }
}
