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


namespace Bibloteka
{
    public partial class Forma_për_Menaxhimin_e_Materialeve_Bibliografike : Form
    {
        private string currentUserRole;

        public Forma_për_Menaxhimin_e_Materialeve_Bibliografike()
        {
            InitializeComponent();
            

          
        }
        public Forma_për_Menaxhimin_e_Materialeve_Bibliografike(string role)
        {
            InitializeComponent();
            currentUserRole = role;


        }


        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            menaxhimi_i_materialit newform = new menaxhimi_i_materialit();
            newform.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void btnklientet_Click(object sender, EventArgs e)
        {
            menaxhimi_i_klienteve newform = new menaxhimi_i_klienteve();
            newform.Show();
            this.Hide();
        }

        private void btnhuazimet_Click(object sender, EventArgs e)
        {
            huazimet_e_materialit newform = new huazimet_e_materialit();
            newform.Show();
            this.Hide();
        }

        private void btnmnxhpagesa_Click(object sender, EventArgs e)
        {
            menaxhimin_e_pagesave newform = new menaxhimin_e_pagesave();
            newform.Show();
            this.Hide();
        }

        private void btnpagesat_Click(object sender, EventArgs e)
        {
            pagesat_totale_te_klienteve newform = new pagesat_totale_te_klienteve();
            newform.Show();
            this.Hide();
        }

        private void btnvonuara_Click(object sender, EventArgs e)
        {
            huazimet_e_vonuara newform = new huazimet_e_vonuara();
            newform.Show();
            this.Hide();
        }

        private void btnuserat_Click(object sender, EventArgs e)
        {
            if (currentUserRole != "Administrator")
            {
                MessageBox.Show("Vetëm administratorët kanë qasje në këtë seksion!", "Qasje e Refuzuar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            userat userForm = new userat();
            userForm.Show();
        
        }

        private void Forma_për_Menaxhimin_e_Materialeve_Bibliografike_Load(object sender, EventArgs e)
        {

        }

        private void btnaktivemain_Click(object sender, EventArgs e)
        {
            Klientet_e_Aktive newform = new Klientet_e_Aktive();
            newform.Show();
            this.Hide();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("A jeni i sigurt që dëshironi të dilni?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {



                // Mbyllja e formës kryesore dhe kthimi te login form
                this.Hide();
                hyrje loginForm = new hyrje();
                loginForm.Show();
            }
        }
    }
}
