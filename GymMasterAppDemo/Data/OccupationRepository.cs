using System.Collections.Generic;
using System.Data.SqlClient;
using GymMasterAppDemo.Models;

namespace GymMasterAppDemo.Data
{
    public class OccupationRepository : IOccupationRepository
    {
        private readonly string _connectionString;

        public OccupationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<OccupationItemModel> GetAll()
        {
            var list = new List<OccupationItemModel>();

            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    SELECT OccupationId, OccupationDesc
                    FROM OccupationList
                    ORDER BY OccupationDesc";

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new OccupationItemModel
                        {
                            OccupationId = reader["OccupationId"]?.ToString(),
                            OccupationDesc = reader["OccupationDesc"]?.ToString()
                        });
                    }
                }
            }

            return list;
        }
    }
}