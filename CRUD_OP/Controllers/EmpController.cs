using CRUD_OP.Data;
using CRUD_OP.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_OP.Controllers
{
    public class EmpController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmpController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public JsonResult DepartmentList()
        {
            var data = _context.Departments.ToList();
            return Json(data);
        }
        [HttpGet]
        public JsonResult EmployeList()
        {
            var data = _context.Employees.ToList();
            return Json(data);
        }

        [HttpPost]
        public JsonResult addEmployee(Employee objData)
        {
            var data = new Employee()
            {
                Name = objData.Name,
                PhoneNumber = objData.PhoneNumber,
                DepartmentId = objData.DepartmentId,
            };
            _context.Employees.Add(data);
            _context.SaveChanges();
            return Json("Data Insert Successfully!");
        }

        public JsonResult Delete(int id)
        {
            var data = _context.Employees.Where(x => x.EmployeeId == id).SingleOrDefault();
            _context.Employees.Remove(data);
            _context.SaveChanges();
            return Json("Data is Deleted");
        }
        [HttpGet]
        public JsonResult GetbyId(int Id)
        {
            var data = _context.Employees.Where(x => x.EmployeeId == Id).SingleOrDefault();
            return Json(data);
        }

        [HttpPost]
        public JsonResult Update(Employee employee)
        {
            _context.Employees.Update(employee);
            _context.SaveChanges();
            return Json("Update Data");
        }

    }
}

