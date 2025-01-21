using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LanguageInstall.Data.Data;
using LanguageInstall.Data.Model;

namespace LanguageInstallMVC.Controllers
{
    public class TranslationsController : Controller
    {
        private readonly AppDbContext _context;

        public TranslationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Translations
        public async Task<IActionResult> Index(string languageCode = null, string searchText = null)
        {
            // Start building the query with the Include
            var translationsQuery = _context.Translation
                .Include(t => t.MainTable) // Ensure Include is here
                .Where(t => string.IsNullOrEmpty(languageCode) || t.LanguageCode == languageCode);

            // If searchText is provided, filter by EnglishText or TranslatedText
            if (!string.IsNullOrEmpty(searchText))
            {
                translationsQuery = translationsQuery.Where(t =>
                    t.MainTable.EnglishText.Contains(searchText) || t.TranslatedText.Contains(searchText));
            }

            // Await the translations query to get the filtered results
            var translations = await translationsQuery.ToListAsync();

            // Fetch distinct language codes
            var languageCodes = await _context.Translation
                .Select(t => t.LanguageCode)
                .Distinct()
                .ToListAsync();

            // Pass the language codes and search text to the view
            ViewData["LanguageCodes"] = languageCodes;
            ViewBag.lang = languageCodes;
            ViewBag.SearchText = searchText;

            return View(translations);
        }


        // GET: Translations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var translation = await _context.Translation
                .Include(t => t.MainTable)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (translation == null)
            {
                return NotFound();
            }

            return View(translation);
        }

        // GET: Translations/Create
        public IActionResult Create()
        {
            ViewData["MainTableID"] = new SelectList(_context.MainTables, "ID", "EnglishText");
            return View();
        }

        // POST: Translations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,MainTableID,LanguageCode,TranslatedText")] Translation translation)
        {
            try
            {
                _context.Add(translation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
            //if (ModelState.IsValid)
            //{
            //    _context.Add(translation);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["MainTableID"] = new SelectList(_context.MainTables, "ID", "EnglishText", translation.MainTableID);
            //return View(translation);
        }

        // GET: Translations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var translation = await _context.Translation.FindAsync(id);
            if (translation == null)
            {
                return NotFound();
            }
            ViewData["MainTableID"] = new SelectList(_context.MainTables, "ID", "EnglishText", translation.MainTableID);
            return View(translation);
        }

        // POST: Translations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,MainTableID,LanguageCode,TranslatedText")] Translation translation)
        {
            if (id != translation.ID)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(translation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TranslationExists(translation.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            //ViewData["MainTableID"] = new SelectList(_context.MainTables, "ID", "EnglishText", translation.MainTableID);
            //return View(translation);
        }

        // GET: Translations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var translation = await _context.Translation
                .Include(t => t.MainTable)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (translation == null)
            {
                return NotFound();
            }

            return View(translation);
        }

        // POST: Translations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var translation = await _context.Translation.FindAsync(id);
            if (translation != null)
            {
                _context.Translation.Remove(translation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TranslationExists(int id)
        {
            return _context.Translation.Any(e => e.ID == id);
        }
    }
}
