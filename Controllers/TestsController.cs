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

        public IActionResult AddTest(long? RegistrationNo)
        {
            if(RegistrationNo == null)
            {
                return NotFound("RegistrationNo Null");
            }
            Airplane airplane = (Airplane)_context.Airplane.Where(a => a.RegistrationNo == RegistrationNo).First();
            if(airplane == null)
            {
                return NotFound("Airplane " + RegistrationNo + " not found");
            }
            int? ModelNo = airplane.ModelNo;
            if(ModelNo == null)
            {
                return NotFound();
            }
            //ViewData["RegistrationNo"] = new SelectList(_context.Airplane, "RegistrationNo", "RegistrationNo");
            LinkedList<long?> reglist = new LinkedList<long?>();
            reglist.AddFirst(RegistrationNo);
            var techswithexp = _context.Technician.Join(_context.ExpertiseIn, t => t.Ssn, e => e.Ssn, (t, e) => new { t.Ssn, e.ModelNo }).Where(e => e.ModelNo == ModelNo);
            var validtechnicians = techswithexp.Join(_context.Employee, t => t.Ssn, e => e.Ssn, (t, e) => new { e.Ssn, e.Name });
            ViewData["RegistrationNo"] = new SelectList(reglist);
            ViewData["Ssn"] = new SelectList(validtechnicians, "Ssn", "Name");
            ViewData["TestNo"] = new SelectList(_context.TestInfo, "TestNo", "Name");
            ViewData["errstring"] = new LinkedList<string>();
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTest([Bind("TestNo,RegistrationNo,Ssn,Date,NoHours,Score")] Test test)
        {
            string errstring = "";
            if (ModelState.IsValid)
            {
                if (_context.Test.Where(t => t.RegistrationNo == test.RegistrationNo && t.Date == test.Date && t.TestNo == test.TestNo).ToList().Count > 0)
                {
                    errstring = "A test of the same type already exists on that date";
                }
                else if (_context.TestInfo.Where(t => t.TestNo == test.TestNo).First().MaxScore < test.Score)
                {
                    errstring = "Score cannot be greater than the max score for a test";
                }
                else
                {
                    _context.Add(test);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(AirplaneData), "Airplanes");
                }
            }

            Airplane airplane = (Airplane)_context.Airplane.Where(a => a.RegistrationNo == test.RegistrationNo).First();
            if (airplane == null)
            {
                return NotFound("Airplane " + test.RegistrationNo + " not found");
            }
            int? ModelNo = airplane.ModelNo;
            if (ModelNo == null)
            {
                return NotFound();
            }
            //ViewData["RegistrationNo"] = new SelectList(_context.Airplane, "RegistrationNo", "RegistrationNo");
            LinkedList<long?> reglist = new LinkedList<long?>();
            LinkedList<string> errlist = new LinkedList<string>();
            reglist.AddFirst(test.RegistrationNo);
            errlist.AddFirst(errstring);
            var techswithexp = _context.Technician.Join(_context.ExpertiseIn, t => t.Ssn, e => e.Ssn, (t, e) => new { t.Ssn, e.ModelNo }).Where(e => e.ModelNo == ModelNo);
            var validtechnicians = techswithexp.Join(_context.Employee, t => t.Ssn, e => e.Ssn, (t, e) => new { e.Ssn, e.Name });
            ViewData["RegistrationNo"] = new SelectList(reglist);
            ViewData["Ssn"] = new SelectList(validtechnicians, "Ssn", "Name");
            ViewData["TestNo"] = new SelectList(_context.TestInfo, "TestNo", "Name");
            ViewData["errstring"] = errlist;
            return View();
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
