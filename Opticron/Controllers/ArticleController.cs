using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Opticron.Data;
using Opticron.Models;

namespace Opticron.Controllers
{
    public class ArticleController : Controller
    {

        // GET: Articles
        /*public async Task<IActionResult> Admin()
        {
            var _Articles = await _context.Articles.ToListAsync();
            if (_Articles.Count < 1)
                await CreateTestData();

            return View(_Articles);
        }*/


        private readonly ApplicationDbContext _context;

        public ArticleController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Index()
        {
            // Check if the user is authenticated as admin using session
            var isAdmin = HttpContext.Session.GetString("IsAdmin");
            if (isAdmin != "true")
            {
                // Redirect to the login page or display an access denied message.
                return RedirectToAction("Login", "Account");
            }

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

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,CallToAction,Thumbnail")] Article article)
        {
            if (ModelState.IsValid)
            {
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }


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
                return RedirectToAction(nameof(Index));
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


        private async Task CreateTestData()
        {
            foreach (var item in GetTestArticleList())
            {
                _context.Articles.Add(item);
                await _context.SaveChangesAsync();
            }
        }

        private IEnumerable<Article> GetTestArticleList()
        {
            return new List<Article>
            {
                new Article { Title = "Article Title 01", Description = "Default article description", CallToAction = "View" },
                new Article { Title = "Article Title 02", Description = "Default article description", CallToAction = "View" },
                new Article { Title = "Article Title 03", Description = "Default article description", CallToAction = "View" },
                new Article { Title = "Article Title 04", Description = "Default article description", CallToAction = "View" },
                new Article { Title = "Article Title 05", Description = "Default article description", CallToAction = "View" },
                new Article { Title = "Article Title 06", Description = "Default article description", CallToAction = "View" },
                new Article { Title = "Article Title 07", Description = "Default article description", CallToAction = "View" },
                new Article { Title = "Article Title 08", Description = "Default article description", CallToAction = "View" },
                new Article { Title = "Article Title 09", Description = "Default article description", CallToAction = "View" },
                new Article { Title = "Article Title 10", Description = "Default article description", CallToAction = "View" }
            };
        }
    }
}
