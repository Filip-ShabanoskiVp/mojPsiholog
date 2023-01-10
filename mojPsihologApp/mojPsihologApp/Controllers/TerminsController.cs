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
            var korisnickoime = HttpContext.Session.GetString("korisnickoime");
            var korisnik = _context.Korisniks.Where(k => k.Korisnickoime == korisnickoime);
            ViewBag.korisnickoime = korisnickoime;
            var mojPsihologContext = _context.Termins.Include(t => t.KorisnickoimeNavigation)
                .Include(t => t.KorisnickoimeNavigation.KorisnickoimeNavigation);
 

            string param = Request.Query["IdTermin"].ToString();

            ViewBag.idTermin = param;

            var uloga = "";
            foreach(var u in korisnik)
            {
                uloga = u.Uloga;
            }
            if(uloga != "")
            {
                ViewBag.uloga = uloga;
            }
            if (uloga == "psiholog")
            {

                return View(await mojPsihologContext.Where(k=>k.Korisnickoime==korisnickoime)
                    .ToListAsync());
            }

            return View(await mojPsihologContext.ToListAsync());
        }

        public async Task<IActionResult> terminiTekovenMesecIGodinaGradPrilepOdPocekeodCasI40()
        {
            var korisnickoime = HttpContext.Session.GetString("korisnickoime");
            var korisnik = _context.Korisniks.Where(k => k.Korisnickoime == korisnickoime);
            ViewBag.korisnickoime = korisnickoime;
            var mojPsihologContext = _context.Termins.Include(t => t.KorisnickoimeNavigation);

           
            var terminVoTekovnaGodina = mojPsihologContext.Where(x => DateTime.SpecifyKind((DateTime)x.Datum, DateTimeKind.Utc).Year == DateTime.Now.Year)
                   .Where(x => DateTime.SpecifyKind((DateTime)x.Datum, DateTimeKind.Utc).Month == DateTime.Now.Month)
               .Where(x => x.KorisnickoimeNavigation.KorisnickoimeNavigation.Grad == "Prilep")
                   .Where(x => x.KorisnickoimeNavigation.KorisnickoimeNavigation.Meil.Contains("sk"))
                   .GroupBy(x => new { x.Korisnickoime, x.Datum });

            var part2 = terminVoTekovnaGodina.Select(g => new
            {
                vreme = g.FirstOrDefault().Vreme,
                korisnickoime = g.FirstOrDefault().Korisnickoime,
                datum = g.FirstOrDefault().Datum

            });

            
            var max = part2.Max(x => x.vreme);

            var kor = "";
            DateTime datum = new DateTime();
            foreach (var part in part2)
            {
                if (part.vreme == max && max >= 1.4)
                {
                    kor = part.korisnickoime;
                    datum = (DateTime)part.datum;
                }
            }
            @ViewBag.korisnicko = kor;
            @ViewBag.datum = datum;

            return View();

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
            if (termin.Vreme!=null && termin.Datum!=null && termin.Grad!=null) {


                termin.KorisnickoimeNavigation = _context.Psihologs.Where(p => p.Korisnickoime
                == HttpContext.Session.GetString("korisnickoime")).FirstOrDefault();

                termin.Korisnickoime = HttpContext.Session.GetString("korisnickoime");
                termin.Datum = DateTime.SpecifyKind((DateTime)termin.Datum, DateTimeKind.Utc);


                _context.Add(termin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            if (termin.Vreme == null)
            {
                ModelState.AddModelError("ErrorVreme", "Внесете време!");
            }
            if (termin.Datum == null)
            {
                ModelState.AddModelError("ErrorDatum", "Внесете датум!!");
            }
            if (termin.Grad == null)
            {
                ModelState.AddModelError("ErrorGrad", "Внесете град!");
            }
            ViewBag.korisnickoime = HttpContext.Session.GetString("korisnickoime");
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

