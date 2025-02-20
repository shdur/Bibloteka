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
using System.Text.RegularExpressions;

namespace Bibloteka
{
    public partial class menaxhimi_i_klienteve : Form
    {
        private string ConnectionString = "Data Source=DESKTOP-82AE33I;Initial Catalog=Bibloteka;Integrated Security=True;TrustServerCertificate=True";
        
        private ErrorProvider errorProvider = new ErrorProvider();
        public menaxhimi_i_klienteve()
        {
            InitializeComponent();
            txtklemail.TextChanged += txtklemail_TextChanged;

        }

        private void btnklmat_Click(object sender, EventArgs e)
        {
            menaxhimi_i_materialit newform = new menaxhimi_i_materialit();
            newform.Show();
            this.Hide();
        }

        private void btnklhuaz_Click(object sender, EventArgs e)
        {
            huazimet_e_materialit newform = new huazimet_e_materialit();
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

        private void menaxhimi_i_klienteve_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'biblotekaDataSet1.Clients' table. You can move, or remove it, as needed.
            this.clientsTableAdapter.Fill(this.biblotekaDataSet1.Clients);
            LoadData();

            label1.Text= "Mirësevini, " + UserSession.Username;
            klperditeso.Enabled = false;



        }



        private void kltruaj_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "INSERT INTO Clients (First_Name, Last_Name, Date_of_Birth, Email, Phone, Address, Membership_Active) " +
                               "VALUES (@First_Name, @Last_Name, @Date_of_Birth, @Email, @Phone, @Address, @Membership_Active)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Merr te dhenat nga forma
                    cmd.Parameters.AddWithValue("@First_Name", txtklemri.Text);
                    cmd.Parameters.AddWithValue("@Last_Name", txtklmbiemri.Text);
                    cmd.Parameters.AddWithValue("@Date_of_Birth", dateTimePickerlindja.Value);
                    cmd.Parameters.AddWithValue("@Email", txtklemail.Text);
                    cmd.Parameters.AddWithValue("@Phone", txtklteli.Text);
                    cmd.Parameters.AddWithValue("@Address", txtkladresa.Text);
                    cmd.Parameters.AddWithValue("@Membership_Active", combostatusi.SelectedItem.ToString() == "Active");

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Klienti u ruajt me sukses!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadData();
                        ClearFields();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gabim gjatë ruajtjes së klientit: " + ex.Message, "Gabim", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }
        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Clients"; 
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }
        private void ClearFields()
        {
            klientid.Text = "";
            txtklemri.Text = "";
            txtklmbiemri.Text = "";
            dateTimePickerlindja.Value = DateTime.Now;
            txtklemail.Text = "";
            txtklteli.Text = "";
            txtkladresa.Text = "";
            combostatusi.SelectedIndex = -1; 
        }




        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count) // Kontrollo që rreshti është valid
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                if (row.Cells[1].Value != null)
                    txtklemri.Text = row.Cells[1].Value.ToString();

                if (row.Cells[2].Value != null)
                    txtklmbiemri.Text = row.Cells[2].Value.ToString();

                if (row.Cells[7].Value != DBNull.Value && row.Cells[7].Value is DateTime)
                {
                    dateTimePickerlindja.Value = Convert.ToDateTime(row.Cells[7].Value);
                }
                else
                {
                    dateTimePickerlindja.Value = DateTime.Now; // Jep një vlerë default nëse është bosh
                }
                // Jep një vlerë default nëse është bosh

                if (row.Cells[4].Value != null)
                    txtklemail.Text = row.Cells[4].Value.ToString();

                if (row.Cells[5].Value != null)
                    txtklteli.Text = row.Cells[5].Value.ToString();

                if (row.Cells[6].Value != null)
                    txtkladresa.Text = row.Cells[6].Value.ToString();

                if (row.Cells[8].Value != DBNull.Value)
                {
                    bool isActive = Convert.ToBoolean(row.Cells[8].Value);
                    combostatusi.SelectedItem = isActive ? "Active" : "Inactive";
                }
;

                if (row.Cells[0].Value != null)
                    klientid.Text = row.Cells[0].Value.ToString();

                // Aktivizo butonin "Modifiko"
                klperditeso.Enabled = true;
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

        private void klperditeso_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "UPDATE Clients SET First_Name=@First_Name, Last_Name=@Last_Name, Date_of_Birth=@Date_of_Birth, Email=@Email, Phone=@Phone, Address=@Address, Membership_Active=@Membership_Active WHERE Client_ID=@Client_ID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Client_ID", klientid.Text);
                    cmd.Parameters.AddWithValue("@First_Name", txtklemri.Text);
                    cmd.Parameters.AddWithValue("@Last_Name", txtklmbiemri.Text);
                    cmd.Parameters.AddWithValue("@Date_of_Birth", dateTimePickerlindja.Value);
                    cmd.Parameters.AddWithValue("@Email", txtklemail.Text);
                    cmd.Parameters.AddWithValue("@Phone", txtklteli.Text);
                    cmd.Parameters.AddWithValue("@Address", txtkladresa.Text);
                    cmd.Parameters.AddWithValue("@Membership_Active", combostatusi.SelectedItem.ToString() == "Active");

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Të dhënat u përditësuan me sukses!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadData(); // Rifresko të dhënat në DataGridView
                        ClearFields(); // Pastro fushat
                        klperditeso.Enabled = false; // Çaktivizo butonin "Modifiko"
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gabim gjatë përditësimit: " + ex.Message, "Gabim", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void txtklemail_TextChanged(object sender, EventArgs e)
        {
            string email = txtklemail.Text.Trim();

            if (IsValidEmail(email))
            {
                errorProvider.SetError(txtklemail, ""); // Hiq gabimin nëse email-i është valid
            }
            else
            {
                errorProvider.SetError(txtklemail, "Email-i duhet të përmbajë '@' dhe të jetë në formatin e duhur!");
            }

        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
