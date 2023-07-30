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
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            // Perform your login authentication logic here.
            // For a simple example, let's check if the username and password are both "admin".
            if (username == "admin" && password == "admin")
            {
                // Redirect to the new page upon successful login.
                return RedirectToAction("Admin");
            }

            // Login failed, return to the login page with an error message.
            ViewBag.ErrorMessage = "Invalid username or password.";
            return View();
        }

        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Admin()
        {
            var articles = await _context.Articles.ToListAsync();
            return View(articles);
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Articles == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }


        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,CallToAction,Thumbnail")] Article article)
        {
            if (ModelState.IsValid)
            {
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Admin));
            }
            return View(article);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Articles == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Title,Description,CallToAction,Thumbnail")] Article article)
        {
            if (id != article.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(article.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Admin));
            }
            return View(article);
        }



        [HttpDelete]
        public async Task<JsonResult> Delete(Int64 id)
        {
            try
            {
                var _Articles = await _context.Articles.FindAsync(id);

                if (_Articles != null)
                {
                    _context.Articles.Remove(_Articles);
                }
                await _context.SaveChangesAsync();
                return new JsonResult(_Articles);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool CategoryExists(long id)
        {
            return (_context.Articles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
