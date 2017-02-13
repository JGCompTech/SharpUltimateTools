using System;
using static JGCompTech.CSharp.Tools.RegistryInfo;

namespace JGCompTech.CSharp.Tools.OSInfo
{
    /// <summary>
    /// Gets And Decrypts The Current Product Key From The Registry
    /// </summary>
    public static class ProductKey
    {
        /// <summary>
        /// Gets And Decrypts The Current Product Key From The Registry
        /// </summary>
        /// <returns>Returns Product Key As A String</returns>
        ///
        public static String Key
        {
            get
            {
                byte[] digitalProductId = null;
                const String key = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion";
                const String value = "DigitalProductId";
                digitalProductId = getByteValue(HKEY.LOCAL_MACHINE, key, value);

                if (digitalProductId.IsNull()) { return "Cannot Retrieve Product Key."; }

                return CheckIf.IsWin8OrLater ? DecodeKeyWin8AndUp(digitalProductId) : DecodeKeyWin7AndBelow(digitalProductId);
            }
        }

        /// <summary>
        /// Returns the decoded product key from the provided byte array. Works with Windows 7 and below.
        /// </summary>
        /// <param name="digitalProductId"></param>
        public static String DecodeKeyWin7AndBelow(byte[] digitalProductId)
        {
            digitalProductId.ExceptionIfNull("The specified " + nameof(digitalProductId) + " cannot be null!", nameof(digitalProductId));
            // Length of decoded product key
            const int decodeKeyLength = 29;
            // Length of decoded product key in byte-Form.
            // Each byte represents 2 chars.
            const int decodeStringLength = 15;
            // Offset of first byte of encoded product key in
            //  'DigitalProductIdxxx" REGBINARY value. Offset = 34H.
            const int keyStartIndex = 52;
            // Offset of last byte of encoded product key in
            //  'DigitalProductIdxxx" REGBINARY value. Offset = 43H.
            const int keyEndIndex = keyStartIndex + decodeStringLength;
            // Possible alpha-numeric characters in product key.
            const string digits = "BCDFGHJKMPQRTVWXY2346789";
            // Array of containing the decoded product key.
            var decodedChars = new char[decodeKeyLength];
            // Extract byte 52 to 67 inclusive.
            var hexPid = new System.Collections.ArrayList();
            for (int i = keyStartIndex; i <= keyEndIndex; i++) hexPid.Add(digitalProductId[i]);
            for (int i = decodeKeyLength - 1; i >= 0; i--)
            {
                // Do the actual decoding.
                var digitMapIndex = 0;
                for (int j = decodeStringLength - 1; j >= 0; j--)
                {
                    var byteValue = (digitMapIndex << 8) | (byte)hexPid[j];
                    hexPid[j] = (byte)(byteValue / 24);
                    digitMapIndex = byteValue % 24;
                    decodedChars[i] = digits[digitMapIndex];
                }
            }
            var key = new string(decodedChars);
            // Every sixth char is a separator.
            for (var i = 5; i < key.Length; i += 6) key = key.Insert(i, "-");
            return key;
        }

        /// <summary>
        /// Returns the decoded product key from the provided byte array. Works with Windows 8 and up.
        /// </summary>
        /// <param name="digitalProductId"></param>
        public static String DecodeKeyWin8AndUp(byte[] digitalProductId)
        {
            digitalProductId.ExceptionIfNull("The specified " + nameof(digitalProductId) + " cannot be null!", nameof(digitalProductId));
            var key = String.Empty;
            // Length of decoded product key in byte-Form.
            // Each byte represents 2 chars.
            const int decodeStringLength = 15;
            // Offset of first byte of encoded product key in
            //  'DigitalProductIdxxx" REGBINARY value. Offset = 34H.
            const int keyStartIndex = 52;
            // Offset of last byte of encoded product key in
            //  'DigitalProductIdxxx" REGBINARY value. Offset = 43H.
            const int keyEndIndex = keyStartIndex + decodeStringLength;
            var isWin8 = (byte)((digitalProductId[keyEndIndex - 1] / 6) & 1);
            digitalProductId[keyEndIndex - 1] = (byte)((digitalProductId[keyEndIndex - 1] & 247) | (isWin8 & 2) * 4);

            // Possible alpha-numeric characters in product key.
            const string digits = "BCDFGHJKMPQRTVWXY2346789";
            var last = 0;
            for (var i = 24; i >= 0; i--)
            {
                var current = 0;
                for (var j = decodeStringLength - 1; j >= 0; j--)
                {
                    current *= 256;
                    current = digitalProductId[j + keyStartIndex] + current;
                    digitalProductId[j + keyStartIndex] = (byte)(current / 24);
                    current %= 24;
                    last = current;
                }
                key = digits[current] + key;
            }
            var keypart1 = key.Substring(1, last);
            const string insert = "N";
            key = key.Substring(1).Replace(keypart1, keypart1 + insert);
            if (last == 0) key = insert + key;
            // Every sixth char is a separator.
            for (var i = 5; i < key.Length; i += 6) key = key.Insert(i, "-");
            return key;
        }
    }
}
