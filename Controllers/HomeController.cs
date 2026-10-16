using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Electronic_Clearance_Platform.Models;

namespace Electronic_Clearance_Platform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // حساب الإحصائيات تلقائياً من قاعدة البيانات
            ViewBag.TotalRequests = await _context.ClearanceRequests.CountAsync();
            ViewBag.ApprovedRequests = await _context.ClearanceRequests.CountAsync(r => r.Status == "مقبول");
            ViewBag.RejectedRequests = await _context.ClearanceRequests.CountAsync(r => r.Status == "مرفوض");
            ViewBag.PendingRequests = ViewBag.TotalRequests - (ViewBag.ApprovedRequests + ViewBag.RejectedRequests);

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
    }
}