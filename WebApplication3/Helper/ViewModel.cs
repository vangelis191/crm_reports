using WebApplication3.Models;

namespace WebApplication3.Helper;

public class ViewModel
{
    public List<Customer>? Customers { get; set; }
    public List<CallWithCustomerInfo>? Calls { get; set; }
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
}