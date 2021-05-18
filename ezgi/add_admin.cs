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
    public partial class add_admin : Form
    {
        public add_admin()
        {
            InitializeComponent();
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        void Clear()
        {
            textBox5.Text = textBox8.Text = textBox7.Text = textBox6.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string constring = "Data Source = eaziserver.database.windows.net; Initial Catalog = EzgiDataBase; Persist Security Info=True;User ID = EZGI; Password=Cracker2019";

            string query = "Select * from [admin] where admin_name='" + textBox5.Text.Trim() + "'";
            string query2 = "Select * from [admin] where admin_email= '" + textBox8.Text.Trim() + "'";

            SqlDataAdapter sda = new SqlDataAdapter(query, constring);
            SqlDataAdapter sda2 = new SqlDataAdapter(query2, constring);

            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            sda.Fill(dt);
            sda2.Fill(dt2);


            if (textBox6.Text.Trim() == textBox9.Text.Trim())
            {
                if (dt.Rows.Count != 1 && dt2.Rows.Count != 1)
                {
                    using (SqlConnection con = new SqlConnection(constring))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("addadmin", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@admin_name", textBox5.Text.Trim());
                        cmd.Parameters.AddWithValue("@admin_email", textBox8.Text.Trim());
                        cmd.Parameters.AddWithValue("@admin_mobile", textBox7.Text.Trim());
                        cmd.Parameters.AddWithValue("@admin_password", textBox6.Text.Trim());
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Admin has been added successfull");
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

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            admin form = new admin();
            form.Show();
            this.Hide();
        }
    }
}
