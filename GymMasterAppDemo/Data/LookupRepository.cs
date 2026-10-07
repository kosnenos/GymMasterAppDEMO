using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GymMasterAppDemo.Models;

namespace GymMasterAppDemo.Data
{
    public class LookupRepository : ILookupRepository
    {
        private readonly string _connectionString;

        public LookupRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<ServiceOptionModel> GetServices()
        {
            var services = new List<ServiceOptionModel>();

            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    SELECT ServiceCode, ServiceDescription
                    FROM ServicesList
                    ORDER BY ServiceDescription";

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        services.Add(new ServiceOptionModel
                        {
                            ServiceCode = reader["ServiceCode"] == DBNull.Value
                                ? null
                                : reader["ServiceCode"].ToString(),
                            ServiceDescription = reader["ServiceDescription"] == DBNull.Value
                                ? string.Empty
                                : reader["ServiceDescription"].ToString()
                        });
                    }
                }
            }

            return services;
        }

        public List<MembershipTypeOptionModel> GetMembershipTypes()
        {
            var types = new List<MembershipTypeOptionModel>();

            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    SELECT MembershipType, Description
                    FROM MembershipTypeList
                    ORDER BY MembershipType";

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string membershipType = reader["MembershipType"] == DBNull.Value
                            ? null
                            : reader["MembershipType"].ToString();

                        types.Add(new MembershipTypeOptionModel
                        {
                            MembershipType = membershipType,
                            Description = reader["Description"] == DBNull.Value
                                ? string.Empty
                                : reader["Description"].ToString(),
                            DurationDays = MembershipCalculationHelper.GetDurationDays(membershipType)
                        });
                    }
                }
            }

            return types;
        }

        public List<PaymentMethodOptionModel> GetPaymentMethods()
        {
            var methods = new List<PaymentMethodOptionModel>();

            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    SELECT MethodType, MethodDescription
                    FROM PayMethodList
                    ORDER BY MethodDescription";

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        methods.Add(new PaymentMethodOptionModel
                        {
                            MethodType = reader["MethodType"] == DBNull.Value
                                ? null
                                : reader["MethodType"].ToString(),
                            MethodDescription = reader["MethodDescription"] == DBNull.Value
                                ? string.Empty
                                : reader["MethodDescription"].ToString()
                        });
                    }
                }
            }

            return methods;
        }

        public List<StatusOptionModel> GetStatuses()
        {
            var statuses = new List<StatusOptionModel>();

            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    SELECT StatusCode, StatusDesc
                    FROM StatusList
                    ORDER BY StatusCode";

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        statuses.Add(new StatusOptionModel
                        {
                            StatusCode = reader["StatusCode"] == DBNull.Value
                                ? null
                                : reader["StatusCode"].ToString(),
                            StatusDesc = reader["StatusDesc"] == DBNull.Value
                                ? null
                                : reader["StatusDesc"].ToString()
                        });
                    }
                }
            }

            return statuses;
        }

        public List<WorkoutGoalOptionModel> GetWorkoutGoals()
        {
            var goals = new List<WorkoutGoalOptionModel>();

            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    SELECT GoalCode, Goal, Description
                    FROM WorkoutGoals
                    ORDER BY Goal";

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        goals.Add(new WorkoutGoalOptionModel
                        {
                            GoalCode = reader["GoalCode"] == DBNull.Value
                                ? null
                                : reader["GoalCode"].ToString(),
                            Goal = reader["Goal"] == DBNull.Value
                                ? string.Empty
                                : reader["Goal"].ToString(),
                            Description = reader["Description"] == DBNull.Value
                                ? string.Empty
                                : reader["Description"].ToString()
                        });
                    }
                }
            }

            return goals;
        }

        public List<string> GetMuscleGroups()
        {
            var muscleGroups = new List<string>();

            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    SELECT DISTINCT MuscleGroup
                    FROM Exercises
                    WHERE MuscleGroup IS NOT NULL
                      AND LTRIM(RTRIM(MuscleGroup)) <> ''
                    ORDER BY MuscleGroup";

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        muscleGroups.Add(reader["MuscleGroup"].ToString());
                    }
                }
            }

            return muscleGroups;
        }

        public List<ExerciseOptionModel> GetExercisesByMuscleGroup(string muscleGroup)
        {
            var exercises = new List<ExerciseOptionModel>();

            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    SELECT ExerciseCode, ExerciseName, MuscleGroup
                    FROM Exercises
                    WHERE MuscleGroup = @MuscleGroup
                    ORDER BY ExerciseName";

                command.Parameters.Add("@MuscleGroup", SqlDbType.VarChar, 50).Value =
                    string.IsNullOrWhiteSpace(muscleGroup)
                        ? (object)DBNull.Value
                        : muscleGroup.Trim();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        exercises.Add(new ExerciseOptionModel
                        {
                            ExerciseCode = reader["ExerciseCode"] == DBNull.Value
                                ? null
                                : reader["ExerciseCode"].ToString(),
                            ExerciseName = reader["ExerciseName"] == DBNull.Value
                                ? string.Empty
                                : reader["ExerciseName"].ToString(),
                            MuscleGroup = reader["MuscleGroup"] == DBNull.Value
                                ? string.Empty
                                : reader["MuscleGroup"].ToString()
                        });
                    }
                }
            }

            return exercises;
        }
    }
}