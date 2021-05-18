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
    public partial class auth_bef_changes : Form
    {
        public auth_bef_changes()
        {
            InitializeComponent();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            profile f1 = new profile();
            f1.Show();
            this.Hide();
        }
        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            string constring = "Data Source = eaziserver.database.windows.net; Initial Catalog = EzgiDataBase; Persist Security Info=True;User ID = EZGI; Password=Cracker2019";
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            string query = "select user_password from [user] where user_name='" + Login.SetValueForIdentity + "'";
            SqlCommand id_cmd = new SqlCommand(query, con);
            SqlDataReader myReader = id_cmd.ExecuteReader();
            string password = "";
            while (myReader.Read())
            {
                password = myReader[0].ToString();
                break;
            }
            con.Close();
            if (password == textBox1.Text)
            {
                profile.Profile.VerifyUserBeforeChanges();

                profile f1 = new profile();
                this.Hide();
            }
            else
                MessageBox.Show("Incorrect password!");
        }
    }
}
