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
        _repository.DeleteCall(id);
    }

    public Call GetCallById(int id)
    {
        return  _repository.getCallById(id);
    }

    public void SaveCall(Call editedCall)
    {
        _repository.SaveCall(editedCall);
    }

    public void AddNewCustomer(Customer customer)
    {
        _repository.AddNewCustomer(customer);
    }

    public void AddNewCall(Call call)
    {
        _repository.AddNewCall(call);
    }
}