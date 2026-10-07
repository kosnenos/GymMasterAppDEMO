using System;
using GymMasterAppDemo.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;

namespace GymMasterAppDemo.Services
{
    public class DatabaseBackupService
    {
        private readonly string _connectionString;
        private readonly string _databaseName;

        public DatabaseBackupService(string appConnectionString, string databaseName)
        {
            DbConfig.ValidateConnectionString(appConnectionString);
            if (!string.Equals(databaseName, DbConfig.DatabaseName, StringComparison.Ordinal))
                throw new InvalidOperationException(DbConfig.SafetyMessage);

            _connectionString = appConnectionString;
            _databaseName = DbConfig.DatabaseName;
        }

        public string CreateBackup(string backupFullPath, Action<int> progressChanged)
        {
            if (string.IsNullOrWhiteSpace(backupFullPath))
                throw new ArgumentException("Η διαδρομή του backup δεν είναι έγκυρη.");

            string folderPath = Path.GetDirectoryName(backupFullPath);
            if (string.IsNullOrWhiteSpace(folderPath))
                throw new ArgumentException("Ο φάκελος αποθήκευσης του backup δεν είναι έγκυρος.");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string safePath = backupFullPath.Replace("'", "''");

            string sql = string.Format(@"
BACKUP DATABASE [{0}]
TO DISK = N'{1}'
WITH COPY_ONLY, INIT, STATS = 1;",
                _databaseName,
                safePath);

            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            {
                connection.FireInfoMessageEventOnUserErrors = true;
                connection.InfoMessage += delegate (object sender, SqlInfoMessageEventArgs e)
                {
                    int percent = ExtractPercentFromMessage(e.Message);
                    if (percent >= 0)
                    {
                        progressChanged(percent);
                    }
                };

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandTimeout = 0;

                    connection.Open();
                    progressChanged(0);

                    command.ExecuteNonQuery();

                    progressChanged(100);
                }
            }

            return backupFullPath;
        }

        private int ExtractPercentFromMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return -1;

            Match match = Regex.Match(message, @"(\d{1,3})\s+percent", RegexOptions.IgnoreCase);
            if (!match.Success)
                return -1;

            int percent;
            if (!int.TryParse(match.Groups[1].Value, out percent))
                return -1;

            if (percent < 0) percent = 0;
            if (percent > 100) percent = 100;

            return percent;
        }
    }
}