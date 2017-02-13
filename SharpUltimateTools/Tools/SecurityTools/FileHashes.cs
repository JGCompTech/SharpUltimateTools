using System;
using System.IO;
using System.Security.Cryptography;

namespace JGCompTech.CSharp.Tools.SecurityTools
{
    /// <summary>
    /// Returns hashs of the specified files.
    /// </summary>
    public static class FileHashes
    {
        /// <summary>
        /// HashType
        /// </summary>
        public enum HashType
        {
            /// <summary>
            /// MD5
            /// </summary>
            MD5,
            /// <summary>
            /// SHA1
            /// </summary>
            SHA1,
            /// <summary>
            /// SHA256
            /// </summary>
            SHA256,
            /// <summary>
            /// SHA384
            /// </summary>
            SHA384,
            /// <summary>
            /// SHA512
            /// </summary>
            SHA512
        }

        /// <summary>
        /// Read the file and calculate the checksum
        ///</summary>
        /// <param name="type">the hash type to use</param>
        /// <param name="fileName">the file to read</param>
        /// <returns>the hex representation of the hash using uppercase chars</returns>
        public static String getFileHash(HashType type, String fileName)
        {
            try
            {
                var HashValue = new byte[0];

                using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    switch (type)
                    {
                        case HashType.MD5:
                            using (var h = new MD5CryptoServiceProvider()) { HashValue = h.ComputeHash(fileStream); }
                            break;
                        case HashType.SHA1:
                            using (var h = new SHA1CryptoServiceProvider()) { HashValue = h.ComputeHash(fileStream); }
                            break;
                        case HashType.SHA256:
                            using (var h = new SHA256CryptoServiceProvider()) { HashValue = h.ComputeHash(fileStream); }
                            break;
                        case HashType.SHA384:
                            using (var h = new SHA384CryptoServiceProvider()) { HashValue = h.ComputeHash(fileStream); }
                            break;
                        case HashType.SHA512:
                            using (var h = new SHA512CryptoServiceProvider()) { HashValue = h.ComputeHash(fileStream); }
                            break;
                    }
                }

                return BitConverter.ToString(HashValue).Replace("-", String.Empty);
            }
            catch (Exception ex) { return ex.Message; }
        }
    }
}
