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
    public class MainTablesController : Controller
    {
        private readonly AppDbContext _context;

        public MainTablesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: MainTables
        public async Task<IActionResult> Index()
        {
            return View(await _context.MainTables.ToListAsync());
        }

        // GET: MainTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mainTable = await _context.MainTables
                .FirstOrDefaultAsync(m => m.ID == id);
            if (mainTable == null)
            {
                return NotFound();
            }

            return View(mainTable);
        }

        // GET: MainTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MainTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,EnglishText")] MainTable mainTable)
        {
            //if (ModelState.IsValid)
            //{
            try
            {
                _context.Add(mainTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
                
            //}
            //return View(mainTable);
        }

        // GET: MainTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mainTable = await _context.MainTables.FindAsync(id);
            if (mainTable == null)
            {
                return NotFound();
            }
            return View(mainTable);
        }

        // POST: MainTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,EnglishText")] MainTable mainTable)
        {
            if (id != mainTable.ID)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(mainTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MainTableExists(mainTable.ID))
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
            //return View(mainTable);
        }

        // GET: MainTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mainTable = await _context.MainTables
                .FirstOrDefaultAsync(m => m.ID == id);
            if (mainTable == null)
            {
                return NotFound();
            }

            return View(mainTable);
        }

        // POST: MainTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mainTable = await _context.MainTables.FindAsync(id);
            if (mainTable != null)
            {
                _context.MainTables.Remove(mainTable);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MainTableExists(int id)
        {
            return _context.MainTables.Any(e => e.ID == id);
        }
    }
}
