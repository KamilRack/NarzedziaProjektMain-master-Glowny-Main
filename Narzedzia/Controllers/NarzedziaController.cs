﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Narzedzia.Data;
using Narzedzia.Models;

namespace Narzedzia.Controllers
{
    public class NarzedziaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDAL _dal;

        public NarzedziaController(ApplicationDbContext context, IDAL dal)
        {
            _context = context;
            _dal = dal;
        }

        // GET: Narzedzia
        [Authorize(Roles = "admin, nadzor")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Narzedzia.Include(n => n.Kategorie).Include(n => n.Producenci).Include(n => n.Uzytkownicy);
            return View(await applicationDbContext.ToListAsync());
        }

        [Authorize(Roles = "admin, nadzor, pracownik")]

        public async Task<IActionResult> ListGraphic()
        {
            var applicationDbContext = _context.Narzedzia.Include(n => n.Kategorie).Include(n => n.Producenci).Include(n => n.Uzytkownicy);
            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize(Roles = "admin, nadzor")]
        // GET: Narzedzia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Narzedzia == null)
            {
                return NotFound();
            }

            var narzedzie = await _context.Narzedzia
                .Include(n => n.Kategorie)
                .Include(n => n.Producenci)
                .Include(n => n.Uzytkownicy)
                .FirstOrDefaultAsync(m => m.NarzedzieId == id);
            ViewBag.Status = narzedzie.Status;
            if (narzedzie == null)
            {
                return NotFound();
            }

            return View(narzedzie);
        }

        // GET: Narzedzia/Create
        public IActionResult Create()
        {
            ViewData["KategoriaId"] = new SelectList(_context.Kategorie.Where(c => c.Active == true), "KategoriaId", "NazwaKategorii");
            ViewData["ProducentId"] = new SelectList(_context.Producenci.Where(c => c.Active == true), "ProducentId", "NazwaProducenta");
            ViewData["UzytkownikId"] = new SelectList(_context.Uzytkownicy, "Id", "Id");
            return View();
        }

        // POST: Narzedzia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NarzedzieId,ProducentId,KategoriaId,DataPrzyjecia,UzytkownikId,NumerNarzedzia,Nazwa,Status,Uwagi,ZdjecieFileName")] Narzedzie narzedzie, IFormFile ZdjecieFileName)
        {
            if (ModelState.IsValid)
            {
                if (ZdjecieFileName != null && ZdjecieFileName.Length > 0)
                {
                    // Wygeneruj unikalny identyfikator dla nazwy pliku na serwerze (np. GUID)
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ZdjecieFileName.FileName);
                    string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/narzedziagraphic", uniqueFileName);

                    using (var stream = new FileStream(uploadPath, FileMode.Create))
                    {
                        await ZdjecieFileName.CopyToAsync(stream);
                    }

                    // Przypisz nazwę pliku do właściwości ZdjecieFileName modelu Narzedzie
                    narzedzie.ZdjecieFileName = uniqueFileName;
                }
                else
                {
                    // Jeśli nie przesłano pliku, przypisz domyślną nazwę pliku
                    narzedzie.ZdjecieFileName = "052ffaa7-6567-4203-84e9-79118520d54d_test2.jpg"; // Zmodyfikuj nazwę pliku według własnych potrzeb
                }

                _context.Add(narzedzie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Jeśli walidacja nie powiodła się, ponownie zwróć widok Create z błędami
            ViewData["KategoriaId"] = new SelectList(_context.Kategorie, "KategoriaId", "NazwaKategorii", narzedzie.KategoriaId);
            ViewData["ProducentId"] = new SelectList(_context.Producenci, "ProducentId", "NazwaProducenta", narzedzie.ProducentId);
            ViewData["UzytkownikId"] = new SelectList(_context.Uzytkownicy, "Id", "Id", narzedzie.UzytkownikId);
            return View(narzedzie);
        }

        // GET: Narzedzia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narzedzie = await _context.Narzedzia
                .Include(n => n.Uzytkownicy)
                .FirstOrDefaultAsync(m => m.NarzedzieId == id);

            if (narzedzie == null)
            {
                return NotFound();
            }

            ViewData["KategoriaId"] = new SelectList(_context.Kategorie, "KategoriaId", "NazwaKategorii", narzedzie.KategoriaId);
            ViewData["ProducentId"] = new SelectList(_context.Producenci, "ProducentId", "NazwaProducenta", narzedzie.ProducentId);
            ViewData["UzytkownikId"] = new SelectList(_context.Uzytkownicy, "Id", "Email", narzedzie.UzytkownikId);

            if (narzedzie.UzytkownikId == null)
            {
                ViewData["UzytkownikId"] = new SelectList(_context.Uzytkownicy, "Id", "Email");
            }
            else
            {
                ViewData["UzytkownikId"] = new SelectList(_context.Uzytkownicy, "Id", "Email", narzedzie.UzytkownikId);
                ViewData["Obecny"] = narzedzie.Uzytkownicy?.Email;
                ViewData["ObecnyId"] = narzedzie.UzytkownikId;
            }
            var updatedNarzedzie = await _context.Narzedzia
      .Include(a => a.Uzytkownicy)
      .FirstOrDefaultAsync(m => m.NarzedzieId == id);

            if (updatedNarzedzie != null)
            {
                narzedzie = updatedNarzedzie;
            }
            //
            return View(narzedzie);
        }


        // POST: Narzedzia/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NarzedzieId,ProducentId,KategoriaId,DataPrzyjecia,NumerNarzedzia,Nazwa,Status,UzytkownikId,Uwagi,ZdjecieFileName")] Narzedzie narzedzie, IFormFile? ZdjecieFileName, string? obecny, bool usunZdjecie = false)
        {
            if (id != narzedzie.NarzedzieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingNarzedzie = await _context.Narzedzia
                        .Include(n => n.Uzytkownicy)
                        .FirstOrDefaultAsync(m => m.NarzedzieId == id);

                    if (existingNarzedzie == null)
                    {
                        return NotFound();
                    }

                    // Usunięcie zdjęcia, jeśli użytkownik zaznaczył odpowiednią opcję
                    if (usunZdjecie && !string.IsNullOrEmpty(existingNarzedzie.ZdjecieFileName))
                    {
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/narzedziagraphic", existingNarzedzie.ZdjecieFileName);
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }

                        existingNarzedzie.ZdjecieFileName = null;
                    }

                    // Usunięcie zdjęcia, jeśli użytkownik zaznaczył odpowiednią opcję
                    if (usunZdjecie && !string.IsNullOrEmpty(existingNarzedzie.ZdjecieFileName))
                    {
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/narzedziagraphic", existingNarzedzie.ZdjecieFileName);
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }

                        existingNarzedzie.ZdjecieFileName = null;
                    }

                    // Jeśli użytkownik nie chce zmieniać zdjęcia i pole ZdjecieFileName nie jest puste, zachowaj obecne zdjęcie
                    if (ZdjecieFileName == null && !string.IsNullOrEmpty(existingNarzedzie.ZdjecieFileName))
                    {
                        narzedzie.ZdjecieFileName = existingNarzedzie.ZdjecieFileName;
                    }
                    else if (ZdjecieFileName != null && ZdjecieFileName.Length > 0)
                    {
                        // Jeśli użytkownik przesyła nowe zdjęcie, zaktualizuj je
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ZdjecieFileName.FileName);
                        string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/narzedziagraphic", uniqueFileName);

                        using (var stream = new FileStream(uploadPath, FileMode.Create))
                        {
                            await ZdjecieFileName.CopyToAsync(stream);
                        }

                        narzedzie.ZdjecieFileName = uniqueFileName;
                    }

                    // Dodaj przypisanie dla UzytkownikId tylko, jeśli obecny nie jest null
                    if (!(obecny == null))
                    {
                        existingNarzedzie.UzytkownikId = obecny;
                    }

                    // Aktualizacja pozostałych pól narzędzia
                    existingNarzedzie.ProducentId = narzedzie.ProducentId;
                    existingNarzedzie.KategoriaId = narzedzie.KategoriaId;
                    existingNarzedzie.DataPrzyjecia = narzedzie.DataPrzyjecia;
                    existingNarzedzie.NumerNarzedzia = narzedzie.NumerNarzedzia;
                    existingNarzedzie.Nazwa = narzedzie.Nazwa;
                    existingNarzedzie.Status = narzedzie.Status;
                    existingNarzedzie.Uwagi = narzedzie.Uwagi;

                    _context.Update(existingNarzedzie);
                    await _context.SaveChangesAsync();

                    ViewData["UzytkownikId"] = new SelectList(_context.Uzytkownicy, "Id", "Imie_Nazwisko", existingNarzedzie.UzytkownikId);
                    ViewData["Obecny"] = existingNarzedzie.Uzytkownicy?.Imie_Nazwisko;
                    ViewData["ObecnyId"] = existingNarzedzie.UzytkownikId;

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NarzedzieExists(narzedzie.NarzedzieId))
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

            // Inicjalizacja SelectList
            ViewData["KategoriaId"] = new SelectList(_context.Kategorie, "KategoriaId", "NazwaKategorii", narzedzie.KategoriaId);
            ViewData["ProducentId"] = new SelectList(_context.Producenci, "ProducentId", "NazwaProducenta", narzedzie.ProducentId);
            ViewData["UzytkownikId"] = new SelectList(_context.Uzytkownicy, "Id", "Id", narzedzie.UzytkownikId);

            if (narzedzie.UzytkownikId == null)
    {
        ViewData["UzytkownikId"] = new SelectList(_context.Uzytkownicy, "Id", "Email");
    }
    else
    {
        ViewData["UzytkownikId"] = new SelectList(_context.Uzytkownicy, "Id", "Email", narzedzie.UzytkownikId);
        ViewData["Obecny"] = narzedzie.Uzytkownicy?.Email;
        ViewData["ObecnyId"] = narzedzie.UzytkownikId;
    }

            return View(narzedzie);
        }
        // GET: Narzedzia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Narzedzia == null)
            {
                return NotFound();
            }

            var narzedzie = await _context.Narzedzia
                .Include(n => n.Kategorie)
                .Include(n => n.Producenci)
                .Include(n => n.Uzytkownicy)
                .FirstOrDefaultAsync(m => m.NarzedzieId == id);
            if (narzedzie == null)
            {
                return NotFound();
            }
            if (!NarzedzieZlikwidowane((int)id))
            {
                ViewBag.DeleteMessage = "Nie można usunąć wybranego elementu, gdyż nie został wcześniej zlikwidowany (zmiana statusu).";
            }

            return View(narzedzie);
        }

        // POST: Narzedzia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Narzedzia == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Narzedzia'  is null.");
            }
            var narzedzie = await _context.Narzedzia.FindAsync(id);
            if (narzedzie != null)
            {
                _context.Narzedzia.Remove(narzedzie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NarzedzieExists(int id)
        {
          return _context.Narzedzia.Any(e => e.NarzedzieId == id);
        }

        private bool NarzedzieZlikwidowane(int id)
        {
            var status =  _context.Narzedzia.FirstOrDefault(x => x.NarzedzieId == id).Status;
            if (status == Status.zlikwidowane) { return true; }
            return false;
        }
    }
}
