using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Opticron.Data;
using Opticron.Models;

namespace Opticron.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArticleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Articles
        /*public async Task<IActionResult> Admin()
        {
            var _Articles = await _context.Articles.ToListAsync();
            if (_Articles.Count < 1)
                await CreateTestData();

            return View(_Articles);
        }*/


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
