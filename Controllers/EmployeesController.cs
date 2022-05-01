using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Airport_Database.Data;
using Airport_Database.Models;
using System.Web;


namespace Airport_Database.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly AirportDatabaseContext _context;

        public EmployeesController(AirportDatabaseContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index(string searchString)
        {

            //List<Employee> employees = _context.Employee.ToList();
            //List<Technician> departments = _context.Technician.ToList();
            //List<TrafficController> incentives = _context.TrafficController.ToList();

            //var employeeRecord = from e in employees
            //                     join d in departments on e.Department_Id equals d.DepartmentId into table1
            //                     from d in table1.ToList()
            //                     join i in incentives on e.Incentive_Id equals i.IncentiveId into table2
            //                     from i in table2.ToList()
            //                     select new ViewModel
            //                     {
            //                         employee = e,
            //                         department = d,
            //                         incentive = i
            //                     };
            //return View(employeeRecord);
            var employees = from e in _context.Employee
                            select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(s => s.Name!.Contains(searchString));
            }

            return View(await employees.ToListAsync());
        }


        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.Ssn == id);
            //if (employee == null)
            //{
            //    return NotFound();
            //}
            //var techie = from e in _context.Employee
            //             join t in _context.Technician on e.Ssn equals t.Ssn
            //             select new { techName = e.Name };


                             //var controller = from e in _context.Employee
                             //             join tc in _context.TrafficController on e.Ssn equals tc.Ssn
                             //             select new { techName = e.Name };

            return View(employee);
        }


        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ssn,Name,Salary,UnionMemNo,PhoneNo,Street,Zip,State,City")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }
        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Ssn,Name,Salary,UnionMemNo,PhoneNo,Street,Zip,State,City")] Employee employee)
        {
            if (id != employee.Ssn)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Ssn))
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
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.Ssn == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Ssn == id);
        }
    }
}
