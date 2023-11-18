using Microsoft.Data.SqlClient;
using WebApplication3.Helper;
using WebApplication3.Models;
using WebApplication3.Repository;

namespace WebApplication3.services;

public class CustomerService
{  
    
  
    private readonly CustomerRepository _repository;

    public CustomerService(CustomerRepository customerRepository)
    {
        _repository = customerRepository;
    }
    public  List<Customer> GetAllCustomers()
    {
     return  _repository.GetAllCustomers();
    }

    public void DeleteUser(int id)
    {
        _repository.DeleteUser(id);
    }
    public  Customer GetCustomerById(int id)
    {
        return  _repository.getCtomerById(id);
    }

    public  void SaveCustomer(Customer customer)
    {
        _repository.SaveCustomer(customer);
    }
    
    public  List<CallWithCustomerInfo> GetAllCallsWithCustomers()
    {
        return _repository.GetAllCallsWithCustomers();
    }

    public void DeleteCall(int id)
    {
        throw new NotImplementedException();
    }
}