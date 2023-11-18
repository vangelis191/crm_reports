namespace WebApplication3.Models;

// Report.cs in the Models folder
public class Report
{
    public int Id { get; set; }
    public string Content { get; set; }
    // Other properties related to reports

    // Foreign key for the associated customer
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
}
