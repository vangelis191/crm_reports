namespace WebApplication3.Helper;

public class CallWithCustomerInfo
{
    // Add properties to represent the columns in the result set
    public int CallId { get; set; }
    public DateTime DateOfCall { get; set; }
    public TimeSpan TimeOfCall { get; set; }
    public string Subject { get; set; }
    public string Description { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string CustomerSurname { get; set; }
    public string Address { get; set; }
    public string PostCode { get; set; }
    public string Country { get; set; }
    public DateTime DateOfBirth { get; set; }
}