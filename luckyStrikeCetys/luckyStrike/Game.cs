using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;
using luckyStrike.Properties;
using System.Media;

namespace luckyStrike
{
    
    public partial class Game : Form
    {
        //recursos para botones con imagenes.
        Image spin = Resources.rule2;
        Image exit = Resources.exit;
        Image estadisticas = Resources.statistics;
        Image rules = Resources.teacher3;
        Image user = Resources.user2;
        Image stop = Resources.stop3;
        Image title_rules = Resources.TtlteRules1;
        Image title_statistcs = Resources.TitleStatistics1;
        Image title_profile = Resources.TitleProfile1;
       
        static List<Imagenes> List_Imagenes_left = new List<Imagenes>();
        static List<Imagenes> List_Imagenes_middle = new List<Imagenes>();
        static List<Imagenes> List_Imagenes_right = new List<Imagenes>();
        static imagen1 imagen1 = new imagen1();
        static imagen2 imagen2 = new imagen2();
        static imagen3 imagen3 = new imagen3();
        static imagen4 imagen4 = new imagen4();
        static imagen5 imagen5 = new imagen5();
        static imagen6 imagen6 = new imagen6();
        static imagen7 imagen7 = new imagen7();
        bool Thread_BLeft, Thread_BMiddle, Thread_BRight, pointed;
        int Posicion_Left = 0, Posicion_Middle = 0, Posicion_Right = 0, ganancia = 0,Total_Tockets = 0, count = 0, triunfos = 0, n;
        static Imagenes[,] combinaciones = new Imagenes[7, 3];
        static Imagenes[,] Ganadora = new Imagenes[3, 3];
        static int lo = 0;
        static int hi = 10;
        
        public Game()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
           
            this.FormClosing += Game_FormClosing;
            SelectImages();
            //picbox transparency
            pictureBox1.BackColor = Color.Transparent;
            pictureBox2.BackColor = Color.Transparent;
            pictureBox3.BackColor = Color.Transparent;
            pictureBox4.BackColor = Color.Transparent;
            pictureBox5.BackColor = Color.Transparent;
            pictureBox6.BackColor = Color.Transparent;
            pictureBox7.BackColor = Color.Transparent;
            pictureBox8.BackColor = Color.Transparent;
            pictureBox9.BackColor = Color.Transparent;
            pic_estadisticas.BackColor = Color.Transparent;
            pic_estadisticas.SendToBack();
            pic_profile.SendToBack();
            Main_header.BackColor = Color.Transparent;
            howTowin.BackColor = Color.Transparent;
            pic_profile.BackColor = Color.Transparent;
            ButLogout.Image = exit;
            ButLogout.BackColor = Color.Transparent;
            ButStop.Image = stop;
            ButStop.BackColor = Color.Transparent;
            ButEstadistica.Image = estadisticas;
            ButEstadistica.BackColor = Color.Transparent;
            ButRules.Image = rules;
            ButRules.BackColor = Color.Transparent;
            ButSpin.Image = spin;
            ButSpin.BackColor = Color.Transparent;
            ButUser.Image = user;
            ButUser.BackColor = Color.Transparent;
      

      
           
            
        }

      

        private void Game_Load(object sender, EventArgs e)
        {
           
            label_user.Text = Form1.Session;


            SqlHelper.DBConnectionClose();
            String commandText = "SELECT * FROM vw_currentTokens";


            SqlDataReader reader = SqlHelper.ExecuteReader(commandText, CommandType.Text);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                  
                    Tokenslbl.Text = Convert.ToString(reader.GetValue(0));
                    Total_Tockets = Convert.ToInt32(reader.GetValue(0));

                }
            }
            else
            {
                Tokenslbl.Text = "100";
                Total_Tockets = 100;
            }


        

        }


     

        private void ButLogout_Click(object sender, EventArgs e)
        {
            DataBaseOperation.signOut();
            this.Close();
        }


        private void ButUser_Click(object sender, EventArgs e)
        {
            Main_header.Visible = true;
            Main_header.Image = title_profile;
            pic_profile.Visible = false;
            pic_estadisticas.Visible = false;
            pic_profile.Visible = true;

            SqlHelper.DBConnectionInit();
            String commandText = "SELECT * FROM vw_getUser";

            howTowin.Visible = false;
          
           

            pictureBoxV.Visible = false;
          
            pictureBoxLado.Visible = false;
            pictureBoxHorizontal.Visible = false;
            pictureBoxV2.Visible = false;

            SqlDataReader reader = SqlHelper.ExecuteReader(commandText, CommandType.Text);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    labelHeader1Text.Visible = true;
                    labelHeader2Text.Visible = true;
                    labelHeader3Text.Visible = true;
                    labelHeader4Text.Visible = false;
                    labelHeader5Text.Visible = false;
                    labelHeader6Text.Visible = false;

                    labelHeader1Text.Text = Convert.ToString(reader.GetValue(0));
                    labelHeader2Text.Text = Convert.ToString(reader.GetValue(1));
                    labelHeader3Text.Text = Convert.ToString(reader.GetValue(2));

                }
            }

            SqlHelper.DBConnectionClose();

        }

        public static void SelectImages()
        {
            Random r = new Random();
            for (int i = 0; i < 21; i++)
            {
                int n = r.Next(0, 7);
                List_Imagenes_left.Add(LoadImages(n));
            }
            for (int i = 0; i < 21; i++)
            {
                int n = r.Next(0, 7);
                List_Imagenes_middle.Add(LoadImages(n));
            }
            for (int i = 0; i < 21; i++)
            {
                int n = r.Next(0, 7);
                List_Imagenes_right.Add(LoadImages(n));
            }
        }

        private void ButSpin_Click(object sender, EventArgs e)
        {
            Random_Lehmer();
            ButStop.Enabled = true;
            ButSpin.Enabled = false;
            Total_Tockets -= 5;
          
            Thread point = new Thread(labelpoint);
            count++;

            pointed = true;
            Thread_BLeft = true;
            Thread_BMiddle = true;
            Thread_BRight = true;
            Thread leftThread = new Thread(Movementleft);
            Thread rightThread = new Thread(MovementRight);
            Thread middleThread = new Thread(MovementMiddle);
           
            leftThread.IsBackground = true;
            rightThread.IsBackground = true;
            middleThread.IsBackground = true;
            point.IsBackground = true;

            point.Start();
            leftThread.Start();
            middleThread.Start();
            rightThread.Start();

           
        }

        internal static Imagenes LoadImages(int ImagesId)
        {
            Imagenes imagengeneral;
            if (ImagesId == 1)
            {
                imagengeneral = imagen1;
            }
            else if (ImagesId == 2)
            {
                imagengeneral = imagen2;
            }
            else if (ImagesId == 3)
            {
                imagengeneral = imagen3;
            }
            else if (ImagesId == 4)
            {
                imagengeneral = imagen4;
            }
            else if (ImagesId == 5)
            {
                imagengeneral = imagen5;
            }
            else if (ImagesId == 6)
            {
                imagengeneral = imagen6;
            }
            else
            {
                imagengeneral = imagen7;
            }
            return imagengeneral;
        }

        public void labelpoint()
        {
            while (true)
            {
                if (this.IsDisposed)
                {

                }
                else
                {
                    if (pointed == true)
                    {

                        this.Tokenslbl.BeginInvoke((MethodInvoker)delegate ()
                        {
                            if (this.IsDisposed)
                            {

                            }
                            else
                            {
                                this.Tokenslbl.Text = Total_Tockets.ToString();
                            }
                        }
                        );
                        Thread.Sleep(200);
                    }
                }


            }
        }

        static void Random_Lehmer()
        {
            int[] counts = new int[10];
            Random r = new Random();
            int seed1 = r.Next(1, 100);
            LehmerRng lehmer = new LehmerRng(seed1);
            for (int i = 0; i < 100; ++i)
            {
                double x = lehmer.Next();

                int ri = (int)((hi - lo) * x + lo);
                ++counts[ri];
            }
            int k = 0;
            for (int j = 0; j < 3; j++)
            {

                for (int i = 0; i < 3; i++)
                {
                    k++;
                    Ganadora[j, i] = LoadImages(counts[k] / 2);

                }
            }

        }
        private void ButStop_MouseEnter(object sender, EventArgs e)
        {
            int stop_width = stop.Width + ((stop.Width * 8) / 100);
            int stop_height = stop.Height + ((stop.Height * 8) / 100);
            Bitmap stop_1 = new Bitmap(stop_width, stop_height);
            Graphics g = Graphics.FromImage(stop_1);
            g.DrawImage(stop, new Rectangle(Point.Empty, stop_1.Size));
            ButStop.Image = stop_1;
        }

        private void ButStop_MouseLeave(object sender, EventArgs e)
        {
            ButStop.Image = stop;
        }

        private void ButStop_Click(object sender, EventArgs e)
        {
            ButStop.Enabled = false;
            ButSpin.Enabled = true;
            Thread stopbutton = new Thread(StopGame);
            stopbutton.Start();
            stopbutton.IsBackground = true;
            
        }

        private void Game_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown
                || e.CloseReason == CloseReason.ApplicationExitCall
                || e.CloseReason == CloseReason.TaskManagerClosing
                || e.CloseReason == CloseReason.UserClosing
                || e.CloseReason == CloseReason.MdiFormClosing
                || e.CloseReason == CloseReason.FormOwnerClosing)
            {
                DataBaseOperation.signOut();
            }
            e.Cancel = true;
          
            this.Hide();
            Form1 frm = new Form1();
            frm.Show();
            DataBaseOperation.stats(triunfos, count *5, Convert.ToInt32(Tokenslbl.Text));
            
            triunfos = 0;
        }


        public void Movementleft()
        {
            int UpPosLeft, DownPosLeft;
            while (Thread_BLeft == true)
            {
                for (int i = Posicion_Left; i < List_Imagenes_left.Count; i++)
                {
                    UpPosLeft = i - 1;
                    DownPosLeft = i + 1;
                    if (i == 0)
                    {
                        UpPosLeft = List_Imagenes_left.Count - 1;
                    }
                    if (i == List_Imagenes_left.Count - 1)
                    {
                        DownPosLeft = 0;
                    }
                    pictureBox1.ImageLocation = List_Imagenes_left[DownPosLeft]._path;
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    Ganadora[0, 0] = List_Imagenes_left[DownPosLeft];
                    pictureBox2.ImageLocation = List_Imagenes_left[i]._path;
                    pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                    Ganadora[1, 0] = List_Imagenes_left[i];
                    pictureBox3.ImageLocation = List_Imagenes_left[UpPosLeft]._path;
                    pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                    Ganadora[2, 0] = List_Imagenes_left[UpPosLeft];
                    Thread.Sleep(200);
                    if (Thread_BLeft == false)
                    {
                        if (i == List_Imagenes_left.Count - 1)
                        {
                            Posicion_Left = 0;
                        }
                        else
                        {
                            Posicion_Left = i;
                        }
                        pictureBox1.ImageLocation = Ganadora[0, 0]._path;
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox2.ImageLocation = Ganadora[1, 0]._path;
                        pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox3.ImageLocation = Ganadora[2, 0]._path;
                        pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                        break;
                    }
                }
                if (Thread_BLeft == true)
                {
                    Posicion_Left = 0;
                }
            }

        }
        public void MovementMiddle()
        {
            int UpPosMiddle, DownPosMiddle;
            while (Thread_BMiddle == true)
            {
                for (int i = Posicion_Middle; i < List_Imagenes_middle.Count; i++)
                {
                    UpPosMiddle = i - 1;
                    DownPosMiddle = i + 1;
                    if (i == 0)
                    {
                        UpPosMiddle = List_Imagenes_middle.Count - 1;
                    }
                    if (i == List_Imagenes_middle.Count - 1)
                    {
                        DownPosMiddle = 0;
                    }
                    pictureBox4.ImageLocation = List_Imagenes_middle[DownPosMiddle]._path;
                    pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
                    Ganadora[0, 1] = List_Imagenes_middle[DownPosMiddle];
                    pictureBox5.ImageLocation = List_Imagenes_middle[i]._path;
                    pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
                    Ganadora[1, 1] = List_Imagenes_middle[i];
                    pictureBox6.ImageLocation = List_Imagenes_middle[UpPosMiddle]._path;
                    pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
                    Ganadora[2, 1] = List_Imagenes_middle[UpPosMiddle];
                    Thread.Sleep(200);
                    if (Thread_BMiddle == false)
                    {
                        if (i == List_Imagenes_middle.Count - 1)
                        {
                            Posicion_Middle = 0;
                        }
                        else
                        {
                            Posicion_Middle = i;
                        }
                        pictureBox4.ImageLocation = Ganadora[0, 1]._path;
                        pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox5.ImageLocation = Ganadora[1, 1]._path;
                        pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox6.ImageLocation = Ganadora[2, 1]._path;
                        pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
                        break;
                    }
                }
                if (Thread_BMiddle == true)
                {
                    Posicion_Middle = 0;
                }
            }

        }
        public void MovementRight()
        {
            int UpPosRight, DownPosRight;
            while (Thread_BRight == true)
            {
                for (int i = Posicion_Right; i < List_Imagenes_right.Count; i++)
                {
                    UpPosRight = i - 1;
                    DownPosRight = i + 1;
                    if (i == 0)
                    {
                        UpPosRight = List_Imagenes_middle.Count - 1;
                    }
                    if (i == List_Imagenes_middle.Count - 1)
                    {
                        DownPosRight = 0;
                    }
                    pictureBox7.ImageLocation = List_Imagenes_right[DownPosRight]._path;
                    pictureBox7.SizeMode = PictureBoxSizeMode.StretchImage;
                    Ganadora[0, 2] = List_Imagenes_right[DownPosRight];
                    pictureBox8.ImageLocation = List_Imagenes_right[i]._path;
                    pictureBox8.SizeMode = PictureBoxSizeMode.StretchImage;
                    Ganadora[1, 2] = List_Imagenes_right[i];
                    pictureBox9.ImageLocation = List_Imagenes_right[UpPosRight]._path;
                    pictureBox9.SizeMode = PictureBoxSizeMode.StretchImage;
                    Ganadora[2, 2] = List_Imagenes_right[UpPosRight];
                    Thread.Sleep(200);
                    if (Thread_BRight == false)
                    {
                        if (i == List_Imagenes_right.Count - 1)
                        {
                            Posicion_Right = 0;
                        }
                        else
                        {
                            Posicion_Right = i;
                        }
                        pictureBox7.ImageLocation = Ganadora[0, 2]._path;
                        pictureBox7.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox8.ImageLocation = Ganadora[1, 2]._path;
                        pictureBox8.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox9.ImageLocation = Ganadora[2, 2]._path;
                        pictureBox9.SizeMode = PictureBoxSizeMode.StretchImage;
                        break;
                    }
                }
                if (Thread_BRight == true)
                {
                    Posicion_Right = 0;
                }
            }



        }
        public void StopGame()
        {
            Thread_BLeft = false;
            Thread.Sleep(300);
            Thread_BMiddle = false;
            Thread.Sleep(300);
            Thread_BRight = false;
            Thread.Sleep(300);
            Check();
         }
      
   
        public void Check()
        {
            Generated_Winning();
            ganancia = 0;
            for (int k = 0; k < 7; k++)
            {

                if (combinaciones[k, 0] == combinaciones[k, 1] && combinaciones[k, 0] == combinaciones[k, 2] && combinaciones[k, 1] == combinaciones[k, 2])
                {
                    if (k < 3)
                    {
                        
                        MessageBox.Show("Ganaste " + combinaciones[k, 0]._point + " Puntos");
                        ganancia += combinaciones[k, 0]._point;
                    }
                    else if (k > 2 && k < 5)
                    {
                        
                        MessageBox.Show("Ganaste " + combinaciones[k, 0]._point * 1.2 + " Puntos");
                        ganancia += Convert.ToInt32(combinaciones[k, 0]._point*1.2);
                    }
                    else if (k > 4)
                    {
                        
                        MessageBox.Show("Ganaste " + combinaciones[k, 0]._point * 1.1 + " Puntos");
                        ganancia += Convert.ToInt32(combinaciones[k, 0]._point*1.1);
                    }
                   

                }

                
            }
           
            Total_Tockets= Total_Tockets+ganancia;
            triunfos = triunfos + ganancia;

            ganancia = 0;

        }
        public void Generated_Winning()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        combinaciones[0, 0] = Ganadora[i, j];
                        combinaciones[3, 0] = Ganadora[i, j];
                        combinaciones[5, 0] = Ganadora[i, j];
                    }
                    else if (i == 0 && j == 1)
                    {
                        combinaciones[0, 1] = Ganadora[i, j];
                        combinaciones[6, 0] = Ganadora[i, j];
                    }
                    else if (i == 0 && j == 2)
                    {
                        combinaciones[0, 2] = Ganadora[i, j];
                        combinaciones[4, 0] = Ganadora[i, j];
                        combinaciones[5, 1] = Ganadora[i, j];
                    }
                    else if (i == 1 && j == 0)
                    {
                        combinaciones[1, 0] = Ganadora[i, j];
                    }
                    else if (i == 1 && j == 1)
                    {
                        combinaciones[1, 1] = Ganadora[i, j];
                        combinaciones[3, 1] = Ganadora[i, j];
                        combinaciones[4, 1] = Ganadora[i, j];
                    }
                    else if (i == 1 && j == 2)
                    {
                        combinaciones[1, 2] = Ganadora[i, j];
                    }
                    else if (i == 2 && j == 0)
                    {
                        combinaciones[2, 0] = Ganadora[i, j];
                        combinaciones[4, 2] = Ganadora[i, j];
                        combinaciones[6, 1] = Ganadora[i, j];
                    }
                    else if (i == 2 && j == 1)
                    {
                        combinaciones[2, 1] = Ganadora[i, j];
                        combinaciones[5, 2] = Ganadora[i, j];
                    }
                    else if (i == 2 && j == 2)
                    {
                        combinaciones[2, 2] = Ganadora[i, j];
                        combinaciones[3, 2] = Ganadora[i, j];
                        combinaciones[6, 2] = Ganadora[i, j];
                    }
                }
            }
        }

        private void ButRules_Click(object sender, EventArgs e)
        {
            Main_header.Visible = true;
            Main_header.Image = title_rules;
            pic_estadisticas.Visible = false;
            howTowin.Visible = true;
            pic_profile.Visible = false;



            pictureBoxV.Visible = true;
        
            pictureBoxLado.Visible = true;
            pictureBoxHorizontal.Visible = true;
            pictureBoxV2.Visible = true;

           
          

            labelHeader1Text.Visible = false;
            labelHeader2Text.Visible = false;
            labelHeader3Text.Visible = false;

            labelHeader4Text.Visible = false;
            labelHeader5Text.Visible = false;
            labelHeader6Text.Visible = false;

        }

        private void ButEstadistica_Click(object sender, EventArgs e)
        {
            Main_header.Visible = true;
            Main_header.Image = title_statistcs;
            pic_estadisticas.Visible = true;
            howTowin.Visible = false;
            pic_profile.Visible = false;






            pictureBoxV.Visible = false;
            pictureBoxLado.Visible = false;
            pictureBoxHorizontal.Visible = false;
            pictureBoxV2.Visible = false;

            SqlHelper.DBConnectionInit();
            String commandText = "SELECT PlaytimeTotal, TokenWinsTotal, TokenloseTotal FROM vw_TotalStats u INNER JOIN [User] s ON u.UserId = s.UserId where active = 1";
            String commandText2 = "SELECT * FROM vw_lastSessionStats ";


            labelHeader1Text.Visible = false;
            labelHeader2Text.Visible = false;
            labelHeader3Text.Visible = false;


            SqlDataReader reader2 = SqlHelper.ExecuteReader(commandText2, CommandType.Text);
          
            if (reader2.HasRows)
            {
                while (reader2.Read())
                {
                    labelHeader1Text.Visible = true;
                    labelHeader2Text.Visible = true;
                    labelHeader3Text.Visible = true;

                    labelHeader1Text.Text = Convert.ToString(reader2.GetValue(0));
                    labelHeader2Text.Text = Convert.ToString(reader2.GetValue(1));
                    labelHeader3Text.Text = Convert.ToString(reader2.GetValue(2));

                }
            }
            SqlHelper.DBConnectionClose();

            SqlHelper.DBConnectionInit();
            SqlDataReader reader = SqlHelper.ExecuteReader(commandText, CommandType.Text);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    labelHeader4Text.Visible = true;
                    labelHeader5Text.Visible = true;
                    labelHeader6Text.Visible = true;

                    labelHeader4Text.Text = Convert.ToString(reader.GetValue(0));
                    labelHeader5Text.Text = Convert.ToString(reader.GetValue(1));
                    labelHeader6Text.Text = Convert.ToString(reader.GetValue(2));

                }
            }
            SqlHelper.DBConnectionClose();



        }

        private void pic_profile_Click(object sender, EventArgs e)
        {

        }


        private void Tokenslbl_Click(object sender, EventArgs e)
        {

        }





        //metodos de efecto de botones
        private void ButSpin_MouseLeave(object sender, EventArgs e)
        {
            ButSpin.Image = spin;
        }

        private void ButUser_MouseLeave(object sender, EventArgs e)
        {
            ButUser.Image = user;
        }

        private void ButRules_MouseLeave(object sender, EventArgs e)
        {

            ButRules.Image = rules;
        }

        private void ButEstadistica_MouseLeave(object sender, EventArgs e)
        {
            ButEstadistica.Image = estadisticas;
        }

        private void ButEstadistica_MouseEnter(object sender, EventArgs e)
        {
            int estadisticas_width = estadisticas.Width + ((estadisticas.Width * 10) / 100);
            int estadisticas_height = estadisticas.Height + ((estadisticas.Height * 10) / 100);
            Bitmap estadisticas_1 = new Bitmap(estadisticas_width, estadisticas_height);
            Graphics g = Graphics.FromImage(estadisticas_1);
            g.DrawImage(estadisticas, new Rectangle(Point.Empty, estadisticas_1.Size));
            ButEstadistica.Image = estadisticas_1;
        }

        private void ButRules_MouseEnter(object sender, EventArgs e)
        {
            int rules_width = rules.Width + ((rules.Width * 10) / 100);
            int rules_height = rules.Height + ((rules.Height * 10) / 100);
            Bitmap rules_1 = new Bitmap(rules_width, rules_height);
            Graphics g = Graphics.FromImage(rules_1);
            g.DrawImage(rules, new Rectangle(Point.Empty, rules_1.Size));
            ButRules.Image = rules_1;
        }

        private void ButUser_MouseEnter(object sender, EventArgs e)
        {
            int user_width = user.Width + ((user.Width * 10) / 100);
            int user_height = user.Height + ((user.Height * 10) / 100);
            Bitmap user_1 = new Bitmap(user_width, user_height);
            Graphics g = Graphics.FromImage(user_1);
            g.DrawImage(user, new Rectangle(Point.Empty, user_1.Size));
            ButUser.Image = user_1;
        }

        private void ButSpin_MouseEnter(object sender, EventArgs e)
        {
            int spin_width = spin.Width + ((spin.Width * 10) / 100);
            int spin_height = spin.Height + ((spin.Height * 10) / 100);
            Bitmap spin_1 = new Bitmap(spin_width, spin_height);
            Graphics g = Graphics.FromImage(spin_1);
            g.DrawImage(spin, new Rectangle(Point.Empty, spin_1.Size));
            ButSpin.Image = spin_1;
        }

        private void ButLogout_MouseEnter(object sender, EventArgs e)
        {
            int exit_width = exit.Width + ((exit.Width * 10) / 100);
            int exit_height = exit.Height + ((exit.Height * 10) / 100);
            Bitmap exit_1 = new Bitmap(exit_width, exit_height);
            Graphics g = Graphics.FromImage(exit_1);
            g.DrawImage(exit, new Rectangle(Point.Empty, exit_1.Size));
            ButLogout.Image = exit_1;
        }

        private void ButLogout_MouseLeave(object sender, EventArgs e)
        {
            ButLogout.Image = exit;
        }

      
        }
    public class LehmerRng
    {
        private const int a = 16807;
        private const int m = 2147483647;
        private const int q = 127773;
        private const int r = 2836;
        private int seed;
        public LehmerRng(int seed)
        {
            if (seed <= 0 || seed == int.MaxValue)
                throw new Exception("Bad seed");
            this.seed = seed;
        }
        public double Next()
        {
            int hi = seed / q;
            int lo = seed % q;
            seed = (a * lo) - (r * hi);
            if (seed <= 0)
                seed = seed + m;
            return (seed * 1.0) / m;
        }
    }
}
