using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Helper;
using WebApplication3.Models;
using WebApplication3.services;

namespace WebApplication3.Controllers;

[Authorize(Roles = "MANAGER")]
public class ManageCustomersController : Controller
{
    private readonly CustomerService _service;
    
    public ManageCustomersController(CustomerService service)
    {

        _service = service;

    }

    
    
    
    [HttpGet("/customers")]
    public IActionResult Customers([FromQuery(Name = "page")] int page = 1)
    {
        ViewData["customers"] = _service.GetAllCustomers();
        var pageSize = 10;
        var pageCount = (int)Math.Ceiling((double)_service.GetAllCustomers().Count / pageSize);

        var model = new ViewModel()
        {
            Customers = _service.GetAllCustomers().Skip((page - 1) * pageSize).Take(pageSize).ToList(),
            CurrentPage = page,
            PageCount = pageCount
        };
      
        return View(model);
    }
    
    [HttpGet("add-customer")]
    public ActionResult EditCustomer()
    {

        return View("AddCustomer");
    }
    
    [HttpGet("customer/{id}")]
    public ActionResult EditCustomer(int id)
    {
        ViewBag.CustomerId = id;
         ViewBag.Customer = _service.GetCustomerById(id);
        return View("EditCustomers");
    }
    
    [HttpPost("add-save-customer")]
    public void AddCustomer(IFormCollection form)
    {
        
        string customerName = form["CustomerName"];
        string customerSurname = form["CustomerSurname"];
        string address = form["Address"];
        string postCode = form["PostCode"];
        string country = form["Country"];
        string DateOfBirth = form["DateOfBirth"];
        

        Customer customer = new Customer
        {
          
            CustomerName = customerName,
            CustomerSurname = customerSurname,
            Address = address,
            PostCode = postCode,
            Country = country,
            DateOfBirth =DateTime.Parse(DateOfBirth)

       
        };
        _service.AddNewCustomer(customer);
    }
    
    [HttpPost("edit-customer")]
    public void EditCustomer(IFormCollection form)
    {
   
        int customerId = int.Parse(form["CustomerId"]);
        string customerName = form["CustomerName"];
        string customerSurname = form["CustomerSurname"];
        string address = form["Address"];
        string postCode = form["PostCode"];
        string country = form["Country"];


        Customer editedCustomer = new Customer
        {
            CustomerId = customerId,
            CustomerName = customerName,
            CustomerSurname = customerSurname,
            Address = address,
            PostCode = postCode,
            Country = country,


       
        };
        _service.SaveCustomer(editedCustomer);
    }

    [HttpGet("deleteCustomer/{id}")]
    public ActionResult DeleteCustomer(int id)
    {
        _service.DeleteUser(id);
        return RedirectToAction("Customers");
    }

   
}