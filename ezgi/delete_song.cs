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
    public partial class delete_song : Form
    {
        public delete_song()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "Data Source = eaziserver.database.windows.net; Initial Catalog = EzgiDataBase; Persist Security Info=True;User ID = EZGI; Password=Cracker2019";
            SqlDataAdapter adapter = new SqlDataAdapter();
            connection.Open();
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("select song_name from [song] where song_name LIKE @name", connection);
            cmd.Parameters.Add(new SqlParameter("@name", "%" + textBox1.Text.Trim() + "%"));
            cmd.ExecuteNonQuery();
            dr = cmd.ExecuteReader();
            AutoCompleteStringCollection col = new AutoCompleteStringCollection();
            while (dr.Read())
            {
                col.Add(dr.GetString(0));
            }
            textBox1.AutoCompleteCustomSource = col;
            connection.Close();
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            try
            {
                string constring = "Data Source = eaziserver.database.windows.net; Initial Catalog = EzgiDataBase; Persist Security Info=True;User ID = EZGI; Password=Cracker2019";
                SqlConnection con = new SqlConnection(constring);
                con.Open();
                string query2 = "Delete from [song] where song_name='" + textBox1.Text + "'";
                SqlCommand update_cmd = new SqlCommand(query2, con);
                update_cmd.ExecuteNonQuery();
                MessageBox.Show("song has been deleted successfully ");
                textBox1.Text.Remove(0, textBox1.TextLength);
                con.Close();
            }
            catch
            {

            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            admin form = new admin();
            form.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
