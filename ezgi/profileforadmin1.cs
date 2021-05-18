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
    public partial class profileforadmin1 : Form
    {
        public profileforadmin1()
        {
            InitializeComponent();
            label6.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;

        }

        private void profile_Load(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {

            label6.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;


        }

        private void bunifuFlatButton1_Click_1(object sender, EventArgs e)
        {
            admin.adm.Show();
            this.Hide();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {

            string constring = "Data Source = eaziserver.database.windows.net; Initial Catalog = EzgiDataBase; Persist Security Info=True;User ID = EZGI; Password=Cracker2019";
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            string query2 = "UPDATE [user] SET user_name='" + textBox1.Text + "', user_email='" + textBox2.Text + "', user_mobile='" + textBox3.Text + "' where user_name='" + textBox4.Text.Trim() + "'";
            SqlCommand update_cmd = new SqlCommand(query2, con);
            update_cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("User information updated");
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            label6.Visible = true;
            label8.Visible = true;
            label9.Visible = true;
            label6.Text = textBox1.Text;
            label8.Text = textBox3.Text;
            label9.Text = textBox2.Text;
        }


        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "Data Source = eaziserver.database.windows.net; Initial Catalog = EzgiDataBase; Persist Security Info=True;User ID = EZGI; Password=Cracker2019";
            SqlDataAdapter adapter = new SqlDataAdapter();
            connection.Open();
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("select user_name from [user] where user_name LIKE @name", connection);
            cmd.Parameters.Add(new SqlParameter("@name", "%" + textBox4.Text.Trim() + "%"));
            cmd.ExecuteNonQuery();
            dr = cmd.ExecuteReader();
            AutoCompleteStringCollection col = new AutoCompleteStringCollection();
            while (dr.Read())
            {
                col.Add(dr.GetString(0));
            }
            textBox4.AutoCompleteCustomSource = col;
            connection.Close();
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            {
                string constring = "Data Source = eaziserver.database.windows.net; Initial Catalog = EzgiDataBase; Persist Security Info=True;User ID = EZGI; Password=Cracker2019";
                SqlConnection con = new SqlConnection(constring);
                con.Open();
                string query2 = "Delete from [user] where user_name='" + textBox4.Text + "'";
                SqlCommand update_cmd = new SqlCommand(query2, con);
                update_cmd.ExecuteNonQuery();
                MessageBox.Show("User has been deleted successfully ");
                textBox4.Text.Remove(0, textBox4.TextLength);
                con.Close();
            }
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            string constring = "Data Source = eaziserver.database.windows.net; Initial Catalog = EzgiDataBase; Persist Security Info=True;User ID = EZGI; Password=Cracker2019";
            SqlConnection con = new SqlConnection(constring);
            con.Open();

            string query = "select user_id from [user] where user_name='" + textBox4.Text.Trim() + "'";
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

            string query1 = "select user_name, user_email, user_mobile from [user] where user_id='" + ID + "'";
            SqlCommand info_cmd = new SqlCommand(query1, con);
            myReader = info_cmd.ExecuteReader();
            while (myReader.Read())
            {
                label6.Text = myReader[0] as string;
                label9.Text = myReader[1] as string;
                label8.Text = myReader[2] as string;

                break;
            }
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            label6.Visible = true;
            label8.Visible = true;
            label9.Visible = true;
            con.Close();
        }
    }
}