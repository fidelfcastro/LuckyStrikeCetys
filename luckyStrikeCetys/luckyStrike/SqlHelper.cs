using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace luckyStrike
{
    class SqlHelper
    {
        static SingletonDBConnection SingletonDB;

        //TO DO: Add try-catch logic to each method.
        public static Int32 ExecuteNonQuery(String commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand(commandText, SingletonDB.GetDBConnection());
            cmd.CommandTimeout = 15;
            cmd.CommandType = commandType; // There're three command types: StoredProcedure, Text, TableDirect. The TableDirect type is only for OLE DB.
            cmd.Parameters.AddRange(parameters);
            return cmd.ExecuteNonQuery();
        }

        // Set the connection, command, and then execute the command with query and return the reader.
        public static SqlDataReader ExecuteReader(String commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand(commandText, SingletonDB.GetDBConnection());
            cmd.CommandTimeout = 15;
            cmd.CommandType = commandType; // There're three command types: StoredProcedure, Text, TableDirect. The TableDirect type is only for OLE DB.
            cmd.Parameters.AddRange(parameters);
            SqlDataReader reader = cmd.ExecuteReader();

            return reader;

        }

        public static void DBConnectionInit()
        {
            SingletonDB = SingletonDBConnection.getDbInstance();
            //SingletonDB.GetDBConnection();
        }

        public static void DBConnectionClose()
        {

           SingletonDB.CloseDBConnection();
        }
    }
}
