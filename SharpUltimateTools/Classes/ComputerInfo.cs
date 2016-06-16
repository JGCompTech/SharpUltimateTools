using System;
using System.IO;
using Microsoft.CSharp.Tools.Objects;
using Microsoft.CSharp.Tools.Objects.HW;
using Microsoft.CSharp.Tools.Objects.OS;

namespace Microsoft.CSharp.Tools
{
    /// <summary>
    /// Creates an object that holds info about the computer.
    /// </summary>
    public class ComputerInfo
    {
        /// <summary>
        /// Constructor initializes values();
        /// </summary>
        public ComputerInfo()
        {
            Hardware = ReinitializeHardware();
            OS = ReinitalizeOS();
        }

        /// <summary>
        /// List of posible computers
        /// </summary>
        public enum ComputerList
        {
            /// <summary>
            /// Localhost
            /// </summary>
            Localhost
        }

        /// <summary>
        /// Returns information about the Computers hardware.
        /// </summary>
        public HWObject Hardware { get; set; }
        /// <summary>
        /// Returns information about the Computers operating system.
        /// </summary>
        public OSObject OS { get; set; }

        /// <summary>
        /// Initalizes the hardware class.
        /// </summary>
        /// <returns></returns>
        public static HWObject ReinitializeHardware()
        {
            var Hardware = new HWObject
            {
                SystemOEM = HWInfo.OEM.Name,
                ProductName = HWInfo.OEM.ProductName
            };

            #region BIOS
            var BIOS = new BIOSObject
            {
                Name = HWInfo.BIOS.Vendor + " " + HWInfo.BIOS.Version,
                ReleaseDate = HWInfo.BIOS.ReleaseDate,
                Vendor = HWInfo.BIOS.Vendor,
                Version = HWInfo.BIOS.Version
            };
            Hardware.BIOS = BIOS;
            #endregion

            #region Network
            var Network = new NetworkObject
            {
                ConnectionStatus = HWInfo.Network.ConnectionStatus,
                InternalIPAddress = HWInfo.Network.InternalIPAddress
            };

            var error = String.Empty;
            var ExternalIP = HWInfo.Network.ExternalIPAddress(out error);

            if (Network.ConnectionStatus) { Network.ExternalIPAddress = ExternalIP; }
            else { Network.ExternalIPAddress = "0.0.0.0"; }


            Hardware.Network = Network;
            #endregion

            #region Processor
            var Processor = new ProcessorObject
            {
                Name = HWInfo.Processor.Name,
                Cores = HWInfo.Processor.Cores
            };
            Hardware.Processor = Processor;
            #endregion

            #region RAM
            var RAM = new RAMObject
            {
                TotalInstalled = HWInfo.RAM.GetTotalRam
            };
            Hardware.RAM = RAM;
            #endregion

            #region Storage
            var Storage = new StorageObject();

            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                var drivetype = String.Empty;
                var ActiveDrive = false;
                if (drive.IsReady)
                {
                    if (drive.DriveType == DriveType.Fixed)
                    {
                        try
                        {
                            if (drive.TotalSize != 0.0 && drive.TotalFreeSpace != 0.0)
                            {
                                ActiveDrive = true; drivetype = "Fixed";
                            }
                        }
                        catch (Exception) { throw; }
                    }
                    if (drive.DriveType == DriveType.Removable)
                    {
                        try
                        {
                            if (drive.TotalSize != 0.0 && drive.TotalFreeSpace != 0.0)
                            {
                                ActiveDrive = true; drivetype = "Removable";
                            }
                        }
                        catch (Exception) { throw; }
                    }

                    if (ActiveDrive)
                    {
                        var newdrive = new DriveObject
                        {
                            Name = drive.Name,
                            Format = drive.DriveFormat,
                            Label = drive.VolumeLabel,
                            TotalSize = Convert.ToDouble(drive.TotalSize).ConvertBytes(),
                            TotalFree = Convert.ToDouble(drive.AvailableFreeSpace).ConvertBytes(),
                            DriveType = drivetype
                        };
                        Storage.InstalledDrives.Add(newdrive);
                        if (drive.Name.Trim() == HWInfo.Storage.SystemDrivePath)
                        {
                            Storage.SystemDrive = newdrive;
                        }
                    }
                }
            }

            Hardware.Storage = Storage;
            #endregion

            return Hardware;
        }
        /// <summary>
        /// Initalizes the software class.
        /// </summary>
        /// <returns></returns>
        public static OSObject ReinitalizeOS()
        {
            var OS = new OSObject
            {
                ComputerName = OSInfo.NameStrings.ComputerNameActive,
                ComputerNamePending = OSInfo.NameStrings.ComputerNamePending,
                DomainName = OSInfo.UserInfo.CurrentDomainName,
                LoggedInUserName = OSInfo.UserInfo.LoggedInUserName,
                RegisteredOrganization = OSInfo.UserInfo.RegisteredOrganization,
                RegisteredOwner = OSInfo.UserInfo.RegisteredOwner
            };

            var InstallInfo = new InstallInfoObject
            {
                ActivationStatus = OSInfo.CheckIf.IsActivatedWMI,
                Architecture = OSInfo.Architecture.String,
                DisplayVersion = OSInfo.NameStrings.DisplayVersion,
                Name = OSInfo.NameStrings.NameString,
                ProductKey = OSInfo.ProductKey.Key,
                ServicePack = OSInfo.ServicePack.String,
                ServicePackNumber = OSInfo.ServicePack.Number
            };

            var Version = new VersionObject
            {
                Build = OSInfo.Version.Build,
                Main = OSInfo.Version.Main,
                Major = OSInfo.Version.Major,
                Minor = OSInfo.Version.Minor,
                Number = OSInfo.Version.Number,
                Revision = OSInfo.Version.Revision
            };

            OS.InstallInfo.Version = Version;
            OS.InstallInfo = InstallInfo;
            return OS;
        }
    }
}
