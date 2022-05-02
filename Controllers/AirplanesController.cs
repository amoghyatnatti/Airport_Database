using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Airport_Database.Models;
using Microsoft.EntityFrameworkCore.Internal;

namespace Airport_Database.Controllers
{
    public class AirplanesController : Controller
    {
        private readonly AirportDatabaseContext _context;

        public AirplanesController(AirportDatabaseContext context)
        {
            _context = context;
        }

        // GET: Airplanes
        public async Task<IActionResult> Index()
        {
            var airportDatabaseContext = _context.Airplane.Include(a => a.ModelNoNavigation);
            return View(await airportDatabaseContext.ToListAsync());
        }

        public async Task<IActionResult> AirplaneData()
        {
            var airplane = _context.Airplane;
            var model = _context.Model;
            var airplanedata = await airplane.Join(model, a => a.ModelNo, m => m.ModelNo, (a, m) => new AirplaneData(a.RegistrationNo, a.ModelNo, m.Capacity, m.Weight)).ToListAsync();
            return View(airplanedata);
        }

        public async Task<IActionResult> ViewTests(long? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var tests = _context.Test.Where(a => a.RegistrationNo == id);
            var testinfo = _context.TestInfo.Join(tests, t => t.TestNo, t => t.TestNo, (ti, t) => new { t.TestNo, t.RegistrationNo, t.Ssn, t.Score, ti.MaxScore, ti.Name, t.Date});
            return View(await testinfo.Join(_context.Employee, t => t.Ssn, e => e.Ssn, (t, e) => new AirplaneTest(t.TestNo, t.RegistrationNo, t.Score, t.MaxScore, t.Date, t.Name, e.Name, e.Ssn)).ToListAsync());
        }

        // GET: Airplanes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airplane = await _context.Airplane
                .Include(a => a.ModelNoNavigation)
                .FirstOrDefaultAsync(m => m.RegistrationNo == id);
            if (airplane == null)
            {
                return NotFound();
            }

            return View(airplane);
        }

        // GET: Airplanes/Create
        public IActionResult Create()
        {
            ViewData["ModelNo"] = new SelectList(_context.Model, "ModelNo", "ModelNo");
            return View();
        }

        // POST: Airplanes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegistrationNo,ModelNo")] Airplane airplane)
        {
            if (ModelState.IsValid)
            {
                _context.Add(airplane);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AirplaneData));
            }
            ViewData["ModelNo"] = new SelectList(_context.Model, "ModelNo", "ModelNo", airplane.ModelNo);
            return View(airplane);
        }

        // GET: Airplanes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airplane = await _context.Airplane.FindAsync(id);
            if (airplane == null)
            {
                return NotFound();
            }
            ViewData["ModelNo"] = new SelectList(_context.Model, "ModelNo", "ModelNo", airplane.ModelNo);
            return View(airplane);
        }

        // POST: Airplanes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("RegistrationNo,ModelNo")] Airplane airplane)
        {
            if (id != airplane.RegistrationNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(airplane);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AirplaneExists(airplane.RegistrationNo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(AirplaneData));
            }
            ViewData["ModelNo"] = new SelectList(_context.Model, "ModelNo", "ModelNo", airplane.ModelNo);
            return View(airplane);
        }

        // GET: Airplanes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airplane = await _context.Airplane
                .Include(a => a.ModelNoNavigation)
                .FirstOrDefaultAsync(m => m.RegistrationNo == id);
            if (airplane == null)
            {
                return NotFound();
            }

            return View(airplane);
        }

        // POST: Airplanes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var airplane = await _context.Airplane.FindAsync(id);
            _context.Airplane.Remove(airplane);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AirplaneData));
        }

        private bool AirplaneExists(long id)
        {
            return _context.Airplane.Any(e => e.RegistrationNo == id);
        }
    }
}
