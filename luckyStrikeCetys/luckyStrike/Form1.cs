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
    public partial class Form1 : Form
    {

        public static string Session;
     
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void signButton_Click(object sender, EventArgs e)
        {
          
            this.Hide();
            createUser crt = new createUser();
            crt.Show();
            usernameTextBox.Text = "";
            passwordTextBox.Text = "";
            
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            Session = usernameTextBox.Text;
            DataBaseOperation.GetUser(usernameTextBox.Text, passwordTextBox.Text);
            usernameTextBox.Text = "";
            passwordTextBox.Text = "";
            this.Hide();

        }

   

        private void passwordTextBox_TextChanged(object sender, EventArgs e)
        {
            passwordTextBox.UseSystemPasswordChar = true;
        }

        private void passwordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Session = usernameTextBox.Text;
                DataBaseOperation.GetUser(usernameTextBox.Text, passwordTextBox.Text);
                usernameTextBox.Text = "";
                passwordTextBox.Text = "";
                this.Hide();
            }
        }
    }
}
