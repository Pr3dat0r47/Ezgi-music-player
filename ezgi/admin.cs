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
    public partial class admin : Form
    {
        public static admin adm;
        public admin()
        {
            InitializeComponent();
            adm = this;
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            Add_Song upload = new Add_Song();
            upload.Show();
            this.Hide();
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            add_artist form = new add_artist();
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

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            profileforadmin1 form = new profileforadmin1();
            form.Show();
            this.Hide();
        }

        private void Button_delete_song_Click(object sender, EventArgs e)
        {
            delete_song form = new delete_song();
            form.Show();
            this.Hide();
        }

        private void Button_Delete_artist_Click(object sender, EventArgs e)
        {
            delete_artist form = new delete_artist();
            form.Show();
            this.Hide();
        }

        private void Button_Delete_category_Click(object sender, EventArgs e)
        {
            delete_category form = new delete_category();
            form.Show();
            this.Hide();
        }

        private void add_admin_Click(object sender, EventArgs e)
        {
            add_admin form = new add_admin();
            form.Show();
            this.Hide();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            add_category form = new add_category();
            form.Show();
            this.Hide();
        }
    }
}
