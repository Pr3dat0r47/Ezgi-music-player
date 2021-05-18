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
    public partial class profile : Form
    {
        public static profile Profile;
        public void VerifyUserBeforeChanges()
        {

            label3.Visible = false;
            string constring = "Data Source = eaziserver.database.windows.net; Initial Catalog = EzgiDataBase; Persist Security Info=True;User ID = EZGI; Password=Cracker2019";
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            string query2 = "UPDATE [user] SET user_name='" + textBox1.Text + "', user_email='" + textBox2.Text + "', user_mobile='" + textBox3.Text + "', user_password='" + textBox4.Text + "' where user_name='" + Login.SetValueForIdentity + "'";
            SqlCommand update_cmd = new SqlCommand(query2, con);
            update_cmd.ExecuteNonQuery();
            con.Close();
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            label6.Visible = true;
            label7.Visible = true;
            label8.Visible = true;
            label9.Visible = true;
            MessageBox.Show("User information updated");
            label6.Text = textBox1.Text;
            label7.Text = textBox4.Text;
            label8.Text = textBox3.Text;
            label9.Text = textBox2.Text;
            Login.SetValueForIdentity = textBox1.Text;
        }
        public profile()
        {
            InitializeComponent();
            Profile = this;
            string constring = "Data Source = eaziserver.database.windows.net; Initial Catalog = EzgiDataBase; Persist Security Info=True;User ID = EZGI; Password=Cracker2019";
            SqlConnection con = new SqlConnection(constring);
            con.Open();

            string query = "select user_id from [user] where user_name='" + Login.SetValueForIdentity + "'";
            SqlCommand id_cmd = new SqlCommand(query, con);
            SqlDataReader myReader = id_cmd.ExecuteReader();
            string ID = "";
            while (myReader.Read())
            {
                ID = myReader[0].ToString();
                break;
            }
            con.Close();
            con.Open();

            string query1 = "select user_name, user_email, user_mobile, user_password from [user] where user_id='" + ID + "'";
            SqlCommand info_cmd = new SqlCommand(query1, con);
            myReader = info_cmd.ExecuteReader();
            while (myReader.Read())
            {
                label6.Text = myReader[0] as string;
                label9.Text = myReader[1] as string;
                label8.Text = myReader[2] as string;
                label7.Text = myReader[3] as string;
                break;
            }
            con.Close();

        }
        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            label3.Visible = true;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            textBox5.Visible = true;
        }

        private void bunifuFlatButton1_Click_1(object sender, EventArgs e)
        {
            Main.main.Show();
            this.Hide();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            if (textBox5.Text != textBox4.Text)
            {
                MessageBox.Show("passwords entered don't match");
            }
            else if (textBox5.Text == textBox4.Text)
            {
                auth_bef_changes f1 = new auth_bef_changes();
                f1.Show();
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}