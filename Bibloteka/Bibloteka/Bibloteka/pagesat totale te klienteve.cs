using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bibloteka
{
    public partial class pagesat_totale_te_klienteve : Form
    {
        private string ConnectionString = "Data Source=DESKTOP-82AE33I;Initial Catalog=Bibloteka;Integrated Security=True;TrustServerCertificate=True";
       
        public pagesat_totale_te_klienteve()
        {
            InitializeComponent();
        }

        private void pagesat_totale_te_klienteve_Load(object sender, EventArgs e)
        {
            LoadTimePeriods();
            label1.Text = "Mirësevini, " + UserSession.Username;
        }

        private void LoadTimePeriods()
        {
            comboBoxtotalee.Items.Clear();
            comboBoxtotalee.Items.Add("Last day");
            comboBoxtotalee.Items.Add("Last week");
            comboBoxtotalee.Items.Add("Last Month");
            comboBoxtotalee.Items.Add("Last Year");

            comboBoxtotalee.SelectedIndex = 0;  // Përzgjedh opsionin default
        }

        private void comboBoxtotalee_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterPayments();
        }

        private void FilterPayments()
        {
            DateTime startDate = DateTime.Now;  // Data e fillimit për periudhën kohore
            DateTime endDate = DateTime.Now;  // Data e përfundimit

            switch (comboBoxtotalee.SelectedItem.ToString())
            {
                case "Last day":
                    startDate = DateTime.Now.AddDays(-1);  // Merr datën e djeshme
                    break;
                case "Last week":
                    startDate = DateTime.Now.AddDays(-7);  // Merr 7 ditët e fundit
                    break;
                case "Last Month":
                    startDate = DateTime.Now.AddMonths(-1);  // Merr datën e muajit të kaluar
                    break;
                case "Last Year":
                    startDate = DateTime.Now.AddYears(-1);  // Merr datën e vitit të kaluar
                    break;
            }
            using(SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = @"
                    SELECT 
                        C.Client_ID, 
                        C.First_Name, 
                        C.Last_Name, 
                        SUM(P.Amount) AS Total_Paid
                    FROM Payments P
                    INNER JOIN Clients C ON P.Client_ID = C.Client_ID
                    WHERE P.Payment_Date >= @StartDate AND P.Payment_Date <= @EndDate
                    GROUP BY C.Client_ID, C.First_Name, C.Last_Name";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Parametrat për periudhën kohore
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Shfaq të dhënat në UI
                    dataGridViewgaesat.DataSource=dt;
                }
            }

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
            huazimet_e_vonuara newform = new huazimet_e_vonuara();
            newform.Show();
            this.Hide();
        }

        private void btnmnxhuser_Click(object sender, EventArgs e)
        {
            

            userat userForm = new userat();
            userForm.Show();
            this.Hide();
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
    }
}
