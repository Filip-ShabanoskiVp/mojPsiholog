using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagePack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using mojPsihologApp.Models;
using mojPsihologApp.mojPsihologDbContext;

namespace mojPsihologApp.Controllers
{
    public class DavaOcenkaController : Controller
    {
        private readonly MojPsihologContext _context;

        public DavaOcenkaController(MojPsihologContext context)
        {
            _context = context;
        }

        // GET: DavaOcenka
        public async Task<IActionResult> Index()
        {

            var mojPsihologContext = _context.DavaOcenkas.Include(d => d.IdOcenkaNavigation).Include(d => d.KorisnickoimepacientNavigation).Include(d => d.KorisnickoimepsihologNavigation);

            var korisnickoime = HttpContext.Session.GetString("korisnickoime");

            ViewBag.korisnickoime = korisnickoime;

            var uloga = _context.Korisniks.Where(k => k.Korisnickoime == korisnickoime).FirstOrDefault();

            if (uloga != null)
            {
                ViewBag.uloga = uloga.Uloga;
            }


            return View(await mojPsihologContext.ToListAsync());
        }


        public async Task<IActionResult> najdobarPsiholog()
        {

            var mojPsihologContext = _context.DavaOcenkas.Include(d => d.IdOcenkaNavigation).Include(d => d.KorisnickoimepacientNavigation).Include(d => d.KorisnickoimepsihologNavigation);

            var korisnickoime = HttpContext.Session.GetString("korisnickoime");

            ViewBag.korisnickoime = korisnickoime;

            var averages = mojPsihologContext.GroupBy(x => x.Korisnickoimepsiholog)
                .Select(x => new
                {
                    x.Key,
                    avg = x.Average(x => x.IdOcenkaNavigation.Ocenka1)
                });

            var max = averages.Max(x => x.avg);
            var psiholog = "";
            foreach (var p in averages)
            {
                if (p.avg == max)
                {
                    max = p.avg;
                    psiholog = p.Key;
                }
            }


            if (_context.Termins.Where(x => x.Korisnickoime == psiholog).Any())
            {
                ViewBag.maxProsek = max;
                var maxKorisnik = _context.Korisniks.Where(x => x.Korisnickoime == psiholog).ToList();
                foreach (var k in maxKorisnik)
                {
                    @ViewBag.kor = k.Korisnickoime;
                    @ViewBag.ime = k.Ime;
                    @ViewBag.prezime = k.Prezime;
                }
            }
            return View();
        }



        // GET: DavaOcenka/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.DavaOcenkas == null)
            {
                return NotFound();
            }

            var davaOcenka = await _context.DavaOcenkas
                .Include(d => d.IdOcenkaNavigation)
                .Include(d => d.KorisnickoimepacientNavigation)
                .Include(d => d.KorisnickoimepsihologNavigation)
                .FirstOrDefaultAsync(m => m.Korisnickoimepacient == id);
            if (davaOcenka == null)
            {
                return NotFound();
            }

            return View(davaOcenka);
        }

        // GET: DavaOcenka/Create
        public IActionResult Create()
        {
            var ocenka = _context.Ocenkas;
            ViewData["Ocenka"] = new SelectList(_context.Ocenkas, "IdOcenka", "Ocenka1");
            ViewData["Korisnickoimepacient"] = new SelectList(_context.Pacients, "Korisnickoime", "Korisnickoime");
            ViewData["Korisnickoimepsiholog"] = new SelectList(_context.Psihologs, "Korisnickoime", "Korisnickoime");
            return View();
        }

        // POST: DavaOcenka/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DavaOcenka davaOcenka)
        {
           
            var currentUser = HttpContext.Session.GetString("korisnickoime");
            davaOcenka.Korisnickoimepacient = currentUser;
           
            davaOcenka.KorisnickoimepacientNavigation = _context.Pacients.Where(p => p.Korisnickoime
              == HttpContext.Session.GetString("korisnickoime")).FirstOrDefault();

            var pacient = _context.DavaOcenkas.Where(x => x.Korisnickoimepacient == currentUser);

            foreach(var p in pacient)
            {

                if (p.Korisnickoimepsiholog == davaOcenka.Korisnickoimepsiholog)
                {
                    ModelState.AddModelError("Error", "Корисникот "+ p.Korisnickoimepacient + " веќе дал оценка за психологот " + p.Korisnickoimepsiholog);
                    ViewData["Ocenka"] = new SelectList(_context.Ocenkas, "IdOcenka", "Ocenka1");
                    ViewData["Korisnickoimepsiholog"] = new SelectList(_context.Psihologs, "Korisnickoime", "Korisnickoime", davaOcenka.Korisnickoimepsiholog);
                    return View(davaOcenka);
                }
            }


            _context.Add(davaOcenka);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            }

        // GET: DavaOcenka/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.DavaOcenkas == null)
            {
                return NotFound();
            }

            var davaOcenka = await _context.DavaOcenkas.FindAsync(id);
            if (davaOcenka == null)
            {
                return NotFound();
            }
            ViewData["IdOcenka"] = new SelectList(_context.Ocenkas, "IdOcenka", "IdOcenka", davaOcenka.IdOcenka);
            ViewData["Korisnickoimepacient"] = new SelectList(_context.Pacients, "Korisnickoime", "Korisnickoime", davaOcenka.Korisnickoimepacient);
            ViewData["Korisnickoimepsiholog"] = new SelectList(_context.Psihologs, "Korisnickoime", "Korisnickoime", davaOcenka.Korisnickoimepsiholog);
            return View(davaOcenka);
        }

        // POST: DavaOcenka/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Korisnickoimepacient,Korisnickoimepsiholog,IdOcenka")] DavaOcenka davaOcenka)
        {
            if (id != davaOcenka.Korisnickoimepacient)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(davaOcenka);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DavaOcenkaExists(davaOcenka.Korisnickoimepacient))
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
            ViewData["IdOcenka"] = new SelectList(_context.Ocenkas, "IdOcenka", "IdOcenka", davaOcenka.IdOcenka);
            ViewData["Korisnickoimepacient"] = new SelectList(_context.Pacients, "Korisnickoime", "Korisnickoime", davaOcenka.Korisnickoimepacient);
            ViewData["Korisnickoimepsiholog"] = new SelectList(_context.Psihologs, "Korisnickoime", "Korisnickoime", davaOcenka.Korisnickoimepsiholog);
            return View(davaOcenka);
        }

        // GET: DavaOcenka/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.DavaOcenkas == null)
            {
                return NotFound();
            }

            var davaOcenka = await _context.DavaOcenkas
                .Include(d => d.IdOcenkaNavigation)
                .Include(d => d.KorisnickoimepacientNavigation)
                .Include(d => d.KorisnickoimepsihologNavigation)
                .FirstOrDefaultAsync(m => m.Korisnickoimepacient == id);
            if (davaOcenka == null)
            {
                return NotFound();
            }

            return View(davaOcenka);
        }

        // POST: DavaOcenka/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.DavaOcenkas == null)
            {
                return Problem("Entity set 'MojPsihologContext.DavaOcenkas'  is null.");
            }
            var davaOcenka = await _context.DavaOcenkas.FindAsync(id);
            if (davaOcenka != null)
            {
                _context.DavaOcenkas.Remove(davaOcenka);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DavaOcenkaExists(string id)
        {
          return _context.DavaOcenkas.Any(e => e.Korisnickoimepacient == id);
        }
    }
}

