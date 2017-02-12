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
        /// Returns a SHA1 hash of the specified file.
        /// </summary>
        /// <param name="fileName"></param>
        public static String GetSHA1Hash(String fileName)
        {
            var strResult = String.Empty;
            var strHashData = String.Empty;
            byte[] arrbytHashValue;
            FileStream oFileStream = null;
            using (SHA1CryptoServiceProvider oSHAHasher = new SHA1CryptoServiceProvider())
            {
                try
                {
                    oFileStream = GetFileStream(fileName);
                    arrbytHashValue = oSHAHasher.ComputeHash(oFileStream);
                    oFileStream.Close();

                    strHashData = BitConverter.ToString(arrbytHashValue);
                    strHashData = strHashData.Replace("-", String.Empty);
                    strResult = strHashData;
                }
                catch (Exception ex) { return ex.Message; }
            }
            return (strResult);
        }

        /// <summary>
        /// Returns a SHA256 hash of the specified file.
        /// </summary>
        /// <param name="fileName"></param>
        public static String GetSHA256Hash(String fileName)
        {
            var strResult = String.Empty;
            var strHashData = String.Empty;

            byte[] arrbytHashValue;
            FileStream oFileStream = null;

            using (SHA256CryptoServiceProvider oSHAHasher = new SHA256CryptoServiceProvider())
            {
                try
                {
                    oFileStream = GetFileStream(fileName);
                    arrbytHashValue = oSHAHasher.ComputeHash(oFileStream);
                    oFileStream.Close();

                    strHashData = BitConverter.ToString(arrbytHashValue);
                    strHashData = strHashData.Replace("-", String.Empty);
                    strResult = strHashData;
                }
                catch (Exception ex) { return ex.Message; }
            }
            return (strResult);
        }

        /// <summary>
        /// Returns a SHA384 hash of the specified file.
        /// </summary>
        /// <param name="fileName"></param>
        public static String GetSHA384Hash(String fileName)
        {
            var strResult = String.Empty;
            var strHashData = String.Empty;

            byte[] arrbytHashValue;
            FileStream oFileStream = null;

            using (SHA384CryptoServiceProvider oSHAHasher = new SHA384CryptoServiceProvider())
            {
                try
                {
                    oFileStream = GetFileStream(fileName);
                    arrbytHashValue = oSHAHasher.ComputeHash(oFileStream);
                    oFileStream.Close();

                    strHashData = BitConverter.ToString(arrbytHashValue);
                    strHashData = strHashData.Replace("-", String.Empty);
                    strResult = strHashData;
                }
                catch (Exception ex) { return ex.Message; }
            }
            return (strResult);
        }

        /// <summary>
        /// Returns a SHA512 hash of the specified file.
        /// </summary>
        /// <param name="fileName"></param>
        public static String GetSHA512Hash(String fileName)
        {
            var strResult = String.Empty;
            var strHashData = String.Empty;

            byte[] arrbytHashValue;
            FileStream oFileStream = null;

            using (SHA512CryptoServiceProvider oSHAHasher = new SHA512CryptoServiceProvider())
            {
                try
                {
                    oFileStream = GetFileStream(fileName);
                    arrbytHashValue = oSHAHasher.ComputeHash(oFileStream);
                    oFileStream.Close();

                    strHashData = BitConverter.ToString(arrbytHashValue);
                    strHashData = strHashData.Replace("-", String.Empty);
                    strResult = strHashData;
                }
                catch (Exception ex) { return ex.Message; }
            }
            return (strResult);
        }

        /// <summary>
        /// Returns a MD5 hash of the specified file.
        /// </summary>
        /// <param name="fileName"></param>
        public static String GetMD5Hash(String fileName)
        {
            var strResult = String.Empty;
            var strHashData = String.Empty;

            byte[] arrbytHashValue;
            FileStream oFileStream = null;

            using (MD5CryptoServiceProvider oMD5Hasher = new MD5CryptoServiceProvider())
            {
                try
                {
                    oFileStream = GetFileStream(fileName);
                    arrbytHashValue = oMD5Hasher.ComputeHash(oFileStream);
                    oFileStream.Close();

                    strHashData = BitConverter.ToString(arrbytHashValue);
                    strHashData = strHashData.Replace("-", String.Empty);
                    strResult = strHashData;
                }
                catch (Exception ex) { return ex.Message; }
            }
            return (strResult);
        }

        internal static FileStream GetFileStream(String pathName)
        {
            return (new FileStream(pathName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
        }
    }
}
