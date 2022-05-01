using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Airport_Database.Models;

namespace Airport_Database.Controllers
{
    public class TestInfoesController : Controller
    {
        private readonly AirportDatabaseContext _context;

        public TestInfoesController(AirportDatabaseContext context)
        {
            _context = context;
        }

        // GET: TestInfoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TestInfo.ToListAsync());
        }

        // GET: TestInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testInfo = await _context.TestInfo
                .FirstOrDefaultAsync(m => m.TestNo == id);
            if (testInfo == null)
            {
                return NotFound();
            }

            return View(testInfo);
        }

        // GET: TestInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TestInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TestNo,Name,MaxScore")] TestInfo testInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(testInfo);
        }

        // GET: TestInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testInfo = await _context.TestInfo.FindAsync(id);
            if (testInfo == null)
            {
                return NotFound();
            }
            return View(testInfo);
        }

        // POST: TestInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TestNo,Name,MaxScore")] TestInfo testInfo)
        {
            if (id != testInfo.TestNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestInfoExists(testInfo.TestNo))
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
            return View(testInfo);
        }

        // GET: TestInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testInfo = await _context.TestInfo
                .FirstOrDefaultAsync(m => m.TestNo == id);
            if (testInfo == null)
            {
                return NotFound();
            }

            return View(testInfo);
        }

        // POST: TestInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var testInfo = await _context.TestInfo.FindAsync(id);
            _context.TestInfo.Remove(testInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestInfoExists(int id)
        {
            return _context.TestInfo.Any(e => e.TestNo == id);
        }
    }
}
