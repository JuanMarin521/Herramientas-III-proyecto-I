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

namespace AppDataBase
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            int PersonID;

            string LastName, FirstName;

            PersonID = Convert.ToInt32(txtPersonID.Text);
            LastName = txtLastName.Text;
            FirstName = txtFirstName.Text;

            SqlConnection connection;
            string connectionString = "Data Source=LAPTOP-5OE3AFLL\\SQLEXPRESS;Initial Catalog=miBaseDeDatos;Integrated Security=True;Encrypt=False;";
            string query = "SELECT * FROM Personas";

            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();

                SqlCommand cmd = new SqlCommand("insert into Personas(PersonID,FirstName,LastName) VALUES(@PersonID, @FirstName, @LastName)", connection);
                cmd.Parameters.AddWithValue("@PersonID", PersonID);
                cmd.Parameters.AddWithValue("@FirstName", FirstName);
                cmd.Parameters.AddWithValue("@LastName", LastName);
                //Probando gitini
                

                cmd.ExecuteNonQuery();

                // SqlDataAdapter es útil para llenar DataSets o DataTables
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable clientesTable = new DataTable();
                    adapter.Fill(clientesTable);

                    // Asignar el DataTable como origen de datos del DataGridView
                    dgvClientes.DataSource = clientesTable;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al insertar datos: " + ex.Message);
            }


        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            int PersonID;
            string LastName, FirstName;

            PersonID = Convert.ToInt32(txtPersonID.Text);
            LastName = txtLastName.Text;
            FirstName = txtFirstName.Text;

            string connectionString = "Data Source=LAPTOP-5OE3AFLL\\SQLEXPRESS;Initial Catalog=miBaseDeDatos;Integrated Security=True;Encrypt=False;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Personas SET LastName = @LastName, FirstName = @FirstName WHERE PersonID = @PersonID";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@PersonID", PersonID);
                        cmd.Parameters.AddWithValue("@LastName", LastName);
                        cmd.Parameters.AddWithValue("@FirstName", FirstName);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Datos actualizados correctamente.");
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el registro para actualizar.");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al actualizar datos: " + ex.Message);
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int PersonID;
            string LastName, FirstName;

            PersonID = Convert.ToInt32(txtPersonID.Text);
            LastName = txtLastName.Text;
            FirstName = txtFirstName.Text;

            string connectionString = "Data Source=LAPTOP-5OE3AFLL\\SQLEXPRESS;Initial Catalog=miBaseDeDatos;Integrated Security=True;Encrypt=False;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Personas WHERE PersonID = @PersonID";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@PersonID", PersonID);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Datos eliminados correctamente.");
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el registro para eliminar.");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al eliminar datos: " + ex.Message);
            }

        }

        private void btnVerTodos_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=LAPTOP-5OE3AFLL\\SQLEXPRESS;Initial Catalog=miBaseDeDatos;Integrated Security=True;Encrypt=False;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                 connection.Open();
                    string query = "SELECT * FROM Personas";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                    DataTable resultadoTable = new DataTable();
                    adapter.Fill(resultadoTable);
                    dgvClientes.DataSource = resultadoTable;
                    }
                }
            }
        catch (SqlException ex)
                {
            MessageBox.Show("Error al buscar datos: " + ex.Message);
                }
            
        
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(txtPersonID.Text);
            string connectionString = "Data Source=LAPTOP-5OE3AFLL\\SQLEXPRESS;Initial Catalog=miBaseDeDatos;Integrated Security=True;Encrypt=False;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Personas WHERE PersonID = @PersonID";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@PersonID", PersonID);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable resultadoTable = new DataTable();
                            adapter.Fill(resultadoTable);

                            dgvClientes.DataSource = resultadoTable;

                            if (resultadoTable.Rows.Count > 0)
                            {
                                txtLastName.Text = resultadoTable.Rows[0]["LastName"].ToString();
                                txtFirstName.Text = resultadoTable.Rows[0]["FirstName"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("No se encontró el registro.");
                            }


                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al buscar datos: " + ex.Message);
            }

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtPersonID.Clear();
            txtLastName.Clear();
            txtFirstName.Clear();

        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
