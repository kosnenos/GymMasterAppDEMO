using System.Data.SqlClient;

namespace GymMasterAppDemo.Data
{
    internal static class DbDiagnostics
    {
        public static void TestConnection()
        {
            using (var con = Db.CreateConnection(Db.ConnectionString))
            {
                con.Open();
            }
        }
    }
}