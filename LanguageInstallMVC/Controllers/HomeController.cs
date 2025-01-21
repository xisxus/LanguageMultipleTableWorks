using System.Diagnostics;
using LanguageInstall.Service.Service;
using LanguageInstallMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace LanguageInstallMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILocalizationService _translationService;

        public HomeController(ILogger<HomeController> logger, ILocalizationService translationService)
        {
            _logger = logger;
            _translationService = translationService;
        }


        public async Task<IActionResult> Index()
        {
            var languageCode = HttpContext.Items["Language"] as string ?? "en";

            // Fetch localized text from the translation service or database
            ViewBag.PageTitle = await _translationService.GetTranslationAsync("Leave Application Entry", languageCode);
            ViewBag.Home = await _translationService.GetTranslationAsync("Home", languageCode);
            ViewBag.HumanResourceManagement = await _translationService.GetTranslationAsync("Human Resource Management", languageCode);
            ViewBag.LeaveManagement = await _translationService.GetTranslationAsync("Leave Management", languageCode);
            ViewBag.Operation = await _translationService.GetTranslationAsync("Operation", languageCode);
            ViewBag.CompanyLabel = await _translationService.GetTranslationAsync("Company", languageCode);
            ViewBag.EmployeeIdLabel = await _translationService.GetTranslationAsync("Employee ID", languageCode);
            ViewBag.EmployeeNameLabel = await _translationService.GetTranslationAsync("Employee Name", languageCode);
            ViewBag.DesignationLabel = await _translationService.GetTranslationAsync("Designation", languageCode);
            ViewBag.DepartmentLabel = await _translationService.GetTranslationAsync("Department", languageCode);
            ViewBag.EntryIdLabel = await _translationService.GetTranslationAsync("Entry ID", languageCode);
            ViewBag.SupervisorLabel = await _translationService.GetTranslationAsync("Immediate Supervisor", languageCode);
            ViewBag.LeaveFormatLabel = await _translationService.GetTranslationAsync("Apply Leave Format", languageCode);
            ViewBag.LeaveTypeLabel = await _translationService.GetTranslationAsync("Leave Type", languageCode);
            ViewBag.ReasonLabel = await _translationService.GetTranslationAsync("Reason", languageCode);
            ViewBag.FileAttachmentLabel = await _translationService.GetTranslationAsync("File Attachment", languageCode);
            ViewBag.SubmitButton = await _translationService.GetTranslationAsync("Leave Apply", languageCode);
            ViewBag.CancelButton = await _translationService.GetTranslationAsync("Cancel", languageCode);
            ViewBag.HalfDayLeave = await _translationService.GetTranslationAsync("Half Day Leave", languageCode);
            ViewBag.FullDayLeave = await _translationService.GetTranslationAsync("Full Day Leave", languageCode);
            ViewBag.SickLeave = await _translationService.GetTranslationAsync("Sick Leave", languageCode);
            ViewBag.CasualLeave = await _translationService.GetTranslationAsync("Casual Leave", languageCode);
            ViewBag.LeaveDuration = await _translationService.GetTranslationAsync("Leave Duration", languageCode);
            ViewBag.AdditionalInfoTitle = await _translationService.GetTranslationAsync("Additional Information", languageCode);
            ViewBag.SelectOption = await _translationService.GetTranslationAsync("--Select--", languageCode);
            ViewBag.SelectEmployee = await _translationService.GetTranslationAsync("Select Employee", languageCode);
            ViewBag.SelectLeaveFormat = await _translationService.GetTranslationAsync("Select Leave Format", languageCode);
            ViewBag.SelectLeaveType = await _translationService.GetTranslationAsync("Select Leave Type", languageCode);
            ViewBag.ReasonPlaceholder = await _translationService.GetTranslationAsync("Enter Reason", languageCode);
            ViewBag.Test = await _translationService.GetTranslationAsync("Test", languageCode);
            ViewBag.Mobile = await _translationService.GetTranslationAsync("Mobile", languageCode);
            ViewBag.Laptop = await _translationService.GetTranslationAsync("Laptop", languageCode);

            return View();
        }


        public async Task<IActionResult> RazorHelper()
        {
           

            return View();

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ChangeLanguage(string languageCode)
        {
            Response.Cookies.Append("Language", languageCode, new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
