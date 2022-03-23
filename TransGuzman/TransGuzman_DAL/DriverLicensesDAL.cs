using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransGuzman_Entities;

namespace TransGuzman_DAL.Models
{
    public static class DriverLicensesDAL
    {
        private static CustomConnection _connectionManager = new CustomConnection();

        public static async Task<bool> CreateNewAsyncDAL(DriverLicense license)
        {
            var connection = await _connectionManager.GetConnectionAsync();
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO permisos_conducir VALUES (@license, @type, @expireDate)";
            command.Parameters.AddWithValue("@license", license.LicenseID);
            command.Parameters.AddWithValue("@type", license.LicenseType);
            command.Parameters.AddWithValue("@expireDate", license.ExpireDate);

            var succeeded = await command.ExecuteNonQueryAsync() > 0;
            await _connectionManager.CloseConnectionAsync(connection);
            return succeeded;
        }

        public static async Task<List<LicenseType>> GetLicenseTypesAsync()
        {
            var returnList = new List<LicenseType>();
            var connection = await _connectionManager.GetConnectionAsync();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM tipos_permiso";

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var nextType = new LicenseType();
                    object[] columns = new object[1];

                    reader.GetValues(columns);
                    nextType.Name = (string)columns[0];
                    returnList.Add(nextType);
                }
            }
            await _connectionManager.CloseConnectionAsync(connection);
            return returnList;
        }
    }
}
