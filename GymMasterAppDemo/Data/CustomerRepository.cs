using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GymMasterAppDemo.Models;

namespace GymMasterAppDemo.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<CustomerListModel> GetActiveCustomers()
        {
            var customers = new List<CustomerListModel>();

            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    SELECT 
                        c.Id,
                        c.LastName,
                        c.FirstName,
                        c.FatherName,
                        c.Gender,
                        c.Birthday,
                        o.OccupationDesc AS OccupationDescription,
                        c.HealthId,
                        c.Address,
                        c.City,
                        c.Mobile,
                        c.Home,
                        c.Email,
                        c.CreationDate,
                        c.Comments
                    FROM Customers c
                    LEFT JOIN OccupationList o ON c.OccupationId = o.OccupationId
                    WHERE c.Status = 1
                    ORDER BY c.Id DESC";

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(MapListCustomer(reader));
                    }
                }
            }

            return customers;
        }

        public List<CustomerListModel> SearchActiveCustomers(string searchValue)
        {
            var customers = new List<CustomerListModel>();
            bool isNumeric = long.TryParse(searchValue, out long customerId);

            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    SELECT 
                        c.Id,
                        c.LastName,
                        c.FirstName,
                        c.FatherName,
                        c.Gender,
                        c.Birthday,
                        o.OccupationDesc AS OccupationDescription,
                        c.HealthId,
                        c.Address,
                        c.City,
                        c.Mobile,
                        c.Home,
                        c.Email,
                        c.CreationDate,
                        c.Comments
                    FROM Customers c
                    LEFT JOIN OccupationList o ON c.OccupationId = o.OccupationId
                    WHERE c.Status = 1
                      AND (
                            c.LastName LIKE @LastName + '%'
                            OR (@IsNumeric = 1 AND c.Id = @Id)
                          )
                    ORDER BY c.Id DESC";

                command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = searchValue ?? string.Empty;
                command.Parameters.Add("@IsNumeric", SqlDbType.Bit).Value = isNumeric;
                command.Parameters.Add("@Id", SqlDbType.BigInt).Value = isNumeric ? customerId : 0;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(MapListCustomer(reader));
                    }
                }
            }

            return customers;
        }

        public CustomerModel GetById(long id)
        {
            CustomerModel customer = null;

            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    SELECT 
                        Id,
                        LastName,
                        FirstName,
                        FatherName,
                        Birthday,
                        Gender,
                        OccupationId,
                        HealthId,
                        Address,
                        City,
                        Mobile,
                        Home,
                        Email,
                        Status,
                        CreationDate,
                        Comments
                    FROM Customers
                    WHERE Id = @Id";

                command.Parameters.Add("@Id", SqlDbType.BigInt).Value = id;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        customer = new CustomerModel
                        {
                            Id = Convert.ToInt64(reader["Id"]),
                            LastName = reader["LastName"]?.ToString(),
                            FirstName = reader["FirstName"]?.ToString(),
                            FatherName = reader["FatherName"]?.ToString(),
                            Birthday = reader["Birthday"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["Birthday"]),
                            Gender = reader["Gender"]?.ToString(),
                            OccupationId = reader["OccupationId"]?.ToString(),
                            HealthId = reader["HealthId"] == DBNull.Value ? (long?)null : Convert.ToInt64(reader["HealthId"]),
                            Address = reader["Address"]?.ToString(),
                            City = reader["City"]?.ToString(),
                            Mobile = reader["Mobile"]?.ToString(),
                            Home = reader["Home"]?.ToString(),
                            Email = reader["Email"]?.ToString(),
                            Status = Convert.ToInt32(reader["Status"]) == 1,
                            CreationDate = Convert.ToDateTime(reader["CreationDate"]),
                            Comments = reader["Comments"]?.ToString()
                        };
                    }
                }
            }

            return customer;
        }

        public bool CustomerIdExists(long id)
        {
            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT COUNT(1) FROM Customers WHERE Id = @Id";
                command.Parameters.Add("@Id", SqlDbType.BigInt).Value = id;

                return Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
        }

        public void Add(CustomerModel customer)
        {
            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
            INSERT INTO Customers
            (
                Id, LastName, FirstName, FatherName, Birthday, Gender, OccupationId,
                HealthId, Address, City, Mobile, Home, Email, Status,
                CreationDate, Comments
            )
            VALUES
            (
                @Id, @LastName, @FirstName, @FatherName, @Birthday, @Gender, @OccupationId,
                @HealthId, @Address, @City, @Mobile, @Home, @Email, 1,
                @CreationDate, @Comments
            )";

                FillCustomerParameters(command, customer);
                command.ExecuteNonQuery();
            }
        }

        public void Edit(CustomerModel customer)
        {
            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
            UPDATE Customers
            SET
                LastName = @LastName,
                FirstName = @FirstName,
                FatherName = @FatherName,
                Birthday = @Birthday,
                Gender = @Gender,
                OccupationId = @OccupationId,
                HealthId = @HealthId,
                Address = @Address,
                City = @City,
                Mobile = @Mobile,
                Home = @Home,
                Email = @Email,
                Status = @Status,
                CreationDate = @CreationDate,
                Comments = @Comments
            WHERE Id = @Id";

                FillCustomerParameters(command, customer);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateHealthId(long customerId, long healthId)
        {
            using (SqlConnection connection = Db.CreateConnection(_connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
            UPDATE Customers
            SET HealthId = @HealthId
            WHERE Id = @CustomerId";

                command.Parameters.Add("@HealthId", SqlDbType.BigInt).Value = healthId;
                command.Parameters.Add("@CustomerId", SqlDbType.BigInt).Value = customerId;
                command.ExecuteNonQuery();
            }
        }

        private void FillCustomerParameters(SqlCommand command, CustomerModel customer)
        {
            command.Parameters.Clear();

            command.Parameters.Add("@Id", SqlDbType.BigInt).Value = customer.Id;
            command.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = customer.LastName;
            command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = customer.FirstName;
            command.Parameters.Add("@FatherName", SqlDbType.NVarChar, 50).Value = (object)customer.FatherName ?? DBNull.Value;
            command.Parameters.Add("@Birthday", SqlDbType.Date).Value = (object)customer.Birthday ?? DBNull.Value;
            command.Parameters.Add("@Gender", SqlDbType.NChar, 1).Value = (object)customer.Gender ?? DBNull.Value;
            command.Parameters.Add("@OccupationId", SqlDbType.NVarChar, 4).Value = (object)customer.OccupationId ?? DBNull.Value;
            command.Parameters.Add("@HealthId", SqlDbType.BigInt).Value = (object)customer.HealthId ?? DBNull.Value;
            command.Parameters.Add("@Address", SqlDbType.NVarChar, 100).Value = (object)customer.Address ?? DBNull.Value;
            command.Parameters.Add("@City", SqlDbType.NVarChar, 50).Value = (object)customer.City ?? DBNull.Value;
            command.Parameters.Add("@Mobile", SqlDbType.NVarChar, 10).Value = customer.Mobile;
            command.Parameters.Add("@Home", SqlDbType.NVarChar, 10).Value = (object)customer.Home ?? DBNull.Value;
            command.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = customer.Email;
            command.Parameters.Add("@Status", SqlDbType.Bit).Value = customer.Status;
            command.Parameters.Add("@CreationDate", SqlDbType.DateTime).Value = customer.CreationDate;
            command.Parameters.Add("@Comments", SqlDbType.NVarChar).Value = (object)customer.Comments ?? DBNull.Value;
        }

        private CustomerListModel MapListCustomer(SqlDataReader reader)
        {
            return new CustomerListModel
            {
                Id = Convert.ToInt64(reader["Id"]),
                LastName = reader["LastName"]?.ToString(),
                FirstName = reader["FirstName"]?.ToString(),
                FatherName = reader["FatherName"]?.ToString(),
                Gender = reader["Gender"]?.ToString(),
                Birthday = reader["Birthday"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["Birthday"]),
                OccupationDescription = reader["OccupationDescription"]?.ToString(),
                HealthId = reader["HealthId"] == DBNull.Value ? (long?)null : Convert.ToInt64(reader["HealthId"]),
                Address = reader["Address"]?.ToString(),
                City = reader["City"]?.ToString(),
                Mobile = reader["Mobile"]?.ToString(),
                Home = reader["Home"]?.ToString(),
                Email = reader["Email"]?.ToString(),
                CreationDate = Convert.ToDateTime(reader["CreationDate"]),
                Comments = reader["Comments"]?.ToString()
            };
        }
    }
}