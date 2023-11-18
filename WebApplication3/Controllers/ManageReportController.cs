using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Controllers;

[Authorize(Roles = "Manager")]
public class ManageReportController : Controller
{
    // ... controller actions for managing reports ...
}