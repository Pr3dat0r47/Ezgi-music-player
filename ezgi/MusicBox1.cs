using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ezgi
{
    public partial class MusicBox1 : UserControl
    {
        Main mainPage = Main.main;
        string IconsUrl = "http://ezgimusic.000webhostapp.com/Icons/";
        public string name;
        public string icon;
        public string type;

        public MusicBox1(string Name, string Icon,string Type)
        {
            InitializeComponent();
            name = Name;
            icon = Icon;
            type = Type;
            label1.Text = name;
            pictureBox1.ImageLocation = IconsUrl + Icon;
            if (type == "song")
                label1.Text = name.Substring(0, name.Length - 4);
            else
                label1.Text = name;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (type == "song")
            {
                mainPage.GetSong(name);
            }
            else if (type == "genre")
            {
                mainPage.selected_genre(name);
            }
            else
            {
                mainPage.selected_artist(name);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (type == "song")
            {
                mainPage.GetSong(name);
            }
            else if (type == "genre")
            {
                mainPage.selected_genre(name);
            }
            else
            {
                mainPage.selected_artist(name);
            }

        }
    }
}