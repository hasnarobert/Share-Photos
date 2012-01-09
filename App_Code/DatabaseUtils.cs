using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace SharePhotos
{
    namespace Database
    {
        public class DatabaseUtils
        {
            public static void safeOpen(SqlConnection connection)
            {
                if (connection != null) {
                    if (connection.State == System.Data.ConnectionState.Closed) {
                        connection.Open();
                    }
                    if (connection.State == System.Data.ConnectionState.Broken) {
                        throw new Exception("Can not open a broken connection");
                    }
                }
            }

            public static void safeClose(SqlConnection connection) {
                if (connection != null) {
                    if (connection.State == System.Data.ConnectionState.Broken) {
                        throw new Exception("Can not close a broken connection");
                    }
                    if (connection.State != System.Data.ConnectionState.Closed) {
                        connection.Close();
                    }
                }
            }

            public static void safeClose(SqlDataReader reader) {
                if (reader != null) {
                    if (reader.IsClosed == false) {
                        reader.Close();
                    }
                }
            }
        }
    }
}