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
using System.IO;
using System.Net;
namespace ezgi
{
    public partial class add_category : Form
    {
        string ftpUsername = "ezgi@eltonsey.5gbfree.com";
        string ftpPassword = "EZGI2019";
        string server = "ftp://eltonsey.5gbfree.com/Icons/";
        string localFilePath;
        string filename;

        public add_category()
        {
            InitializeComponent();

        }

        private void Submit_Click(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string constring = "Data Source = eaziserver.database.windows.net; Initial Catalog = EzgiDataBase; Persist Security Info=True;User ID = EZGI; Password=Cracker2019";
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            string query = "Select * from genre where genre_name='" + add_category_name.Text.Trim() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            bool catex = dt.Rows.Count > 0;
            if (catex)
            {
                MessageBox.Show("Category Already Exist In Our Server ..");
            }
            else if (add_cat_icon.Text != "")
            {
                System.Net.ServicePointManager.Expect100Continue = false;
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(string.Format("{0}/{1}", server, filename.Trim())));
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;
                Stream ftpstream = request.GetRequestStream();
                FileStream fs = File.OpenRead(localFilePath);
                byte[] buffer = new byte[1024];
                int byteRead = 0;
                do
                {
                    byteRead = fs.Read(buffer, 0, 1024);
                    ftpstream.Write(buffer, 0, byteRead);
                } while (byteRead != 0);
            }
            if (!catex)
            {
                SqlCommand cmd = new SqlCommand("addGenre", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@genre_name", add_category_name.Text.Trim());
                cmd.Parameters.AddWithValue("@genre_icon", add_cat_icon.Text.Trim());
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Multiselect = false, Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    FileInfo fi = new FileInfo(ofd.FileName);
                    filename = fi.Name;
                    localFilePath = fi.FullName;
                    add_cat_icon.Text = filename;
                }
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Done Adding Category");
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (add_category_name.Text != "")
                backgroundWorker1.RunWorkerAsync();
            else
                MessageBox.Show("Please Enter Category Name..");
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            admin form = new admin();
            form.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Minimized;
        }
    }
}
