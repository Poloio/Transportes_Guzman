using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransGuzman_Entities;

namespace TransGuzman_DAL
{
    public class ProvincesDAL
    {

        private static CustomConnection _connectionManager = new CustomConnection();

        public static async Task<List<Province>> GetAllAsync()
        {
            var returnList = new List<Province>();
            var connection = await _connectionManager.GetConnectionAsync();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM provincias";

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var nextProvince = new Province();
                    object[] columns = new object[2];

                    reader.GetValues(columns);
                    nextProvince.ID = Convert.ToInt32(columns[0]);
                    nextProvince.Name = (string)columns[1];
                    returnList.Add(nextProvince);
                }
            }
            await _connectionManager.CloseConnectionAsync(connection);
            return returnList;
        }
    }
}
