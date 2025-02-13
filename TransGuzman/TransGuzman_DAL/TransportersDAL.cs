﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TransGuzman_Entities;

namespace TransGuzman_DAL
{
    //Iba a usar Entity Framework pero iba a invertir demasiado tiempo investigando,
    //pendiente mirarlo porque es más profundo de lo que parecía, pero muy potente.
    public static class TransportersDAL
    {
        private static CustomConnection _connectionManager = new CustomConnection();

        #region Data retrieving methods
        /// <summary>
        /// Acceses the database and retrieves all transporters in it.
        /// </summary>
        /// <returns>a list of <see cref="Transporter"/>.</returns>
        public async static Task<List<Transporter>> GetAllAsyncDAL()
        {
            var returnList = new List<Transporter>();
            var connection = await _connectionManager.GetConnectionAsync();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM transportistas";

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var nextTransporter = new Transporter();
                    object[] columns = new object[5];

                    reader.GetValues(columns);
                    nextTransporter.TransporterID = columns[0].ToString();
                    nextTransporter.IDLicense = (string)columns[1];
                    nextTransporter.FirstName = (string)columns[2];
                    nextTransporter.LastName = (string)columns[3];
                    nextTransporter.YearOfBirth = Convert.ToInt32(columns[4]);//Viene un Int16, lo paso a Int32
                    returnList.Add(nextTransporter);
                }
            }
            await _connectionManager.CloseConnectionAsync(connection);
            return returnList;
        }

        /// <summary>
        /// Gets all transporters as a DataTable from the DB asynchronically.
        /// </summary>
        /// <returns>a <see cref="DataTable"/></returns>
        public static async Task<DataTable> GetDataTableAsync()
        {
            var dataTable = new DataTable();
            var connection = await _connectionManager.GetConnectionAsync();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM transportistas";

            using (var dataAdapter = new SqlDataAdapter(command))
            {
                await Task.Run( () => dataAdapter.Fill(dataTable));
            }
            await _connectionManager.CloseConnectionAsync(connection);
            return dataTable;
        }

        
        /// <summary>
        /// Retrives the transporter with the given ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a <see cref="Transporter"/> object.</returns>
        public static async Task<Transporter> GetTransporterAsyncDAL(string employeeId)
        {
            Transporter transporter = null;
            var connection = await _connectionManager.GetConnectionAsync();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM transportistas WHERE id_empleado = @id";
            command.Parameters.AddWithValue("@id", employeeId);

            using (var reader = await command.ExecuteReaderAsync())
            {
                transporter = new Transporter();
                while (await reader.ReadAsync())
                {
                    object[] columns = new object[5];
                    reader.GetValues(columns);
                    transporter.TransporterID = columns[0].ToString();
                    transporter.IDLicense = (string)columns[1];
                    transporter.FirstName = (string)columns[2];
                    transporter.LastName = (string)columns[3];
                    transporter.YearOfBirth = Convert.ToInt32(columns[4]);//Viene un Int16, lo paso a Int32
                }
            }
            await _connectionManager.CloseConnectionAsync(connection);
            return transporter;
        }

        /// <summary>
        /// Retrives the transporter with the given Driver license number.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a <see cref="Transporter"/> object.</returns>
        public static async Task<string> GetTransporterIDByLicenseAsyncDAL(string licenseNumber)
        {
            var employeeID = "";
            var connection = await _connectionManager.GetConnectionAsync();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT id_empleado FROM transportistas WHERE permiso_dni = @id";
            command.Parameters.AddWithValue("@id", licenseNumber);

            using (var reader = await command.ExecuteReaderAsync())
            { 
                while (await reader.ReadAsync())
                {
                    object[] columns = new object[5];
                    reader.GetValues(columns);
                    employeeID = columns[0].ToString();
                }
            }
            await _connectionManager.CloseConnectionAsync(connection);
            return employeeID;
        }
        #endregion

        #region Data Modification
        /// <summary>
        /// Deletes the transporter with the given ID.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>true if anything was deleted.</returns>
        public async static Task<bool> DeleteByIDAsyncDAL(string employeeId)
        {
            var connection = await _connectionManager.GetConnectionAsync();
            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM transportistas WHERE id_empleado = @employeeId";
            command.Parameters.AddWithValue("@employeeId", employeeId);
            return await command.ExecuteNonQueryAsync() > 0;
        }

        /// <summary>
        /// Updates the transporter with the same ID than the one passed through parameter.
        /// </summary>
        /// <param name="newTransporter"></param>
        /// <returns>true if any change was made.</returns>
        public static async Task<bool> UpdateTransporterAsyncDAL(Transporter newTransporter)
        {
            var connection = await _connectionManager.GetConnectionAsync();
            var command = connection.CreateCommand();
            command.CommandText = "UPDATE transportistas SET permiso_dni = @license, nombre = @firstName, apellidos = @lastName, anio_nacimiento = @yearOfBirth " +
                "WHERE id_empleado = @id";
            command.Parameters.AddWithValue("@id", new Guid(newTransporter.TransporterID));
            command.Parameters.AddWithValue("@license", newTransporter.IDLicense);
            command.Parameters.AddWithValue("@firstName", newTransporter.FirstName);
            command.Parameters.AddWithValue("@lastName", newTransporter.LastName);
            command.Parameters.AddWithValue("@yearOfBirth", newTransporter.YearOfBirth);

            var suceeded = await command.ExecuteNonQueryAsync() > 0;
            connection.Close();
            return suceeded;
        }

        /// <summary>
        /// Inserts the transporter into the DB with the data in the one passed through parameters.
        /// </summary>
        /// <param name="newTransporter"></param>
        /// <returns>true if any change was made.</returns>
        public static async Task<bool> CreateNewAsyncDAL(Transporter newTransporter)
        {
            var connection = await _connectionManager.GetConnectionAsync();
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO transportistas VALUES (NEWID(), @license, @firstName, @lastName, @yearOfBirth)";
            command.Parameters.AddWithValue("@license", newTransporter.IDLicense);
            command.Parameters.AddWithValue("@firstName", newTransporter.FirstName);
            command.Parameters.AddWithValue("@lastName", newTransporter.LastName);
            command.Parameters.AddWithValue("@yearOfBirth", newTransporter.YearOfBirth);

            var succeeded = await command.ExecuteNonQueryAsync() > 0;
            await _connectionManager.CloseConnectionAsync(connection);
            return succeeded;
        }
        #endregion
    }
}
