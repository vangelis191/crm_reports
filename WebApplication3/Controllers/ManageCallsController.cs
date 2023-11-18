using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Helper;
using WebApplication3.Models;
using WebApplication3.services;

namespace WebApplication3.Controllers;

[Authorize(Roles = "EMPLOYEE")]
public class ManageCallsController : Controller
{
    private readonly CustomerService _service;
    
    public ManageCallsController(CustomerService service)
    {

        _service = service;

    }

    
    [HttpGet("/CustomerCalls")]
    public IActionResult CustomerCalls([FromQuery(Name = "page")] int page = 1)
    {
   
        
        ViewData["calls"] = _service.GetAllCallsWithCustomers();
        var pageSize = 10;
        var pageCount = (int)Math.Ceiling((double)_service.GetAllCustomers().Count / pageSize);

        var model = new ViewModel()
        {
            Calls = _service.GetAllCallsWithCustomers().Skip((page - 1) * pageSize).Take(pageSize).ToList(),
            CurrentPage = page,
            PageCount = pageCount
        };

        return View(model);
    }
    
    
    [HttpGet("call/{id}")]
    public ActionResult EditCalls(int id)
    {
        ViewBag.CustomerId = id;
        ViewBag.Customer = _service.GetCustomerById(id);
        return View(null);
    }
    
    [HttpPost("edit-call")]
    public void EditCalls(IFormCollection form)
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

    [HttpGet("deleteCall/{id}")]
    public ActionResult DeleteCall(int id)
    {
        _service.DeleteCall(id);
        return RedirectToAction("Dashboard","Home");
    }
 

}
