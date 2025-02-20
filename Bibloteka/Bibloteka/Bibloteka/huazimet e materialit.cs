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
using System.Reflection.Emit;


namespace Bibloteka
{
    public partial class huazimet_e_materialit : Form
    {
        private string ConnectionString = "Data Source=DESKTOP-82AE33I;Initial Catalog=Bibloteka;Integrated Security=True;TrustServerCertificate=True";
        
        public huazimet_e_materialit()
        {
            InitializeComponent();
            
        }
        
        private void menaxhimi_e_pagesave_Load(object sender, EventArgs e)
        {
            //LoadsClients();
           // LoadMaterials();
            
        }
        private void LoadsClients()
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


                    combohuaklienti.DataSource = dt;
                    combohuaklienti.DisplayMember = "First_Name"; // Tregon emrin e klientit
                    combohuaklienti.ValueMember = "Client_ID"; // Ruhet Client_ID
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gabim gjatë marrjes së klientëve: " + ex.Message);
                }
            }
        }

        private void LoadMaterials()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT Material_ID, Title FROM Bibliographic_Materials";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(dt);


                    combohuamateriali.DataSource = dt;
                    combohuamateriali.DisplayMember = "Title"; // Tregon emrin e Title
                    combohuamateriali.ValueMember = "Material_ID"; // Ruhet Material_ID
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gabim gjatë marrjes së klientëve: " + ex.Message);
                }
            }
        }

        private void ClearFields()
        {
            combohuaklienti.SelectedIndex = -1;
            combohuamateriali.SelectedIndex = -1;
            dateTimePickerhuazimi.Value = DateTime.Now;
            dateTimePickerkthimi.Value = DateTime.Now;
            numericUpDowntarifa.Value = 0;
            dataGridViewhuaz.DataSource = null;
            LoadPayments(); // Rifreskon DataGridView pas pastrimit
        }

        private void LoadPayments()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT Loan_ID, Client_ID, Material_ID, Loan_Date, Return_Date FROM Loans";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(dt);
                    dataGridViewhuaz.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gabim gjatë marrjes së pagesave: " + ex.Message);
                }
            }
        }

        



        private void huazimet_e_materialit_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'biblotekaDataSet3.Loans' table. You can move, or remove it, as needed.
            this.loansTableAdapter.Fill(this.biblotekaDataSet3.Loans);
            // TODO: This line of code loads data into the 'biblotekaDataSet2.Clients' table. You can move, or remove it, as needed.
            this.clientsTableAdapter.Fill(this.biblotekaDataSet2.Clients);
            label1huaz.Text = "Mirësevini, " + UserSession.Username;
            LoadsClients();
             LoadMaterials();
            


        }

        private void btnhuaruaj_Click(object sender, EventArgs e)
        {
            if (combohuaklienti.SelectedItem == null || combohuamateriali.SelectedItem == null)
            {
                MessageBox.Show("Ju lutem zgjidhni një klient.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int clientID = Convert.ToInt32(combohuaklienti.SelectedValue);
            int materialID = Convert.ToInt32(combohuamateriali.SelectedValue);
            DateTime loanDate = dateTimePickerhuazimi.Value;
            DateTime returnDate = dateTimePickerkthimi.Value;
            decimal penaltyFee = numericUpDowntarifa.Value;

            string query = "INSERT INTO Loans (Client_ID, Material_ID, Loan_Date, Return_Date) VALUES (@Client_ID, @Material_ID, @Loan_Date, @Return_Date)";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Client_ID", clientID);
                command.Parameters.AddWithValue("@Material_ID", materialID);
                command.Parameters.AddWithValue("@Loan_Date", loanDate);
                command.Parameters.AddWithValue("@Return_date", returnDate);


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
            menaxhimin_e_pagesave newform = new menaxhimin_e_pagesave();
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
