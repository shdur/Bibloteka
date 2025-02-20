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
    public partial class menaxhimin_e_pagesave : Form
    {
        private string ConnectionString = "Data Source=DESKTOP-82AE33I;Initial Catalog=Bibloteka;Integrated Security=True;TrustServerCertificate=True";
        
        public menaxhimin_e_pagesave()
        {
            InitializeComponent();
            
        }
        

        private void btnklmat_Click(object sender, EventArgs e)
        {
            menaxhimi_i_klienteve newform = new menaxhimi_i_klienteve();
            newform.Show();
            this.Hide();
        }

        private void btnklhuaz_Click(object sender, EventArgs e)
        {
            menaxhimi_i_materialit newform = new menaxhimi_i_materialit();
            newform.Show();
            this.Hide();
        }

        private void btnklpag_Click(object sender, EventArgs e)
        {
            huazimet_e_materialit newform = new huazimet_e_materialit();
            newform.Show();
            this.Hide();
        }

        private void btnklpagtotal_Click(object sender, EventArgs e)
        {
            pagesat_totale_te_klienteve newform = new pagesat_totale_te_klienteve();
            newform.Show();
            this.Hide();
        }

        private void btnklvon_Click(object sender, EventArgs e)
        {
            huazimet_e_vonuara newform = new huazimet_e_vonuara();
            newform.Show();
            this.Hide();
        }

        private void btnkluser_Click(object sender, EventArgs e)
        {
            

            userat userForm = new userat();
            userForm.Show();
            this.Hide();
        }

        private void menaxhimin_e_pagesave_Load(object sender, EventArgs e)
        {
            LoadClients();
            LoadPayments();
            label1.Text = "Mirësevini, " + UserSession.Username;
        }
        private void LoadClients()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT Client_ID, First_Name FROM Clients";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(dt);
                   

                    mnxhpagkl.DataSource = dt;
                    mnxhpagkl.DisplayMember = "First_Name"; // Tregon emrin e klientit
                    mnxhpagkl.ValueMember = "Client_ID"; // Ruhet Client_ID
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gabim gjatë marrjes së klientëve: " + ex.Message);
                }
            }
        }

        private void ClearFields()
        {
            mnxhpagkl.SelectedIndex = -1;
            numericUpDownshuma.Value = 0;
            mnxhpaglloji.SelectedIndex = -1;
            dateTimePickerpageses.Value = DateTime.Now;
            dataGridView1.DataSource = null;
            LoadPayments(); // Rifreskon DataGridView pas pastrimit
        }
        private void LoadPayments()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT Payment_ID, Client_ID, Amount, Payment_Date, Payment_Type FROM Payments";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gabim gjatë marrjes së pagesave: " + ex.Message);
                }
            }
        }
        private void mnxhpagruaj_Click(object sender, EventArgs e)
        {
            if (mnxhpagkl.SelectedValue == null)
            {
                MessageBox.Show("Ju lutem zgjidhni një klient.");
                return;
            }

            int clientID = Convert.ToInt32(mnxhpagkl.SelectedValue);
            decimal amount = numericUpDownshuma.Value;
            string paymentType = mnxhpaglloji.SelectedItem?.ToString();
            DateTime paymentDate = dateTimePickerpageses.Value;

            if (string.IsNullOrEmpty(paymentType))
            {
                MessageBox.Show("Ju lutem zgjidhni llojin e pagesës.");
                return;
            }

            string query = "INSERT INTO Payments (Client_ID, Amount, Payment_Date, Payment_Type) VALUES (@Client_ID, @Amount, @Payment_Date, @Payment_Type)";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Client_ID", clientID);
                command.Parameters.AddWithValue("@Amount", amount);
                command.Parameters.AddWithValue("@Payment_Date", paymentDate);
                command.Parameters.AddWithValue("@Payment_Type", paymentType);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Pagesa u regjistrua me sukses!");
                    LoadPayments(); // Rifreskon DataGridView pas regjistrimit
                    ClearFields(); // Pastron fushat dhe rifreskon DataGridView pas regjistrimit
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gabim gjatë regjistrimit të pagesës: " + ex.Message);
                }
            }
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
