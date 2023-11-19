using System.Data;
using Microsoft.Data.SqlClient;
using WebApplication3.Models;
using WebApplication3.Helper;


namespace WebApplication3.Repository;

public class CustomerRepository
{
    
    private readonly string _connectionString;

    public CustomerRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    public List<Customer> GetAllCustomers()
    {
        
        List<Customer> customers = new List<Customer>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            
            
            using (SqlCommand command = new SqlCommand("GetCustomers", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Customer customer = new Customer
                        {
                            
                            CustomerId = (int)reader["CustomerId"],
                            CustomerName = (string)reader["CustomerName"],
                            CustomerSurname = (string)reader["CustomerSurname"],
                            Address = (string)reader["Address"],
                            PostCode = (string)reader["PostCode"],
                            Country = (string)reader["Country"],
                            DateOfBirth = (DateTime)reader["DateOfBirth"]
                        };

                        customers.Add(customer);
                    }
                }
            }
        }

     

        return customers;
    }

    public  Customer getCtomerById(int id)
    {
        Customer customer = null;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            int customerId = id;

            using (SqlCommand command = new SqlCommand("GetCustomerById", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameter for CustomerId
                command.Parameters.AddWithValue("@CustomerId", customerId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customer = new Customer
                        {
                            CustomerId = (int)reader["CustomerId"],
                            CustomerName = (string)reader["CustomerName"],
                            CustomerSurname = (string)reader["CustomerSurname"],
                            Address = (string)reader["Address"],
                            PostCode = (string)reader["PostCode"],
                            Country = (string)reader["Country"],
                            DateOfBirth = (DateTime)reader["DateOfBirth"]
                        };

                     
                    }
                }
            }
        }

    
        return customer;
    }

    public void DeleteUser(int id)
    {
        // Call the stored procedure to delete the customer by ID
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            using (var command = new SqlCommand("DeleteCustomer", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                
                command.Parameters.AddWithValue("@CustomerId", id);
                
                command.ExecuteNonQuery();
            }
        }

    }
    public void SaveCustomer(Customer customer)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand("SaveCustomer", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
                command.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                command.Parameters.AddWithValue("@CustomerSurname", customer.CustomerSurname);
                command.Parameters.AddWithValue("@Address", customer.Address);
                command.Parameters.AddWithValue("@PostCode", customer.PostCode);
                command.Parameters.AddWithValue("@Country", customer.Country);
                //command.Parameters.AddWithValue("@DateOfBirth", customer.DateOfBirth);
                command.ExecuteNonQuery();
            }
        }
    }

    public List<CallWithCustomerInfo> GetAllCallsWithCustomers()
    {
        List<CallWithCustomerInfo> calls = new List<CallWithCustomerInfo>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand("GetAllCallsWithCustomers", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CallWithCustomerInfo call = new CallWithCustomerInfo
                        {
                            CallId = (int)reader["CallID"],
                            DateOfCall = (DateTime)reader["DateOfCall"],
                            TimeOfCall = (TimeSpan)reader["TimeOfCall"],
                            Subject = (string)reader["Subject"],
                            Description = (string)reader["Description"],
                            CustomerId = (int)reader["CustomerId"],
                            CustomerName = (string)reader["CustomerName"],
                            CustomerSurname = (string)reader["CustomerSurname"],
                            Address = (string)reader["Address"],
                            PostCode = (string)reader["PostCode"],
                            Country = (string)reader["Country"],
                            DateOfBirth = (DateTime)reader["DateOfBirth"]
                        };

                        calls.Add(call);
                    }
                }
            }
        }

        return calls;
    }

    public void DeleteCall(int id)
    {
        // Call the stored procedure to delete the customer by ID
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            using (var command = new SqlCommand("DeleteCall", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                
                command.Parameters.AddWithValue("@CallId", id);
                
                command.ExecuteNonQuery();
            }
        }
    }

    public Call getCallById(int id)
    {
        Call call = null;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            int callId = id;

            using (SqlCommand command = new SqlCommand("GetByCallById", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameter for CustomerId
                command.Parameters.AddWithValue("@CallId", callId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        call = new Call
                        {
                            CallId = (int)reader["CallId"],
                            CustomerId = (int)reader["CustomerId"],
                            TimeOfCall = (TimeSpan)reader["TimeOfCall"],
                            DateOfCall = (DateTime)reader["DateOfCall"],
                            Subject = (string)reader["Subject"],
                            Description = (string)reader["Description"],
                     
                        };

                     
                    }
                }
            }
        }

    
        return call;
    }

    public void SaveCall(Call call)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand("SaveCall", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@CallId", call.CallId);
                command.Parameters.AddWithValue("@DateOfCall", call.DateOfCall);
                command.Parameters.AddWithValue("@TimeOfCall", call.TimeOfCall);
                command.Parameters.AddWithValue("@Subject", call.Subject);
                command.Parameters.AddWithValue("@Description", call.Description);
                command.Parameters.AddWithValue("@CustomerId", call.CustomerId);
                command.ExecuteNonQuery();
            }
        }
    }


    public void AddNewCustomer(Customer customer)
    {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("InsertCustomer", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                    cmd.Parameters.AddWithValue("@CustomerSurname", customer.CustomerSurname);
                    cmd.Parameters.AddWithValue("@Address", customer.Address);
                    cmd.Parameters.AddWithValue("@PostCode", customer.PostCode);
                    cmd.Parameters.AddWithValue("@Country", customer.Country);
                    cmd.Parameters.AddWithValue("@DateOfBirth", customer.DateOfBirth);

                    cmd.ExecuteNonQuery();

                }
            
        }

    }

    public void AddNewCall(Call call)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            using (SqlCommand cmd = new SqlCommand("InsertCall", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CustomerId", call.CustomerId);
                cmd.Parameters.AddWithValue("@DateOfCall", call.DateOfCall);
                cmd.Parameters.AddWithValue("@TimeOfCall", call.TimeOfCall);
                cmd.Parameters.AddWithValue("@Subject", call.Subject);
                cmd.Parameters.AddWithValue("@Description", call.Description);

                cmd.ExecuteNonQuery();
            }

        }
    }
}
