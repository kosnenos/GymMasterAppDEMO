using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GymMasterAppDemo.Models;

namespace GymMasterAppDemo.Data
{
    public class WorkoutProgramRepository : IWorkoutProgramRepository
    {
        private readonly string _connectionString;

        public WorkoutProgramRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<WorkoutProgramModel> GetByCustomerId(long customerId)
        {
            var programs = new List<WorkoutProgramModel>();

            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    SELECT
                        pw.Id,
                        pw.CustomerId,
                        pw.GoalCode,
                        wg.Goal AS GoalDescription,
                        pw.Duration,
                        pw.Frequency,
                        pw.StartDate,
                        pw.EndDate,
                        pw.Comments
                    FROM ProgramsWorkout pw
                    LEFT JOIN WorkoutGoals wg
                        ON wg.GoalCode = pw.GoalCode
                    WHERE pw.CustomerId = @CustomerId
                    ORDER BY pw.StartDate DESC, pw.Id DESC";

                command.Parameters.Add("@CustomerId", SqlDbType.BigInt).Value = customerId;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        programs.Add(MapWorkoutProgram(reader));
                    }
                }
            }

            return programs;
        }

        public WorkoutProgramModel GetById(long workoutProgramId)
        {
            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    SELECT
                        pw.Id,
                        pw.CustomerId,
                        pw.GoalCode,
                        wg.Goal AS GoalDescription,
                        pw.Duration,
                        pw.Frequency,
                        pw.StartDate,
                        pw.EndDate,
                        pw.Comments
                    FROM ProgramsWorkout pw
                    LEFT JOIN WorkoutGoals wg
                        ON wg.GoalCode = pw.GoalCode
                    WHERE pw.Id = @Id";

                command.Parameters.Add("@Id", SqlDbType.BigInt).Value = workoutProgramId;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        return MapWorkoutProgram(reader);
                }
            }

            return null;
        }

        public List<WorkoutProgramDetailModel> GetDetailsByProgramId(long workoutProgramId)
        {
            var details = new List<WorkoutProgramDetailModel>();

            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    SELECT
                        d.Id,
                        d.ProgramId,
                        d.DayOfWeek,
                        e.MuscleGroup,
                        d.ExerciseCode,
                        e.ExerciseName,
                        d.Sets,
                        d.Reps,
                        d.RestTime
                    FROM ProgramsWorkoutDetails d
                    INNER JOIN Exercises e
                        ON e.ExerciseCode = d.ExerciseCode
                    WHERE d.ProgramId = @ProgramId
                    ORDER BY
                        CASE d.DayOfWeek
                            WHEN N'1η Μέρα' THEN 1
                            WHEN N'2η Μέρα' THEN 2
                            WHEN N'3η Μέρα' THEN 3
                            WHEN N'4η Μέρα' THEN 4
                            WHEN N'5η Μέρα' THEN 5
                            WHEN N'6η Μέρα' THEN 6
                            ELSE 99
                        END,
                        d.Id";

                command.Parameters.Add("@ProgramId", SqlDbType.BigInt).Value = workoutProgramId;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        details.Add(MapWorkoutProgramDetail(reader));
                    }
                }
            }

            return details;
        }

        public WorkoutProgramPrintHeaderModel GetPrintHeader(long workoutProgramId)
        {
            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    SELECT
                        pw.Id AS ProgramId,
                        pw.CustomerId,
                        c.LastName,
                        c.FirstName,
                        wg.Goal,
                        pw.Frequency,
                        pw.Duration,
                        pw.StartDate,
                        pw.EndDate,
                        pw.Comments
                    FROM ProgramsWorkout pw
                    INNER JOIN Customers c
                        ON c.Id = pw.CustomerId
                    LEFT JOIN WorkoutGoals wg
                        ON wg.GoalCode = pw.GoalCode
                    WHERE pw.Id = @ProgramId";

                command.Parameters.Add("@ProgramId", SqlDbType.BigInt).Value = workoutProgramId;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        return MapWorkoutProgramPrintHeader(reader);
                }
            }

            return null;
        }

        public List<WorkoutProgramPrintDetailModel> GetPrintDetails(long workoutProgramId)
        {
            var details = new List<WorkoutProgramPrintDetailModel>();

            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    SELECT
                        d.DayOfWeek,
                        e.MuscleGroup,
                        e.ExerciseName,
                        d.Sets,
                        d.Reps,
                        d.RestTime
                    FROM ProgramsWorkoutDetails d
                    INNER JOIN Exercises e
                        ON e.ExerciseCode = d.ExerciseCode
                    WHERE d.ProgramId = @ProgramId
                    ORDER BY
                        CASE d.DayOfWeek
                            WHEN N'1η Μέρα' THEN 1
                            WHEN N'2η Μέρα' THEN 2
                            WHEN N'3η Μέρα' THEN 3
                            WHEN N'4η Μέρα' THEN 4
                            WHEN N'5η Μέρα' THEN 5
                            WHEN N'6η Μέρα' THEN 6
                            ELSE 99
                        END,
                        e.MuscleGroup,
                        e.ExerciseName,
                        d.Id";

                command.Parameters.Add("@ProgramId", SqlDbType.BigInt).Value = workoutProgramId;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        details.Add(MapWorkoutProgramPrintDetail(reader));
                    }
                }
            }

            return details;
        }

        public long Add(WorkoutProgramModel program, List<WorkoutProgramDetailModel> details)
        {
            if (program == null)
                throw new ArgumentNullException(nameof(program));

            if (details == null)
                details = new List<WorkoutProgramDetailModel>();

            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    long newProgramId;

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.Transaction = transaction;
                        command.CommandText = @"
                            INSERT INTO ProgramsWorkout
                            (
                                CustomerId,
                                GoalCode,
                                Duration,
                                Frequency,
                                StartDate,
                                EndDate,
                                Comments
                            )
                            VALUES
                            (
                                @CustomerId,
                                @GoalCode,
                                @Duration,
                                @Frequency,
                                @StartDate,
                                @EndDate,
                                @Comments
                            );

                            SELECT CAST(SCOPE_IDENTITY() AS BIGINT);";

                        AddWorkoutProgramParameters(command, program);

                        newProgramId = Convert.ToInt64(command.ExecuteScalar());
                    }

                    InsertDetails(connection, transaction, newProgramId, details);

                    transaction.Commit();
                    return newProgramId;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Update(WorkoutProgramModel program, List<WorkoutProgramDetailModel> details)
        {
            if (program == null)
                throw new ArgumentNullException(nameof(program));

            if (details == null)
                details = new List<WorkoutProgramDetailModel>();

            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.Transaction = transaction;
                        command.CommandText = @"
                            UPDATE ProgramsWorkout
                            SET
                                CustomerId = @CustomerId,
                                GoalCode = @GoalCode,
                                Duration = @Duration,
                                Frequency = @Frequency,
                                StartDate = @StartDate,
                                EndDate = @EndDate,
                                Comments = @Comments
                            WHERE Id = @Id";

                        AddWorkoutProgramParameters(command, program);
                        command.Parameters.Add("@Id", SqlDbType.BigInt).Value = program.Id;

                        command.ExecuteNonQuery();
                    }

                    using (SqlCommand deleteDetailsCommand = new SqlCommand())
                    {
                        deleteDetailsCommand.Connection = connection;
                        deleteDetailsCommand.Transaction = transaction;
                        deleteDetailsCommand.CommandText = @"
                            DELETE FROM ProgramsWorkoutDetails
                            WHERE ProgramId = @ProgramId";

                        deleteDetailsCommand.Parameters.Add("@ProgramId", SqlDbType.BigInt).Value = program.Id;
                        deleteDetailsCommand.ExecuteNonQuery();
                    }

                    InsertDetails(connection, transaction, program.Id, details);

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Delete(long workoutProgramId)
        {
            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            {
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    using (SqlCommand deleteDetailsCommand = new SqlCommand())
                    {
                        deleteDetailsCommand.Connection = connection;
                        deleteDetailsCommand.Transaction = transaction;
                        deleteDetailsCommand.CommandText = @"
                            DELETE FROM ProgramsWorkoutDetails
                            WHERE ProgramId = @ProgramId";

                        deleteDetailsCommand.Parameters.Add("@ProgramId", SqlDbType.BigInt).Value = workoutProgramId;
                        deleteDetailsCommand.ExecuteNonQuery();
                    }

                    using (SqlCommand deleteProgramCommand = new SqlCommand())
                    {
                        deleteProgramCommand.Connection = connection;
                        deleteProgramCommand.Transaction = transaction;
                        deleteProgramCommand.CommandText = @"
                            DELETE FROM ProgramsWorkout
                            WHERE Id = @Id";

                        deleteProgramCommand.Parameters.Add("@Id", SqlDbType.BigInt).Value = workoutProgramId;
                        deleteProgramCommand.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        private void InsertDetails(
            SqlConnection connection,
            SqlTransaction transaction,
            long programId,
            List<WorkoutProgramDetailModel> details)
        {
            foreach (WorkoutProgramDetailModel detail in details)
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Transaction = transaction;
                    command.CommandText = @"
                        INSERT INTO ProgramsWorkoutDetails
                        (
                            ProgramId,
                            ExerciseCode,
                            Sets,
                            Reps,
                            RestTime,
                            DayOfWeek
                        )
                        VALUES
                        (
                            @ProgramId,
                            @ExerciseCode,
                            @Sets,
                            @Reps,
                            @RestTime,
                            @DayOfWeek
                        )";

                    command.Parameters.Add("@ProgramId", SqlDbType.BigInt).Value = programId;
                    command.Parameters.Add("@ExerciseCode", SqlDbType.VarChar, 7).Value =
                        string.IsNullOrWhiteSpace(detail.ExerciseCode)
                            ? (object)DBNull.Value
                            : detail.ExerciseCode.Trim();

                    command.Parameters.Add("@Sets", SqlDbType.Int).Value = detail.Sets;
                    command.Parameters.Add("@Reps", SqlDbType.Int).Value = detail.Reps;
                    command.Parameters.Add("@RestTime", SqlDbType.VarChar, 20).Value =
                        string.IsNullOrWhiteSpace(detail.RestTime)
                            ? (object)DBNull.Value
                            : detail.RestTime.Trim();

                    command.Parameters.Add("@DayOfWeek", SqlDbType.VarChar, 20).Value =
                        string.IsNullOrWhiteSpace(detail.DayOfWeek)
                            ? (object)DBNull.Value
                            : detail.DayOfWeek.Trim();

                    command.ExecuteNonQuery();
                }
            }
        }

        private void AddWorkoutProgramParameters(SqlCommand command, WorkoutProgramModel program)
        {
            command.Parameters.Add("@CustomerId", SqlDbType.BigInt).Value = program.CustomerId;
            command.Parameters.Add("@GoalCode", SqlDbType.VarChar, 4).Value =
                string.IsNullOrWhiteSpace(program.GoalCode)
                    ? (object)DBNull.Value
                    : program.GoalCode.Trim();

            command.Parameters.Add("@Duration", SqlDbType.Int).Value = program.Duration;
            command.Parameters.Add("@Frequency", SqlDbType.Int).Value = program.Frequency;
            command.Parameters.Add("@StartDate", SqlDbType.Date).Value = program.StartDate.Date;
            command.Parameters.Add("@EndDate", SqlDbType.Date).Value = program.EndDate.Date;
            command.Parameters.Add("@Comments", SqlDbType.NVarChar).Value =
                string.IsNullOrWhiteSpace(program.Comments)
                    ? (object)DBNull.Value
                    : program.Comments.Trim();
        }

        private WorkoutProgramModel MapWorkoutProgram(SqlDataReader reader)
        {
            WorkoutProgramModel model = new WorkoutProgramModel();

            model.Id = Convert.ToInt64(reader["Id"]);
            model.CustomerId = Convert.ToInt64(reader["CustomerId"]);
            model.GoalCode = reader["GoalCode"] == DBNull.Value ? null : reader["GoalCode"].ToString();
            model.GoalDescription = reader["GoalDescription"] == DBNull.Value ? null : reader["GoalDescription"].ToString();
            model.Duration = reader["Duration"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Duration"]);
            model.Frequency = reader["Frequency"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Frequency"]);
            model.StartDate = Convert.ToDateTime(reader["StartDate"]);
            model.EndDate = Convert.ToDateTime(reader["EndDate"]);
            model.Comments = reader["Comments"] == DBNull.Value ? null : reader["Comments"].ToString();

            return model;
        }

        private WorkoutProgramDetailModel MapWorkoutProgramDetail(SqlDataReader reader)
        {
            WorkoutProgramDetailModel model = new WorkoutProgramDetailModel();

            model.Id = Convert.ToInt64(reader["Id"]);
            model.ProgramId = Convert.ToInt64(reader["ProgramId"]);
            model.DayOfWeek = reader["DayOfWeek"] == DBNull.Value ? null : reader["DayOfWeek"].ToString();
            model.MuscleGroup = reader["MuscleGroup"] == DBNull.Value ? null : reader["MuscleGroup"].ToString();
            model.ExerciseCode = reader["ExerciseCode"] == DBNull.Value ? null : reader["ExerciseCode"].ToString();
            model.ExerciseName = reader["ExerciseName"] == DBNull.Value ? null : reader["ExerciseName"].ToString();
            model.Sets = reader["Sets"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Sets"]);
            model.Reps = reader["Reps"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Reps"]);
            model.RestTime = reader["RestTime"] == DBNull.Value ? null : reader["RestTime"].ToString();

            return model;
        }

        private WorkoutProgramPrintHeaderModel MapWorkoutProgramPrintHeader(SqlDataReader reader)
        {
            WorkoutProgramPrintHeaderModel model = new WorkoutProgramPrintHeaderModel();

            model.ProgramId = Convert.ToInt64(reader["ProgramId"]);
            model.CustomerId = Convert.ToInt64(reader["CustomerId"]);
            model.LastName = reader["LastName"] == DBNull.Value ? null : reader["LastName"].ToString();
            model.FirstName = reader["FirstName"] == DBNull.Value ? null : reader["FirstName"].ToString();
            model.Goal = reader["Goal"] == DBNull.Value ? null : reader["Goal"].ToString();
            model.Frequency = reader["Frequency"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Frequency"]);
            model.Duration = reader["Duration"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Duration"]);
            model.StartDate = Convert.ToDateTime(reader["StartDate"]);
            model.EndDate = Convert.ToDateTime(reader["EndDate"]);
            model.Comments = reader["Comments"] == DBNull.Value ? null : reader["Comments"].ToString();

            return model;
        }

        private WorkoutProgramPrintDetailModel MapWorkoutProgramPrintDetail(SqlDataReader reader)
        {
            WorkoutProgramPrintDetailModel model = new WorkoutProgramPrintDetailModel();

            model.DayOfWeek = reader["DayOfWeek"] == DBNull.Value ? null : reader["DayOfWeek"].ToString();
            model.MuscleGroup = reader["MuscleGroup"] == DBNull.Value ? null : reader["MuscleGroup"].ToString();
            model.ExerciseName = reader["ExerciseName"] == DBNull.Value ? null : reader["ExerciseName"].ToString();
            model.Sets = reader["Sets"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Sets"]);
            model.Reps = reader["Reps"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Reps"]);
            model.RestTime = reader["RestTime"] == DBNull.Value ? null : reader["RestTime"].ToString();

            return model;
        }
    }
}