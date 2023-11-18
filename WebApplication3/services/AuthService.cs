using System.Data;
using Microsoft.Data.SqlClient;

namespace WebApplication3.services;


    public class AuthService
    {

        private readonly string _connectionString;

        public AuthService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool ValidateUserCredentials(string username, string password)
        {

        
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("ValidateUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

         
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    int userCount = (int)command.ExecuteScalar();

                    return userCount > 0;
                }
            }
        }
        public string GetUserRole(string username)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

        
                using (SqlCommand command = new SqlCommand("GetUserRole", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Username", username);

               
                    var result = command.ExecuteScalar();

                   
                    if (result != null)
                    {
                        return result.ToString();
                    }
                }
            }
            
            return "";
        }

        
      
    }

