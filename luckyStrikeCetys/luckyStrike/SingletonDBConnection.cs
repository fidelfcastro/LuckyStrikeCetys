using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace luckyStrike
{
    class SingletonDBConnection
    {

        private static SingletonDBConnection dbInstance;
       private readonly SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-N7C417RO\SQLSERVER;Initial Catalog=luckystrike;Integrated Security=True");
       // private readonly SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-MPH8CD6;Initial Catalog=luckystrike;Integrated Security=True");
        private SingletonDBConnection()
        {
        }

        public static SingletonDBConnection getDbInstance()
        {
            if (dbInstance == null)
            {
                dbInstance = new SingletonDBConnection();
            }
            return dbInstance;
        }

        public SqlConnection GetDBConnection()
        {
            try
            {
                conn.Open();
            }
            catch (SqlException e)
            {
                //TO DO: logfile

            }
            finally
            {
                //TO DO: logfile
            }
            return conn;
        }
        public void CloseDBConnection()
        {
            try
            {
                conn.Close();
            }
            catch (SqlException e)
            {
                //TO DO: logfile
            }
            finally
            {
                //TO DO: logfile
            }
        }

    }

}
