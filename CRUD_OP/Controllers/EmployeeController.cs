using CRUD_OP.Data;
using CRUD_OP.Models;
//using CRUD_OP.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult Index(string searchString,string term = "", string orderBy = " ", int currentPage = 1)
        {


            var employees = _context.Employees.Include(x => x.Department).OrderBy(x => x.Name).ToList();
            var empData = new EmployeeViewModel();
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.Trim().Replace(" ", "").ToLower();
                employees = employees.Where(e => e.Name.Replace(" ", "").ToLower().Contains(searchString)
                                             || e.PhoneNumber.Replace(" ", "").ToLower().Contains(searchString)).ToList();
            }

            //employees = employees.Where(e=> e.Name.Contains(searchString)
            int totalRecords = employees.Count();
            int pageSize = 5;
            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            var Eemployees = employees.Skip((currentPage - 1) * pageSize).Take(pageSize);
            //current page=1 , skip =(1-1=0)*5 =1 take=page size which is 5
            //current page = 2 ,skip=(2-1)*5 = 5  take =5 record 
            empData.Employees= Eemployees.ToList();
            empData.CurrentPage = currentPage;
            empData.TotalPages = totalPages;
            empData.Term = term;
            empData.pageSize = pageSize;
            empData.OrderBy = orderBy;
            return View(empData);
        }

    


        public IActionResult Create()
        {
            ViewBag.Departments = _context.Departments.OrderBy(x => x.DepartmentName).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Employees.Add(employee);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                
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
                    ModelState.AddModelError("", "An error occurred while saving your data. Please try again.");
                }
            }
        }

        ViewBag.Departments = _context.Departments.OrderBy(d => d.DepartmentName).ToList();
            return View(employee);
    }



    public IActionResult Edit(int id)
        {
            ViewBag.Departments = _context.Departments.OrderBy(d => d.DepartmentName).ToList(); //ViewBag.Departments: ViewBag is a dynamic property of the controller base class that you can use to pass data from the controller to the view.
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
