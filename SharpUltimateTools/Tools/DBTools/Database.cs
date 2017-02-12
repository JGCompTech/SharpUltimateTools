using System;
using System.Globalization;

namespace JGCompTech.CSharp.Tools.DBTools
{
    /// <summary>
    /// Holds the info object For the database.
    /// </summary>
    public class Database
    {
        /// <summary>
        /// Holds the path to the database.
        /// </summary>
        public String Path { get; set; }

        /// <summary>
        /// Holds the password to the database.
        /// </summary>
        public String Password { get; set; }

        /// <summary>
        /// Read-only variable that returns the connection string to the database.
        /// </summary>
        public String ConnectionString => String.Format(CultureInfo.CurrentCulture, "Data Source={0};Version=3;Password={1};", Path, Password);
    }
}
