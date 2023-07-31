using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Opticron.Data;
using Opticron.Models;

namespace Opticron.Controllers
{
    public class SpecialOfferController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SpecialOfferController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Index()
        {
            var specialOffers = await _context.SpecialOffers.ToListAsync();
            return View(specialOffers);
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.SpecialOffers == null)
            {
                return NotFound();
            }

            var specialOffer = await _context.SpecialOffers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specialOffer == null)
            {
                return NotFound();
            }

            return View(specialOffer);
        }



        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Heading,Subheading,Thumbnail")] SpecialOffer specialOffer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(specialOffer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specialOffer);
        }


        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.SpecialOffers == null)
            {
                return NotFound();
            }

            var specialOffer = await _context.SpecialOffers.FindAsync(id);
            if (specialOffer == null)
            {
                return NotFound();
            }
            return View(specialOffer);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Heading,Subheading,Thumbnail")] SpecialOffer specialOffer)
        {
            if (id != specialOffer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(specialOffer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(specialOffer.Id))
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
            return View(specialOffer);
        }



        [HttpDelete]
        public async Task<JsonResult> Delete(Int64 id)
        {
            try
            {
                var _SpecialOffers = await _context.SpecialOffers.FindAsync(id);

                if (_SpecialOffers != null)
                {
                    _context.SpecialOffers.Remove(_SpecialOffers);
                }
                await _context.SaveChangesAsync();
                return new JsonResult(_SpecialOffers);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool CategoryExists(long id)
        {
            return (_context.SpecialOffers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
