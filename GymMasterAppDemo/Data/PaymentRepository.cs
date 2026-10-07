using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GymMasterAppDemo.Models;

namespace GymMasterAppDemo.Data
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly string _connectionString;

        public PaymentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<PaymentModel> GetByMembershipId(long membershipId)
        {
            List<PaymentModel> payments = new List<PaymentModel>();

            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                                    SELECT
                                        p.Id,
                                        p.MembershipId,
                                        p.PaymentDate,
                                        p.PaymentTime,
                                        p.MethodType,
                                        p.Amount,
                                        p.CreationDate,
                                        p.Comment,
                                        pm.MethodDescription AS MethodDescriptionText
                                    FROM Payments p
                                    LEFT JOIN PayMethodList pm ON pm.MethodType = p.MethodType
                                    WHERE p.MembershipId = @MembershipId
                                    ORDER BY p.PaymentDate DESC, p.PaymentTime DESC, p.Id DESC";

                command.Parameters.Add("@MembershipId", SqlDbType.BigInt).Value = membershipId;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PaymentModel payment = new PaymentModel();
                        payment.Id = Convert.ToInt64(reader["Id"]);
                        payment.MembershipId = Convert.ToInt64(reader["MembershipId"]);
                        payment.PaymentDate = Convert.ToDateTime(reader["PaymentDate"]);
                        payment.PaymentTime = (TimeSpan)reader["PaymentTime"];
                        payment.MethodType = reader["MethodType"] == DBNull.Value ? null : reader["MethodType"].ToString();
                        payment.Amount = Convert.ToDecimal(reader["Amount"]);
                        payment.CreationDate = Convert.ToDateTime(reader["CreationDate"]);
                        payment.Comment = reader["Comment"] == DBNull.Value ? null : reader["Comment"].ToString();
                        payment.MethodDescriptionText = reader["MethodDescriptionText"] == DBNull.Value
                            ? string.Empty
                            : reader["MethodDescriptionText"].ToString();

                        payments.Add(payment);
                    }
                }
            }

            return payments;
        }

        public PaymentModel GetById(long paymentId)
        {
            PaymentModel payment = null;

            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    SELECT
                        Id,
                        MembershipId,
                        PaymentDate,
                        PaymentTime,
                        MethodType,
                        Amount,
                        CreationDate,
                        Comment
                    FROM Payments
                    WHERE Id = @Id";

                command.Parameters.Add("@Id", SqlDbType.BigInt).Value = paymentId;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        payment = MapPayment(reader);
                    }
                }
            }

            return payment;
        }

        public long Insert(PaymentModel payment)
        {
            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    INSERT INTO Payments
                    (
                        MembershipId,
                        PaymentDate,
                        PaymentTime,
                        MethodType,
                        Amount,
                        CreationDate,
                        Comment
                    )
                    VALUES
                    (
                        @MembershipId,
                        @PaymentDate,
                        @PaymentTime,
                        @MethodType,
                        @Amount,
                        @CreationDate,
                        @Comment
                    );

                    SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";

                FillPaymentParameters(command, payment);

                return Convert.ToInt64(command.ExecuteScalar());
            }
        }

        public decimal GetTotalPaidByMembershipId(long membershipId)
        {
            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    SELECT ISNULL(SUM(Amount), 0)
                    FROM Payments
                    WHERE MembershipId = @MembershipId";

                command.Parameters.Add("@MembershipId", SqlDbType.BigInt).Value = membershipId;

                return Convert.ToDecimal(command.ExecuteScalar());
            }
        }

        public void DeleteByMembershipId(long membershipId)
        {
            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    DELETE FROM Payments
                    WHERE MembershipId = @MembershipId";

                command.Parameters.Add("@MembershipId", SqlDbType.BigInt).Value = membershipId;
                command.ExecuteNonQuery();
            }
        }

        private void FillPaymentParameters(SqlCommand command, PaymentModel payment)
        {
            command.Parameters.Clear();

            command.Parameters.Add("@MembershipId", SqlDbType.BigInt).Value = payment.MembershipId;
            command.Parameters.Add("@PaymentDate", SqlDbType.Date).Value = payment.PaymentDate.Date;
            command.Parameters.Add("@PaymentTime", SqlDbType.Time).Value = payment.PaymentTime;
            command.Parameters.Add("@MethodType", SqlDbType.VarChar, 5).Value = (object)payment.MethodType ?? DBNull.Value;

            SqlParameter amountParameter = command.Parameters.Add("@Amount", SqlDbType.Decimal);
            amountParameter.Precision = 18;
            amountParameter.Scale = 2;
            amountParameter.Value = payment.Amount;

            command.Parameters.Add("@CreationDate", SqlDbType.DateTime).Value = payment.CreationDate;
            command.Parameters.Add("@Comment", SqlDbType.NVarChar).Value = (object)payment.Comment ?? DBNull.Value;
        }

        private PaymentModel MapPayment(SqlDataReader reader)
        {
            return new PaymentModel
            {
                Id = Convert.ToInt64(reader["Id"]),
                MembershipId = Convert.ToInt64(reader["MembershipId"]),
                PaymentDate = Convert.ToDateTime(reader["PaymentDate"]),
                PaymentTime = (TimeSpan)reader["PaymentTime"],
                MethodType = reader["MethodType"] == DBNull.Value ? null : reader["Method"].ToString(),
                Amount = Convert.ToDecimal(reader["Amount"]),
                CreationDate = Convert.ToDateTime(reader["CreationDate"]),
                Comment = reader["Comment"] == DBNull.Value ? null : reader["Comment"].ToString()
            };
        }

        public void Update(PaymentModel payment)
        {
            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
            UPDATE Payments
            SET
                PaymentDate = @PaymentDate,
                PaymentTime = @PaymentTime,
                MethodType = @MethodType,
                Amount = @Amount
            WHERE Id = @Id";

                command.Parameters.Add("@Id", SqlDbType.BigInt).Value = payment.Id;
                command.Parameters.Add("@PaymentDate", SqlDbType.Date).Value = payment.PaymentDate.Date;
                command.Parameters.Add("@PaymentTime", SqlDbType.Time).Value = payment.PaymentTime;
                command.Parameters.Add("@MethodType", SqlDbType.VarChar, 5).Value =
                    (object)payment.MethodType ?? DBNull.Value;

                SqlParameter amountParameter = command.Parameters.Add("@Amount", SqlDbType.Decimal);
                amountParameter.Precision = 18;
                amountParameter.Scale = 2;
                amountParameter.Value = payment.Amount;

                command.ExecuteNonQuery();
            }
        }

        public void Delete(long paymentId)
        {
            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
            DELETE FROM Payments
            WHERE Id = @Id";

                command.Parameters.Add("@Id", SqlDbType.BigInt).Value = paymentId;
                command.ExecuteNonQuery();
            }
        }
    }
}