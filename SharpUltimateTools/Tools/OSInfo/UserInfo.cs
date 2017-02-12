using System;
using static JGCompTech.CSharp.Tools.RegistryInfo;

namespace JGCompTech.CSharp.Tools.OSInfo
{
    /// <summary>
    /// Gets info about the currently logged in user account.
    /// </summary>
    public static class UserInfo
    {
        /// <summary>
        /// Gets the current Registered Organization.
        /// </summary>
        public static String RegisteredOrganization
        {
            get
            {
                String key = "Software\\Microsoft\\Windows NT\\CurrentVersion";
                String value = "RegisteredOrganization";
                return RegistryInfo.getStringValue(HKEY.LOCAL_MACHINE, key, value);
            }
        }

        /// <summary>
        /// Gets the current Registered Owner.
        /// </summary>
        public static String RegisteredOwner
        {
            get
            {
                String key = "Software\\Microsoft\\Windows NT\\CurrentVersion";
                String value = "RegisteredOwner";
                return RegistryInfo.getStringValue(HKEY.LOCAL_MACHINE, key, value);
            }
        }

        /// <summary>
        /// Gets the user name of the person who is currently logged on to the Windows operating system.
        /// </summary>
        public static String LoggedInUserName => Environment.UserName;

        /// <summary>
        /// Gets the network domain name associated with the current user.
        /// </summary>
        /// <exception cref="PlatformNotSupportedException">The operating system does not support retrieving the network domain name.</exception>
        /// <exception cref="InvalidOperationException">The network domain name cannot be retrieved.</exception>
        public static String CurrentDomainName => Environment.UserDomainName;
    }
}
