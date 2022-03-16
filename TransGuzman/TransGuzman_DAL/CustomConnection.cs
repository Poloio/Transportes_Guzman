using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace TransGuzman_DAL
{
    public class CustomConnection
    {
        public String server { get; set; }
        public String dataBase { get; set; }
        public String user { get; set; }
        public String pass { get; set; }

        public CustomConnection()
        {
            server = @"VSSPC054\CARAGONDB";
            dataBase = "transportes_guzman";
            user = "prueba";
            pass = "123";
        }

        /// <summary>
        /// Método que abre una conexión con la base de datos
        /// </summary>
        /// <pre>Nada.</pre>
        /// <returns>Una conexión abierta con la base de datos</returns>
        public async Task<SqlConnection> GetConnectionAsync()
        {
            SqlConnection connection = new SqlConnection();

            connection.ConnectionString = $"server={server};database={dataBase};uid={user};pwd={pass};";
            await connection.OpenAsync();
            return connection;
        }

        /// <summary>
        /// Este metodo cierra una conexión con la Base de datos
        /// </summary>
        /// <post>La conexion es cerrada</post>
        /// <param name="connection">SqlConnection pr referencia. Conexion a cerrar
        /// </param>
        public async Task CloseConnectionAsync(SqlConnection connection)
        {
            try
            {
                await connection.CloseAsync();
            }
            catch (SqlException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
