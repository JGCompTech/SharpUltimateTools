using System;

namespace JGCompTech.CSharp.Tools.HWInfo.Objects
{
    /// <summary>
    /// Network Object
    /// </summary>
    public class NetworkObject
    {
        /// <summary>
        /// Internal IP Address
        /// </summary>
        public String InternalIPAddress { get; internal set; } = String.Empty;
        /// <summary>
        /// External IP Address
        /// </summary>
        public String ExternalIPAddress { get; internal set; } = String.Empty;
        /// <summary>
        /// Internet Connection Status
        /// </summary>
        public Boolean ConnectionStatus { get; internal set; } = false;
    }
}