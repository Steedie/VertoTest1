using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Opticron.Data;
using Opticron.Models;

namespace Opticron.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            // Skip login if user is already logged in
            var isAdmin = HttpContext.Session.GetString("IsAdmin");
            if (isAdmin == "true")
            {
                return RedirectToAction("Index", "Admin");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "admin")
            {
                // Set session value
                HttpContext.Session.SetString("IsAdmin", "true");

                // Redirect to the new page upon successful login.
                return RedirectToAction("Index", "Admin");
            }

            // Login failed, return to the login page with an error message.
            ViewBag.ErrorMessage = "Invalid username or password.";
            return View();
        }
    }
}
