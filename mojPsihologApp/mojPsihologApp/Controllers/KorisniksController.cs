using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mojPsihologApp.Models;
using mojPsihologApp.mojPsihologDbContext;
using Microsoft.IdentityModel.Tokens;

namespace mojPsihologApp.Controllers
{
    public class KorisniksController : Controller
    {
        private readonly MojPsihologContext _context;

        public KorisniksController(MojPsihologContext context)
        {
            _context = context;
        }

        // GET: Korisniks
        public async Task<IActionResult> Index()
        {
            ViewBag.korisnickoime = HttpContext.Session.GetString("korisnickoime");
            return View(await _context.Korisniks.ToListAsync());
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string Korisnickoime, string Lozinka)
        {
            if (Korisnickoime!=null || Lozinka!=null)
            {
                var korisniks = await _context.Korisniks.Where(k => k.Korisnickoime == Korisnickoime).FirstOrDefaultAsync();
                if (korisniks != null && korisniks.Lozinka == Lozinka)
                {
                    HttpContext.Session.SetString("korisnickoime", korisniks.Korisnickoime);
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        public async Task<IActionResult> LogOut(string id)
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }


        // GET: Korisniks/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Korisniks == null)
            {
                return NotFound();
            }

            var korisnik = await _context.Korisniks
                .FirstOrDefaultAsync(m => m.Korisnickoime == id);
            if (korisnik == null)
            {
                return NotFound();
            }

            return View(korisnik);
        }

        // GET: Korisniks/Create
        public IActionResult Register()
        {
            List<Uloga> ulogi = new List<Uloga>();
            var u1 = new Uloga()
            {
                ime = "psiholog"
            };

            var u2 = new Uloga()
            {
                ime = "pacient"
            };

            ulogi.Add(u1);
            ulogi.Add(u2);

            ViewData["uloga"] = new SelectList(ulogi.ToList(), "ime", "ime");
            return View();
        }

        // POST: Korisniks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Korisnik korisnik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(korisnik);
                await _context.SaveChangesAsync();
                if (korisnik.Uloga == "psiholog")
                {
                    Psiholog psiholog = new Psiholog()
                    {
                        Korisnickoime = korisnik.Korisnickoime,
                        KorisnickoimeNavigation = _context.Korisniks.Where(p => p.Korisnickoime == korisnik.Korisnickoime)
                        .FirstOrDefault()
                    };
                    _context.Psihologs.Add(psiholog);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    Pacient pacient = new Pacient()
                    {
                        Korisnickoime = korisnik.Korisnickoime,
                        KorisnickoimeNavigation = _context.Korisniks.Where(p => p.Korisnickoime == korisnik.Korisnickoime)
                       .FirstOrDefault()
                    };

                    _context.Pacients.Add(pacient);
                    await _context.SaveChangesAsync();

                }
                return RedirectToAction(nameof(Login));
            }

            List<Uloga> ulogi = new List<Uloga>();
            var u1 = new Uloga()
            {
                ime = "psiholog"
            };

            var u2 = new Uloga()
            {
                ime = "pacient"
            };

            ulogi.Add(u1);
            ulogi.Add(u2);

            ViewData["uloga"] = new SelectList(ulogi.ToList(), "ime", "ime");
            return View(korisnik);
        }

        // GET: Korisniks/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Korisniks == null)
            {
                return NotFound();
            }

            var korisnik = await _context.Korisniks.FindAsync(id);
            if (korisnik == null)
            {
                return NotFound();
            }
            return View(korisnik);
        }

        // POST: Korisniks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Korisnickoime,Ime,Prezime,Uloga,Telefon,Meil,Lozinka,Ulica,Grad")] Korisnik korisnik)
        {
            if (id != korisnik.Korisnickoime)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(korisnik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KorisnikExists(korisnik.Korisnickoime))
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
            return View(korisnik);
        }

        // GET: Korisniks/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Korisniks == null)
            {
                return NotFound();
            }

            var korisnik = await _context.Korisniks
                .FirstOrDefaultAsync(m => m.Korisnickoime == id);
            if (korisnik == null)
            {
                return NotFound();
            }

            return View(korisnik);
        }

        // POST: Korisniks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Korisniks == null)
            {
                return Problem("Entity set 'MojPsihologContext.Korisniks'  is null.");
            }
            var korisnik = await _context.Korisniks.FindAsync(id);
            if (korisnik != null)
            {
                _context.Korisniks.Remove(korisnik);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KorisnikExists(string id)
        {
            return _context.Korisniks.Any(e => e.Korisnickoime == id);
        }
    }
}
