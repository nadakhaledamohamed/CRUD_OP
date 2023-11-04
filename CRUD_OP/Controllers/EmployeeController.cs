using CRUD_OP.Data;
using CRUD_OP.Models;
//using CRUD_OP.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
//using Microsoft.IdentityModel.Tokens;

//using System.Drawing.Printing;

namespace CRUD_OP.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string searchString, int currentPage = 1)
        {
            var employees = _context.Employees.Where(e => (string.IsNullOrEmpty(searchString) == true || e.Name.Contains(searchString.Trim())
                                           || e.PhoneNumber.Contains(searchString.Trim()))).Include(x => x.Department).OrderBy(x => x.Name).ToList();

            return View(new EmployeeViewModel
            {
                CurrentPage = currentPage,
                Employees = employees,
                pageSize = 5,
                TotalPages = (int)Math.Ceiling((double)employees.Count() / 5),
            });
        }




        public IActionResult Create()
        {
            ViewBag.Departments = _context.Departments.OrderBy(x => x.DepartmentName).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee model)
        {
            if (ModelState.IsValid)
            {
                var employee = _context.Employees.FirstOrDefault(e => e.Name.Equals(model.Name));
                if (employee is null)
                {
                    _context.Employees.Add(model);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewBag.Departments = _context.Departments.OrderBy(d => d.DepartmentName).ToList();
            ModelState.AddModelError("", "The provided Name or Phone Number already exists in the database.");
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Departments = _context.Departments.OrderBy(d => d.DepartmentName).ToList();
            var Emp = _context.Employees.Find(id);

            return View("Create", Emp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model != null)
                    {
                        _context.Employees.Update(model);
                        _context.SaveChanges();
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException != null && ex.InnerException is SqlException sqlEx &&
                        (sqlEx.Number == 2627 || sqlEx.Number == 547))
                    {
                        ModelState.AddModelError("", "The provided Name or Phone Number already exists in the database.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "An error occurred while updating your data. Please try again.");
                    }
                }
            }

            ViewBag.Departments = _context.Departments.OrderBy(d => d.DepartmentName).ToList();
            return View("Create", model); // Using "Create" view for Edit functionality
        }
        public IActionResult Delete(int id)
        {
            var result = _context.Employees.Find(id);
            if (result != null)
            {
                _context.Employees.Remove(result);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
