using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace TransGuzman_DAL
{
    public static class UsersDAL
    {
        private static CustomConnection _connectionManager = new CustomConnection();

        
        /// <summary>
        /// Checks if there is any user with the given credentials.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>true if the credentials are correct.</returns>
        public static bool CheckUserCredentials(string username, string password)
        {
            var connection = _connectionManager.getConnection();
            var command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@passsword", password);
            connection.Close();
            return (int)command.ExecuteScalar() != -1;
        }
    }
}
