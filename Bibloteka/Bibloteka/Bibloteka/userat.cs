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
    public partial class userat : Form
    {
        private string ConnectionString = "Data Source=DESKTOP-82AE33I;Initial Catalog=Bibloteka;Integrated Security=True;TrustServerCertificate=True";
        private string currentUserRole;

        public userat()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void userat_Load(object sender, EventArgs e)
        {
            LoadUsers();
            label1.Text = "Mirësevini, " + UserSession.Username;
            btnmodifikouser.Enabled = false;
        }

        private void btnruajuser_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "INSERT INTO Users (Username, Password,Role) " +
                               "VALUES (@Username, @Password, @Role)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", txtuser.Text);
                    cmd.Parameters.AddWithValue("@Password", txtfjalekalimi.Text);
                    cmd.Parameters.AddWithValue("@Role", comboroli.SelectedItem.ToString());

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Përdoruesi u ruajt me sukses.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                    LoadUsers();
                }
            }

        }
        private void LoadUsers()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "SELECT User_ID,Password, Username, Role FROM Users";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridViewuser.DataSource = dt;
                }
            }
        }
        private void ClearFields()
        {
            txtuser.Text = "";
            txtfjalekalimi.Text = "";
            comboroli.SelectedIndex = -1;
        }

        private void btnmnxhvon_Click(object sender, EventArgs e)
        {
            huazimet_e_vonuara newform = new huazimet_e_vonuara();
            newform.Show();
            this.Hide();
        }

        private void btnmenxhkl_Click(object sender, EventArgs e)
        {
            menaxhimi_i_klienteve newform = new menaxhimi_i_klienteve();
            newform.Show();
            this.Hide();
        }

        private void btnhuazmat_Click(object sender, EventArgs e)
        {
            huazimet_e_materialit newform = new huazimet_e_materialit();
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

        private void btnmnxhuser_Click(object sender, EventArgs e)
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

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "SELECT * Users";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridViewuser.DataSource = dt;
            }
        }

        private void dataGridViewuser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewuser.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridViewuser.SelectedRows[0];

                // Plotëso TextBox-et me të dhënat e zgjedhura
                userid.Text = row.Cells[0].Value.ToString();
                txtuser.Text = row.Cells[2].Value.ToString();
                txtfjalekalimi.Text = row.Cells[1].Value.ToString();
                
                comboroli.SelectedItem = row.Cells[3].Value.ToString();


                // Aktivizo butonin UPDATE
                btnmodifikouser.Enabled = true;
            }
        }

        private void btnmodifikouser_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "UPDATE Users SET Username=@Username, Password=@Password, Role=@Role, " +
                                "WHERE Role=@Role";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@User_ID", userid.Text);
                    cmd.Parameters.AddWithValue("@Username", txtuser.Text);
                    cmd.Parameters.AddWithValue("@Password", txtfjalekalimi.Text);
                    cmd.Parameters.AddWithValue("@Role", comboroli.SelectedItem.ToString());

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Materiali u përditësua me sukses!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Rifresko DataGridView pas përditësimit
                        LoadData();
                        ClearFields();
                        btnmodifikouser.Enabled = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gabim gjatë përditësimit: " + ex.Message, "Gabim", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
