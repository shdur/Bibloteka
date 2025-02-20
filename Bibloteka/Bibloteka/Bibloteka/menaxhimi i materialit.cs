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
using System.Xml.Linq;

namespace Bibloteka
{
    public partial class menaxhimi_i_materialit : Form
    {
        private string ConnectionString = "Data Source=DESKTOP-82AE33I;Initial Catalog=Bibloteka;Integrated Security=True;TrustServerCertificate=True";
        
        public menaxhimi_i_materialit()
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

        private void matruaj_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "INSERT INTO Bibliographic_Materials (Title, Author, Co_Authors, Publisher, Publication_Date, ISBN, DOI, Material_Type, Available_Copies) " +
                               "VALUES (@Title, @Author, @Co_Authors, @Publisher, @Publication_Date, @ISBN, @DOI, @Material_Type, @Available_Copies)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Merr të dhënat nga forma
                    cmd.Parameters.AddWithValue("@Title", txtmattitulli.Text);
                    cmd.Parameters.AddWithValue("@Author", txtmatautori.Text);
                    cmd.Parameters.AddWithValue("@Co_Authors", txtmatbashkautori.Text);
                    cmd.Parameters.AddWithValue("@Publisher", txtmatbotuesi.Text);
                    cmd.Parameters.AddWithValue("@Publication_Date", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@ISBN", txtmatisbn.Text);
                    cmd.Parameters.AddWithValue("@DOI", txtmatDOI.Text);
                    cmd.Parameters.AddWithValue("@Material_Type", txtmatlloji.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Available_Copies", numericUpDown1.Value);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Materiali u ruajt me sukses!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gabim gjatë ruajtjes së materialit: " + ex.Message, "Gabim", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void menaxhimi_i_materialit_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'biblotekaDataSet.Bibliographic_Materials' table. You can move, or remove it, as needed.
            this.bibliographic_MaterialsTableAdapter.Fill(this.biblotekaDataSet.Bibliographic_Materials);
            label1.Text = "Mirësevini, " + UserSession.Username;
            matperditeso.Enabled = false;


        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        { }
            
        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Bibliographic_Materials";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView4.DataSource = dt;
            }
        }

        private void matperditeso_Click(object sender, EventArgs e)
        {

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "UPDATE Bibliographic_Materials SET Title=@Title, Author=@Author, Co_Authors=@Co_Authors, " +
                               "Publisher=@Publisher, Publication_Date=@Publication_Date, ISBN=@ISBN, DOI=@DOI, " +
                               "Material_Type=@Material_Type, Available_Copies=@Available_Copies " +
                               "WHERE Material_ID=@Material_ID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Përditëso të dhënat nga forma
                    cmd.Parameters.AddWithValue("@Material_ID", materialid.Text);
                    cmd.Parameters.AddWithValue("@Title", txtmattitulli.Text);
                    cmd.Parameters.AddWithValue("@Author", txtmatautori.Text);
                    cmd.Parameters.AddWithValue("@Co_Authors", txtmatbashkautori.Text);
                    cmd.Parameters.AddWithValue("@Publisher", txtmatbotuesi.Text);
                    cmd.Parameters.AddWithValue("@Publication_Date", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@ISBN", txtmatisbn.Text);
                    cmd.Parameters.AddWithValue("@DOI", txtmatDOI.Text);
                    cmd.Parameters.AddWithValue("@Material_Type", txtmatlloji.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Available_Copies", numericUpDown1.Value);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Materiali u përditësua me sukses!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Rifresko DataGridView pas përditësimit
                        LoadData();
                        ClearFields();
                        matperditeso.Enabled = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gabim gjatë përditësimit: " + ex.Message, "Gabim", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }


        }


        
        private void ClearFields()
        {
            materialid.Clear();
            txtmattitulli.Clear();
            txtmatautori.Clear();
            txtmatbashkautori.Clear();
            txtmatbotuesi.Clear();
            txtmatisbn.Clear();
            txtmatDOI.Clear();
            txtmatlloji.SelectedIndex = -1;
            numericUpDown1.Value = 0;
            dateTimePicker1.Value = DateTime.Now;
        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView4.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView4.SelectedRows[0];

                // Plotëso TextBox-et me të dhënat e zgjedhura
                materialid.Text = row.Cells[0].Value.ToString();
                txtmattitulli.Text = row.Cells[1].Value.ToString();
                txtmatautori.Text = row.Cells[2].Value.ToString();
                txtmatbashkautori.Text = row.Cells[3].Value.ToString();
                txtmatbotuesi.Text = row.Cells[4].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(row.Cells[5].Value);
                txtmatisbn.Text = row.Cells[6].Value.ToString();
                txtmatDOI.Text = row.Cells[7].Value.ToString();
                txtmatlloji.SelectedItem = row.Cells[8].Value.ToString();
                object value = row.Cells[9].Value;

                if (value != null && int.TryParse(value.ToString(), out int availableCopies))
                {
                    numericUpDown1.Value = availableCopies;
                }
                else
                {
                    numericUpDown1.Value = 0; // Ose një vlerë tjetër default
                }

                // Aktivizo butonin UPDATE
                matperditeso.Enabled = true;
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


