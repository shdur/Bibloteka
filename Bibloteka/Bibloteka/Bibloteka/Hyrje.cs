using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;

namespace Bibloteka
{
    public partial class hyrje : Form
    {
         private string ConnectionString = "Data Source=DESKTOP-82AE33I;Initial Catalog=Bibloteka;Integrated Security=True;TrustServerCertificate=True";
        public static string LoggedInUserRole;
        public hyrje()
        {
            InitializeComponent();
        }

        private void textuserlog_Enter(object sender, EventArgs e)
        {

        }

        private void textuserlog_Leave(object sender, EventArgs e)
        {

        }

        private void textpwlog_Enter(object sender, EventArgs e)
        {

        }

        private void textpwlog_Leave(object sender, EventArgs e)
        {

        }

        private void btnlog_Click(object sender, EventArgs e)
        {

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "SELECT Role FROM Users WHERE Username = @Username AND Password = @Password";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", textuserlog.Text);
                    cmd.Parameters.AddWithValue("@Password", textpwlog.Text);

                    object role = cmd.ExecuteScalar(); // Marrim rolin nga databaza

                    if (role != null)
                    {
                        string currentUserRole = role.ToString(); // Ruajmë rolin e përdoruesit

                        // Ruajmë username-in dhe rolin në UserSession
                        UserSession.Username = textuserlog.Text;
                        UserSession.Role = currentUserRole;

                        // Kalojmë rolin dhe username në formën tjetër
                        Forma_për_Menaxhimin_e_Materialeve_Bibliografike mainForm = new Forma_për_Menaxhimin_e_Materialeve_Bibliografike(currentUserRole);
                        
                        mainForm.Show();
                        this.Hide();
                    }
                    
                    else
                    {
                        MessageBox.Show("Emri i përdoruesit ose fjalëkalimi është gabim!", "Gabim", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void hyrje_Load(object sender, EventArgs e)
        {

        }
    }
}  
