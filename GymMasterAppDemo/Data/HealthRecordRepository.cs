using System;
using System.Data;
using System.Data.SqlClient;
using GymMasterAppDemo.Models;

namespace GymMasterAppDemo.Data
{
    public class HealthRecordRepository : IHealthRecordRepository
    {
        private readonly string _connectionString;

        public HealthRecordRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public long Add(HealthRecordModel model)
        {
            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;

                command.CommandText = @"
                    INSERT INTO HealthRecord
                    (
                        HasBodyPain, BodyPainDesc, IsObest, IsSmoker, FamilyHeartHistory,
                        HasHypertasis, HasDiabetes, HasHeartIssues, HasAsthma, HasThyroedes,
                        HasArthritis, HasOsteoporosis, HasAllergies, HasMyosceletic,
                        MyoskeleticDesc, HasOther, OtherDesc, EmergencyContact,
                        EmergencyPhone, Comment
                    )
                    VALUES
                    (
                        @HasBodyPain, @BodyPainDesc, @IsObest, @IsSmoker, @FamilyHeartHistory,
                        @HasHypertasis, @HasDiabetes, @HasHeartIssues, @HasAsthma, @HasThyroedes,
                        @HasArthritis, @HasOsteoporosis, @HasAllergies, @HasMyosceletic,
                        @MyoskeleticDesc, @HasOther, @OtherDesc, @EmergencyContact,
                        @EmergencyPhone, @Comment
                    );

                    SELECT CAST(SCOPE_IDENTITY() AS BIGINT);";

                FillParameters(command, model);

                return Convert.ToInt64(command.ExecuteScalar());
            }
        }

        public void Edit(HealthRecordModel model)
        {
            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;

                command.CommandText = @"
                    UPDATE HealthRecord
                    SET
                        HasBodyPain = @HasBodyPain,
                        BodyPainDesc = @BodyPainDesc,
                        IsObest = @IsObest,
                        IsSmoker = @IsSmoker,
                        FamilyHeartHistory = @FamilyHeartHistory,
                        HasHypertasis = @HasHypertasis,
                        HasDiabetes = @HasDiabetes,
                        HasHeartIssues = @HasHeartIssues,
                        HasAsthma = @HasAsthma,
                        HasThyroedes = @HasThyroedes,
                        HasArthritis = @HasArthritis,
                        HasOsteoporosis = @HasOsteoporosis,
                        HasAllergies = @HasAllergies,
                        HasMyosceletic = @HasMyosceletic,
                        MyoskeleticDesc = @MyoskeleticDesc,
                        HasOther = @HasOther,
                        OtherDesc = @OtherDesc,
                        EmergencyContact = @EmergencyContact,
                        EmergencyPhone = @EmergencyPhone,
                        Comment = @Comment
                    WHERE Id = @Id";

                FillParameters(command, model);
                command.Parameters.Add("@Id", SqlDbType.BigInt).Value = model.Id;

                command.ExecuteNonQuery();
            }
        }

        public HealthRecordModel GetById(long id)
        {
            HealthRecordModel model = null;

            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    SELECT
                        Id, HasBodyPain, BodyPainDesc, IsObest, IsSmoker, FamilyHeartHistory,
                        HasHypertasis, HasDiabetes, HasHeartIssues, HasAsthma, HasThyroedes,
                        HasArthritis, HasOsteoporosis, HasAllergies, HasMyosceletic,
                        MyoskeleticDesc, HasOther, OtherDesc, EmergencyContact,
                        EmergencyPhone, Comment
                    FROM HealthRecord
                    WHERE Id = @Id";

                command.Parameters.Add("@Id", SqlDbType.BigInt).Value = id;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        model = new HealthRecordModel
                        {
                            Id = Convert.ToInt64(reader["Id"]),
                            HasBodyPain = reader["HasBodyPain"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(reader["HasBodyPain"]),
                            BodyPainDesc = reader["BodyPainDesc"]?.ToString(),
                            IsObest = reader["IsObest"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(reader["IsObest"]),
                            IsSmoker = reader["IsSmoker"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(reader["IsSmoker"]),
                            FamilyHeartHistory = reader["FamilyHeartHistory"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(reader["FamilyHeartHistory"]),
                            HasHypertasis = reader["HasHypertasis"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(reader["HasHypertasis"]),
                            HasDiabetes = reader["HasDiabetes"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(reader["HasDiabetes"]),
                            HasHeartIssues = reader["HasHeartIssues"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(reader["HasHeartIssues"]),
                            HasAsthma = reader["HasAsthma"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(reader["HasAsthma"]),
                            HasThyroedes = reader["HasThyroedes"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(reader["HasThyroedes"]),
                            HasArthritis = reader["HasArthritis"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(reader["HasArthritis"]),
                            HasOsteoporosis = reader["HasOsteoporosis"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(reader["HasOsteoporosis"]),
                            HasAllergies = reader["HasAllergies"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(reader["HasAllergies"]),
                            HasMyosceletic = reader["HasMyosceletic"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(reader["HasMyosceletic"]),
                            MyoskeleticDesc = reader["MyoskeleticDesc"]?.ToString(),
                            HasOther = reader["HasOther"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(reader["HasOther"]),
                            OtherDesc = reader["OtherDesc"]?.ToString(),
                            EmergencyContact = reader["EmergencyContact"]?.ToString(),
                            EmergencyPhone = reader["EmergencyPhone"]?.ToString(),
                            Comment = reader["Comment"]?.ToString()
                        };
                    }
                }
            }

            return model;
        }

        private void FillParameters(SqlCommand command, HealthRecordModel model)
        {
            command.Parameters.Clear();

            command.Parameters.Add("@HasBodyPain", SqlDbType.Bit).Value = (object)model.HasBodyPain ?? DBNull.Value;
            command.Parameters.Add("@BodyPainDesc", SqlDbType.NVarChar, 250).Value = (object)model.BodyPainDesc ?? DBNull.Value;

            command.Parameters.Add("@IsObest", SqlDbType.Bit).Value = (object)model.IsObest ?? DBNull.Value;
            command.Parameters.Add("@IsSmoker", SqlDbType.Bit).Value = (object)model.IsSmoker ?? DBNull.Value;
            command.Parameters.Add("@FamilyHeartHistory", SqlDbType.Bit).Value = (object)model.FamilyHeartHistory ?? DBNull.Value;
            command.Parameters.Add("@HasHypertasis", SqlDbType.Bit).Value = (object)model.HasHypertasis ?? DBNull.Value;
            command.Parameters.Add("@HasDiabetes", SqlDbType.Bit).Value = (object)model.HasDiabetes ?? DBNull.Value;
            command.Parameters.Add("@HasHeartIssues", SqlDbType.Bit).Value = (object)model.HasHeartIssues ?? DBNull.Value;
            command.Parameters.Add("@HasAsthma", SqlDbType.Bit).Value = (object)model.HasAsthma ?? DBNull.Value;
            command.Parameters.Add("@HasThyroedes", SqlDbType.Bit).Value = (object)model.HasThyroedes ?? DBNull.Value;
            command.Parameters.Add("@HasArthritis", SqlDbType.Bit).Value = (object)model.HasArthritis ?? DBNull.Value;
            command.Parameters.Add("@HasOsteoporosis", SqlDbType.Bit).Value = (object)model.HasOsteoporosis ?? DBNull.Value;
            command.Parameters.Add("@HasAllergies", SqlDbType.Bit).Value = (object)model.HasAllergies ?? DBNull.Value;
            command.Parameters.Add("@HasMyosceletic", SqlDbType.Bit).Value = (object)model.HasMyosceletic ?? DBNull.Value;
            command.Parameters.Add("@MyoskeleticDesc", SqlDbType.NVarChar, 250).Value = (object)model.MyoskeleticDesc ?? DBNull.Value;

            command.Parameters.Add("@HasOther", SqlDbType.Bit).Value = (object)model.HasOther ?? DBNull.Value;
            command.Parameters.Add("@OtherDesc", SqlDbType.NVarChar, 250).Value = (object)model.OtherDesc ?? DBNull.Value;

            command.Parameters.Add("@EmergencyContact", SqlDbType.NVarChar, 100).Value = (object)model.EmergencyContact ?? DBNull.Value;
            command.Parameters.Add("@EmergencyPhone", SqlDbType.NVarChar, 20).Value = (object)model.EmergencyPhone ?? DBNull.Value;
            command.Parameters.Add("@Comment", SqlDbType.NVarChar).Value = (object)model.Comment ?? DBNull.Value;
        }
    }
}