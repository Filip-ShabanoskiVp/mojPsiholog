using System;
using System.Collections;
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
    public class PacientotZakazuvaTerminsController : Controller
    {
        private readonly MojPsihologContext _context;

        public PacientotZakazuvaTerminsController(MojPsihologContext context)
        {

            _context = context;
        }

        // GET: PacientotZakazuvaTermins
        public async Task<IActionResult> Index()
        {

            var korisnickoime = HttpContext.Session.GetString("korisnickoime");
            var korisnik = _context.Korisniks.Where(k => k.Korisnickoime == korisnickoime);
            ViewBag.korisnickoime = korisnickoime;

            var uloga = _context.Korisniks.Where(k => k.Korisnickoime == korisnickoime).FirstOrDefault();

            if (uloga != null)
            {
               ViewBag.uloga = uloga.Uloga;
            }


            var mojPsihologContext = _context.PacientotZakazuvaTermins.Include(p => p.IdTerminNavigation).Include(p => p.KorisnickoimeNavigation);
            return View(await mojPsihologContext.ToListAsync());
        }

        public async Task<IActionResult> pacientiZakazanoPocekeOd1Termin()
        {


            var korisnickoime = HttpContext.Session.GetString("korisnickoime");
            var korisnik = _context.Korisniks.Where(k => k.Korisnickoime == korisnickoime);
            ViewBag.korisnickoime = korisnickoime;



            var query1 = _context.DavaOcenkas.GroupBy(x => x.Korisnickoimepacient).Select(x =>
             new
             {
                 korisnickoime = x.Key,
                 broj = x.Count() > 2
             }).Where(x => x.broj == true).ToList();


            var query2 = (from q in query1
                          join k in _context.Korisniks on q.korisnickoime equals k.Korisnickoime
                          join pzt in _context.PacientotZakazuvaTermins on k.Korisnickoime equals pzt.korisnickoime
                          where k.Prezime.StartsWith("Sla")
                          group pzt by new {pzt.korisnickoime}into grp
                          select new
                          {
                              korisnickoime = grp.Key.korisnickoime,
                              broj = grp.Count() > 1    
                          }).Where(x =>x.broj == true).Select(x=>x.korisnickoime).ToList();

            var lista = _context.Korisniks.Where(kor => query2.Any(x => x.Equals(kor.Korisnickoime)));

            ViewBag.lista = lista;

            return View();
        }

        // GET: PacientotZakazuvaTermins/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.PacientotZakazuvaTermins == null)
            {
                return NotFound();
            }

            var pacientotZakazuvaTermin = await _context.PacientotZakazuvaTermins
                .Include(p => p.IdTerminNavigation)
                .Include(p => p.KorisnickoimeNavigation)
                .FirstOrDefaultAsync(m => m.korisnickoime == id);
            if (pacientotZakazuvaTermin == null)
            {
                return NotFound();
            }

            return View(pacientotZakazuvaTermin);
        }

        // GET: PacientotZakazuvaTermins/Create
        public IActionResult Create()
        {
            var korisnickoime = HttpContext.Session.GetString("korisnickoime");
            var korisnik = _context.Korisniks.Where(k => k.Korisnickoime == korisnickoime);
            ViewBag.korisnickoime = korisnickoime;

            ViewData["idTermin"] = new SelectList(_context.Termins, "IdTermin", "IdTermin");
            return View();
        }

        // POST: PacientotZakazuvaTermins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("korisnickoime,idTermin")] PacientotZakazuvaTermin pacientotZakazuvaTermin,Termin termin)
        {

            var currentUser = HttpContext.Session.GetString("korisnickoime");
            pacientotZakazuvaTermin.korisnickoime = currentUser;

            pacientotZakazuvaTermin.KorisnickoimeNavigation = _context.Pacients.Where(p => p.Korisnickoime
              == HttpContext.Session.GetString("korisnickoime")).FirstOrDefault();

            //var termin = await _context.Termins.FindAsync(id);

            //pacientotZakazuvaTermin.idTermin = termin.IdTermin;
         

            var pacient = _context.PacientotZakazuvaTermins.Where(x => x.korisnickoime == currentUser);

            foreach (var p in pacient)
            {

                if (p.idTermin == pacientotZakazuvaTermin.idTermin)
                {
                    ModelState.AddModelError("Error", "Корисникот " + p.korisnickoime + " веќе закажал за термин со id " + p.idTermin);
                    ViewData["idTermin"] = new SelectList(_context.Termins, "IdTermin", "IdTermin");
                    return View(pacientotZakazuvaTermin);
                }
            }


            _context.Add(pacientotZakazuvaTermin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        
        }

        // GET: PacientotZakazuvaTermins/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.PacientotZakazuvaTermins == null)
            {
                return NotFound();
            }

            var pacientotZakazuvaTermin = await _context.PacientotZakazuvaTermins.FindAsync(id);
            if (pacientotZakazuvaTermin == null)
            {
                return NotFound();
            }
            ViewData["idTermin"] = new SelectList(_context.Termins, "IdTermin", "IdTermin", pacientotZakazuvaTermin.idTermin);
            ViewData["korisnickoime"] = new SelectList(_context.Pacients, "Korisnickoime", "Korisnickoime", pacientotZakazuvaTermin.korisnickoime);
            return View(pacientotZakazuvaTermin);
        }

        // POST: PacientotZakazuvaTermins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("korisnickoime,idTermin")] PacientotZakazuvaTermin pacientotZakazuvaTermin)
        {
            if (id != pacientotZakazuvaTermin.korisnickoime)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pacientotZakazuvaTermin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacientotZakazuvaTerminExists(pacientotZakazuvaTermin.korisnickoime))
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
            ViewData["idTermin"] = new SelectList(_context.Termins, "IdTermin", "IdTermin", pacientotZakazuvaTermin.idTermin);
            ViewData["korisnickoime"] = new SelectList(_context.Pacients, "Korisnickoime", "Korisnickoime", pacientotZakazuvaTermin.korisnickoime);
            return View(pacientotZakazuvaTermin);
        }

        // GET: PacientotZakazuvaTermins/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.PacientotZakazuvaTermins == null)
            {
                return NotFound();
            }

            var pacientotZakazuvaTermin = await _context.PacientotZakazuvaTermins
                .Include(p => p.IdTerminNavigation)
                .Include(p => p.KorisnickoimeNavigation)
                .FirstOrDefaultAsync(m => m.korisnickoime == id);
            if (pacientotZakazuvaTermin == null)
            {
                return NotFound();
            }

            return View(pacientotZakazuvaTermin);
        }

        // POST: PacientotZakazuvaTermins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.PacientotZakazuvaTermins == null)
            {
                return Problem("Entity set 'MojPsihologContext.PacientotZakazuvaTermins'  is null.");
            }
            var pacientotZakazuvaTermin = await _context.PacientotZakazuvaTermins.FindAsync(id);
            if (pacientotZakazuvaTermin != null)
            {
                _context.PacientotZakazuvaTermins.Remove(pacientotZakazuvaTermin);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacientotZakazuvaTerminExists(string id)
        {
          return _context.PacientotZakazuvaTermins.Any(e => e.korisnickoime == id);
        }
    }
}
