using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;

// Returns information about the computer's hardware
namespace Microsoft.CSharp.Tools.HWInfo
{
    /// <summary>
    /// Returns information about the system BIOS.
    /// </summary>
    public static class BIOS
    {
        /// <summary>
        /// Returns the system BIOS release date stored in the registry.
        /// </summary>
        public static String ReleaseDate
        {
            get
            {
                try
                {
                    var key = OSInfo.RegistryHives.LocalMachine.OpenSubKey("HARDWARE\\DESCRIPTION\\System\\BIOS");
                    return key.GetValue("BIOSReleaseDate").ToString();
                }
                catch (NullReferenceException)
                {
                    return String.Empty;
                }
            }
        }

        /// <summary>
        /// Returns the system BIOS version stored in the registry.
        /// </summary>
        public static String Version
        {
            get
            {
                try
                {
                    var key = OSInfo.RegistryHives.LocalMachine.OpenSubKey("HARDWARE\\DESCRIPTION\\System\\BIOS");
                    return key.GetValue("BIOSVersion").ToString();
                }
                catch (NullReferenceException)
                {
                    return String.Empty;
                }
            }
        }

        /// <summary>
        /// Returns the system BIOS vendor name stored in the registry.
        /// </summary>
        public static String Vendor
        {
            get
            {
                try
                {
                    var key = OSInfo.RegistryHives.LocalMachine.OpenSubKey("HARDWARE\\DESCRIPTION\\System\\BIOS");
                    return key.GetValue("BIOSVendor").ToString();
                }
                catch (NullReferenceException)
                {
                    return String.Empty;
                }
            }
        }
    }
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
    /// <summary>
    /// Network Information
    /// </summary>
    public static class Network
    {
        /// <summary>
        /// Returns the Internal IP Address.
        /// </summary>
        public static String InternalIPAddress
        {
            get
            {
                try
                {
                    var IP = String.Empty;
                    foreach (System.Net.IPAddress ip in System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList)
                    {
                        if (ip.AddressFamily.ToString() == "InterNetwork") IP = ip.ToString();
                    }
                    return IP;
                }
                catch (NullReferenceException)
                {
                    return String.Empty;
                }
            }
        }

        /// <summary>
        /// Returns the External IP Address by connecting to "http://api.ipify.org".
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public static String ExternalIPAddress(out String error)
        {
            var IP = String.Empty;
            error = String.Empty;
            try
            {
                using (System.Net.WebClient ipclient = new System.Net.WebClient())
                {
                    IP = ipclient.DownloadString("http://api.ipify.org");
                    return IP;
                }
            }
            catch (System.Net.WebException ex)
            {
                if (ex.Message == "The remote name could not be resolved: 'http://api.ipify.org'") { return IP; }
            }
            catch (Exception ex) { error = ex.Message; return IP; }
            return IP;
        }

        /// <summary>
        /// Returns status of internet connection
        /// </summary>
        public static Boolean ConnectionStatus
        {
            get
            {
                try
                {
                    using (var client = new System.Net.WebClient())
                    using (var stream = client.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
                catch (Exception) { return false; }
            }
        }
    }
    /// <summary>
    /// Manufaturer Information
    /// </summary>
    public static class OEM
    {
        /// <summary>
        /// Returns the system manufacturer name that is stored in the registry.
        /// </summary>
        public static String Name
        {
            get
            {
                try
                {
                    String value;
                    RegistryKey key;
                    key = OSInfo.RegistryHives.LocalMachine.OpenSubKey("HARDWARE\\DESCRIPTION\\System\\BIOS");
                    value = key.GetValue("SystemManufacturer").ToString();
                    if (value.IsNullOrEmpty())
                    {
                        key = OSInfo.RegistryHives.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\OEMInFormation");
                        value = key.GetValue("Manufacturer").ToString();
                    }
                    return value;
                }
                catch (NullReferenceException)
                {
                    return String.Empty;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return String.Empty;
                }
            }
        }

        /// <summary>
        /// Returns the system product name that is stored in the registry.
        /// </summary>
        public static String ProductName
        {
            get
            {
                try
                {
                    String value;
                    RegistryKey key;
                    key = OSInfo.RegistryHives.LocalMachine.OpenSubKey("HARDWARE\\DESCRIPTION\\System\\BIOS");
                    value = key.GetValue("SystemProductName").ToString();
                    if (value.IsNullOrEmpty())
                    {
                        key = OSInfo.RegistryHives.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\OEMInFormation");
                        value = key.GetValue("Model").ToString();
                    }
                    return value;
                }
                catch (NullReferenceException)
                {
                    return String.Empty;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return String.Empty;
                }
            }
        }
    }
    /// <summary>
    /// Processor Information
    /// </summary>
    public static class Processor
    {
        /// <summary>
        /// Returns the system processor name that is stored in the registry.
        /// </summary>
        public static String Name
        {
            get
            {
                try
                {
                    var key = OSInfo.RegistryHives.LocalMachine.OpenSubKey("HARDWARE\\DESCRIPTION\\System\\CentralProcessor\\0");
                    return key.GetValue("ProcessorNameString").ToString();
                }
                catch (NullReferenceException)
                {
                    return String.Empty;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return String.Empty;
                }
            }
        }

        /// <summary>
        /// Returns the number of cores available on the system processor.
        /// </summary>
        public static Int32 Cores
        {
            get
            {
                return Environment.ProcessorCount;
            }
        }
    }
    /// <summary>
    /// RAM Information
    /// </summary>
    public static class RAM
    {
        /// <summary>
        /// Returns the total ram installed on the Computer.
        /// </summary>
        public static String GetTotalRam
        {
            get
            {
                try
                {
                    long installedMemory = 0;
                    //NativeMethods.MEMORYSTATUSEX memStatus = new NativeMethods.MEMORYSTATUSEX();
                    NativeMethods.GetPhysicallyInstalledSystemMemory(out installedMemory);
                    return installedMemory.ConvertKilobytes();
                }
                catch (NullReferenceException)
                {
                    return String.Empty;
                }
            }
        }
    }
}
