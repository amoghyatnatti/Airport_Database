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
    public class ExpertiseInsController : Controller
    {
        private readonly AirportDatabaseContext _context;

        public ExpertiseInsController(AirportDatabaseContext context)
        {
            _context = context;
        }

        // GET: ExpertiseIns
        public async Task<IActionResult> Index()
        {
            var airportDatabaseContext = _context.ExpertiseIn.Include(e => e.ModelNoNavigation).Include(e => e.SsnNavigation);
            return View(await airportDatabaseContext.ToListAsync());
        }

        // GET: ExpertiseIns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expertiseIn = await _context.ExpertiseIn
                .Include(e => e.ModelNoNavigation)
                .Include(e => e.SsnNavigation)
                .FirstOrDefaultAsync(m => m.Ssn == id);
            if (expertiseIn == null)
            {
                return NotFound();
            }

            return View(expertiseIn);
        }

        // GET: ExpertiseIns/Create
        public IActionResult Create()
        {
            ViewData["ModelNo"] = new SelectList(_context.Model, "ModelNo", "ModelNo");
            ViewData["Ssn"] = new SelectList(_context.Employee, "Ssn", "Name");
            return View();
        }

        // POST: ExpertiseIns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ssn,ModelNo")] ExpertiseIn expertiseIn)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expertiseIn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModelNo"] = new SelectList(_context.Model, "ModelNo", "ModelNo", expertiseIn.ModelNo);
            ViewData["Ssn"] = new SelectList(_context.Employee, "Ssn", "Name", expertiseIn.Ssn);
            return View(expertiseIn);
        }

        // GET: ExpertiseIns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expertiseIn = await _context.ExpertiseIn.FindAsync(id);
            if (expertiseIn == null)
            {
                return NotFound();
            }
            ViewData["ModelNo"] = new SelectList(_context.Model, "ModelNo", "ModelNo", expertiseIn.ModelNo);
            ViewData["Ssn"] = new SelectList(_context.Employee, "Ssn", "Name", expertiseIn.Ssn);
            return View(expertiseIn);
        }

        // POST: ExpertiseIns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Ssn,ModelNo")] ExpertiseIn expertiseIn)
        {
            if (id != expertiseIn.Ssn)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expertiseIn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpertiseInExists(expertiseIn.Ssn))
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
            ViewData["ModelNo"] = new SelectList(_context.Model, "ModelNo", "ModelNo", expertiseIn.ModelNo);
            ViewData["Ssn"] = new SelectList(_context.Employee, "Ssn", "Name", expertiseIn.Ssn);
            return View(expertiseIn);
        }

        // GET: ExpertiseIns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expertiseIn = await _context.ExpertiseIn
                .Include(e => e.ModelNoNavigation)
                .Include(e => e.SsnNavigation)
                .FirstOrDefaultAsync(m => m.Ssn == id);
            if (expertiseIn == null)
            {
                return NotFound();
            }

            return View(expertiseIn);
        }

        // POST: ExpertiseIns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expertiseIn = await _context.ExpertiseIn.FindAsync(id);
            _context.ExpertiseIn.Remove(expertiseIn);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpertiseInExists(int id)
        {
            return _context.ExpertiseIn.Any(e => e.Ssn == id);
        }
    }
}
