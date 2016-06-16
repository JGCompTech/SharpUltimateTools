using System;
using Microsoft.CSharp.Tools.Objects.OS;

namespace Microsoft.CSharp.Tools.Objects
{
    /// <summary>
    /// OS Object
    /// </summary>
    public class OSObject
    {
        /// <summary>
        /// Computer Name
        /// </summary>
        public String ComputerName { get; internal set; } = String.Empty;
        /// <summary>
        /// Computer Name Pending
        /// </summary>
        public String ComputerNamePending { get; internal set; } = String.Empty;
        /// <summary>
        /// Install Info Object
        /// </summary>
        public InstallInfoObject InstallInfo { get; internal set; } = new InstallInfoObject();
        /// <summary>
        /// Registered Organization Name
        /// </summary>
        public String RegisteredOrganization { get; internal set; } = String.Empty;
        /// <summary>
        /// Registered Owner Name
        /// </summary>
        public String RegisteredOwner { get; internal set; } = String.Empty;
        /// <summary>
        /// Logged In Username
        /// </summary>
        public String LoggedInUserName { get; internal set; } = String.Empty;
        /// <summary>
        /// Currently Joined Domain Name
        /// </summary>
        public String DomainName { get; internal set; } = String.Empty;
    }
}