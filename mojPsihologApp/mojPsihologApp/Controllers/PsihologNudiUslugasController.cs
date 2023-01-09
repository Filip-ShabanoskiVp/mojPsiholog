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
    public class PsihologNudiUslugasController : Controller
    {
        private readonly MojPsihologContext _context;

        public PsihologNudiUslugasController(MojPsihologContext context)
        {
            _context = context;
        }

        // GET: PsihologNudiUslugas
        public async Task<IActionResult> Index()
        {
            var mojPsihologContext = _context.PsihologNudiUslugas.Include(p => p.IdUslugaNavigation).Include(p => p.KorisnickoimeNavigation);
            return View(await mojPsihologContext.ToListAsync());
        }

        // GET: PsihologNudiUslugas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.PsihologNudiUslugas == null)
            {
                return NotFound();
            }

            var psihologNudiUsluga = await _context.PsihologNudiUslugas
                .Include(p => p.IdUslugaNavigation)
                .Include(p => p.KorisnickoimeNavigation)
                .FirstOrDefaultAsync(m => m.korisnickoime == id);
            if (psihologNudiUsluga == null)
            {
                return NotFound();
            }

            return View(psihologNudiUsluga);
        }

        // GET: PsihologNudiUslugas/Create
        public IActionResult Create()
        {
            ViewData["idUsluga"] = new SelectList(_context.Uslugas, "IdUsluga", "IdUsluga");
            ViewData["korisnickoime"] = new SelectList(_context.Psihologs, "Korisnickoime", "Korisnickoime");
            return View();
        }

        // POST: PsihologNudiUslugas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("korisnickoime,idUsluga")] PsihologNudiUsluga psihologNudiUsluga)
        {
            if (ModelState.IsValid)
            {
                _context.Add(psihologNudiUsluga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["idUsluga"] = new SelectList(_context.Uslugas, "IdUsluga", "IdUsluga", psihologNudiUsluga.idUsluga);
            ViewData["korisnickoime"] = new SelectList(_context.Psihologs, "Korisnickoime", "Korisnickoime", psihologNudiUsluga.korisnickoime);
            return View(psihologNudiUsluga);
        }

        // GET: PsihologNudiUslugas/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.PsihologNudiUslugas == null)
            {
                return NotFound();
            }

            var psihologNudiUsluga = await _context.PsihologNudiUslugas.FindAsync(id);
            if (psihologNudiUsluga == null)
            {
                return NotFound();
            }
            ViewData["idUsluga"] = new SelectList(_context.Uslugas, "IdUsluga", "IdUsluga", psihologNudiUsluga.idUsluga);
            ViewData["korisnickoime"] = new SelectList(_context.Psihologs, "Korisnickoime", "Korisnickoime", psihologNudiUsluga.korisnickoime);
            return View(psihologNudiUsluga);
        }

        // POST: PsihologNudiUslugas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("korisnickoime,idUsluga")] PsihologNudiUsluga psihologNudiUsluga)
        {
            if (id != psihologNudiUsluga.korisnickoime)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(psihologNudiUsluga);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PsihologNudiUslugaExists(psihologNudiUsluga.korisnickoime))
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
            ViewData["idUsluga"] = new SelectList(_context.Uslugas, "IdUsluga", "IdUsluga", psihologNudiUsluga.idUsluga);
            ViewData["korisnickoime"] = new SelectList(_context.Psihologs, "Korisnickoime", "Korisnickoime", psihologNudiUsluga.korisnickoime);
            return View(psihologNudiUsluga);
        }

        // GET: PsihologNudiUslugas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.PsihologNudiUslugas == null)
            {
                return NotFound();
            }

            var psihologNudiUsluga = await _context.PsihologNudiUslugas
                .Include(p => p.IdUslugaNavigation)
                .Include(p => p.KorisnickoimeNavigation)
                .FirstOrDefaultAsync(m => m.korisnickoime == id);
            if (psihologNudiUsluga == null)
            {
                return NotFound();
            }

            return View(psihologNudiUsluga);
        }

        // POST: PsihologNudiUslugas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.PsihologNudiUslugas == null)
            {
                return Problem("Entity set 'MojPsihologContext.PsihologNudiUslugas'  is null.");
            }
            var psihologNudiUsluga = await _context.PsihologNudiUslugas.FindAsync(id);
            if (psihologNudiUsluga != null)
            {
                _context.PsihologNudiUslugas.Remove(psihologNudiUsluga);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PsihologNudiUslugaExists(string id)
        {
          return _context.PsihologNudiUslugas.Any(e => e.korisnickoime == id);
        }
    }
}
