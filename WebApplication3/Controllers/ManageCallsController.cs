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
        ViewBag.CallId = id;
        ViewBag.Call = _service.GetCallById(id);
        return View("EditCalls");
    }
    
    [HttpPost("edit-call")]
    public void EditCalls(IFormCollection form)
    {
        int callId = int.Parse(form["CallId"]);
        int customerId = int.Parse(form["CustomerId"]);
        var TimeOfCall = form["TimeOfCall"];
        var DateOfCall = form["DateOfCall"];
        string Subject = form["Subject"];
        string Description = form["Description"];



        Call editedCall = new Call
        {
            CallId = callId,
            CustomerId = customerId,
            TimeOfCall = TimeSpan.Parse(TimeOfCall),
            DateOfCall = DateTime.Parse(DateOfCall),
            Subject = Subject,
            Description = Description,


       
        };
        _service.SaveCall(editedCall);
    }

    [HttpGet("deleteCall/{id}")]
    public ActionResult DeleteCall(int id)
    {
        _service.DeleteCall(id);
        return RedirectToAction("CustomerCalls");
    }
 

}
