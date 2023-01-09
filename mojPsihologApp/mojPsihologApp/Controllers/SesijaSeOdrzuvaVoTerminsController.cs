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
    public class SesijaSeOdrzuvaVoTerminsController : Controller
    {
        private readonly MojPsihologContext _context;

        public SesijaSeOdrzuvaVoTerminsController(MojPsihologContext context)
        {
            _context = context;
        }

        // GET: SesijaSeOdrzuvaVoTermins
        public async Task<IActionResult> Index()
        {
            var mojPsihologContext = _context.SesijaSeOdrzuvaVoTermins.Include(s => s.IdSesijaNavigation).Include(s => s.IdTerminNavigation);
            return View(await mojPsihologContext.ToListAsync());
        }



        public async Task<IActionResult> SesijaOdrzanaPred1GodISegasnataImeFilipOdPrilep()
        
        {
            var sesiiOdrzani = _context.SesijaSeOdrzuvaVoTermins
                .Include(s => s.IdSesijaNavigation).Include(s => s.IdTerminNavigation)
                .Include(s =>s.IdTerminNavigation.KorisnickoimeNavigation).Include(s =>s.IdTerminNavigation.KorisnickoimeNavigation.KorisnickoimeNavigation)
                .Where(x => DateTime.SpecifyKind((DateTime)x.IdTerminNavigation.Datum, DateTimeKind.Utc).Year
                >= DateTime.Now.Year - 1 && DateTime.SpecifyKind((DateTime)x.IdTerminNavigation.Datum, DateTimeKind.Utc).Year
                <= DateTime.Now.Year).Where(k => 
                k.IdTerminNavigation.KorisnickoimeNavigation
                .KorisnickoimeNavigation.Grad == "Prilep" &&
                k.IdTerminNavigation.KorisnickoimeNavigation
                .KorisnickoimeNavigation.Ime == "Filip").Select(x=>new
                {
                   korisnickoime =  x.IdTerminNavigation.Korisnickoime
                }).Distinct();

            List<string> list = new List<string>();
            foreach(var item in sesiiOdrzani)
            {
                list.Add(item.korisnickoime);
            }

            ViewBag.sesiiOdrzani = list;

            return View();
        }

        // GET: SesijaSeOdrzuvaVoTermins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SesijaSeOdrzuvaVoTermins == null)
            {
                return NotFound();
            }

            var sesijaSeOdrzuvaVoTermin = await _context.SesijaSeOdrzuvaVoTermins
                .Include(s => s.IdSesijaNavigation)
                .Include(s => s.IdTerminNavigation)
                .FirstOrDefaultAsync(m => m.idSesija == id);
            if (sesijaSeOdrzuvaVoTermin == null)
            {
                return NotFound();
            }

            return View(sesijaSeOdrzuvaVoTermin);
        }

        // GET: SesijaSeOdrzuvaVoTermins/Create
        public IActionResult Create()
        {
            ViewData["idSesija"] = new SelectList(_context.Sesijas, "IdSesija", "IdSesija");
            ViewData["idTermin"] = new SelectList(_context.Termins, "IdTermin", "IdTermin");
            return View();
        }

        // POST: SesijaSeOdrzuvaVoTermins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idSesija,idTermin")] SesijaSeOdrzuvaVoTermin sesijaSeOdrzuvaVoTermin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sesijaSeOdrzuvaVoTermin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["idSesija"] = new SelectList(_context.Sesijas, "IdSesija", "IdSesija", sesijaSeOdrzuvaVoTermin.idSesija);
            ViewData["idTermin"] = new SelectList(_context.Termins, "IdTermin", "IdTermin", sesijaSeOdrzuvaVoTermin.idTermin);
            return View(sesijaSeOdrzuvaVoTermin);
        }

        // GET: SesijaSeOdrzuvaVoTermins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SesijaSeOdrzuvaVoTermins == null)
            {
                return NotFound();
            }

            var sesijaSeOdrzuvaVoTermin = await _context.SesijaSeOdrzuvaVoTermins.FindAsync(id);
            if (sesijaSeOdrzuvaVoTermin == null)
            {
                return NotFound();
            }
            ViewData["idSesija"] = new SelectList(_context.Sesijas, "IdSesija", "IdSesija", sesijaSeOdrzuvaVoTermin.idSesija);
            ViewData["idTermin"] = new SelectList(_context.Termins, "IdTermin", "IdTermin", sesijaSeOdrzuvaVoTermin.idTermin);
            return View(sesijaSeOdrzuvaVoTermin);
        }

        // POST: SesijaSeOdrzuvaVoTermins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idSesija,idTermin")] SesijaSeOdrzuvaVoTermin sesijaSeOdrzuvaVoTermin)
        {
            if (id != sesijaSeOdrzuvaVoTermin.idSesija)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sesijaSeOdrzuvaVoTermin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SesijaSeOdrzuvaVoTerminExists(sesijaSeOdrzuvaVoTermin.idSesija))
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
            ViewData["idSesija"] = new SelectList(_context.Sesijas, "IdSesija", "IdSesija", sesijaSeOdrzuvaVoTermin.idSesija);
            ViewData["idTermin"] = new SelectList(_context.Termins, "IdTermin", "IdTermin", sesijaSeOdrzuvaVoTermin.idTermin);
            return View(sesijaSeOdrzuvaVoTermin);
        }

        // GET: SesijaSeOdrzuvaVoTermins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SesijaSeOdrzuvaVoTermins == null)
            {
                return NotFound();
            }

            var sesijaSeOdrzuvaVoTermin = await _context.SesijaSeOdrzuvaVoTermins
                .Include(s => s.IdSesijaNavigation)
                .Include(s => s.IdTerminNavigation)
                .FirstOrDefaultAsync(m => m.idSesija == id);
            if (sesijaSeOdrzuvaVoTermin == null)
            {
                return NotFound();
            }

            return View(sesijaSeOdrzuvaVoTermin);
        }

        // POST: SesijaSeOdrzuvaVoTermins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SesijaSeOdrzuvaVoTermins == null)
            {
                return Problem("Entity set 'MojPsihologContext.SesijaSeOdrzuvaVoTermins'  is null.");
            }
            var sesijaSeOdrzuvaVoTermin = await _context.SesijaSeOdrzuvaVoTermins.FindAsync(id);
            if (sesijaSeOdrzuvaVoTermin != null)
            {
                _context.SesijaSeOdrzuvaVoTermins.Remove(sesijaSeOdrzuvaVoTermin);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SesijaSeOdrzuvaVoTerminExists(int id)
        {
          return _context.SesijaSeOdrzuvaVoTermins.Any(e => e.idSesija == id);
        }
    }
}
