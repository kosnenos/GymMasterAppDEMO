using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GymMasterAppDemo.Models;

namespace GymMasterAppDemo.Data
{
    public class AnalyticsRepository : IAnalyticsRepository
    {
        private readonly string _connectionString;

        public AnalyticsRepository()
        {
            _connectionString = DbConfig.ConnectionString;
        }

        private SqlConnection CreateConnection()
        {
            return Db.CreateConnection(_connectionString);
        }

        public List<RecentCustomerReportItem> GetRecentCustomers()
        {
            var items = new List<RecentCustomerReportItem>();

            const string sql = @"
                    SELECT
                        c.Id AS CustomerId,
                        LTRIM(RTRIM(c.LastName + ' ' + c.FirstName)) AS FullName,
                        ISNULL(c.Mobile, '') AS MobilePhone,
                        CAST(c.CreationDate AS date) AS RegistrationDate
                    FROM Customers c
                    WHERE CAST(c.CreationDate AS date) >= DATEADD(MONTH, -1, CAST(GETDATE() AS date))
                    ORDER BY c.CreationDate DESC, c.Id DESC;";

            using (var con = CreateConnection())
            using (var cmd = new SqlCommand(sql, con))
            {
                con.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        items.Add(new RecentCustomerReportItem
                        {
                            CustomerId = Convert.ToInt64(reader["CustomerId"]),
                            FullName = Convert.ToString(reader["FullName"]),
                            MobilePhone = Convert.ToString(reader["MobilePhone"]),
                            RegistrationDate = Convert.ToDateTime(reader["RegistrationDate"])
                        });
                    }
                }
            }

            return items;
        }

        public List<ExpiringTodayReportItem> GetExpiringTodayMemberships()
        {
            var items = new List<ExpiringTodayReportItem>();

            const string sql = @"
                            SELECT
                                m.Id AS MembershipId,
                                c.Id AS CustomerId,
                                LTRIM(RTRIM(c.LastName + ' ' + c.FirstName)) AS CustomerName,
                                ISNULL(mt.Description, m.MembershipType) AS MembershipType,
                                CAST(m.StartDate AS date) AS StartDate,
                                CAST(m.Price AS decimal(18,2)) AS Price,
                                ISNULL(sl.StatusDesc, m.StatusCode) AS Status
                            FROM Membership m
                            INNER JOIN Customers c
                                ON c.Id = m.CustomerId
                            LEFT JOIN MembershipTypeList mt
                                ON mt.MembershipType = m.MembershipType
                            LEFT JOIN StatusList sl
                                ON sl.StatusCode = m.StatusCode
                            WHERE CAST(m.EndDate AS date) = CAST(GETDATE() AS date)
                            ORDER BY c.LastName, c.FirstName, m.Id DESC;";

            using (var con = CreateConnection())
            using (var cmd = new SqlCommand(sql, con))
            {
                con.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        items.Add(new ExpiringTodayReportItem
                        {
                            MembershipId = Convert.ToInt64(reader["MembershipId"]),
                            CustomerId = Convert.ToInt64(reader["CustomerId"]),
                            CustomerName = Convert.ToString(reader["CustomerName"]),
                            MembershipType = Convert.ToString(reader["MembershipType"]),
                            StartDate = Convert.ToDateTime(reader["StartDate"]),
                            Price = Convert.ToDecimal(reader["Price"]),
                            Status = Convert.ToString(reader["Status"])
                        });
                    }
                }
            }

            return items;
        }

        public List<UnpaidMembershipReportItem> GetUnpaidMemberships()
        {
            var items = new List<UnpaidMembershipReportItem>();

            const string sql = @"
                            SELECT
                                m.Id AS MembershipId,
                                c.Id AS CustomerId,
                                LTRIM(RTRIM(c.LastName + ' ' + c.FirstName)) AS CustomerName,
                                ISNULL(mt.Description, m.MembershipType) AS MembershipType,
                                CAST(m.EndDate AS date) AS EndDate,
                                CAST(m.Price AS decimal(18,2)) AS Price,
                                CAST(m.Price - ISNULL(SUM(p.Amount), 0) AS decimal(18,2)) AS Balance,
                                ISNULL(sl.StatusDesc, m.StatusCode) AS Status
                            FROM Membership m
                            INNER JOIN Customers c
                                ON c.Id = m.CustomerId
                            LEFT JOIN MembershipTypeList mt
                                ON mt.MembershipType = m.MembershipType
                            LEFT JOIN StatusList sl
                                ON sl.StatusCode = m.StatusCode
                            LEFT JOIN Payments p
                                ON p.MembershipId = m.Id
                            WHERE m.StatusCode = '0'
                            GROUP BY
                                m.Id,
                                c.Id,
                                c.LastName,
                                c.FirstName,
                                mt.Description,
                                m.MembershipType,
                                m.EndDate,
                                m.Price,
                                sl.StatusDesc,
                                m.StatusCode
                            HAVING (m.Price - ISNULL(SUM(p.Amount), 0)) > 0
                            ORDER BY m.EndDate ASC, c.LastName, c.FirstName, m.Id DESC;";

            using (var con = CreateConnection())
            using (var cmd = new SqlCommand(sql, con))
            {
                con.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        items.Add(new UnpaidMembershipReportItem
                        {
                            MembershipId = Convert.ToInt64(reader["MembershipId"]),
                            CustomerId = Convert.ToInt64(reader["CustomerId"]),
                            CustomerName = Convert.ToString(reader["CustomerName"]),
                            MembershipType = Convert.ToString(reader["MembershipType"]),
                            EndDate = Convert.ToDateTime(reader["EndDate"]),
                            Price = Convert.ToDecimal(reader["Price"]),
                            Balance = Convert.ToDecimal(reader["Balance"]),
                            Status = Convert.ToString(reader["Status"])
                        });
                    }
                }
            }

            return items;
        }
    }
}