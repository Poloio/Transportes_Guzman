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
                    nextRoute.RouteID = (int)columns[0];
                    nextRoute.TransporterID = (string)columns[1];
                    nextRoute.VehicleID = (string)columns[2];
                    nextRoute.OriginProvinceID = (int)columns[3];
                    nextRoute.DestinatinProvinceID = (int)columns[4];
                    nextRoute.TraveledKM = (int)columns[5];
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
                    route.RouteID = (int)columns[0];
                    route.TransporterID = (string)columns[1];
                    route.VehicleID = (string)columns[2];
                    route.OriginProvinceID = (int)columns[3];
                    route.DestinatinProvinceID = (int)columns[4];
                    route.TraveledKM = (int)columns[5];
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
            command.Parameters.AddWithValue("@routeNumber", route.RouteID);
            command.Parameters.AddWithValue("@driverId", route.TransporterID);
            command.Parameters.AddWithValue("@truck", route.VehicleID);
            command.Parameters.AddWithValue("@origin", route.OriginProvinceID);
            command.Parameters.AddWithValue("@destination", route.DestinatinProvinceID);
            command.Parameters.AddWithValue("@distance", route.TraveledKM);
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

            if (String.IsNullOrEmpty(newRoute.TransporterID))
                command.Parameters.AddWithValue("@driverId", DBNull.Value);
            else
                command.Parameters.AddWithValue("@driverId", newRoute.TransporterID);

            if (String.IsNullOrEmpty(newRoute.VehicleID))
                command.Parameters.AddWithValue("@truck", DBNull.Value);
            else
                command.Parameters.AddWithValue("@truck", newRoute.VehicleID);

            command.Parameters.AddWithValue("@origin", newRoute.OriginProvinceID);
            command.Parameters.AddWithValue("@destination", newRoute.DestinatinProvinceID);

            if (newRoute.TraveledKM == 0)
                command.Parameters.AddWithValue("@distance", DBNull.Value);
            else
                command.Parameters.AddWithValue("@distance", newRoute.TraveledKM);

            var succeeded = await command.ExecuteNonQueryAsync() > 0;
            await _connectionManager.CloseConnectionAsync(connection);
            return succeeded;
        }
        #endregion
    }
}
