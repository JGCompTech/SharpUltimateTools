using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using CultureInfo = System.Globalization.CultureInfo;

// Allows connections to databases. Requires System.Data.SQLite.
namespace Microsoft.CSharp.Tools.DBTools
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

    /// <summary>
    /// Contains the classes to communicate with the database. Requires System.Data.SQLite.
    /// </summary>
    public static class SQLite
    {
        internal static SQLiteDataAdapter da;

        /// <summary>
        /// Sets a setting in the Settings Table. Requires System.Data.SQLite.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        /// <param name="ds"></param>
        public static void SetSettings(String name, String value, Database db, DataSet ds)
        {
            name.ExceptionIfNullOrEmpty("The specified name string cannot be null!", nameof(name));
            value.ExceptionIfNullOrEmpty("The specified value string cannot be null!", nameof(value));
            db.ExceptionIfNull("The specified database object cannot be null!", nameof(db));
            ds.ExceptionIfNull("The specified dataset object cannot be null!", nameof(ds));
            DataRow dRow;
            LoadDBTable("Settings", db, ds);

            var inc = 0;
            var maxrows = ds.Tables["Settings"].Rows.Count;
            var Set = false;
            while (inc != maxrows)
            {
                dRow = ds.Tables["Settings"].Rows[inc];
                if (name.ToLower(CultureInfo.CurrentCulture) == dRow.ItemArray.GetValue(1).ToString().ToLower(CultureInfo.CurrentCulture))
                {
                    dRow[2] = value;
                    Set = true;
                }
                inc++;
            }

            if (!Set)
            {
                dRow = ds.Tables["Settings"].NewRow();
                dRow[1] = name;
                dRow[2] = value;

                ds.Tables["Settings"].Rows.Add(dRow);
            }

            UpdateDBTable("Settings", db, ds);
        }

        /// <summary>
        /// Gets a setting from the Settings Table. Requires System.Data.SQLite.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="db"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static String GetSettings(String name, Database db, DataSet ds)
        {
            name.ExceptionIfNullOrEmpty("The specified name string cannot be null!", nameof(name));
            db.ExceptionIfNull("The specified database object cannot be null!", nameof(db));
            ds.ExceptionIfNull("The specified dataset object cannot be null!", nameof(ds));
            DataRow dRow;
            LoadDBTable("Settings", db, ds);

            var inc = 0;
            var maxrows = ds.Tables["Settings"].Rows.Count;
            while (inc != maxrows)
            {
                dRow = ds.Tables["Settings"].Rows[inc];
                if (name.ToLower(CultureInfo.CurrentCulture) == dRow.ItemArray.GetValue(1).ToString().ToLower(CultureInfo.CurrentCulture))
                {
                    return dRow.ItemArray.GetValue(2).ToString().ToLower(CultureInfo.CurrentCulture);
                }
                inc++;
            }
            return null;
        }

        /// <summary>
        /// Loads the specified table. Requires System.Data.SQLite.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="db"></param>
        /// <param name="ds"></param>
        public static void LoadDBTable(String table, Database db, DataSet ds)
        {
            table.ExceptionIfNullOrEmpty("The specified table string cannot be null!", nameof(table));
            db.ExceptionIfNull("The specified database object cannot be null!", nameof(db));
            ds.ExceptionIfNull("The specified dataset object cannot be null!", nameof(ds));
            if (File.Exists(db.Path))
            {
                var cs = db.ConnectionString;
                using (SQLiteConnection con = new SQLiteConnection(cs, true))
                {
                    con.Open();
                    var sql = "SELECT * FROM " + table;
                    da = new SQLiteDataAdapter(sql, con);
                    if (ds.Tables.Contains(table))
                    {
                        if (ds.Tables[table].Rows.Count > 0)
                        {
                            ds.Tables[table].Rows.Clear();
                        }
                    }
                    da.Fill(ds, table);
                }
            }
            else { throw new Exceptions.DatabaseFileNotFoundException("Database File " + db.Path + " Not Found!"); }
        }

        /// <summary>
        /// Saves the specified dataset to the specified table. Requires System.Data.SQLite.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="db"></param>
        /// <param name="ds"></param>
        public static void UpdateDBTable(String table, Database db, DataSet ds)
        {
            table.ExceptionIfNullOrEmpty("The specified table string cannot be null!", nameof(table));
            db.ExceptionIfNull("The specified database object cannot be null!", nameof(db));
            ds.ExceptionIfNull("The specified dataset object cannot be null!", nameof(ds));
            var cs = db.ConnectionString;
            using (SQLiteConnection con = new SQLiteConnection(cs, true))
            {
                con.Open();
                var sql = "SELECT * FROM " + table;
                da = new SQLiteDataAdapter(sql, con);
                using (SQLiteCommandBuilder cb = new SQLiteCommandBuilder(da))
                {
                    cb.DataAdapter.Update(ds.Tables[table]);
                }
            }
        }
    }
}
