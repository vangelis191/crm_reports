using Microsoft.Data.SqlClient;
using WebApplication3.Models;

namespace WebApplication3.Repository;

public class EmployeeRepository
{
    
    private readonly string _connectionString;

    public EmployeeRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    public List<Employee> GetAllEmployees()
    {
        
        List<Employee> employees = new List<Employee>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            string query = "SELECT * FROM Employee";
            
            using (SqlCommand command = new SqlCommand("GetEmployees", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employee employee = new Employee
                        {
                            // Assuming columns "Id", "Name", and "OtherColumn" exist in the Employee table
                            EmployeeId = (int)reader["EmployeeId"],
                            Username = (string)reader["Username"],
                        };

                        employees.Add(employee);
                    }
                }
            }
        }

        foreach (Employee employee in employees)
        {
            Console.WriteLine($"EmployeeId: {employee.EmployeeId}, Username: {employee.Username}");
        }

        return employees;
    }
}