using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CultureInfo = System.Globalization.CultureInfo;

namespace Microsoft.CSharp.Tools
{
    /// <summary>
    /// General Extensions
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Allows for automatic anonymous method invocation.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="mi"></param>
        public static void Invoke(this System.Windows.Forms.Control control, System.Windows.Forms.MethodInvoker mi)
        {
            control.Invoke(mi);
            return;
        }
        /// <summary>
        /// Turns any object to null
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Object ToNull(this Object input) => null;

        /// <summary>
        /// Turns any object into an Exception
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Exception ToException(this Object input) => new Exception(input.ToString());

        /// <summary>
        /// Tests if the collection is empty. Throws an <see cref="ArgumentNullException"/>
        /// if the the value is null
        /// </summary>
        /// <param name="input">The collection to test.</param>
        /// <returns>True if the collection is empty.</returns>
        public static Boolean IsEmpty(this ICollection input)
        {
            input.ExceptionIfNull("The collection cannot be null.", "collection");

            return input.Count == 0;
        }
        /// <summary>
        /// 	Returns TRUE, if specified target reference equals null.
        /// 	Otherwise returns FALSE.
        /// </summary>
        /// <param name = "input">Target reference. Can be null.</param>
        public static Boolean IsNull(this Object input) => IsNull<Object>(input);

        /// <summary>
        /// 	Returns TRUE, if specified target reference equals null.
        /// 	Otherwise returns FALSE.
        /// </summary>
        /// <typeparam name = "T">Type of target.</typeparam>
        /// <param name = "input">Target reference. Can be null.</param>
        public static Boolean IsNull<T>(this T input) => ReferenceEquals(input, null);

        /// <summary>
        /// 	Returns TRUE, if specified target reference does not equal null.
        /// 	Otherwise returns FALSE.
        /// </summary>
        /// <param name = "input">Target reference. Can be null.</param>
        public static Boolean IsNotNull(this Object input) => IsNotNull<Object>(input);

        /// <summary>
        /// 	Returns TRUE, if specified target reference does not equal null.
        /// 	Otherwise returns FALSE.
        /// </summary>
        /// <typeparam name = "T">Type of target.</typeparam>
        /// <param name = "input">Target reference. Can be null.</param>
        public static Boolean IsNotNull<T>(this T input) => !ReferenceEquals(input, null);

        /// <summary>
        /// Checks if a Collection is null or empty
        /// </summary>
        /// <param name="input">Target reference. Can be null.</param>
        /// <returns></returns>
        public static Boolean IsNullOrEmpty(this ICollection input) => (input == null || input.Count == 0);

        /// <summary>
        /// Checks if a Dictionary is null or empty
        /// </summary>
        /// <param name="input">Target reference. Can be null.</param>
        /// <returns></returns>
        public static Boolean IsNullOrEmpty(this IDictionary input) => (input == null || input.Count == 0);

        /// <summary>
        /// Checks if a List is null or empty
        /// </summary>
        /// <param name="input">Target reference. Can be null.</param>
        /// <returns></returns>
        public static Boolean IsNullOrEmpty(this IList input) => (input == null || input.Count == 0);

        /// <summary>
        /// 	Adds a value uniquely to to a collection and returns a value whether the value was added or not.
        /// </summary>
        /// <typeparam name = "T">The generic collection value type</typeparam>
        /// <param name = "input">The collection.</param>
        /// <param name = "value">The value to be added.</param>
        /// <returns>Indicates whether the value was added or not</returns>
        /// <example>
        /// 	<code>
        /// 		list.AddUnique(1); // returns true;
        /// 		list.AddUnique(1); // returns false the second time;
        /// 	</code>
        /// </example>
        public static Boolean AddUnique<T>(this ICollection<T> input, T value)
        {
            if (!input.Contains(value))
            {
                input.Add(value);
                return true;
            }
            return false;
        }
        /// <summary>
        /// 	Inserts an item uniquely to to a list and returns a value whether the item was inserted or not.
        /// </summary>
        /// <typeparam name = "T">The generic list item type.</typeparam>
        /// <param name = "input">The list to be inserted into.</param>
        /// <param name = "index">The index to insert the item at.</param>
        /// <param name = "item">The item to be added.</param>
        /// <returns>Indicates whether the item was inserted or not</returns>
        public static Boolean InsertUnique<T>(this IList<T> input, int index, T item)
        {
            if (!input.Contains(item))
            {
                input.Insert(index, item);
                return true;
            }
            return false;
        }
        /// <summary>
        /// 	Performs an action for each item in the enumerable
        /// </summary>
        /// <typeparam name = "T">The enumerable data type</typeparam>
        /// <param name = "values">The data values.</param>
        /// <param name = "action">The action to be performed.</param>
        /// <example>
        /// 	var values = new[] { "1", "2", "3" };
        /// 	values.ConvertList&lt;string, int&gt;().ForEach(Console.WriteLine);
        /// </example>
        /// <remarks>
        /// 	This method was intended to return the passed values to provide method chaining. Howver due to defered execution the compiler would actually never run the entire code at all.
        /// </remarks>
        internal static void ForEach<T>(this IEnumerable<T> values, Action<T> action)
        {
            foreach (var value in values)
                action?.Invoke(value);
        }
    }

    /// <summary>
    /// String Related Extensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Checks if an string is null or empty
        /// </summary>
        /// <param name="input">Target reference. Can be null.</param>
        /// <returns></returns>
        public static Boolean IsNullOrEmpty(this String input) => String.IsNullOrEmpty(input);

        /// <summary>
        /// Checks if an string is not null or empty
        /// </summary>
        /// <param name = "input">Target reference. Can be null.</param>
        /// <returns></returns>
        public static Boolean IsNotNullOrEmpty(this String input) => (!input.IsNullOrEmpty());

        /// <summary>
        /// 	Checks if a string is null or empty and returns a default value if fails
        /// </summary>
        /// <param name = "value"></param>
        /// <param name = "defaultValue"></param>
        /// <returns>Either the string or the default value.</returns>
        public static String IfNullOrEmpty(this String value, String defaultValue)
        {
            return (value.IsNullOrEmpty() ? defaultValue : value);
        }
        /// <summary>
        /// It returns true if string is null or empty or just a white space otherwise it returns false
        /// </summary>
        /// <param name = "input">Target reference. Can be null.</param>
        /// <returns></returns>
        public static Boolean IsNullOrEmptyOrWhiteSpace(this String input)
        {
            return input.IsNullOrEmpty() || input.Trim().IsNullOrEmpty();
        }
        /// <summary>
        /// Checks if a string is an valid URL
        /// </summary>
        /// <param name = "input">Target reference.</param>
        /// <returns></returns>
        public static Boolean IsValidUrl(this String input)
        {
            return Regex.IsMatch(input, @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?", RegexOptions.Compiled);
        }
        /// <summary>
        /// Checks if a string is a valid IPv4 address
        /// </summary>
        /// <param name = "input">Target reference.</param>
        /// <returns></returns>
        public static Boolean IsValidIPAddress(this String input)
        {
            return Regex.IsMatch(input,
                    @"\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b", RegexOptions.Compiled);
        }
        /// <summary>
        /// Checks if a string is an valid e-mail address
        /// </summary>
        /// <param name = "input">Target reference.</param>
        /// <returns></returns>
        public static Boolean IsValidEmailAddress(this String input)
        {
            return Regex.IsMatch(input, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", RegexOptions.Compiled);
        }
        /// <summary>
        /// 	Ensures that a string starts with a given prefix.
        /// </summary>
        /// <param name = "input"></param>
        /// <param name = "prefix"></param>
        /// <param name = "ignoreCase"></param>
        /// <returns>The string value including the prefix</returns>
        /// <example>
        /// 	<code>
        /// 		var extension = "txt";
        /// 		var fileName = string.Concat(file.Name, extension.EnsureStartsWith(".", true));
        /// 	</code>
        /// </example>
        public static String EnsureStartsWith(this String input, String prefix, Boolean ignoreCase)
        {
            return input.StartsWith(prefix, ignoreCase, CultureInfo.CurrentCulture) ? input : String.Concat(prefix, input);
        }
        /// <summary>
        /// 	Ensures that a string ends with a given suffix.
        /// </summary>
        /// <param name = "input"></param>
        /// <param name = "suffix"></param>
        /// <param name = "ignoreCase"></param>
        /// <returns>The string value including the suffix</returns>
        /// <example>
        /// 	<code>
        /// 		var url = "http://www.google.com";
        /// 		url = url.EnsureEndsWith("/", true));
        /// 	</code>
        /// </example>
        public static String EnsureEndsWith(this String input, String suffix, Boolean ignoreCase)
        {
            return input.EndsWith(suffix, ignoreCase, CultureInfo.CurrentCulture) ? input : String.Concat(input, suffix);
        }
        /// <summary>
        /// Converts a string to a Boolean. ArgumentException is thrown if fails
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Boolean ToBoolean(this String input)
        {
            var value = input.ToLower(CultureInfo.CurrentCulture).Trim();
            switch(value)
            {
                case "true":
                    return true;
                case "false":
                    return false;
                case "t":
                    return true;
                case "f":
                    return false;
                case "yes":
                    return true;
                case "no":
                    return false;
                case "y":
                    return true;
                case "n":
                    return false;
                case "1":
                    return true;
                case "0":
                    return false;
            }
            throw new ArgumentException("Input is not a boolean value.");
        }
        /// <summary>
        /// Removes the last character from a string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static String RemoveLastCharacter(this String input) => input.Substring(0, input.Length - 1);

        /// <summary>
        /// Removes the last number of characters from a string
        /// </summary>
        /// <param name="input"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static String RemoveLast(this String input, Int32 number) => input.Substring(0, input.Length - number);

        /// <summary>
        /// Removes the first character from a string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static String RemoveFirstCharacter(this String input) => input.Substring(1);

        /// <summary>
        /// Removes the first number of characters from a string
        /// </summary>
        /// <param name="input"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static String RemoveFirst(this String input, Int32 number) => input.Substring(number);

        /// <summary>
        /// Removes all special characters from the string.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>The adjusted string.</returns>
        public static String RemoveAllSpecialCharacters(this String input)
        {
            var sb = new System.Text.StringBuilder(input.Length);
            foreach (var c in input.Where(c => Char.IsLetterOrDigit(c)))
                sb.Append(c);
            return sb.ToString();
        }
        /// <summary>
        /// 	Reverses / mirrors a string.
        /// </summary>
        /// <param name = "input"></param>
        /// <returns>The reversed string</returns>
        public static String Reverse(this String input)
        {
            if (input.IsNullOrEmpty() || (input.Length == 1))
                return input;

            var chars = input.ToCharArray();
            Array.Reverse(chars);
            return new String(chars);
        }
        /// <summary>
        /// Returns the first part of the string, up until the character c. If c is not found in the
        /// string the whole string is returned.
        /// </summary>
        /// <param name="input">String to truncate</param>
        /// <param name="c">Separator</param>
        public static String LeftOf(this String input, Char c)
        {
            var ndx = input.IndexOf(c);
            if (ndx >= 0)
            {
                return input.Substring(0, ndx);
            }

            return input;
        }
        /// <summary>
        /// Returns right part of the string, after the character c. If c is not found in the
        /// string the whole string is returned.
        /// </summary>
        /// <param name="input">String to truncate</param>
        /// <param name="c">Separator</param>
        public static String RightOf(this String input, Char c)
        {
            var ndx = input.IndexOf(c);
            if (ndx == -1)
                return input;
            return input.Substring(ndx + 1);
        }
        /// <summary>
        /// Returns true if string does not start with the pattern, otherwise false. If patern is null or empty, false will be returned.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static Boolean DoesNotStartWith(this String input, String pattern) => pattern.IsNullOrEmpty() ||
                   input.IsNullOrEmptyOrWhiteSpace() ||
                   !input.StartsWith(pattern, StringComparison.CurrentCulture);

        /// <summary>
        /// Returns true if string does not end with the pattern, otherwise false. If patern is null or empty, false will be returned.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static Boolean DoesNotEndWith(this String input, String pattern) => pattern.IsNullOrEmpty() ||
                     input.IsNullOrEmptyOrWhiteSpace() ||
                     !input.EndsWith(pattern, StringComparison.CurrentCulture);

        /// <summary>
        /// Returns first character in a string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static String FirstChar(this String input)
        {
            if (!input.IsNullOrEmpty())
            {
                return (input.Length >= 1) ? input.Substring(0, 1) : input;
            }
            return null;
        }
        /// <summary>
        /// Returns last character in a string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static String LastChar(this String input)
        {
            if (!input.IsNullOrEmpty())
            {
                return (input.Length >= 1) ? input.Substring(input.Length - 1, 1) : input;
            }
            return null;
        }
        /// <summary>
        /// Returns first number of characters in string
        /// </summary>
        /// <param name="input"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static String FirstChars(this String input, Int32 number)
        {
            if (input.IsNullOrEmpty()) return input;
            return (input.Length < number ? input : input.Substring(0, number));
        }
        /// <summary>
        /// Returns string with first char upercase
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static String UppercaseFirst(this String input)
        {
            // Check for empty string.
            if (input.IsNullOrEmpty()) return string.Empty;
            // Return char and concat substring.
            return char.ToUpper(input[0], CultureInfo.CurrentCulture) + input.Substring(1);
        }
    }

    /// <summary>
    /// DateTime Related Extensions
    /// </summary>
    public static class TimeExtensions
    {
        static readonly DateTime EPOCH = DateTime.SpecifyKind(new DateTime(1970, 1, 1, 0, 0, 0, 0), DateTimeKind.Utc);

        /// <summary>
        /// Converts Unix timestamp to a DateTime
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static DateTime FromUnixTimestamp(this Int64 input) => EPOCH.AddSeconds(input);

        /// <summary>
        /// Converts Unix timestamp to a miliseconds DateTime
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static DateTime FromUnixTimestampUltra(this Int64 input) => EPOCH.AddMilliseconds(input);

        /// <summary>
        /// Converts a DateTime to a Unix timestamp
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Int64 ToUnixTimestamp(this DateTime input)
        {
            var diff = input.ToUniversalTime() - EPOCH;
            return (Int64)diff.TotalSeconds;
        }
        /// <summary>
        /// Converts a miliseconds DateTime to a Unix timestamp
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Int64 ToUnixTimestampUltra(this DateTime input)
        {
            var diff = input.ToUniversalTime() - EPOCH;
            return (Int64)diff.TotalMilliseconds;
        }
        /// <summary>
        /// Checks if a string is DateTime.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Boolean IsDate(this String input)
        {
            if (!input.IsNullOrEmpty())
            {
                DateTime dt;
                return (DateTime.TryParse(input, out dt));
            }
            return false;
        }
        /// <summary>
        /// Checks if a date falls before a date
        /// </summary>
        /// <param name="input"></param>
        /// <param name="from"></param>
        /// <returns></returns>
        public static Boolean IsBefore(this DateTime input, DateTime from) => input.Date > from.Date;

        /// <summary>
        /// Checks if a date falls before DateTime.Now
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Boolean IsBeforeNow(this DateTime input) => input.IsBefore(DateTime.Now);

        /// <summary>
        /// Checks if a date falls after a date
        /// </summary>
        /// <param name="input"></param>
        /// <param name="from"></param>
        /// <returns></returns>
        public static Boolean IsAfter(this DateTime input, DateTime from) => input.Date < from.Date;

        /// <summary>
        /// Checks if a date falls after DateTime.Now
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Boolean IsAfterNow(this DateTime input) => input.IsAfter(DateTime.Now);

        /// <summary>
        /// 	Checks if a date is today.
        /// </summary>
        /// <param name = "input"></param>
        /// <returns>
        /// 	<c>true</c> if the specified date is today; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsToday(this DateTime input) => (input.Date == DateTime.Today);

        /// <summary>
        /// 	Checks if the time only part of two DateTime values are equal.
        /// </summary>
        /// <param name = "input"></param>
        /// <param name = "timeToCompare"></param>
        /// <returns>
        /// 	<c>true</c> if both time values are equal; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsTimeEqual(this DateTime input, DateTime timeToCompare) => (input.TimeOfDay == timeToCompare.TimeOfDay);
    }

    /// <summary>
    /// Number Related Extensions
    /// </summary>
    public static class NumberExtensions
    {
        /// <summary>
        /// Kilobytes
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Int64 KB(this Int32 input) { checked { return input * 1024; } }

        /// <summary>
        /// Megabytes
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Int64 MB(this Int32 input) { checked { return input.KB() * 1024; } }

        /// <summary>
        /// Gigabytes
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Int64 GB(this Int32 input) { checked { return input.MB() * 1024; } }

        /// <summary>
        /// Terabytes
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Int64 TB(this Int32 input) { checked { return input.TB() * 1024; } }

        /// <summary>
        /// Returns the conversion from bytes to the correct version. Ex. 1024 bytes = 1 KB
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static String ConvertBytes(this Double input)
        {
            input = input / 1024;
            if (input >= 0)
            {
                if (input >= 1024)
                {
                    input = input / 1024;
                    if (input >= 1024)
                    {
                        input = input / 1024;
                        if (input >= 1024)
                        {
                            input = input / 1024;
                            if ((input % 1) == 0) { return input.ToString("n0", CultureInfo.CurrentCulture) + " TB"; }
                            return input.ToString("n2", CultureInfo.CurrentCulture) + " TB";
                        }
                        if ((input % 1) == 0) { return input.ToString("n0", CultureInfo.CurrentCulture) + " GB"; }
                        return input.ToString("n2", CultureInfo.CurrentCulture) + " GB";
                    }
                    if ((input % 1) == 0) { return input.ToString("n0", CultureInfo.CurrentCulture) + " MB"; }
                    return input.ToString("n2", CultureInfo.CurrentCulture) + " MB";
                }
                if ((input % 1) == 0) { return input.ToString("n0", CultureInfo.CurrentCulture) + " KB"; }
                return input.ToString("n2", CultureInfo.CurrentCulture) + " KB";
            }
            if ((input % 1) == 0) { return input.ToString("n0", CultureInfo.CurrentCulture) + " Bytes"; }
            return input.ToString("n2", CultureInfo.CurrentCulture) + " Bytes";
        }
        /// <summary>
        /// Returns the conversion from KB to the correct version. Ex. 1024 KB = 1 MB
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static String ConvertKilobytes(this Int64 input)
        {
            input = input / 1024;
            if (input >= 0)
            {
                if (input >= 1024)
                {
                    input = input / 1024;
                    if (input >= 1024)
                    {
                        input = input / 1024;
                        if ((input % 1) == 0) { return input.ToString("n0", CultureInfo.CurrentCulture) + " TB"; }
                        return input.ToString("n2", CultureInfo.CurrentCulture) + " TB";
                    }
                    if ((input % 1) == 0) { return input.ToString("n0", CultureInfo.CurrentCulture) + " GB"; }
                    return input.ToString("n2", CultureInfo.CurrentCulture) + " GB";
                }
                if ((input % 1) == 0) { return input.ToString("n0", CultureInfo.CurrentCulture) + " MB"; }
                return input.ToString("n2", CultureInfo.CurrentCulture) + " MB";
            }
            if ((input % 1) == 0) { return input.ToString("n0", CultureInfo.CurrentCulture) + " KB"; }
            return input.ToString("n2", CultureInfo.CurrentCulture) + " KB";
        }
        /// <summary>
        /// Converts any type in to an Int32
        /// </summary>
        /// <typeparam name="T">Any Object</typeparam>
        /// <param name="input">Value to convert</param>
        /// <returns>The integer, 0 if unsuccessful</returns>
        public static Int32 ToInt32<T>(this T input)
        {
            Int32 result;
            if (Int32.TryParse(input.ToString(), out result))
            {
                return result;
            }
            return 0;
        }
        /// <summary>
        /// Converts any type in to an Int32. Returns defaultValue if fails
        /// </summary>
        /// <param name="input">Value to convert</param>
        /// <typeparam name="T">Any Object</typeparam>
        /// <param name="defaultValue">Default to use</param>
        /// <returns>The defaultValue if unsuccessful</returns>
        public static Int32 ToInt32<T>(this T input, Int32 defaultValue)
        {
            Int32 result;
            if (Int32.TryParse(input.ToString(), out result))
            {
                return result;
            }
            return defaultValue;
        }
        /// <summary>
        /// Checks to see if a string is an Int32
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Boolean IsInt32(this String input)
        {
            Int32 temp;

            return Int32.TryParse(input, out temp);
        }
        /// <summary>
        /// Converts a string to an Int32. Returns defaultValue if fails
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Int32 ToInt32(this String input, Int32 defaultValue)
        {
            Int32 temp;

            return (Int32.TryParse(input, out temp)) ? temp : defaultValue;
        }
        /// <summary>
        /// Checks to see if a string is a Decimal
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Boolean IsDecimal(this String input)
        {
            Decimal temp;

            return Decimal.TryParse(input, out temp);
        }
        /// <summary>
        /// Converts a string to a Double. Returns defaultValue if fails
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Decimal ToDecimal(this String input, Decimal defaultValue)
        {
            Decimal temp;

            return (Decimal.TryParse(input, out temp)) ? temp : defaultValue;
        }
        /// <summary>
        /// Converts a string to a Double
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Double ToDouble(this String input)
        {
            Double retNum;
            var result = Double.TryParse(input, out retNum);
            return result ? retNum : 0;
        }
        /// <summary>
        /// Checks if a number is prime
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Boolean IsPrime(this Int32 input)
        {
            if ((input % 2) == 0) return input == 2;
            var sqrt = (Int32)Math.Sqrt(input);
            for (Int32 t = 3; t <= sqrt; t = t + 2)
            {
                if (input % t == 0) return false;
            }
            return input != 1;
        }
        /// <summary>
        /// Checks if a number is even
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Boolean IsEven(this Int32 input) => input % 2 == 0;

        /// <summary>
        /// Checks if a number is odd
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Boolean IsOdd(this Int32 input) => input % 2 != 0;

        /// <summary>
        /// Returns a number squared
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Int32 Squared(this Int32 input) => input * input;

        /// <summary>
        /// Checks if a number is in between a range
        /// </summary>
        /// <param name="input"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static Boolean IsInRange(this Int32 input, Int32 start, Int32 end) => input >= start && input <= end;

        /// <summary>
        /// Rounds the supplied decimal to the specified amount of decimal points
        /// </summary>
        /// <param name="input">The decimal to round</param>
        /// <param name="decimalPoints">The number of decimal points to round the output value to</param>
        /// <returns>A rounded decimal</returns>
        public static Decimal RoundDecimalPoints(this Decimal input, Int32 decimalPoints) => Math.Round(input, decimalPoints);

        /// <summary>
        /// Rounds the supplied decimal value to two decimal points
        /// </summary>
        /// <param name="input">The decimal to round</param>
        /// <returns>A decimal value rounded to two decimal points</returns>
        public static Decimal RoundToTwoDecimalPoints(this Decimal input) => Math.Round(input, 2);
    }

    /// <summary>
    /// Exception Generating Extensions
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/>
        /// if the the value is null
        /// </summary>
        /// <param name="input">The value to test.</param>
        /// <param name="message">The message to display if the value is null.</param>
        /// <param name="name">The name of the parameter being tested.</param>
        public static Object ExceptionIfNull(this Object input, String message, String name)
        {
            if (input == null)
                throw new ArgumentNullException(name, message);
            return input;
        }
        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/>
        /// if the the Collection is null or empty
        /// </summary>
        /// <param name="input">The value to test.</param>
        /// <param name="message">The message to display if the value is null.</param>
        /// <param name="name">The name of the parameter being tested.</param>
        public static ICollection ExceptionIfNullOrEmpty(this ICollection input, String message, String name)
        {
            if (input == null)
                throw new ArgumentNullException(name, message);
            if (input.Count == 0)
                throw new ArgumentNullException(name, message);
            return input;
        }
        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/>
        /// if the the Dictionary is null or empty
        /// </summary>
        /// <param name="input">The value to test.</param>
        /// <param name="message">The message to display if the value is null.</param>
        /// <param name="name">The name of the parameter being tested.</param>
        public static ICollection ExceptionIfNullOrEmpty(this IDictionary input, String message, String name)
        {
            if (input == null)
                throw new ArgumentNullException(name, message);
            if (input.Count == 0)
                throw new ArgumentNullException(name, message);
            return input;
        }
        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/>
        /// if the the List is null or empty
        /// </summary>
        /// <param name="input">The value to test.</param>
        /// <param name="message">The message to display if the value is null.</param>
        /// <param name="name">The name of the parameter being tested.</param>
        public static ICollection ExceptionIfNullOrEmpty(this IList input, String message, String name)
        {
            if (input == null)
                throw new ArgumentNullException(name, message);
            if (input.Count == 0)
                throw new ArgumentNullException(name, message);
            return input;
        }
        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/>
        /// if the the ReadOnlyCollection is null or empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="message"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IReadOnlyCollection<T> ExceptionIfNullOrEmpty<T>(this IReadOnlyCollection<T> input, String message, String name)
        {
            if (input == null)
                throw new ArgumentNullException(name, message);
            if (input.Count == 0)
                throw new ArgumentNullException(name, message);
            return input;
        }
        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/>
        /// if the the ReadOnlyDictionary is null or empty
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="input"></param>
        /// <param name="message"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IReadOnlyDictionary<T1, T2> ExceptionIfNullOrEmpty<T1, T2>(this IReadOnlyDictionary<T1, T2> input, String message, String name)
        {
            if (input == null)
                throw new ArgumentNullException(name, message);
            if (input.Count == 0)
                throw new ArgumentNullException(name, message);
            return input;
        }
        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/>
        /// if the the ReadOnlyList is null or empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="message"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IReadOnlyList<T> ExceptionIfNullOrEmpty<T>(this IReadOnlyList<T> input, String message, String name)
        {
            if (input == null)
                throw new ArgumentNullException(name, message);
            if (input.Count == 0)
                throw new ArgumentNullException(name, message);
            return input;
        }
        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the string value is null or empty.
        /// </summary>
        /// <param name="input">The value to test.</param>
        /// <param name="message">The message to display if the value is null.</param>
        /// <param name="name">The name of the parameter being tested.</param>
        public static String ExceptionIfNullOrEmpty(this String input, String message, String name)
        {
            if (input.IsNullOrEmpty())
                throw new ArgumentException(message, name);
            return input;
        }
    }

    /// <summary>
    /// Extension methods for the FileInfo and FileInfo-Array classes
    /// </summary>
    public static class FileInfoExtensions
    {
        /// <summary>
        /// Renames a file.
        /// </summary>
        /// <param name = "file">The file.</param>
        /// <param name = "newName">The new name.</param>
        /// <returns>The renamed file</returns>
        /// <example>
        /// 	<code>
        /// 		var file = new FileInfo(@"c:\test.txt");
        /// 		file.Rename("test2.txt");
        /// 	</code>
        /// </example>
        public static FileInfo Rename(this FileInfo file, String newName)
        {
            var filePath = Path.Combine(Path.GetDirectoryName(file.FullName), newName);
            file.MoveTo(filePath);
            return file;
        }
        /// <summary>
        /// Renames a without changing its extension.
        /// </summary>
        /// <param name = "file">The file.</param>
        /// <param name = "newName">The new name.</param>
        /// <returns>The renamed file</returns>
        /// <example>
        /// 	<code>
        /// 		var file = new FileInfo(@"c:\test.txt");
        /// 		file.RenameFileWithoutExtension("test3");
        /// 	</code>
        /// </example>
        public static FileInfo RenameFileWithoutExtension(this FileInfo file, String newName)
        {
            var fileName = String.Concat(newName, file.Extension);
            file.Rename(fileName);
            return file;
        }
        /// <summary>
        /// Changes the files extension.
        /// </summary>
        /// <param name = "file">The file.</param>
        /// <param name = "newExtension">The new extension.</param>
        /// <returns>The renamed file</returns>
        /// <example>
        /// 	<code>
        /// 		var file = new FileInfo(@"c:\test.txt");
        /// 		file.ChangeExtension("xml");
        /// 	</code>
        /// </example>
        public static FileInfo ChangeExtension(this FileInfo file, String newExtension)
        {
            newExtension = newExtension.EnsureStartsWith(".", true);
            var fileName = String.Concat(Path.GetFileNameWithoutExtension(file.FullName), newExtension);
            file.Rename(fileName);
            return file;
        }
        /// <summary>
        /// Changes the extensions of several files at once.
        /// </summary>
        /// <param name = "files">The files.</param>
        /// <param name = "newExtension">The new extension.</param>
        /// <returns>The renamed files</returns>
        /// <example>
        /// 	<code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		files.ChangeExtensions("tmp");
        /// 	</code>
        /// </example>
        public static FileInfo[] ChangeExtensions(this FileInfo[] files, String newExtension)
        {
            files.ForEach(f => f.ChangeExtension(newExtension));
            return files;
        }
        /// <summary>
        /// Deletes several files at once and consolidates any exceptions.
        /// </summary>
        /// <param name = "files">The files.</param>
        /// <example>
        /// 	<code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		files.Delete()
        /// 	</code>
        /// </example>
        public static void Delete(this FileInfo[] files)
        {
            files.Delete(true);
        }
        /// <summary>
        /// Deletes several files at once and optionally consolidates any exceptions.
        /// </summary>
        /// <param name = "files">The files.</param>
        /// <param name = "consolidateExceptions">if set to <c>true</c> exceptions are consolidated and the processing is not interrupted.</param>
        /// <example>
        /// 	<code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		files.Delete()
        /// 	</code>
        /// </example>
        public static void Delete(this FileInfo[] files, Boolean consolidateExceptions)
        {

            if (consolidateExceptions)
            {
                var exceptions = new List<Exception>();

                foreach (var file in files)
                {
                    try { file.Delete(); }
                    catch (Exception e) { exceptions.Add(e); }
                }
                if (exceptions.Any())
                    throw Exceptions.CombinedException.Combine("Error while deleting one or several files, see InnerExceptions array for details.", exceptions);
            }
            else foreach (var file in files) file.Delete();
        }
        /// <summary>
        /// Copies several files to a new folder at once and consolidates any exceptions.
        /// </summary>
        /// <param name = "files">The files.</param>
        /// <param name = "targetPath">The target path.</param>
        /// <returns>The newly created file copies</returns>
        /// <example>
        /// 	<code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		var copiedFiles = files.CopyTo(@"c:\temp\");
        /// 	</code>
        /// </example>
        public static FileInfo[] CopyTo(this FileInfo[] files, String targetPath) => files.CopyTo(targetPath, true);

        /// <summary>
        /// Copies several files to a new folder at once and optionally consolidates any exceptions.
        /// </summary>
        /// <param name = "files">The files.</param>
        /// <param name = "targetPath">The target path.</param>
        /// <param name = "consolidateExceptions">if set to <c>true</c> exceptions are consolidated and the processing is not interrupted.</param>
        /// <returns>The newly created file copies</returns>
        /// <example>
        /// 	<code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		var copiedFiles = files.CopyTo(@"c:\temp\");
        /// 	</code>
        /// </example>
        public static FileInfo[] CopyTo(this FileInfo[] files, String targetPath, Boolean consolidateExceptions)
        {
            var copiedfiles = new List<FileInfo>();
            List<Exception> exceptions = null;

            foreach (var file in files)
            {
                try
                {
                    var fileName = Path.Combine(targetPath, file.Name);
                    copiedfiles.Add(file.CopyTo(fileName));
                }
                catch (Exception e)
                {
                    if (consolidateExceptions)
                    {
                        if (exceptions == null) exceptions = new List<Exception>();
                        exceptions.Add(e);
                    }
                    else throw;
                }
            }

            if ((exceptions != null) && (exceptions.Count > 0))
                throw new Exceptions.CombinedException("Error while copying one or several files, see InnerExceptions array for details.", exceptions.ToArray());

            return copiedfiles.ToArray();
        }
        /// <summary>
        /// Moves several files to a new folder at once and optionally consolidates any exceptions.
        /// </summary>
        /// <param name = "files">The files.</param>
        /// <param name = "targetPath">The target path.</param>
        /// <returns>The moved files</returns>
        /// <example>
        /// 	<code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		files.MoveTo(@"c:\temp\");
        /// 	</code>
        /// </example>
        public static FileInfo[] MoveTo(this FileInfo[] files, String targetPath) => files.MoveTo(targetPath, true);

        /// <summary>
        /// Moves several files to a new folder at once and optionally consolidates any exceptions.
        /// </summary>
        /// <param name = "files">The files.</param>
        /// <param name = "targetPath">The target path.</param>
        /// <param name = "consolidateExceptions">if set to <c>true</c> exceptions are consolidated and the processing is not interrupted.</param>
        /// <returns>The moved files</returns>
        /// <example>
        /// 	<code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		files.MoveTo(@"c:\temp\");
        /// 	</code>
        /// </example>
        public static FileInfo[] MoveTo(this FileInfo[] files, String targetPath, Boolean consolidateExceptions)
        {
            List<Exception> exceptions = null;

            foreach (var file in files)
            {
                try
                {
                    var fileName = Path.Combine(targetPath, file.Name);
                    file.MoveTo(fileName);
                }
                catch (Exception e)
                {
                    if (consolidateExceptions)
                    {
                        if (exceptions == null) exceptions = new List<Exception>();
                        exceptions.Add(e);
                    }
                    else throw;
                }
            }

            if ((exceptions != null) && (exceptions.Count > 0))
                throw new Exceptions.CombinedException("Error while moving one or several files, see InnerExceptions array for details.", exceptions.ToArray());

            return files;
        }
        /// <summary>
        /// Sets file attributes for several files at once
        /// </summary>
        /// <param name = "files">The files.</param>
        /// <param name = "attributes">The attributes to be set.</param>
        /// <returns>The changed files</returns>
        /// <example>
        /// 	<code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		files.SetAttributes(FileAttributes.Archive);
        /// 	</code>
        /// </example>
        public static FileInfo[] SetAttributes(this FileInfo[] files, FileAttributes attributes)
        {
            foreach (var file in files) file.Attributes = attributes;
            return files;
        }
        /// <summary>
        /// Appends file attributes for several files at once (additive to any existing attributes)
        /// </summary>
        /// <param name = "files">The files.</param>
        /// <param name = "attributes">The attributes to be set.</param>
        /// <returns>The changed files</returns>
        /// <example>
        /// 	<code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		files.SetAttributesAdditive(FileAttributes.Archive);
        /// 	</code>
        /// </example>
        public static FileInfo[] SetAttributesAdditive(this FileInfo[] files, FileAttributes attributes)
        {
            foreach (var file in files) file.Attributes = (file.Attributes | attributes);
            return files;
        }
    }

    /// <summary>
    /// Extension methods for the DirectoryInfo class
    /// </summary>
    public static class DirectoryInfoExtensions
    {
        /// <summary>
        /// Gets all files in the directory matching one of the several (!) supplied patterns (instead of just one in the regular implementation).
        /// </summary>
        /// <param name = "directory">The directory.</param>
        /// <param name = "patterns">The patterns.</param>
        /// <returns>The matching files</returns>
        /// <remarks>
        /// 	This methods is quite perfect to be used in conjunction with the newly created FileInfo-Array extension methods.
        /// </remarks>
        /// <example>
        /// 	<code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 	</code>
        /// </example>
        public static FileInfo[] GetFiles(this DirectoryInfo directory, params String[] patterns)
        {
            var files = new List<FileInfo>();
            foreach (var pattern in patterns) files.AddRange(directory.GetFiles(pattern));
            return files.ToArray();
        }
        /// <summary>
        /// Searches the provided directory recursively and returns the first file matching the provided pattern.
        /// </summary>
        /// <param name = "directory">The directory.</param>
        /// <param name = "pattern">The pattern.</param>
        /// <returns>The found file</returns>
        /// <example>
        /// 	<code>
        /// 		var directory = new DirectoryInfo(@"c:\");
        /// 		var file = directory.FindFileRecursive("win.ini");
        /// 	</code>
        /// </example>
        public static FileInfo FindFileRecursive(this DirectoryInfo directory, String pattern)
        {
            var files = directory.GetFiles(pattern);
            if (files.Length > 0) return files[0];

            foreach (var subDirectory in directory.GetDirectories())
            {
                var foundFile = subDirectory.FindFileRecursive(pattern);
                if (foundFile != null) return foundFile;
            }
            return null;
        }
        /// <summary>
        /// Searches the provided directory recursively and returns the first file matching to the provided predicate.
        /// </summary>
        /// <param name = "directory">The directory.</param>
        /// <param name = "predicate">The predicate.</param>
        /// <returns>The found file</returns>
        /// <example>
        /// 	<code>
        /// 		var directory = new DirectoryInfo(@"c:\");
        /// 		var file = directory.FindFileRecursive(f => f.Extension == ".ini");
        /// 	</code>
        /// </example>
        public static FileInfo FindFileRecursive(this DirectoryInfo directory, Func<FileInfo, Boolean> predicate)
        {
            foreach (var file in directory.GetFiles())
            {
                if (predicate?.Invoke(file) ?? default(Boolean)) return file;
            }

            foreach (var subDirectory in directory.GetDirectories())
            {
                var foundFile = subDirectory.FindFileRecursive(predicate);
                if (foundFile != null) return foundFile;
            }
            return null;
        }
        /// <summary>
        /// Searches the provided directory recursively and returns the all files matching the provided pattern.
        /// </summary>
        /// <param name = "directory">The directory.</param>
        /// <param name = "pattern">The pattern.</param>
        /// <remarks>
        /// 	This methods is quite perfect to be used in conjunction with the newly created FileInfo-Array extension methods.
        /// </remarks>
        /// <returns>The found files</returns>
        /// <example>
        /// 	<code>
        /// 		var directory = new DirectoryInfo(@"c:\");
        /// 		var files = directory.FindFilesRecursive("*.ini");
        /// 	</code>
        /// </example>
        public static FileInfo[] FindFilesRecursive(this DirectoryInfo directory, String pattern)
        {
            var foundFiles = new List<FileInfo>();
            FindFilesRecursive(directory, pattern, foundFiles);
            return foundFiles.ToArray();
        }
        static void FindFilesRecursive(DirectoryInfo directory, String pattern, List<FileInfo> foundFiles)
        {
            foundFiles.AddRange(directory.GetFiles(pattern));
            directory.GetDirectories().ForEach(d => FindFilesRecursive(d, pattern, foundFiles));
        }
        /// <summary>
        /// Searches the provided directory recursively and returns the all files matching to the provided predicate.
        /// </summary>
        /// <param name = "directory">The directory.</param>
        /// <param name = "predicate">The predicate.</param>
        /// <returns>The found files</returns>
        /// <remarks>
        /// 	This methods is quite perfect to be used in conjunction with the newly created FileInfo-Array extension methods.
        /// </remarks>
        /// <example>
        /// 	<code>
        /// 		var directory = new DirectoryInfo(@"c:\");
        /// 		var files = directory.FindFilesRecursive(f => f.Extension == ".ini");
        /// 	</code>
        /// </example>
        public static FileInfo[] FindFilesRecursive(this DirectoryInfo directory, Func<FileInfo, Boolean> predicate)
        {
            var foundFiles = new List<FileInfo>();
            FindFilesRecursive(directory, predicate, foundFiles);
            return foundFiles.ToArray();
        }
        static void FindFilesRecursive(DirectoryInfo directory, Func<FileInfo, Boolean> predicate, List<FileInfo> foundFiles)
        {
            foundFiles.AddRange(directory.GetFiles().Where(predicate));
            directory.GetDirectories().ForEach(d => FindFilesRecursive(d, predicate, foundFiles));
        }
        /// <summary>
        /// Copies the entire directory to another one
        /// </summary>
        /// <param name="sourceDirectory">The source directory.</param>
        /// <param name="targetDirectoryPath">The target directory path.</param>
        /// <returns></returns>
        public static DirectoryInfo CopyTo(this DirectoryInfo sourceDirectory, String targetDirectoryPath)
        {
            var targetDirectory = new DirectoryInfo(targetDirectoryPath);
            CopyTo(sourceDirectory, targetDirectory);
            return targetDirectory;
        }
        /// <summary>
        /// Copies the entire directory to another one
        /// </summary>
        /// <param name="sourceDirectory">The source directory.</param>
        /// <param name="targetDirectory">The target directory.</param>
        public static void CopyTo(this DirectoryInfo sourceDirectory, DirectoryInfo targetDirectory)
        {
            if (!targetDirectory.Exists) targetDirectory.Create();

            foreach (var childDirectory in sourceDirectory.GetDirectories())
            {
                CopyTo(childDirectory, Path.Combine(targetDirectory.FullName, childDirectory.Name));
            }

            foreach (var file in sourceDirectory.GetFiles())
            {
                file.CopyTo(Path.Combine(targetDirectory.FullName, file.Name));
            }
        }
    }

    /// <summary>
    /// Extension methods for System.Windows.Forms.TabControl
    /// </summary>
    public static class TabControlExtensions
    {
        static readonly Dictionary<System.Windows.Forms.TabControl, Object[]> _hiddenHeaders = new Dictionary<System.Windows.Forms.TabControl, Object[]>();

        /// <summary>
        /// Hide tab page headers
        /// </summary>
        /// <param name="tabControl">Current TabControl</param>
        public static void HideHeaders(this System.Windows.Forms.TabControl tabControl)
        {
            _hiddenHeaders.Add(tabControl, new Object[] { tabControl.Appearance, tabControl.ItemSize, tabControl.SizeMode });

            tabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            tabControl.ItemSize = new System.Drawing.Size(0, 1);
            tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
        }

        /// <summary>
        /// Show tab page headers
        /// </summary>
        /// <param name="tabControl">Current TabControl</param>
        public static void ShowHeaders(this System.Windows.Forms.TabControl tabControl)
        {
            if (!_hiddenHeaders.ContainsKey(tabControl))
                return;

            var opt = _hiddenHeaders[tabControl];
            _hiddenHeaders.Remove(tabControl);

            tabControl.Appearance = (System.Windows.Forms.TabAppearance)opt[0];
            tabControl.ItemSize = (System.Drawing.Size)opt[1];
            tabControl.SizeMode = (System.Windows.Forms.TabSizeMode)opt[2];
        }

        /// <summary>
        /// Check, tab page headers are visible or hidden
        /// </summary>
        /// <param name="tabControl">Current TabControl</param>
        /// <returns>Returns true if visible</returns>
        public static bool IsHeadersVisible(this System.Windows.Forms.TabControl tabControl) => !_hiddenHeaders.ContainsKey(tabControl);

        /// <summary>
        /// Set visibility of tab page headers
        /// </summary>
        /// <param name="tabControl">Current TabControl</param>
        /// <param name="visible">Visibility of tab page headers</param>
        public static void SetHeadersVisible(this System.Windows.Forms.TabControl tabControl, Boolean visible)
        {
            if (visible) ShowHeaders(tabControl);
            else HideHeaders(tabControl);
        }
    }

    /// <summary>
    /// Extension methods for System.Windows.Forms.TabPage
    /// </summary>
    public static class TabPageExtensions
    {
        static readonly Dictionary<System.Windows.Forms.TabPage, Object[]> _hiddenPages = new Dictionary<System.Windows.Forms.TabPage, Object[]>();

        /// <summary>
        /// Set visibility of current TabPage in the parent TabControl.TabPages collection
        /// </summary>
        /// <param name="tabPage">Current TabPage</param>
        /// <param name="visible">Visibility of this tab page</param>
        public static void SetVisible(this System.Windows.Forms.TabPage tabPage, Boolean visible)
        {
            if (visible) tabPage.ShowTabPage();
            else tabPage.HideTabPage();
        }

        /// <summary>
        /// Checks, tab page into the TabControl.TabPages collection or not
        /// </summary>
        /// <param name="tabPage">Current TabPage</param>
        /// <returns>Returns true if visible</returns>
        public static bool IsVisible(this System.Windows.Forms.TabPage tabPage)
        {
            var tabControl = tabPage.Parent as System.Windows.Forms.TabControl;
            return tabControl != null && tabControl.TabPages.Contains(tabPage);
        }

        /// <summary>
        /// Show tab page.
        /// <para>Returns back previously temporarily deleted tab page to the parent TabControl.TabPages collection</para>
        /// </summary>
        /// <param name="tabPage">Current TabPage</param>
        public static void ShowTabPage(this System.Windows.Forms.TabPage tabPage)
        {
            if (!_hiddenPages.ContainsKey(tabPage)) return;

            var opt = _hiddenPages[tabPage];
            var tabControl = (System.Windows.Forms.TabControl)opt[0];
            var index = (Int32)opt[1];

            _hiddenPages.Remove(tabPage);
            tabControl.TabPages.Insert(index, tabPage);
        }

        /// <summary>
        /// Hide tab page.
        /// <para>Temporarily removes the tab page from the parent TabControl.TabPages collection</para>
        /// </summary>
        /// <param name="tabPage">Current TabPage</param>
        public static void HideTabPage(this System.Windows.Forms.TabPage tabPage)
        {
            var tabControl = (System.Windows.Forms.TabControl)tabPage.Parent;

            _hiddenPages.Add(tabPage, new Object[] { tabControl, tabPage.TabIndex });
            tabControl.TabPages.Remove(tabPage);
        }
    }
}
