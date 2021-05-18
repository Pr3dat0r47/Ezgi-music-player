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
    public partial class Add_Song : Form
    {
        string ftpUsername = "ezgimusic";
        string ftpPassword = "Cracker2019";
        string server = "ftp://files.000webhost.com/public_html/Songs/";
        string serverIcon = "ftp://files.000webhost.com/public_html/Icons/";
        string localFilePath;
        string filename;
        string icon_name;
        string icon_Path;

        public Add_Song()
        {
            InitializeComponent();
        }

        private void Add_Song_Load(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string constring = "Data Source = eaziserver.database.windows.net; Initial Catalog = EzgiDataBase; Persist Security Info=True;User ID = EZGI; Password=Cracker2019";
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            string query = "Select * from song where song_name='" + filename.Trim() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Song Already Exist In Our Server ..");
            }
            else
            {
                double total = 0;
                System.Net.ServicePointManager.Expect100Continue = false;
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(string.Format("{0}/{1}", server, filename.Trim())));
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;
                Stream ftpstream = request.GetRequestStream();
                FileStream fs;
                if (add_song_icon.Text != "Choose Icon ->")
                {
                    fs = File.OpenRead(icon_Path);
                    total += (double)fs.Length;
                }
                fs = File.OpenRead(localFilePath);
                total += (double)fs.Length;
                int byteRead = 0;
                double Read = 0;
                byte[] buffer = new byte[1024];
                do
                {
                    byteRead = fs.Read(buffer, 0, 1024);
                    ftpstream.Write(buffer, 0, byteRead);
                    Read += (double)byteRead;
                    double percentage = Read / total * 100;
                    backgroundWorker1.ReportProgress((int)percentage);

                } while (byteRead != 0);
                if (add_song_icon.Text != "Choose Icon ->")
                {
                    System.Net.ServicePointManager.Expect100Continue = false;
                    request = (FtpWebRequest)WebRequest.Create(new Uri(string.Format("{0}/{1}", serverIcon, icon_name.Trim())));
                    request.Method = WebRequestMethods.Ftp.UploadFile;
                    request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                    request.UsePassive = true;
                    request.UseBinary = true;
                    request.KeepAlive = false;
                    ftpstream = request.GetRequestStream();
                    fs = File.OpenRead(icon_Path);
                    buffer = new byte[1024];
                    byteRead = 0;
                    do
                    {
                        byteRead = fs.Read(buffer, 0, 1024);
                        Read += (double)byteRead;
                        double percentage = Read / total * 100;
                        backgroundWorker1.ReportProgress((int)percentage);
                        ftpstream.Write(buffer, 0, byteRead);
                    } while (byteRead != 0);
                }
                query = "Select * from genre where genre_name='" + Genre_textBox.Text.Trim() + "'";
                sda = new SqlDataAdapter(query, con);
                dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    SqlCommand comd = new SqlCommand("addGenre", con);
                    comd.CommandType = CommandType.StoredProcedure;
                    comd.Parameters.AddWithValue("@genre_name", Genre_textBox.Text.Trim());
                    comd.Parameters.AddWithValue("@genre_icon", "");
                    comd.ExecuteNonQuery();
                }
                query = "Select * from [artist] where artist_name='" + artist_textBox.Text.Trim() + "'";
                sda = new SqlDataAdapter(query, con);
                dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    SqlCommand comd = new SqlCommand("addArtist", con);
                    comd.CommandType = CommandType.StoredProcedure;
                    comd.Parameters.AddWithValue("@artist_name", artist_textBox.Text.Trim());
                    comd.Parameters.AddWithValue("@artist_icon", artist_textBox.Text.Trim());
                    comd.ExecuteNonQuery();
                }
                SqlCommand cmd = new SqlCommand("addSong", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@song_name", filename.Trim());
                cmd.Parameters.AddWithValue("@language", Language_textBox.Text.Trim());
                cmd.Parameters.AddWithValue("@genre_name", Genre_textBox.Text.Trim());
                cmd.Parameters.AddWithValue("@artist_name", artist_textBox.Text.Trim());
                cmd.Parameters.AddWithValue("@song_icon", icon_name);
                cmd.ExecuteNonQuery();
                ftpstream.Close();
            }
            con.Close();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            progressBar1.Update();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Done Uploading " + filename + " Song");
            progressBar1.Value = 0;
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
                MessageBox.Show("Uploading Song Please Wait Untill Finishing");
            else
            {
                if (textBox1.Text != "" && artist_textBox.Text != "" && Genre_textBox.Text != "" && Language_textBox.Text != "")
                    backgroundWorker1.RunWorkerAsync();
                else
                    MessageBox.Show("Please Fill The Boxes ..");
            }
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Multiselect = false, Filter = "All Supported Audio | *.mp3; *.wma | MP3s | *.mp3 | WMAs | *.wma" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    FileInfo fi = new FileInfo(ofd.FileName);
                    filename = fi.Name;
                    localFilePath = fi.FullName;
                    textBox1.Text = filename;
                }
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton1_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Multiselect = false, Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    FileInfo fi = new FileInfo(ofd.FileName);
                    icon_name = fi.Name;
                    icon_Path = fi.FullName;
                    add_song_icon.Text = icon_name;
                }
            }
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
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
