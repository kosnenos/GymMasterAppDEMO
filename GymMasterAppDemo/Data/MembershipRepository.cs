using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GymMasterAppDemo.Models;

namespace GymMasterAppDemo.Data
{
    public class MembershipRepository : IMembershipRepository
    {
        private readonly string _connectionString;

        public MembershipRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<MembershipModel> GetByCustomerId(long customerId)
        {
            var memberships = new List<MembershipModel>();

            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                                SELECT 
                                    m.Id,
                                    m.CustomerId,
                                    m.ServiceCode,
                                    m.MembershipType,
                                    m.Duration,
                                    m.StartDate,
                                    m.EndDate,
                                    m.Price,
                                    m.StatusCode,
                                    m.CreationDate,
                                    m.Comment,
                                    mtl.Description AS MembershipTypeDescription,
                                    sl.ServiceDescription,
                                    st.StatusDesc AS StatusDescriptionText,
                                    ISNULL(SUM(p.Amount), 0) AS TotalPaid
                                FROM Membership m
                                LEFT JOIN Payments p ON p.MembershipId = m.Id
                                LEFT JOIN MembershipTypeList mtl ON mtl.MembershipType = m.MembershipType
                                LEFT JOIN ServicesList sl ON sl.ServiceCode = m.ServiceCode
                                LEFT JOIN StatusList st ON st.StatusCode = m.StatusCode
                                WHERE m.CustomerId = @CustomerId
                                GROUP BY
                                    m.Id,
                                    m.CustomerId,
                                    m.ServiceCode,
                                    m.MembershipType,
                                    m.Duration,
                                    m.StartDate,
                                    m.EndDate,
                                    m.Price,
                                    m.StatusCode,
                                    m.CreationDate,
                                    m.Comment,
                                    mtl.Description,
                                    sl.ServiceDescription,
                                    st.StatusDesc
                                ORDER BY m.StartDate DESC";

                command.Parameters.Add("@CustomerId", SqlDbType.BigInt).Value = customerId;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        memberships.Add(MapMembership(reader));
                    }
                }
            }

            return memberships;
        }

        public List<MembershipModel> SearchByCustomer(string searchValue)
        {
            var memberships = new List<MembershipModel>();
            bool isNumeric = long.TryParse(searchValue, out long customerId);

            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                                    SELECT 
                                        m.Id,
                                        m.CustomerId,
                                        m.ServiceCode,
                                        m.MembershipType,
                                        m.Duration,
                                        m.StartDate,
                                        m.EndDate,
                                        m.Price,
                                        m.StatusCode,
                                        m.CreationDate,
                                        m.Comment,
                                        mtl.Description AS MembershipTypeDescription,
                                        sl.ServiceDescription,
                                        st.StatusDesc AS StatusDescriptionText,
                                        ISNULL(SUM(p.Amount), 0) AS TotalPaid
                                    FROM Membership m
                                    INNER JOIN Customers c ON c.Id = m.CustomerId
                                    LEFT JOIN Payments p ON p.MembershipId = m.Id
                                    LEFT JOIN MembershipTypeList mtl ON mtl.MembershipType = m.MembershipType
                                    LEFT JOIN ServicesList sl ON sl.ServiceCode = m.ServiceCode
                                    LEFT JOIN StatusList st ON st.StatusCode = m.StatusCode
                                    WHERE
                                        c.LastName LIKE @LastName + '%'
                                        OR (@IsNumeric = 1 AND m.CustomerId = @CustomerId)
                                    GROUP BY
                                        m.Id,
                                        m.CustomerId,
                                        m.ServiceCode,
                                        m.MembershipType,
                                        m.Duration,
                                        m.StartDate,
                                        m.EndDate,
                                        m.Price,
                                        m.StatusCode,
                                        m.CreationDate,
                                        m.Comment,
                                        mtl.Description,
                                        sl.ServiceDescription,
                                        st.StatusDesc
                                    ORDER BY m.StartDate DESC";

                command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = searchValue ?? string.Empty;
                command.Parameters.Add("@IsNumeric", SqlDbType.Bit).Value = isNumeric;
                command.Parameters.Add("@CustomerId", SqlDbType.BigInt).Value = isNumeric ? customerId : 0;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        memberships.Add(MapMembership(reader));
                    }
                }
            }

            return memberships;
        }

        public MembershipModel GetById(long membershipId)
        {
            MembershipModel membership = null;

            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                                    SELECT 
                                        m.Id,
                                        m.CustomerId,
                                        m.ServiceCode,
                                        m.MembershipType,
                                        m.Duration,
                                        m.StartDate,
                                        m.EndDate,
                                        m.Price,
                                        m.StatusCode,
                                        m.CreationDate,
                                        m.Comment,
                                        mtl.Description AS MembershipTypeDescription,
                                        sl.ServiceDescription,
                                        st.StatusDesc AS StatusDescriptionText,
                                        ISNULL(SUM(p.Amount), 0) AS TotalPaid
                                    FROM Membership m
                                    LEFT JOIN Payments p ON p.MembershipId = m.Id
                                    LEFT JOIN MembershipTypeList mtl ON mtl.MembershipType = m.MembershipType
                                    LEFT JOIN ServicesList sl ON sl.ServiceCode = m.ServiceCode
                                    LEFT JOIN StatusList st ON st.StatusCode = m.StatusCode
                                    WHERE m.Id = @Id
                                    GROUP BY
                                        m.Id,
                                        m.CustomerId,
                                        m.ServiceCode,
                                        m.MembershipType,
                                        m.Duration,
                                        m.StartDate,
                                        m.EndDate,
                                        m.Price,
                                        m.StatusCode,
                                        m.CreationDate,
                                        m.Comment,
                                        mtl.Description,
                                        sl.ServiceDescription,
                                        st.StatusDesc";

                command.Parameters.Add("@Id", SqlDbType.BigInt).Value = membershipId;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        membership = MapMembership(reader);
                    }
                }
            }

            return membership;
        }

        public bool HasOverlappingMembership(long customerId, DateTime startDate, DateTime endDate)
        {
            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    SELECT COUNT(1)
                    FROM Membership
                    WHERE CustomerId = @CustomerId
                      AND StartDate < @EndDate
                      AND @StartDate < EndDate";

                command.Parameters.Add("@CustomerId", SqlDbType.BigInt).Value = customerId;
                command.Parameters.Add("@StartDate", SqlDbType.Date).Value = startDate.Date;
                command.Parameters.Add("@EndDate", SqlDbType.Date).Value = endDate.Date;

                return Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
        }

        public bool HasOverlappingMembership(long customerId, DateTime startDate, DateTime endDate, long excludeMembershipId)
        {
            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    SELECT COUNT(1)
                    FROM Membership
                    WHERE CustomerId = @CustomerId
                      AND Id <> @ExcludeMembershipId
                      AND StartDate < @EndDate
                      AND @StartDate < EndDate";

                command.Parameters.Add("@CustomerId", SqlDbType.BigInt).Value = customerId;
                command.Parameters.Add("@ExcludeMembershipId", SqlDbType.BigInt).Value = excludeMembershipId;
                command.Parameters.Add("@StartDate", SqlDbType.Date).Value = startDate.Date;
                command.Parameters.Add("@EndDate", SqlDbType.Date).Value = endDate.Date;

                return Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
        }

        public long Insert(MembershipModel membership)
        {
            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    INSERT INTO Membership
                    (
                        CustomerId,
                        ServiceCode,
                        MembershipType,
                        Duration,
                        StartDate,
                        EndDate,
                        Price,
                        StatusCode,
                        Comment
                    )
                    VALUES
                    (
                        @CustomerId,
                        @ServiceCode,
                        @MembershipType,
                        @Duration,
                        @StartDate,
                        @EndDate,
                        @Price,
                        @StatusCode,
                        @Comment
                    );

                    SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";

                FillMembershipParameters(command, membership);

                return Convert.ToInt64(command.ExecuteScalar());
            }
        }

        public void Update(MembershipModel membership)
        {
            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    UPDATE Membership
                    SET
                        CustomerId = @CustomerId,
                        ServiceCode = @ServiceCode,
                        MembershipType = @MembershipType,
                        Duration = @Duration,
                        StartDate = @StartDate,
                        EndDate = @EndDate,
                        Price = @Price,
                        StatusCode = @StatusCode,
                        Comment = @Comment
                    WHERE Id = @Id";

                FillMembershipParameters(command, membership);
                command.Parameters.Add("@Id", SqlDbType.BigInt).Value = membership.Id;

                command.ExecuteNonQuery();
            }
        }

        public void UpdateStatus(long membershipId, string statusCode)
        {
            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    UPDATE Membership
                    SET StatusCode = @StatusCode
                    WHERE Id = @Id";

                command.Parameters.Add("@Id", SqlDbType.BigInt).Value = membershipId;
                command.Parameters.Add("@StatusCode", SqlDbType.VarChar, 5).Value = statusCode;

                command.ExecuteNonQuery();
            }
        }

        public void Delete(long membershipId)
        {
            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            {
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    using (SqlCommand deletePaymentsCommand = new SqlCommand())
                    {
                        deletePaymentsCommand.Connection = connection;
                        deletePaymentsCommand.Transaction = transaction;
                        deletePaymentsCommand.CommandText = @"
                            DELETE FROM Payments
                            WHERE MembershipId = @MembershipId";

                        deletePaymentsCommand.Parameters.Add("@MembershipId", SqlDbType.BigInt).Value = membershipId;
                        deletePaymentsCommand.ExecuteNonQuery();
                    }

                    using (SqlCommand deleteMembershipCommand = new SqlCommand())
                    {
                        deleteMembershipCommand.Connection = connection;
                        deleteMembershipCommand.Transaction = transaction;
                        deleteMembershipCommand.CommandText = @"
                            DELETE FROM Membership
                            WHERE Id = @Id";

                        deleteMembershipCommand.Parameters.Add("@Id", SqlDbType.BigInt).Value = membershipId;
                        deleteMembershipCommand.ExecuteNonQuery();
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

        private void FillMembershipParameters(SqlCommand command, MembershipModel membership)
        {
            command.Parameters.Clear();

            command.Parameters.Add("@CustomerId", SqlDbType.BigInt).Value = membership.CustomerId;
            command.Parameters.Add("@ServiceCode", SqlDbType.VarChar, 10).Value = (object)membership.ServiceCode ?? DBNull.Value;
            command.Parameters.Add("@MembershipType", SqlDbType.VarChar, 10).Value = (object)membership.MembershipType ?? DBNull.Value;
            command.Parameters.Add("@Duration", SqlDbType.Int).Value = membership.Duration;
            command.Parameters.Add("@StartDate", SqlDbType.Date).Value = membership.StartDate.Date;
            command.Parameters.Add("@EndDate", SqlDbType.Date).Value = membership.EndDate.Date;

            SqlParameter priceParameter = command.Parameters.Add("@Price", SqlDbType.Decimal);
            priceParameter.Precision = 18;
            priceParameter.Scale = 2;
            priceParameter.Value = membership.Price;

            command.Parameters.Add("@StatusCode", SqlDbType.VarChar, 5).Value = (object)membership.StatusCode ?? DBNull.Value;
            command.Parameters.Add("@Comment", SqlDbType.NVarChar).Value = (object)membership.Comment ?? DBNull.Value;
        }

        private MembershipModel MapMembership(SqlDataReader reader)
        {
            MembershipModel membership = new MembershipModel();

            membership.Id = Convert.ToInt64(reader["Id"]);
            membership.CustomerId = Convert.ToInt64(reader["CustomerId"]);
            membership.ServiceCode = reader["ServiceCode"] == DBNull.Value ? null : reader["ServiceCode"].ToString();
            membership.MembershipType = reader["MembershipType"] == DBNull.Value ? null : reader["MembershipType"].ToString();
            membership.Duration = Convert.ToInt32(reader["Duration"]);
            membership.StartDate = Convert.ToDateTime(reader["StartDate"]);
            membership.EndDate = Convert.ToDateTime(reader["EndDate"]);
            membership.Price = Convert.ToDecimal(reader["Price"]);
            membership.StatusCode = reader["StatusCode"] == DBNull.Value ? null : reader["StatusCode"].ToString();
            membership.CreationDate = Convert.ToDateTime(reader["CreationDate"]);
            membership.Comment = reader["Comment"] == DBNull.Value ? null : reader["Comment"].ToString();
            membership.TotalPaid = reader["TotalPaid"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotalPaid"]);

            membership.MembershipTypeDescription = reader["MembershipTypeDescription"] == DBNull.Value
                ? string.Empty
                : reader["MembershipTypeDescription"].ToString();

            membership.ServiceDescription = reader["ServiceDescription"] == DBNull.Value
                ? string.Empty
                : reader["ServiceDescription"].ToString();

            membership.StatusDescriptionText = reader["StatusDescriptionText"] == DBNull.Value
                ? string.Empty
                : reader["StatusDescriptionText"].ToString();

            return membership;
        }
    }
}