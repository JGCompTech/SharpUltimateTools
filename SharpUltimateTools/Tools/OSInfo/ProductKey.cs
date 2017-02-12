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
                String key = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion";
                String value = "DigitalProductId";
                digitalProductId = RegistryInfo.getByteValue(HKEY.LOCAL_MACHINE, key, value);

                if (digitalProductId.IsNull()) { return "Cannot Retrieve Product Key."; }

                return CheckIf.IsWin8OrLater ? DecodeKeyWin8AndUp(digitalProductId) : DecodeKey(digitalProductId);
            }
        }

        /// <summary>
        /// Returns the decoded product key from the provided byte array. Works with Windows 7 and below.
        /// </summary>
        /// <param name="digitalProductId"></param>
        public static String DecodeKey(byte[] digitalProductId)
        {
            digitalProductId.ExceptionIfNull("The specified " + nameof(digitalProductId) + " cannot be null!", nameof(digitalProductId));
            // Offset of first byte of encoded product key in
            //  'DigitalProductIdxxx" REGBINARY value. Offset = 34H.
            const int keyStartIndex = 52;
            // Offset of last byte of encoded product key in
            //  'DigitalProductIdxxx" REGBINARY value. Offset = 43H.
            const int keyEndIndex = keyStartIndex + 15;
            // Possible alpha-numeric characters in product key.
            const string digits = "BCDFGHJKMPQRTVWXY2346789";
            // Length of decoded product key
            const int decodeLength = 29;
            // Length of decoded product key in byte-Form.
            // Each byte represents 2 chars.
            const int decodeStringLength = 15;
            // Array of containing the decoded product key.
            var decodedChars = new char[decodeLength];
            // Extract byte 52 to 67 inclusive.
            var hexPid = new System.Collections.ArrayList();
            for (int i = keyStartIndex; i <= keyEndIndex; i++) hexPid.Add(digitalProductId[i]);
            for (int i = decodeLength - 1; i >= 0; i--)
            {
                // Every sixth char is a separator.
                if ((i + 1) % 6 == 0) decodedChars[i] = '-';
                else
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
            }
            return new string(decodedChars);
        }

        /// <summary>
        /// Returns the decoded product key from the provided byte array. Works with Windows 8 and up.
        /// </summary>
        /// <param name="digitalProductId"></param>
        public static String DecodeKeyWin8AndUp(byte[] digitalProductId)
        {
            digitalProductId.ExceptionIfNull("The specified " + nameof(digitalProductId) + " cannot be null!", nameof(digitalProductId));
            var key = String.Empty;
            const int keyOffset = 52;
            var isWin8 = (byte)((digitalProductId[66] / 6) & 1);
            digitalProductId[66] = (byte)((digitalProductId[66] & 0xf7) | (isWin8 & 2) * 4);

            // Possible alpha-numeric characters in product key.
            const string digits = "BCDFGHJKMPQRTVWXY2346789";
            var last = 0;
            for (var i = 24; i >= 0; i--)
            {
                var current = 0;
                for (var j = 14; j >= 0; j--)
                {
                    current = current * 256;
                    current = digitalProductId[j + keyOffset] + current;
                    digitalProductId[j + keyOffset] = (byte)(current / 24);
                    current = current % 24;
                    last = current;
                }
                key = digits[current] + key;
            }
            var keypart1 = key.Substring(1, last);
            const string insert = "N";
            key = key.Substring(1).Replace(keypart1, keypart1 + insert);
            if (last == 0) key = insert + key;
            for (var i = 5; i < key.Length; i += 6) key = key.Insert(i, "-");
            return key;
        }
    }
}
