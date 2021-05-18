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
using System.Runtime.InteropServices;


namespace ezgi
{
    public partial class Main : Form
    {
        string runningWorker = "Home";
        public static Main main;
        string qu;
        public string type;
        BackgroundWorker BGW;
        public Main()
        {
            InitializeComponent();
            main = this;
            Latest();
        
        }
        private int currentSongIndex = -1;
        public void GetSong(string Song_Name)
        {
            axWindowsMediaPlayer1.URL = "http://ezgimusic.000webhostapp.com/Songs/" + Song_Name;
            string selected_song;
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                selected_song = listBox1.GetItemText(listBox1.Items[i]);
                if (Song_Name == selected_song)
                {
                    currentSongIndex = i;
                }
            }
            BeginInvoke(new Action(() =>
            {
                listBox1.SelectedIndex = currentSongIndex;
            }));
        }

        public void selected_genre(string genre_name)
        {
            AllSongsPanel.Controls.Clear();
            listBox1.Items.Clear();
            type = "song";
            qu = "select * from song where genre_name = '" + genre_name + "'";
            if (runningWorker == "Genre" && BGW.IsBusy)
                MessageBox.Show("Please Wait Loading Categories ...");
            else
            {
                BGW.CancelAsync();
                BGW = new BackgroundWorker();
                BGW.WorkerSupportsCancellation = true;
                BGW.DoWork += delegate 
                {
                    FillFLow();
                };
                BGW.RunWorkerAsync();
                runningWorker = "Genre";
            }
        }

        public void selected_artist(string artist_name)
        {
            AllSongsPanel.Controls.Clear();
            listBox1.Items.Clear();
            type = "song";
            qu = "select * from song where artist_name = '" + artist_name + "'";
            if (runningWorker == "Artist" && BGW.IsBusy)
                MessageBox.Show("Please Wait Loading Artist ...");
            else
            {
                BGW.CancelAsync();
                BGW = new BackgroundWorker();
                BGW.WorkerSupportsCancellation = true;
                BGW.DoWork += delegate
                {
                    FillFLow();
                };
                BGW.RunWorkerAsync();
                runningWorker = "Artist";
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
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

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(64)))), ((int)(((byte)(69)))));
        }

        private void pictureBox4_MouseHover(object sender, EventArgs e)
        {
            this.pictureBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(64)))), ((int)(((byte)(69)))));
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(27)))), ((int)(((byte)(32)))));
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(27)))), ((int)(((byte)(32)))));
        }

        //to show the songs
        private void FillFLow()
        {
            try
            {
                listBox1.Items.Clear();
                string constring = "Data Source = eaziserver.database.windows.net; Initial Catalog = EzgiDataBase; Persist Security Info=True;User ID = EZGI; Password=Cracker2019";
                string query = qu;
                SqlConnection con = new SqlConnection(constring);
                SqlCommand sda = new SqlCommand(query, con);
                con.Open();
                SqlDataReader dr = sda.ExecuteReader();
                while (dr.Read())
                {
                    MusicBox1 item = new MusicBox1(dr[type + "_name"].ToString(), dr[type + "_icon"].ToString(),type);
                    item.type = type;
                    item.name = dr[type + "_name"].ToString();
                    item.icon = dr[type + "_icon"].ToString();
                    item.Margin = new System.Windows.Forms.Padding(90 , 30, 30, 30); ;
                    if (type == "song")
                    {
                        listBox1.Items.Add(dr["song_name"].ToString());
                    }
                    else
                    {
                        listBox1.Items.Clear();
                    }
                Invoke(new MethodInvoker(
                   delegate { AllSongsPanel.Controls.Add(item); }
                   ));
                }
             }
            catch
            {
                MessageBox.Show("Please Choose The Tab Again To Refresh ..");
            };
        }
        //to show the liked songs
        //private void fillflowlikes()
        //{
        //    string query = "select user_id from [user] where user_name='" + Login.SetValueForIdentity + "'";
        //    listBox1.Items.Clear();
        //    SqlCommand id_cmd = new SqlCommand(query, con);
        //    SqlDataReader myReader = id_cmd.ExecuteReader();
        //    string ID = "";
        //    while (myReader.Read())
        //    {
        //        ID = myReader[0].ToString();
        //        break;
        //    }
        //    //con.Close();
        //    //con.Open();
        //    string query2 = "select song_name ,song_icon  from [likes] inner join [song] on likes.song_id = song.song_id where [user_id] ='" + ID + "'";
        //    SqlCommand songnameCMD = new SqlCommand(query2, con);
        //    myReader = songnameCMD.ExecuteReader();
        //    while (myReader.Read())
        //    {
        //        MusicBox1 item = new MusicBox1(myReader[type + "_name"].ToString(), myReader["song_icon"].ToString(),type);
        //        AllSongsPanel.Controls.Add(item);
        //        listBox1.Items.Add(myReader["song_name"].ToString());

        //    }
        //}

        //to check the current state of the player
        private void axWindowsMediaPlayer1_Enter(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (e.newState == 8)
            {
                if (currentSongIndex == -1)
                {
                    currentSongIndex = listBox1.SelectedIndex;
                }
                currentSongIndex++;
                if (currentSongIndex < listBox1.Items.Count)
                {
                    string song_from_listbox = listBox1.GetItemText(listBox1.Items[currentSongIndex]);
                    string y = "http://ezgimusic.000webhostapp.com/Songs/" + song_from_listbox;

                    axWindowsMediaPlayer1.URL = y;
                    //to make the play button play automatic
                    BeginInvoke(new Action(() =>
                    {
                        listBox1.SelectedIndex = listBox1.SelectedIndex + 1;
                    }));
                }
                else
                {
                    string song_from_listbox = listBox1.GetItemText(listBox1.Items[0]);
                    string y = "http://ezgimusic.000webhostapp.com/Songs/" + song_from_listbox;

                    axWindowsMediaPlayer1.URL = y;
                    BeginInvoke(new Action(() =>
                    {
                        listBox1.SelectedIndex = 0;
                    }));
                    currentSongIndex = -1;
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected_song = listBox1.GetItemText(listBox1.Items[listBox1.SelectedIndex]);
            string x = "http://ezgimusic.000webhostapp.com/Songs/" + selected_song;
            axWindowsMediaPlayer1.URL = x;

        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            AllSongsPanel.Controls.Clear();
            listBox1.Items.Clear();
            qu = "select * from [song]";
            type = "song";
            if (runningWorker == "All Songs" && BGW.IsBusy)
                MessageBox.Show("Please Wait Loading Songs ...");
            else
            {
                BGW.CancelAsync();
                BGW = new BackgroundWorker();
                BGW.WorkerSupportsCancellation = true;
                BGW.DoWork += delegate
                {
                    FillFLow();
                };
                BGW.RunWorkerAsync();
                runningWorker = "All Songs";
            }
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            profile f11 = new profile();
            f11.Show();
            this.Hide();
        }

        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            WMPLib.IWMPControls3 controls = (WMPLib.IWMPControls3)axWindowsMediaPlayer1.Ctlcontrols;
            controls.stop();
            Login f4 = new Login();
            f4.Show();
            this.Hide();
        }

        private void bunifuFlatButton3_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            AllSongsPanel.Controls.Clear();
            qu = "Select * from [genre]";
            type = "genre";

            if (runningWorker == "Genre" && BGW.IsBusy)
                MessageBox.Show("Please Wait Loading Categories ...");
            else
            {
                BGW.CancelAsync();
                BGW = new BackgroundWorker();
                BGW.WorkerSupportsCancellation = true;
                BGW.DoWork += delegate
                {
                    FillFLow();
                };
                BGW.RunWorkerAsync();
                runningWorker = "Genre";
            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            Latest();
        }

        private void Latest()
        {
            AllSongsPanel.Controls.Clear();
            listBox1.Items.Clear();
            qu = "Select top 9* From [song] order by[song_id] desc";
            type = "song";
            Label label1 = new Label();
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.Location = new System.Drawing.Point(325, 30);
            label1.Margin = new System.Windows.Forms.Padding(this.Width/3 , 30, 400, 30);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(266, 33);
            label1.TabIndex = 0;
            label1.Text = "Latest Added Songs";
            AllSongsPanel.Controls.Add(label1);

            if (runningWorker == "Latest" && BGW.IsBusy)
                MessageBox.Show("Please Wait Loading Latest Songs ...");
            else
            {
                BGW = new BackgroundWorker();
                BGW.WorkerSupportsCancellation = true;
                BGW.DoWork += delegate
                {
                    FillFLow();
                };
                BGW.RunWorkerAsync();
                runningWorker = "Latest";
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            qu = "select * from artist";
            type = "artist";
            AllSongsPanel.Controls.Clear();
            if (runningWorker == "Genre" && BGW.IsBusy)
                MessageBox.Show("Please Wait Loading Artists ...");
            else
            {
                BGW.CancelAsync();
                BGW = new BackgroundWorker();
                BGW.WorkerSupportsCancellation = true;
                BGW.DoWork += delegate
                {
                    FillFLow();
                };
                BGW.RunWorkerAsync();
                runningWorker = "Genre";
            }
        }

    }
}
