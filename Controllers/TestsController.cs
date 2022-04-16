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
    public class TestsController : Controller
    {
        private readonly AirportDatabaseContext _context;

        public TestsController(AirportDatabaseContext context)
        {
            _context = context;
        }

        // GET: Tests
        public async Task<IActionResult> Index()
        {
            var airportDatabaseContext = _context.Test.Include(t => t.RegistrationNoNavigation).Include(t => t.SsnNavigation).Include(t => t.TestNoNavigation);
            return View(await airportDatabaseContext.ToListAsync());
        }

        // GET: Tests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.Test
                .Include(t => t.RegistrationNoNavigation)
                .Include(t => t.SsnNavigation)
                .Include(t => t.TestNoNavigation)
                .FirstOrDefaultAsync(m => m.TestNo == id);
            if (test == null)
            {
                return NotFound();
            }

            return View(test);
        }

        // GET: Tests/Create
        public IActionResult Create()
        {
            ViewData["RegistrationNo"] = new SelectList(_context.Airplane, "RegistrationNo", "RegistrationNo");
            ViewData["Ssn"] = new SelectList(_context.Technician, "Ssn", "Ssn");
            ViewData["TestNo"] = new SelectList(_context.TestInfo, "TestNo", "TestNo");
            return View();
        }

        // POST: Tests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TestNo,RegistrationNo,Ssn,Date,NoHours,Score")] Test test)
        {
            if (ModelState.IsValid)
            {
                _context.Add(test);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RegistrationNo"] = new SelectList(_context.Airplane, "RegistrationNo", "RegistrationNo", test.RegistrationNo);
            ViewData["Ssn"] = new SelectList(_context.Technician, "Ssn", "Ssn", test.Ssn);
            ViewData["TestNo"] = new SelectList(_context.TestInfo, "TestNo", "TestNo", test.TestNo);
            return View(test);
        }

        // GET: Tests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.Test.FindAsync(id);
            if (test == null)
            {
                return NotFound();
            }
            ViewData["RegistrationNo"] = new SelectList(_context.Airplane, "RegistrationNo", "RegistrationNo", test.RegistrationNo);
            ViewData["Ssn"] = new SelectList(_context.Technician, "Ssn", "Ssn", test.Ssn);
            ViewData["TestNo"] = new SelectList(_context.TestInfo, "TestNo", "TestNo", test.TestNo);
            return View(test);
        }

        // POST: Tests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TestNo,RegistrationNo,Ssn,Date,NoHours,Score")] Test test)
        {
            if (id != test.TestNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(test);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestExists(test.TestNo))
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
            ViewData["RegistrationNo"] = new SelectList(_context.Airplane, "RegistrationNo", "RegistrationNo", test.RegistrationNo);
            ViewData["Ssn"] = new SelectList(_context.Technician, "Ssn", "Ssn", test.Ssn);
            ViewData["TestNo"] = new SelectList(_context.TestInfo, "TestNo", "TestNo", test.TestNo);
            return View(test);
        }

        // GET: Tests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.Test
                .Include(t => t.RegistrationNoNavigation)
                .Include(t => t.SsnNavigation)
                .Include(t => t.TestNoNavigation)
                .FirstOrDefaultAsync(m => m.TestNo == id);
            if (test == null)
            {
                return NotFound();
            }

            return View(test);
        }

        // POST: Tests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var test = await _context.Test.FindAsync(id);
            _context.Test.Remove(test);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestExists(int id)
        {
            return _context.Test.Any(e => e.TestNo == id);
        }
    }
}
