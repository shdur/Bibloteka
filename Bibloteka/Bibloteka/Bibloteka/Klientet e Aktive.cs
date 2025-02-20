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
    public partial class Klientet_e_Aktive : Form
    {
        private string ConnectionString = "Data Source=DESKTOP-82AE33I;Initial Catalog=Bibloteka;Integrated Security=True;TrustServerCertificate=True";
        
        public Klientet_e_Aktive()
        {
            InitializeComponent();
        }

        private void btnmenxhkl_Click(object sender, EventArgs e)
        {
            menaxhimi_i_klienteve newform = new menaxhimi_i_klienteve();
            newform.Show();
            this.Hide();
        }

        private void btnhuazmat_Click(object sender, EventArgs e)
        {
            menaxhimi_i_materialit newform = new menaxhimi_i_materialit();
            newform.Show();
            this.Hide();
        }

        private void btnmenxhpag_Click(object sender, EventArgs e)
        {
            huazimet_e_materialit newform = new huazimet_e_materialit();
            newform.Show();
            this.Hide();
        }

        private void btnpagtotal_Click(object sender, EventArgs e)
        {
          menaxhimin_e_pagesave newform = new menaxhimin_e_pagesave();
            newform.Show();
            this.Hide();
        }

        private void btnmnxhvon_Click(object sender, EventArgs e)
        {
            pagesat_totale_te_klienteve newform = new pagesat_totale_te_klienteve();
            newform.Show();
            this.Hide();
        }

        private void btnmnxhuser_Click(object sender, EventArgs e)
        {
            huazimet_e_vonuara newform = new huazimet_e_vonuara();
            newform.Show();
            this.Hide();
        }

        private void Klientet_e_Aktive_Load(object sender, EventArgs e)
        {
            LoadActiveClients();
            label1.Text = "Mirësevini, " + UserSession.Username;
        }
        private void LoadActiveClients()
        {

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Client_ID, First_Name, Last_Name, Date_of_Birth, Email, Phone, Address FROM Clients WHERE Membership_Active = 1";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            

            userat userForm = new userat();
            userForm.Show();
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Forma_për_Menaxhimin_e_Materialeve_Bibliografike newform = new Forma_për_Menaxhimin_e_Materialeve_Bibliografike();
            newform.Show();
        }
    }
}
