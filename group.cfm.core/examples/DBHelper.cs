using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace group.cfm.core.examples
{
    public static class DBHelper
    {
        public const string Scheme = "CFM";
        private const string _ConnectionStringName = "CFM";
        private static SqlConnection _Connection = null;

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <value>
        /// The connection.
        /// </value>
        private static SqlConnection Connection
        {
            get
            {
                _Connection = new SqlConnection();
                    //new SqlConnection(ConfigurationManager.ConnectionStrings[_ConnectionStringName].ConnectionString);
                _Connection.Open();

                return _Connection;
            }
        }

        /// <summary>
        /// Gets the SQL command.
        /// </summary>
        /// <value>
        /// The SQL command.
        /// </value>
        public static SqlCommand Command
        {
            get
            {
                var cmd = Connection.CreateCommand();
                cmd.CommandTimeout = cmd.Connection.ConnectionTimeout;

                return cmd;
            }
        }

        /// <summary>
        /// Parses the value of column, specified by name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader">The reader.</param>
        /// <param name="column">The column.</param>
        /// <returns></returns>
        public static T ParseValue<T>(this SqlDataReader reader, string column)
        {
            var colid = reader.GetOrdinal(column);

            return ParseValue<T>(reader, colid);
        }

        /// <summary>
        /// Parses the value of column, specified by index
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader">The reader.</param>
        /// <param name="column">The column.</param>
        /// <returns></returns>
        public static T ParseValue<T>(this SqlDataReader reader, Int32 column)
        {
            T result = default(T);
            if ((column != -1) && !reader.IsDBNull(column))
            {
                result = (T)reader.GetValue(column);
            }
            else if (typeof(T) == typeof(String))
            {
                return (T)((Object)String.Empty);
            }

            return result;
        }

    }
}
