using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace JGCompTech.CSharp.Tools
{
    /// <summary>
    /// Returns checksums from files.
    /// </summary>
    public static class RSAHashes
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

        /// <summary>
        /// Creates a RSA key pair
        /// </summary>
        /// <returns></returns>
        public static RSAKeyPair GenerateRSAKeyPair()
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

        
    }
}
