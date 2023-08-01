using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Opticron.Data;
using Opticron.Models;

namespace Opticron.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            // Check if the user is authenticated as admin using session
            var isAdmin = HttpContext.Session.GetString("IsAdmin");
            if (isAdmin != "true")
            {
                // Redirect to the login page or display an access denied message.
                return RedirectToAction("Login", "Account");
            }


            return View();
        }

        [HttpPost]
        public IActionResult AdminSignOut()
        {
            HttpContext.Session.SetString("IsAdmin", "false");
            return RedirectToAction("Login", "Account");
        }
    }
}
