using System;
using System.Data;
using MySql.Data.MySqlClient;

public class DbConnect
{
    private MySqlConnection connection;
    public string server;
    public string database;
    public string user;
    public string password;
    public string port;

    public DbConnect()
    {
        // server = "192.168.100.69";
        // database = "tecnogadged";
        // user = "UzielRemote";

        //Password para el cliente
        // password = "225699Uz";
        // port = "3308";

        // Password para desarrollador
        // password = "225699Uz";
        // port = "3307";
        server = "localhost";
        database = "tecnogadged";
        user = "root";
        password = "225699Uz";
        port = "3308";
        String connString = "Server=" + server + ";Database=" + database + ";port=" + port + ";User Id=" + user + ";password=" + password;
        connection = new MySqlConnection(connString);
    }
    public MySqlConnection Connection
    {
        get { return connection; }
    }
    public void OpenConnection()
    {
        try
        {
            connection.Open();
            // MessageBox.Show("Conexión exitosa a la base de datos.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (MySqlException ex)
        {
            MessageBox.Show("Error al conectar a la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public void CloseConnection()
    {
        try
        {
            connection.Close();
            // MessageBox.Show("Conexión cerrada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (MySqlException ex)
        {
            MessageBox.Show("Error al cerrar la conexión: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public DataTable ExecuteQuery(string query, Dictionary<string, object> parameters = null!)
    {
        DataTable dataTable = new DataTable();
        try
        {
            connection.Open();
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                // Agregar los parámetros al comando si existen
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value);
                    }
                }

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }
            }
        }
        catch (MySqlException ex)
        {
            MessageBox.Show("Error al ejecutar la consulta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            connection.Close();
        }
        return dataTable;
    }
}