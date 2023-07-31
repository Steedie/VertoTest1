using Microsoft.AspNetCore.Mvc;
using Opticron.Data;
using Opticron.Models;
using System.Diagnostics;

namespace Opticron.Controllers
{
    public class HomeViewModel
    {
        public List<Article> Articles { get; set; }
        public List<SpecialOffer> SpecialOffers{ get; set; }
        public List<Category> Categories{ get; set; }
    }


    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var model = new HomeViewModel
            {
                Articles = _context.Articles.ToList(),
                SpecialOffers = _context.SpecialOffers.ToList(),
                Categories = _context.Categories.ToList()
            };

            return View(model); // Pass the ViewModel to the view
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