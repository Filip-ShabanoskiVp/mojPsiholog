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
    public class PacientRazgleduvaUslugasController : Controller
    {
        private readonly MojPsihologContext _context;

        public PacientRazgleduvaUslugasController(MojPsihologContext context)
        {
            _context = context;
        }

        // GET: PacientRazgleduvaUslugas
        public async Task<IActionResult> Index()
        {
            var mojPsihologContext = _context.PacientRazgleduvaUslugas.Include(p => p.IdUslugaNavigation).Include(p => p.KorisnickoimeNavigation);
            return View(await mojPsihologContext.ToListAsync());
        }

        // GET: PacientRazgleduvaUslugas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.PacientRazgleduvaUslugas == null)
            {
                return NotFound();
            }

            var pacientRazgleduvaUsluga = await _context.PacientRazgleduvaUslugas
                .Include(p => p.IdUslugaNavigation)
                .Include(p => p.KorisnickoimeNavigation)
                .FirstOrDefaultAsync(m => m.korisnickoime == id);
            if (pacientRazgleduvaUsluga == null)
            {
                return NotFound();
            }

            return View(pacientRazgleduvaUsluga);
        }

        // GET: PacientRazgleduvaUslugas/Create
        public IActionResult Create()
        {
            ViewData["idUsluga"] = new SelectList(_context.Uslugas, "IdUsluga", "IdUsluga");
            ViewData["korisnickoime"] = new SelectList(_context.Pacients, "Korisnickoime", "Korisnickoime");
            return View();
        }

        // POST: PacientRazgleduvaUslugas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("korisnickoime,idUsluga")] PacientRazgleduvaUsluga pacientRazgleduvaUsluga)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pacientRazgleduvaUsluga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["idUsluga"] = new SelectList(_context.Uslugas, "IdUsluga", "IdUsluga", pacientRazgleduvaUsluga.idUsluga);
            ViewData["korisnickoime"] = new SelectList(_context.Pacients, "Korisnickoime", "Korisnickoime", pacientRazgleduvaUsluga.korisnickoime);
            return View(pacientRazgleduvaUsluga);
        }

        // GET: PacientRazgleduvaUslugas/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.PacientRazgleduvaUslugas == null)
            {
                return NotFound();
            }

            var pacientRazgleduvaUsluga = await _context.PacientRazgleduvaUslugas.FindAsync(id);
            if (pacientRazgleduvaUsluga == null)
            {
                return NotFound();
            }
            ViewData["idUsluga"] = new SelectList(_context.Uslugas, "IdUsluga", "IdUsluga", pacientRazgleduvaUsluga.idUsluga);
            ViewData["korisnickoime"] = new SelectList(_context.Pacients, "Korisnickoime", "Korisnickoime", pacientRazgleduvaUsluga.korisnickoime);
            return View(pacientRazgleduvaUsluga);
        }

        // POST: PacientRazgleduvaUslugas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("korisnickoime,idUsluga")] PacientRazgleduvaUsluga pacientRazgleduvaUsluga)
        {
            if (id != pacientRazgleduvaUsluga.korisnickoime)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pacientRazgleduvaUsluga);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacientRazgleduvaUslugaExists(pacientRazgleduvaUsluga.korisnickoime))
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
            ViewData["idUsluga"] = new SelectList(_context.Uslugas, "IdUsluga", "IdUsluga", pacientRazgleduvaUsluga.idUsluga);
            ViewData["korisnickoime"] = new SelectList(_context.Pacients, "Korisnickoime", "Korisnickoime", pacientRazgleduvaUsluga.korisnickoime);
            return View(pacientRazgleduvaUsluga);
        }

        // GET: PacientRazgleduvaUslugas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.PacientRazgleduvaUslugas == null)
            {
                return NotFound();
            }

            var pacientRazgleduvaUsluga = await _context.PacientRazgleduvaUslugas
                .Include(p => p.IdUslugaNavigation)
                .Include(p => p.KorisnickoimeNavigation)
                .FirstOrDefaultAsync(m => m.korisnickoime == id);
            if (pacientRazgleduvaUsluga == null)
            {
                return NotFound();
            }

            return View(pacientRazgleduvaUsluga);
        }

        // POST: PacientRazgleduvaUslugas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.PacientRazgleduvaUslugas == null)
            {
                return Problem("Entity set 'MojPsihologContext.PacientRazgleduvaUslugas'  is null.");
            }
            var pacientRazgleduvaUsluga = await _context.PacientRazgleduvaUslugas.FindAsync(id);
            if (pacientRazgleduvaUsluga != null)
            {
                _context.PacientRazgleduvaUslugas.Remove(pacientRazgleduvaUsluga);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacientRazgleduvaUslugaExists(string id)
        {
          return _context.PacientRazgleduvaUslugas.Any(e => e.korisnickoime == id);
        }
    }
}
