using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mojPsihologApp.Models;
using mojPsihologApp.mojPsihologDbContext;

namespace mojPsihologApp.Controllers
{
    public class TerminsController : Controller
    {
        private readonly MojPsihologContext _context;

        public TerminsController(MojPsihologContext context)
        {
            _context = context;
        }

        // GET: Termins
        public async Task<IActionResult> Index()
        {
            ViewBag.korisnickoime = HttpContext.Session.GetString("korisnickoime");
            var mojPsihologContext = _context.Termins.Include(t => t.KorisnickoimeNavigation);
            return View(await mojPsihologContext.ToListAsync());
        }

        // GET: Termins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Termins == null)
            {
                return NotFound();
            }

            var termin = await _context.Termins
                .Include(t => t.KorisnickoimeNavigation)
                .FirstOrDefaultAsync(m => m.IdTermin == id);
            if (termin == null)
            {
                return NotFound();
            }

            return View(termin);
        }

        // GET: Termins/Create
        public IActionResult Create()
        {
            //  ViewData["Korisnickoime"] = new SelectList(_context.Psihologs, "Korisnickoime", "Korisnickoime");
            return View();
            ViewBag.korisnickoime = HttpContext.Session.GetString("korisnickoime");
        }

        // POST: Termins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Termin termin)
        {
            termin.KorisnickoimeNavigation = _context.Psihologs.Where(p => p.Korisnickoime
            == HttpContext.Session.GetString("korisnickoime")).FirstOrDefault();

            termin.Korisnickoime = HttpContext.Session.GetString("korisnickoime");
            termin.Datum = DateTime.SpecifyKind((DateTime)termin.Datum, DateTimeKind.Utc);
            //  if (ModelState.IsValid)
            //{
            _context.Add(termin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //  }
            //  ViewData["Korisnickoime"] = new SelectList(_context.Psihologs, "Korisnickoime", "Korisnickoime", termin.Korisnickoime);
            return View(termin);
        }

        // GET: Termins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Termins == null)
            {
                return NotFound();
            }

            var termin = await _context.Termins.FindAsync(id);
            if (termin == null)
            {
                return NotFound();
            }
            ViewData["Korisnickoime"] = new SelectList(_context.Psihologs, "Korisnickoime", "Korisnickoime", termin.Korisnickoime);
            return View(termin);
        }

        // POST: Termins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTermin,Korisnickoime,Vreme,Grad,Datum")] Termin termin)
        {
            if (id != termin.IdTermin)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(termin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TerminExists(termin.IdTermin))
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
            ViewData["Korisnickoime"] = new SelectList(_context.Psihologs, "Korisnickoime", "Korisnickoime", termin.Korisnickoime);
            return View(termin);
        }

        // GET: Termins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Termins == null)
            {
                return NotFound();
            }

            var termin = await _context.Termins
                .Include(t => t.KorisnickoimeNavigation)
                .FirstOrDefaultAsync(m => m.IdTermin == id);
            if (termin == null)
            {
                return NotFound();
            }

            return View(termin);
        }

        // POST: Termins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Termins == null)
            {
                return Problem("Entity set 'MojPsihologContext.Termins'  is null.");
            }
            var termin = await _context.Termins.FindAsync(id);
            if (termin != null)
            {
                _context.Termins.Remove(termin);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TerminExists(int id)
        {
            return _context.Termins.Any(e => e.IdTermin == id);
        }
    }
}
