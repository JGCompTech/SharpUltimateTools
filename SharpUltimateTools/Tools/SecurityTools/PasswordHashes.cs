using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace JGCompTech.CSharp.Tools.SecurityTools
{
    /// <summary>
    /// Tools to work with passwords
    /// </summary>
    public static class PasswordHashes
    {
        /// <summary>
        /// Creates a RNG Salt
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static String CreateSalt(int size)
        {
            byte[] buffer;
            //Generate a cryptographic random number.
            using (var rng = new RNGCryptoServiceProvider())
            {
                buffer = new byte[size];
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
    }
}
