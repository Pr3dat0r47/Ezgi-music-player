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
    public partial class delete_category : Form
    {
        public delete_category()
        {
            InitializeComponent();
        }

        private void delete_category_Load(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connection = new SqlConnection("Data Source = eaziserver.database.windows.net; Initial Catalog = EzgiDataBase; Persist Security Info=True;User ID = EZGI; Password=Cracker2019");
                string query = "SELECT * FROM [genre]";
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["genre_name"].ToString());

                }

            }
            catch
            {
                Exception ex = new Exception();
                MessageBox.Show(ex.Message);

            }

        }

        private void delete_Button_Click(object sender, EventArgs e)
        {
            try
            {
                string constring = "Data Source = eaziserver.database.windows.net; Initial Catalog = EzgiDataBase; Persist Security Info=True;User ID = EZGI; Password=Cracker2019";
                SqlConnection con = new SqlConnection(constring);
                con.Open();
                string querycategory = "delete from [song] where genre_name='" + comboBox1.Text + "'";
                SqlCommand deletesongsofcat = new SqlCommand(querycategory, con);
                deletesongsofcat.ExecuteNonQuery();

                string query2 = "Delete from [genre] where genre_name='" + comboBox1.Text + "'";
                SqlCommand update_cmd = new SqlCommand(query2, con);
                update_cmd.ExecuteNonQuery();
                MessageBox.Show("Category has been deleted successfully ");

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

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }


}

