using System;
using Microsoft.CSharp.Tools.Objects.HW;

namespace Microsoft.CSharp.Tools.Objects
{
    /// <summary>
    /// Hardware Data Objects
    /// </summary>
    public class HWObject
    {
        /// <summary>
        /// System OEM
        /// </summary>
        public String SystemOEM { get; internal set; } = String.Empty;
        /// <summary>
        /// Product Name
        /// </summary>
        public String ProductName { get; internal set; } = String.Empty;
        /// <summary>
        /// BIOS Object
        /// </summary>
        public BIOSObject BIOS { get; internal set; } = new BIOSObject();
        /// <summary>
        /// Network Object
        /// </summary>
        public NetworkObject Network { get; internal set; } = new NetworkObject();
        /// <summary>
        /// Processor Object
        /// </summary>
        public ProcessorObject Processor { get; internal set; } = new ProcessorObject();
        /// <summary>
        /// RAM Object
        /// </summary>
        public RAMObject RAM { get; internal set; } = new RAMObject();
        /// <summary>
        /// Storage Object
        /// </summary>
        public StorageObject Storage { get; internal set; } = new StorageObject();
    }
}
