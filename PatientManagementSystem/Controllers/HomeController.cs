using Microsoft.AspNetCore.Mvc;
using PatientManagementSystem.Models;
using System.Diagnostics;

namespace PatientManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            this.db = db;
        }

        public IActionResult Index()
        {
            var details = from patient in db.Patients
                          from staff in db.StaffMembers
                          from labbilling in db.LabBillings
                          from pharmacybilling in db.PharmacyBillings
                          where labbilling.PatientId == patient.Id
                          where patient.Id == staff.PatientId
                          where patient.Id == labbilling.PatientId
                          where pharmacybilling.PatientId == patient.Id
                          select new ViewModelForDashboard
                          {
                              Id = patient.Id,
                              PatientName = patient.FirstName + " " + patient.LastName,
                              StaffName = staff.FirstName + " " + staff.LastName,
                              LabBill = labbilling.Amount,
                              PharmacyBill = pharmacybilling.Amount
                          };

            var patientData = details.ToList();

            return View(patientData);
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
    }
}