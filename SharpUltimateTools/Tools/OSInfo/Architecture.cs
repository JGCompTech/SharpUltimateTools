using System;

namespace JGCompTech.CSharp.Tools.OSInfo
{
    /// <summary>
    /// Determines if the current application is 32 or 64-bit.
    /// </summary>
    public static class Architecture
    {
        /// <summary>
        /// Determines if the current application is 32 or 64-bit.
        /// </summary>
        public static String String => CheckIf.Is64BitOS ? "64 bit" : "32 bit";

        /// <summary>
        /// Determines if the current application is 32 or 64-bit.
        /// </summary>
        public static int Number => CheckIf.Is64BitOS ? 64 : 32;
    }
}
