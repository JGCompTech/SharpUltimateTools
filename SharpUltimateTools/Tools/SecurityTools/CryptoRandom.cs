using System;
using System.Security.Cryptography;

namespace JGCompTech.CSharp.Tools.SecurityTools
{
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
            return (double)BitConverter.ToUInt32(buffer, 0) / uint.MaxValue;
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
        public static int Next() => Next(0, int.MaxValue);

        ///<summary>
        /// Returns a nonnegative random number less than the specified maximum
        ///</summary>
        ///<param name="maxValue">The inclusive upper bound of the random number returned. maxValue must be greater than or equal 0</param>
        public static int Next(int maxValue) => Next(0, maxValue);
    }
}
