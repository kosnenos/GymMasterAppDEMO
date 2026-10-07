using System;
using System.Configuration;
using System.Data.SqlClient;

namespace GymMasterAppDemo.Data
{
    public static class DbConfig
    {
        public const string DatabaseName = "GymMasterDBDemo";
        public const string ConnectionStringName = "GymMasterDbConnection";
        internal const string SafetyMessage =
            "Demo safety check failed. GymMasterAppDemo may connect only to " + DatabaseName + ".";

        public static string ConnectionString
        {
            get
            {
                var setting = ConfigurationManager.ConnectionStrings[ConnectionStringName];
                if (setting == null)
                    throw new ConfigurationErrorsException(
                        "Δεν βρέθηκε το connection string '" + ConnectionStringName + "' στο App.config.");

                ValidateConnectionString(setting.ConnectionString);
                return setting.ConnectionString;
            }
        }

        internal static void ValidateConnectionString(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException(SafetyMessage);

            var builder = new SqlConnectionStringBuilder(connectionString);
            if (!string.Equals(builder.InitialCatalog, DatabaseName, StringComparison.Ordinal)
                || !string.IsNullOrEmpty(builder.AttachDBFilename))
                throw new InvalidOperationException(SafetyMessage);
        }
    }
}
