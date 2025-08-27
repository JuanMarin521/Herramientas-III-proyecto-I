using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; // Asegúrate de tener esta línea para usar SqlConnection

namespace AppDataBase
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void btnIngresar_Click_1(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text;
            string contraseña = txtPassword.Text;

            // Cadena de conexión: ajusta el nombre de la base de datos
            string connectionString = "Data Source=LAPTOP-5OE3AFLL\\SQLEXPRESS;Initial Catalog=miBaseDeDatos;Integrated Security=True;Encrypt=False;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Usuario WHERE usuario = @usuario AND password = @password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@password", contraseña);

                try
                {
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        Form1 mainForm = new Form1();
                        mainForm.Show();
                        this.Hide(); // Oculta el formulario de login
                    }
                    else
                    {
                        MessageBox.Show("Usuario o contraseña incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error de conexión: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
