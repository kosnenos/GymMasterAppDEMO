using System;
using System.Data;
using System.Data.SqlClient;

namespace GymMasterAppDemo.Data
{
    internal static class Db
    {
        public static string ConnectionString => DbConfig.ConnectionString;

        public static SqlConnection CreateConnection(string connectionString)
        {
            DbConfig.ValidateConnectionString(connectionString);
            var connection = new SqlConnection(connectionString);
            connection.StateChange += OnConnectionStateChanged;
            return connection;
        }

        private static void OnConnectionStateChanged(object sender, StateChangeEventArgs e)
        {
            if (e.CurrentState != ConnectionState.Open)
                return;

            var connection = (SqlConnection)sender;
            if (!string.Equals(connection.Database, DbConfig.DatabaseName, StringComparison.Ordinal))
            {
                connection.Close();
                throw new InvalidOperationException(DbConfig.SafetyMessage);
            }
        }
    }
}
