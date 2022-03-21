using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using TransGuzman_Entities;

namespace TransGuzman_DAL
{
    public static class RoutesDAL
    {
        private static CustomConnection _connectionManager = new CustomConnection();

        #region Data retrieving methods
        /// <summary>
        /// Acceses the database and retrieves all transporters in it.
        /// </summary>
        /// <returns>a list of <see cref="Transporter"/>.</returns>
        public async static Task<List<Route>> GetAllAsyncDAL()
        {
            var returnList = new List<Route>();
            var connection = await _connectionManager.GetConnectionAsync();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM rutas";

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var nextRoute = new Route();
                    object[] columns = new object[5];

                    reader.GetValues(columns);
                    nextRoute.Number = (int)columns[0];
                    nextRoute.DriverID = (string)columns[1];
                    nextRoute.TruckLicenseNumber = (string)columns[2];
                    nextRoute.OriginCode = (int)columns[3];
                    nextRoute.DestinationCode = (int)columns[4];
                    nextRoute.Kilometers = (int)columns[5];
                    returnList.Add(nextRoute);
                }
            }
            await _connectionManager.CloseConnectionAsync(connection);
            return returnList;
        }

        public async static Task<bool> DeleteByIDAsyncDAL(int routeNumber)
        {
            var connection = await _connectionManager.GetConnectionAsync();
            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM route WHERE numero_ruta = @routeNumber";
            command.Parameters.AddWithValue("@routeNumber", routeNumber);
            return await command.ExecuteNonQueryAsync() > 0;
        }

        /// <summary>
        /// Gets all transporters as a DataTable view from the DB asynchronically.
        /// </summary>
        /// <returns>a <see cref="DataTable"/></returns>
        public static async Task<DataTable> GetDataTableAsync()
        {
            var dataTable = new DataTable();
            var connection = await _connectionManager.GetConnectionAsync();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM dbo.DisplayRutas()";

            using (var dataAdapter = new SqlDataAdapter(command))
            {
                await Task.Run(() => dataAdapter.Fill(dataTable));
            }
            await _connectionManager.CloseConnectionAsync(connection);
            return dataTable;
        }


        /// <summary>
        /// Retrives the transporter with the given ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a <see cref="Transporter"/> object.</returns>
        public static async Task<Route> GetAsyncDAL(int routeNumber)
        {
            Route route = null;
            var connection = await _connectionManager.GetConnectionAsync();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM rutas WHERE numero_ruta = @routeNumber";
            command.Parameters.AddWithValue("@routeNumber", routeNumber);

            using (var reader = await command.ExecuteReaderAsync())
            {
                route = new Route();
                while (await reader.ReadAsync())
                {
                    object[] columns = new object[5];
                    reader.GetValues(columns);
                    route.Number = (int)columns[0];
                    route.DriverID = (string)columns[1];
                    route.TruckLicenseNumber = (string)columns[2];
                    route.OriginCode = (int)columns[3];
                    route.DestinationCode = (int)columns[4];
                    route.Kilometers = (int)columns[5];
                }
            }
            await _connectionManager.CloseConnectionAsync(connection);
            return route;
        }
        #endregion

        #region Data Modification
        /// <summary>
        /// Updates the transporter with the same ID than the one passed through parameter.
        /// </summary>
        /// <param name="route"></param>
        /// <returns>true if any change was made.</returns>
        public static async Task<bool> UpdateAsync(Route route)
        {
            var connection = await _connectionManager.GetConnectionAsync();
            var command = connection.CreateCommand();
            command.CommandText = "UPDATE route SET id_conductor = @driverId, matricula_vehilculo = @truck, " +
                "provincia_origen = @origin, provincia_destino = @destination, km_recorridos = @distance WHERE numero_ruta = @routeNumber";
            command.Parameters.AddWithValue("@routeNumber", route.Number);
            command.Parameters.AddWithValue("@driverId", route.DriverID);
            command.Parameters.AddWithValue("@truck", route.TruckLicenseNumber);
            command.Parameters.AddWithValue("@origin", route.OriginCode);
            command.Parameters.AddWithValue("@destination", route.DestinationCode);
            command.Parameters.AddWithValue("@distance", route.Kilometers);
            var suceeded = await command.ExecuteNonQueryAsync() > 0;
            await _connectionManager.CloseConnectionAsync(connection);
            return suceeded;
        }

        /// <summary>
        /// Inserts the route into the DB with the data in the one passed through parameters.
        /// </summary>
        /// <param name="newRoute"></param>
        /// <returns>true if any change was made.</returns>
        public static async Task<bool> CreateNewAsyncDAL(Route newRoute)
        {
            var connection = await _connectionManager.GetConnectionAsync();
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO rutas VALUES (@driverId, @truck, @origin, @destination, @distance)";

            if (String.IsNullOrEmpty(newRoute.DriverID))
                command.Parameters.AddWithValue("@driverId", DBNull.Value);
            else
                command.Parameters.AddWithValue("@driverId", newRoute.DriverID);

            if (String.IsNullOrEmpty(newRoute.TruckLicenseNumber))
                command.Parameters.AddWithValue("@truck", DBNull.Value);
            else
                command.Parameters.AddWithValue("@truck", newRoute.TruckLicenseNumber);

            command.Parameters.AddWithValue("@origin", newRoute.OriginCode);
            command.Parameters.AddWithValue("@destination", newRoute.DestinationCode);

            if (newRoute.Kilometers == 0)
                command.Parameters.AddWithValue("@distance", DBNull.Value);
            else
                command.Parameters.AddWithValue("@distance", newRoute.Kilometers);

            var succeeded = await command.ExecuteNonQueryAsync() > 0;
            await _connectionManager.CloseConnectionAsync(connection);
            return succeeded;
        }
        #endregion
    }
}
