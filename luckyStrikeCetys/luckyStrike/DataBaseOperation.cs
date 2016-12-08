using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace luckyStrike
{
    class DataBaseOperation
    {

        public static void GetUser(string user, string password)
        {

            SqlHelper.DBConnectionInit();
            String commandText = "dbo.sp_loginValidation";

            SqlParameter parameterUser = new SqlParameter("@UserName", SqlDbType.NVarChar);
            SqlParameter parameterPassword = new SqlParameter("@Password", SqlDbType.NVarChar);

            parameterUser.Value = user;
            parameterPassword.Value = password;




            SqlDataReader reader = SqlHelper.ExecuteReader(commandText, CommandType.StoredProcedure, parameterUser, parameterPassword);
            if (reader.HasRows && reader.Read())
            {

                
                Game game = new Game();
                game.Show();




            }
            else
            {
                MessageBox.Show("Username or password doesn't exist");
                Form1 frm = new Form1();
                frm.Show();


            }
            SqlHelper.DBConnectionClose();
        }

        public static void singIn(string FirstName, string Lastname, string userName, string password)
        {
            SqlHelper.DBConnectionInit();
            String commandText = "dbo.sp_CreateUser"; 
            SqlParameter parameterFirstName = new SqlParameter("@Name", SqlDbType.NVarChar);
            SqlParameter parameterLastName = new SqlParameter("@LastName", SqlDbType.NVarChar);
            SqlParameter parameterUser = new SqlParameter("@UserName", SqlDbType.NVarChar);
            SqlParameter parameterPassword = new SqlParameter("@Password", SqlDbType.NVarChar);
            parameterFirstName.Value = FirstName;
            parameterLastName.Value = Lastname;
            parameterUser.Value = userName;
            parameterPassword.Value = password;

            
            SqlDataReader reader = SqlHelper.ExecuteReader(commandText, CommandType.StoredProcedure, parameterFirstName, parameterLastName, parameterUser, parameterPassword);
          
            SqlHelper.DBConnectionClose();
        }



        public static void signOut()
        {

            SqlHelper.DBConnectionInit();
            String commandText = "dbo.sp_logout";
            SqlDataReader reader = SqlHelper.ExecuteReader(commandText, CommandType.StoredProcedure);
            SqlHelper.DBConnectionClose();
        }
        public static void stats(int tkWin, int tkLose, int tkCurrent)
        {
            SqlHelper.DBConnectionInit();
            String commandText = "dbo.sp_statsUpdate";
            SqlParameter parameterTkWin = new SqlParameter("@tkWin", SqlDbType.Int);
            SqlParameter parameterTkLose = new SqlParameter("@tkLose", SqlDbType.Int);
            SqlParameter parameterTkCurrent = new SqlParameter("@tkCurrent", SqlDbType.Int);
            parameterTkWin.Value = tkWin;
            parameterTkLose.Value = tkLose;
            parameterTkCurrent.Value = tkCurrent;

            SqlDataReader reader = SqlHelper.ExecuteReader(commandText, CommandType.StoredProcedure, parameterTkWin, parameterTkLose, parameterTkCurrent);
            SqlHelper.DBConnectionClose();
        }



        public static string view_totalStats()
        {
            SqlHelper.DBConnectionInit();
            String commandText = "SELECT * FROM vw_TotalStats";
            SqlDataReader reader = SqlHelper.ExecuteReader(commandText, CommandType.Text);

            SqlHelper.DBConnectionClose();
            return commandText;
        }

        public static void usernameUpdate(string username)
        {
            SqlHelper.DBConnectionInit();
            String commandText = "dbo.sp_editUsername";
            SqlParameter parameterUsername = new SqlParameter("@Username", SqlDbType.NChar);
            parameterUsername.Value = username;

            SqlDataReader reader = SqlHelper.ExecuteReader(commandText, CommandType.StoredProcedure, parameterUsername);
            SqlHelper.DBConnectionClose();
        }



    }
}
