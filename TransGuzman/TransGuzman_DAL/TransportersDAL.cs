using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
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
        public static List<Transporter> GetAllTransportersDAL()
        {
            var returnList = new List<Transporter>();
            var connection = _connectionManager.getConnection();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM transportistas";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var nextTransporter = new Transporter();
                    object[] columns = new object[5];

                    reader.GetValues(columns);
                    nextTransporter.EmployeeID = (string)columns[0];
                    nextTransporter.IDLicense = (string)columns[1];
                    nextTransporter.LastName = (string)columns[2];
                    nextTransporter.LastName = (string)columns[3];
                    nextTransporter.YearOfBirth = (int)columns[4];
                    returnList.Add(nextTransporter);
                }
            }
            connection.Close();
            return returnList;
        }

        
        /// <summary>
        /// Retrives the transporter with the given ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a <see cref="Transporter"/> object.</returns>
        public static Transporter GetTransporterDAL(string id)
        {
            Transporter transporter = null;
            var connection = _connectionManager.getConnection();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM transportistas WHERE id_empleado = @id";
            command.Parameters.AddWithValue("@id", id);

            using (var reader = command.ExecuteReader())
            {
                transporter = new Transporter();
                while (reader.Read())
                {
                    object[] columns = new object[5];
                    reader.GetValues(columns);
                    transporter.EmployeeID = (string)columns[0];
                    transporter.IDLicense = (string)columns[1];
                    transporter.LastName = (string)columns[2];
                    transporter.LastName = (string)columns[3];
                    transporter.YearOfBirth = (int)columns[4];
                }
            }
            connection.Close();
            return transporter;
        }
        #endregion

        #region Data Modification
        /// <summary>
        /// Updates the transporter with the same ID than the one passed through parameter.
        /// </summary>
        /// <param name="newTransporter"></param>
        /// <returns>true if any change was made.</returns>
        public static bool UpdateTransporterDal(Transporter newTransporter)
        {
            var connection = _connectionManager.getConnection();
            var command = connection.CreateCommand();
            command.CommandText = "UPDATE transporters SET permiso_dni = @license, nombre = @firstName, apellidos = @lastName, anio_nacimiento = @yearOfBirth" +
                "WHERE id_empleado = @id";
            command.Parameters.AddWithValue("@id", newTransporter.EmployeeID);
            command.Parameters.AddWithValue("@license", newTransporter.IDLicense);
            command.Parameters.AddWithValue("@firstName", newTransporter.FirstName);
            command.Parameters.AddWithValue("@lastName", newTransporter.EmployeeID);
            command.Parameters.AddWithValue("@yearOfBirth", newTransporter.EmployeeID);

            var suceeded = command.ExecuteNonQuery() > 0;
            connection.Close();
            return suceeded;
        }

        /// <summary>
        /// Inserts the transporter into the DB with the data in the one passed through parameters.
        /// </summary>
        /// <param name="newTransporter"></param>
        /// <returns>true if any change was made.</returns>
        public static bool CreateTransporterDAL(Transporter newTransporter)
        {
            var connection = _connectionManager.getConnection();
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO transportistas VALUES (NEWID(), @license, @firstName, @lastName, @yearOfBirth)";
            command.Parameters.AddWithValue("@id", newTransporter.EmployeeID);
            command.Parameters.AddWithValue("@license", newTransporter.IDLicense);
            command.Parameters.AddWithValue("@firstName", newTransporter.FirstName);
            command.Parameters.AddWithValue("@lastName", newTransporter.EmployeeID);
            command.Parameters.AddWithValue("@yearOfBirth", newTransporter.EmployeeID);

            var succeeded = command.ExecuteNonQuery() > 0;
            connection.Close();
            return succeeded;
        }
        #endregion
    }
}
