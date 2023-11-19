using FastReport;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Helper;
using WebApplication3.services;
namespace WebApplication3.Controllers;


public class ReportController : Controller
{
    private readonly CustomerService _service;
    private readonly IWebHostEnvironment _hostingEnvironment;
    private readonly ILogger<ReportController> _logger; // Inject the logger
    public ReportController(CustomerService service,IWebHostEnvironment hostingEnvironment,ILogger<ReportController> logger)
    {

        _service = service;
        _hostingEnvironment = hostingEnvironment;
        _logger = logger;
    }


    public ActionResult ExportReport()
    {
        try
        {
            _logger.LogInformation("ExportReport action started.");
         
            Report reports = new Report();

            string reportTemplatePath = Path.Combine(_hostingEnvironment.ContentRootPath, "CustomerCalls1.frx");
            _logger.LogInformation($"Template path: {reportTemplatePath}");
            
            
            reports.Load(reportTemplatePath);
            //reports.RegisterData(calls, "Calls");
        
            if (reports.Prepare())
            {
                _logger.LogInformation("Report prepared successfully.");

                FastReport.Export.PdfSimple.PDFSimpleExport pdfExport = new FastReport.Export.PdfSimple.PDFSimpleExport();
                pdfExport.ShowProgress = false;
                pdfExport.Subject = "Customer Calls Repost";
                pdfExport.Title = "Reports";
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                reports.Report.Export(pdfExport, ms);
                reports.Dispose();
                pdfExport.Dispose();
                ms.Position = 0;
                _logger.LogInformation("Report exported successfully.");
                return File(ms, "application/pdf", "reports.pdf");
            }

            _logger.LogError("Report preparation failed.");
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while exporting the report.");
            return null;
        }
        finally
        {
            _logger.LogInformation("ExportReport action completed.");
        }
    }


    [HttpGet("/Reports")]
    public ActionResult Reports()
    {

        return View();
    }
}