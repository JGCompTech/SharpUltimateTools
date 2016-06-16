using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Microsoft.CSharp.Tools
{
    /// <summary>
    /// Returns checksums from files.
    /// </summary>
    public static class SecurityTools
    {
        /// <summary>
        /// Creates an object to store an RSA Private Key and Public Key
        /// </summary>
        public class RSAKeyPair
        {
            /// <summary>
            /// Private Key
            /// </summary>
            public String Private { get; set; }
            /// <summary>
            /// Public Key
            /// </summary>
            public String Public { get; set; }
        }

        ///<summary>
        /// Represents a pseudo-random number generator, a device that produces random data.
        ///</summary>
        public class CryptoRandom : RandomNumberGenerator
        {
            static RandomNumberGenerator r;
            ///<summary>
            /// Creates an instance of the default implementation of a cryptographic random number generator that can be used to generate random data.
            ///</summary>
            public CryptoRandom() { r = Create(); }
            ///<summary>
            /// Fills the elements of a specified array of bytes with random numbers.
            ///</summary>
            ///<param name="data">An array of bytes to contain random numbers.</param>
            public override void GetBytes(byte[] data) { r.GetBytes(data); }
            ///<summary>
            /// Returns a random number between 0.0 and 1.0.
            ///</summary>
            public static double NextDouble()
            {
                var buffer = new byte[4];
                r.GetBytes(buffer);
                return (double)BitConverter.ToUInt32(buffer, 0) / UInt32.MaxValue;
            }
            ///<summary>
            /// Returns a random number within the specified range.
            ///</summary>
            ///<param name="minValue">The inclusive lower bound of the random number returned.</param>
            ///<param name="maxValue">The exclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
            public static int Next(int minValue, int maxValue)
            {
                return (int)Math.Round(NextDouble() * (maxValue - minValue - 1)) + minValue;
            }
            ///<summary>
            /// Returns a nonnegative random number.
            ///</summary>
            public static int Next() => Next(0, Int32.MaxValue);

            ///<summary>
            /// Returns a nonnegative random number less than the specified maximum
            ///</summary>
            ///<param name="maxValue">The inclusive upper bound of the random number returned. maxValue must be greater than or equal 0</param>
            public static int Next(int maxValue) => Next(0, maxValue);
        }

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

        /// <summary>
        /// Creates a RNG Salt
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static String CreateSalt(int size)
        {
            Byte[] buffer;
            //Generate a cryptographic random number.
            using (var rng = new RNGCryptoServiceProvider())
            {
                buffer = new Byte[size];
                rng.GetBytes(buffer);
            }

            // Return a Base64 string representation of the random number.
            return Convert.ToBase64String(buffer);
        }

        /// <summary>
        /// Creates a SHA512 Hash
        /// </summary>
        /// <param name="value"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static String CreateHash(String value, String salt)
        {
            var saltedValue = Encoding.UTF8.GetBytes(value).Concat(Convert.FromBase64String(salt)).ToArray();

            using (var sham = new SHA512Managed())
            {
                return Convert.ToBase64String(sham.ComputeHash(saltedValue));
            }
        }

        /// <summary>
        /// Checks if login hashes match
        /// </summary>
        /// <param name="enteredPassword"></param>
        /// <param name="databasePassword"></param>
        /// <param name="databaseSalt"></param>
        /// <returns></returns>
        public static Boolean CheckHashesMatch(String enteredPassword, String databasePassword, String databaseSalt)
        {
            return (databasePassword.SequenceEqual(CreateHash(enteredPassword, databaseSalt)));
        }

        /// <summary>
        /// Creates a RSA key pair
        /// </summary>
        /// <returns></returns>
        public static RSAKeyPair CreateRSAKeyPair()
        {
            var KeyPair = new RSAKeyPair();
            var cspParams = new CspParameters { ProviderType = 1 };
            using (var rsaProvider = new RSACryptoServiceProvider(2048, cspParams))
            {
                KeyPair.Public = Convert.ToBase64String(rsaProvider.ExportCspBlob(false));
                KeyPair.Private = Convert.ToBase64String(rsaProvider.ExportCspBlob(true));
            }

            return KeyPair;
        }

        /// <summary>
        /// Encrypts a string with RSA encryption
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="unencryptedText"></param>
        /// <returns></returns>
        public static String Encrypt(String publicKey, String unencryptedText)
        {
            var cspParams = new CspParameters { ProviderType = 1 };
            byte[] encryptedBytes;
            using (var rsaProvider = new RSACryptoServiceProvider(cspParams))
            {
                rsaProvider.ImportCspBlob(Convert.FromBase64String(publicKey));

                var plainBytes = Encoding.UTF8.GetBytes(unencryptedText);
                encryptedBytes = rsaProvider.Encrypt(plainBytes, false);
            }

            return Convert.ToBase64String(encryptedBytes);
        }

        /// <summary>
        /// Decrypts a string with RSA encryption
        /// </summary>
        /// <param name="privateKey"></param>
        /// <param name="encryptedText"></param>
        /// <returns></returns>
        public static String Decrypt(String privateKey, String encryptedText)
        {
            var cspParams = new CspParameters { ProviderType = 1 };
            byte[] plainBytes;
            using (var rsaProvider = new RSACryptoServiceProvider(cspParams))
            {
                rsaProvider.ImportCspBlob(Convert.FromBase64String(privateKey));

                var encryptedBytes = Convert.FromBase64String(encryptedText);
                plainBytes = rsaProvider.Decrypt(encryptedBytes, false);
            }

            var plainText = Encoding.UTF8.GetString(plainBytes, 0, plainBytes.Length);

            return plainText;
        }

        internal static FileStream GetFileStream(String pathName)
        {
            return (new FileStream(pathName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
        }
    }
}
