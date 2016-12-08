using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace luckyStrike
{
    public partial class createUser : Form
    {
        public createUser()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.FormClosing += createUser_FormClosing;
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            SqlHelper.DBConnectionInit();
            String commandText = "SELECT Username FROM [User]";
            SqlDataReader reader = SqlHelper.ExecuteReader(commandText, CommandType.Text);

            while(reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (UsernameTextbox.Text == reader.GetString(i))
                    {
                        MessageBox.Show("Username already exists!");
                        UsernameTextbox.Text = "";
                        break;
                    }
                }
              
            }
          

            if (NameTextbox.Text.Length < 3)
            {
             
                MessageBox.Show("Invalid name!!!!");
                passwordTextbox.Text = "";
            }
            else if (LastnameTextbox.Text.Length < 3)
            {
              
                MessageBox.Show("Invalid last name!!!!");
                passwordTextbox.Text = "";
            }
            else if(UsernameTextbox.Text == "")
            {
                
             
                UsernameTextbox.Text = "";
            }
           
           
            else if (passwordTextbox.Text.Length < 3)
            {
             
                MessageBox.Show("Password must contain at least 3 characters!!!!");
                passwordTextbox.Text = "";
            }

            else
            {
                SqlHelper.DBConnectionClose();
                DataBaseOperation.singIn(NameTextbox.Text, LastnameTextbox.Text, UsernameTextbox.Text, passwordTextbox.Text);
                this.Close();
              
            }

            SqlHelper.DBConnectionClose();


        }

        private void createUser_FormClosing(object sender, FormClosingEventArgs e)
        {
           
                Form1 frm = new Form1();
                frm.Show();
            e.Cancel = true;
            this.Hide();

        }

        private void createUser_Load(object sender, EventArgs e)
        {

        }
    }
}