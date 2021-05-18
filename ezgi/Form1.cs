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
namespace ezgi
{

    public partial class Login : Form
    {
        bool isValidate = false;
        string UserName;
        string Password;
        public static string SetValueForIdentity = "";
        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            UserName = textBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string constring = "Data Source = eaziserver.database.windows.net; Initial Catalog = EzgiDataBase; Persist Security Info=True;User ID = EZGI; Password=Cracker2019";
            SqlConnection con = new SqlConnection(constring);
            string queryadmin = "Select * from [admin] where admin_name='" + textBox1.Text.Trim() + "'  and admin_password= '" + textBox2.Text.Trim() + "'";
            SqlDataAdapter sda_admin = new SqlDataAdapter(queryadmin, con);
            DataTable dt_admin = new DataTable();
            sda_admin.Fill(dt_admin);

            if (dt_admin.Rows.Count == 1)
            {
                SetValueForIdentity = textBox1.Text.Trim();
                admin form = new admin();
                form.Show();
                this.Hide();
            }

            else
            {
                string query = "Select * from [user] where user_name='" + textBox1.Text.Trim() + "'  and user_password= '" + textBox2.Text.Trim() + "'";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    SetValueForIdentity = textBox1.Text.Trim();
                    Main f2 = new Main();
                    f2.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Check your username or password ");
                }
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Password = textBox2.Text;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
                this.WindowState = FormWindowState.Normal;
            else
                this.WindowState = FormWindowState.Maximized;

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }
        //18, 97, 160
        //    7, 47, 95
        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(97)))), ((int)(((byte)(160)))));
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            this.pictureBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(97)))), ((int)(((byte)(160)))));
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
        }

        private void SignUpButton_Click(object sender, EventArgs e)
        {
            //18, 97, 160
            //    7, 47, 95
            this.SignUpButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(97)))), ((int)(((byte)(160)))));
            this.SignUpButton.colbackground = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(97)))), ((int)(((byte)(160)))));
            this.SignInButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(47)))), ((int)(((byte)(95)))));
            bunifuTransition1.HideSync(panel2);
            bunifuTransition1.ShowSync(panel4);
        }

        private void SignInButton_Click(object sender, EventArgs e)
        {
            this.SignInButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(97)))), ((int)(((byte)(160)))));
            this.SignUpButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(47)))), ((int)(((byte)(95)))));
            bunifuTransition1.HideSync(panel4);
            bunifuTransition1.ShowSync(panel2);
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        void Clear()
        {
            textBox5.Text = textBox8.Text = textBox7.Text = textBox6.Text = "";
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            string constring = "Data Source = eaziserver.database.windows.net; Initial Catalog = EzgiDataBase; Persist Security Info=True;User ID = EZGI; Password=Cracker2019";

            string query = "Select * from [user] where user_name='" + textBox5.Text.Trim() + "'";
            string query2 = "Select * from [user] where user_email= '" + textBox8.Text.Trim() + "'";

            SqlDataAdapter sda = new SqlDataAdapter(query, constring);
            SqlDataAdapter sda2 = new SqlDataAdapter(query2, constring);

            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            sda.Fill(dt);
            sda2.Fill(dt2);

            if (textBox6.Text.Length >= 6 && textBox8.Text.EndsWith(".com") && textBox7.Text.Length == 11 && textBox7.Text.StartsWith("01")  )
            {
                isValidate = true;
            }
            else 
            {
                MessageBox.Show("Please re-enter your credintials where: "+Environment.NewLine+" password must be more than 7 characters or digits  " + Environment.NewLine+"phone starts with 01 and is 11 digits");
            }


            if (textBox6.Text.Trim() == textBox9.Text.Trim()&& isValidate)
            {
                if (dt.Rows.Count != 1 && dt2.Rows.Count != 1)
                {
                    using (SqlConnection con = new SqlConnection(constring))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Useradd", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@user_name", textBox5.Text.Trim());
                        cmd.Parameters.AddWithValue("@user_email", textBox8.Text.Trim());
                        cmd.Parameters.AddWithValue("@user_mobile", textBox7.Text.Trim());
                        cmd.Parameters.AddWithValue("@user_password", textBox6.Text.Trim());
                        cmd.ExecuteNonQuery();
                        SetValueForIdentity = textBox5.Text.Trim();
                        MessageBox.Show("Sign up successfull");
                        Clear();
                        Main f5 = new Main();
                        f5.Show();
                        this.Hide();
                    }
                }
                else if (dt.Rows.Count == 1)
                {
                    MessageBox.Show("Username already in use");
                }
                else if (dt2.Rows.Count == 1)
                {
                    MessageBox.Show("email already in use");
                }
            }
            else
            {
                MessageBox.Show("Retype your passowrd");
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {



        }
    }
}
