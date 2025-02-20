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
    public partial class huazimet_e_vonuara : Form
    {
        private string ConnectionString = "Data Source=DESKTOP-82AE33I;Initial Catalog=Bibloteka;Integrated Security=True;TrustServerCertificate=True";
        
        public huazimet_e_vonuara()
        {
            InitializeComponent();
        }

        private void huazimet_e_vonuara_Load(object sender, EventArgs e)
        {
            LoadDelayedLoans(null);
            label1.Text = "Mirësevini, " + UserSession.Username;
        }
        private void LoadDelayedLoans(DateTime? filterDate)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "SELECT * FROM DelayedLoans";

                if (filterDate.HasValue)
                {
                    query += " WHERE Return_Date <= @FilterDate";
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (filterDate.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@FilterDate", filterDate.Value);
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
        }
        private void btnFilter_Click(object sender, EventArgs e)
        {
            LoadDelayedLoans(dateTimePicker1.Value);
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Forma_për_Menaxhimin_e_Materialeve_Bibliografike newform = new Forma_për_Menaxhimin_e_Materialeve_Bibliografike();
            newform.Show();
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
            menaxhimin_e_pagesave newform = new menaxhimin_e_pagesave();
            newform.Show();
            this.Hide();
        }

        private void btnpagtotal_Click(object sender, EventArgs e)
        {
            pagesat_totale_te_klienteve newform = new pagesat_totale_te_klienteve();
            newform.Show();
            this.Hide();
        }

        private void btnmnxhvon_Click(object sender, EventArgs e)
        {
            huazimet_e_materialit newform = new huazimet_e_materialit();
            newform.Show();
            this.Hide();
        }

        private void btnmnxhuser_Click(object sender, EventArgs e)
        {
        

            userat userForm = new userat();
            userForm.Show();
            this.Hide();
        }
    }
}
