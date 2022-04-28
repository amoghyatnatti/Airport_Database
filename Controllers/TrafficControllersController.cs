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
    public class TrafficControllersController : Controller
    {
        private readonly AirportDatabaseContext _context;

        public TrafficControllersController(AirportDatabaseContext context)
        {
            _context = context;
        }

        // GET: TrafficControllers
        public async Task<IActionResult> Index(string searchString)
        {
            //var airportDatabaseContext = _context.TrafficController.Include(t => t.SsnNavigation);
            //return View(await airportDatabaseContext.ToListAsync());
            var airportDatabaseContext = from e in _context.Employee
                                         join tc in _context.TrafficController on e.Ssn equals tc.Ssn
                                         select e;
     //       return View(await airportDatabaseContext.ToListAsync());
            var employees = _context.Employee;
            var controllers = _context.TrafficController;

            var employeeRecord = from e in employees
                                 join d in controllers on e.Ssn equals d.Ssn
                                 select new TrafficDetails
                                 {
                                     employee = e,
                                     controller = d,
                                 };
            if (!String.IsNullOrEmpty(searchString))
            {
                employeeRecord = employeeRecord.Where(s => s.employee.Name!.Contains(searchString));
            }

            return View(await employeeRecord.ToListAsync());
        }

        // GET: TrafficControllers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trafficController = await _context.TrafficController
                .Include(t => t.SsnNavigation)
                .FirstOrDefaultAsync(m => m.Ssn == id);
            if (trafficController == null)
            {
                return NotFound();
            }

            return View(trafficController);
        }

        // GET: TrafficControllers/Create
        public IActionResult Create()
        {
            ViewData["Ssn"] = new SelectList(_context.Employee, "Ssn", "Name");
            return View();
        }

        // POST: TrafficControllers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ssn,MostRecentExamDate")] TrafficController trafficController)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trafficController);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Ssn"] = new SelectList(_context.Employee, "Ssn", "Name", trafficController.Ssn);
            return View(trafficController);
        }

        // GET: TrafficControllers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trafficController = await _context.TrafficController.FindAsync(id);
            if (trafficController == null)
            {
                return NotFound();
            }
            ViewData["Ssn"] = new SelectList(_context.Employee, "Ssn", "Name", trafficController.Ssn);
            return View(trafficController);
        }

        // POST: TrafficControllers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Ssn,MostRecentExamDate")] TrafficController trafficController)
        {
            if (id != trafficController.Ssn)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trafficController);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrafficControllerExists(trafficController.Ssn))
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
            ViewData["Ssn"] = new SelectList(_context.Employee, "Ssn", "Name", trafficController.Ssn);
            return View(trafficController);
        }

        // GET: TrafficControllers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trafficController = await _context.TrafficController
                .Include(t => t.SsnNavigation)
                .FirstOrDefaultAsync(m => m.Ssn == id);
            if (trafficController == null)
            {
                return NotFound();
            }

            return View(trafficController);
        }

        // POST: TrafficControllers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trafficController = await _context.TrafficController.FindAsync(id);
            _context.TrafficController.Remove(trafficController);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrafficControllerExists(int id)
        {
            return _context.TrafficController.Any(e => e.Ssn == id);
        }
    }
}
