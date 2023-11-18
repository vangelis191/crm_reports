namespace WebApplication3.Models;


public class Call
{
    public int CallId { get; set; } 

    public int CustomerId { get; set; } 

    public DateTime DateOfCall { get; set; }

    public TimeSpan TimeOfCall { get; set; }

    public string Subject { get; set; }

    public string Description { get; set; }

}
