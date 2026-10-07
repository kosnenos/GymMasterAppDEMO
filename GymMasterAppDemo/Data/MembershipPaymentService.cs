using System;
using System.Data;
using System.Data.SqlClient;
using GymMasterAppDemo.Models;

namespace GymMasterAppDemo.Data
{
    public class MembershipPaymentService
    {
        private readonly string _connectionString;

        public MembershipPaymentService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public long CreateMembershipWithFirstPayment(MembershipModel membership, PaymentModel payment)
        {
            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    long membershipId;

                    using (SqlCommand membershipCommand = new SqlCommand())
                    {
                        membershipCommand.Connection = connection;
                        membershipCommand.Transaction = transaction;
                        membershipCommand.CommandText = @"
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

                        membershipCommand.Parameters.Add("@CustomerId", SqlDbType.BigInt).Value = membership.CustomerId;
                        membershipCommand.Parameters.Add("@ServiceCode", SqlDbType.VarChar, 10).Value =
                            (object)membership.ServiceCode ?? DBNull.Value;
                        membershipCommand.Parameters.Add("@MembershipType", SqlDbType.VarChar, 10).Value =
                            (object)membership.MembershipType ?? DBNull.Value;
                        membershipCommand.Parameters.Add("@Duration", SqlDbType.Int).Value = membership.Duration;
                        membershipCommand.Parameters.Add("@StartDate", SqlDbType.Date).Value = membership.StartDate.Date;
                        membershipCommand.Parameters.Add("@EndDate", SqlDbType.Date).Value = membership.EndDate.Date;

                        SqlParameter priceParameter = membershipCommand.Parameters.Add("@Price", SqlDbType.Decimal);
                        priceParameter.Precision = 18;
                        priceParameter.Scale = 2;
                        priceParameter.Value = membership.Price;

                        membershipCommand.Parameters.Add("@StatusCode", SqlDbType.VarChar, 5).Value =
                            (object)membership.StatusCode ?? DBNull.Value;
                        membershipCommand.Parameters.Add("@Comment", SqlDbType.NVarChar).Value =
                            string.IsNullOrWhiteSpace(membership.Comment) ? (object)DBNull.Value : membership.Comment;

                        membershipId = Convert.ToInt64(membershipCommand.ExecuteScalar());
                    }

                    using (SqlCommand paymentCommand = new SqlCommand())
                    {
                        paymentCommand.Connection = connection;
                        paymentCommand.Transaction = transaction;
                        paymentCommand.CommandText = @"
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
                            )";

                        paymentCommand.Parameters.Add("@MembershipId", SqlDbType.BigInt).Value = membershipId;
                        paymentCommand.Parameters.Add("@PaymentDate", SqlDbType.Date).Value = payment.PaymentDate.Date;
                        paymentCommand.Parameters.Add("@PaymentTime", SqlDbType.Time).Value = payment.PaymentTime;
                        paymentCommand.Parameters.Add("@MethodType", SqlDbType.VarChar, 5).Value =
                            (object)payment.MethodType ?? DBNull.Value;

                        SqlParameter amountParameter = paymentCommand.Parameters.Add("@Amount", SqlDbType.Decimal);
                        amountParameter.Precision = 18;
                        amountParameter.Scale = 2;
                        amountParameter.Value = payment.Amount;

                        paymentCommand.Parameters.Add("@CreationDate", SqlDbType.DateTime).Value = payment.CreationDate;
                        paymentCommand.Parameters.Add("@Comment", SqlDbType.NVarChar).Value =
                            string.IsNullOrWhiteSpace(payment.Comment) ? (object)DBNull.Value : payment.Comment;

                        paymentCommand.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return membershipId;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void AddPaymentAndRefreshStatus(long membershipId, decimal membershipPrice, PaymentModel payment)
        {
            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    using (SqlCommand paymentCommand = new SqlCommand())
                    {
                        paymentCommand.Connection = connection;
                        paymentCommand.Transaction = transaction;
                        paymentCommand.CommandText = @"
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
                            )";

                        paymentCommand.Parameters.Add("@MembershipId", SqlDbType.BigInt).Value = membershipId;
                        paymentCommand.Parameters.Add("@PaymentDate", SqlDbType.Date).Value = payment.PaymentDate.Date;
                        paymentCommand.Parameters.Add("@PaymentTime", SqlDbType.Time).Value = payment.PaymentTime;
                        paymentCommand.Parameters.Add("@MethodType", SqlDbType.VarChar, 5).Value =
                            (object)payment.MethodType ?? DBNull.Value;

                        SqlParameter amountParameter = paymentCommand.Parameters.Add("@Amount", SqlDbType.Decimal);
                        amountParameter.Precision = 18;
                        amountParameter.Scale = 2;
                        amountParameter.Value = payment.Amount;

                        paymentCommand.Parameters.Add("@CreationDate", SqlDbType.DateTime).Value = payment.CreationDate;
                        paymentCommand.Parameters.Add("@Comment", SqlDbType.NVarChar).Value =
                            string.IsNullOrWhiteSpace(payment.Comment) ? (object)DBNull.Value : payment.Comment;

                        paymentCommand.ExecuteNonQuery();
                    }

                    decimal totalPaid;

                    using (SqlCommand totalPaidCommand = new SqlCommand())
                    {
                        totalPaidCommand.Connection = connection;
                        totalPaidCommand.Transaction = transaction;
                        totalPaidCommand.CommandText = @"
                            SELECT ISNULL(SUM(Amount), 0)
                            FROM Payments
                            WHERE MembershipId = @MembershipId";

                        totalPaidCommand.Parameters.Add("@MembershipId", SqlDbType.BigInt).Value = membershipId;
                        totalPaid = Convert.ToDecimal(totalPaidCommand.ExecuteScalar());
                    }

                    string newStatusCode = MembershipCalculationHelper.GetStatusAfterPayment(membershipPrice, totalPaid);

                    using (SqlCommand statusCommand = new SqlCommand())
                    {
                        statusCommand.Connection = connection;
                        statusCommand.Transaction = transaction;
                        statusCommand.CommandText = @"
                            UPDATE Membership
                            SET StatusCode = @StatusCode
                            WHERE Id = @MembershipId";

                        statusCommand.Parameters.Add("@StatusCode", SqlDbType.VarChar, 5).Value = newStatusCode;
                        statusCommand.Parameters.Add("@MembershipId", SqlDbType.BigInt).Value = membershipId;

                        statusCommand.ExecuteNonQuery();
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
    }
}