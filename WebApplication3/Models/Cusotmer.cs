using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Models;

// Customer.cs in the Models folder
public class Customer
{
    
    public int CustomerId { get; set; }

    public string CustomerName { get; set; }
    public string CustomerSurname { get; set; }
    public string Address { get; set; }
    public string PostCode { get; set; }
    public string Country { get; set; }
    public DateTime DateOfBirth { get; set; }

}
