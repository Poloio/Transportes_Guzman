using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace TransGuzman_DAL
{
    public static class VehiclesDAL
    {
        private static CustomConnection _connectionManager = new CustomConnection();

        public static async Task<DataTable> GetDataTableAsyncDAL()
        {
            var dataTable = new DataTable();
            var connection = await _connectionManager.GetConnectionAsync();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM dbo.VehiculosKmsRecorridos()";

            using (var dataAdapter = new SqlDataAdapter(command))
            {
                await Task.Run(() => dataAdapter.Fill(dataTable));
            }
            await _connectionManager.CloseConnectionAsync(connection);
            return dataTable;
        }
    }
}
