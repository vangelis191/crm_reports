using Microsoft.Data.SqlClient;
using WebApplication3.Models;
using WebApplication3.Repository;

namespace WebApplication3.services;

public class EmployeeService
{  
    
  
    private readonly EmployeeRepository _repository;

    public EmployeeService(EmployeeRepository employeeRepository)
    {
        _repository = employeeRepository;
    }
    public  void GetAllEmployees()
    {
      _repository.GetAllEmployees();
    }
}