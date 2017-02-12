using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGCompTech.CSharp.Tools
{
    /// <summary>
    /// Returns data information
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// Returns current timestamp
        /// </summary>
        public static String CurrentShortTimeStamp => DateTime.Now.ToString("MM/dd/yy HH:mm:ss tt", CultureInfo.CurrentCulture);

        /// <summary>
        /// Returns current timestamp extended
        /// </summary>
        public static String CurrentFullTimeStamp => DateTime.Now.ToString("ddd MMMM dd, yyyy hh:mm:ss tt", CultureInfo.CurrentCulture);
    }
}
