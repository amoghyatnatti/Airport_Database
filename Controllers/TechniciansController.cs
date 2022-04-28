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
    public class TechniciansController : Controller
    {
        private readonly AirportDatabaseContext _context;

        public TechniciansController(AirportDatabaseContext context)
        {
            _context = context;
        }

        // GET: Technicians
        public async Task<IActionResult> Index(string searchString)
        {
            var airportDatabaseContext = from e in _context.Employee
                                         join t in _context.Technician on e.Ssn equals t.Ssn
                                         select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                airportDatabaseContext = airportDatabaseContext.Where(s => s.Name!.Contains(searchString));
            }
           
            return View(await airportDatabaseContext.ToListAsync());
        }

        // GET: Technicians/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technician = await _context.Technician
                .Include(t => t.SsnNavigation)
                .FirstOrDefaultAsync(m => m.Ssn == id);
            if (technician == null)
            {
                return NotFound();
            }

            return View(technician);
        }

        // GET: Technicians/Create
        public IActionResult Create()
        {
            ViewData["Ssn"] = new SelectList(_context.Employee, "Ssn", "Name");
            return View();
        }

        // POST: Technicians/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ssn")] Technician technician)
        {
            if (ModelState.IsValid)
            {
                _context.Add(technician);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Ssn"] = new SelectList(_context.Employee, "Ssn", "Name", technician.Ssn);
            return View(technician);
        }

        // GET: Technicians/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technician = await _context.Technician.FindAsync(id);
            if (technician == null)
            {
                return NotFound();
            }
            ViewData["Ssn"] = new SelectList(_context.Employee, "Ssn", "Name", technician.Ssn);
            return View(technician);
        }

        // POST: Technicians/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Ssn")] Technician technician)
        {
            if (id != technician.Ssn)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(technician);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TechnicianExists(technician.Ssn))
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
            ViewData["Ssn"] = new SelectList(_context.Employee, "Ssn", "Name", technician.Ssn);
            return View(technician);
        }

        // GET: Technicians/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technician = await _context.Technician
                .Include(t => t.SsnNavigation)
                .FirstOrDefaultAsync(m => m.Ssn == id);
            if (technician == null)
            {
                return NotFound();
            }

            return View(technician);
        }

        // POST: Technicians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var technician = await _context.Technician.FindAsync(id);
            _context.Technician.Remove(technician);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TechnicianExists(int id)
        {
            return _context.Technician.Any(e => e.Ssn == id);
        }
    }
}
