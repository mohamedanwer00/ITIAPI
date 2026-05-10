using ITIAPI.DTO;
using ITIAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITIAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ITIEntity _context;

        public DepartmentController(ITIEntity context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllDepartment()
        {
            List<Department> departments = _context.Departments.ToList();
            return Ok(departments);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            //Department? department = _context.Departments.FirstOrDefault(d => d.Id == id);
            Department department = _context.Departments.Include(d => d.Employee).FirstOrDefault(d => d.Id == id);
            DepartmentDetailsWithEmployeeName DepDto = new DepartmentDetailsWithEmployeeName();
            DepDto.Id = id;
            DepDto.Name = department.Name;
            foreach (var item in department.Employee)
            {
                DepDto.EmployeesName.Add(item.Name);
            }
            return Ok(DepDto);
        }
        [HttpPost]
        public IActionResult PostAllDepartment(Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Departments.Add(department);
            _context.SaveChanges();
            return Ok("Department added successfully");
        }

        [HttpPut("{id}")]
        public IActionResult PutDepartment(int id, Department department)
        {
            if (ModelState.IsValid == true)
            {
                Department? OldDept = _context.Departments.FirstOrDefault(d => d.Id == id);
                if (OldDept != null)
                {
                    OldDept.Name = department.Name;
                    OldDept.Manager = department.Manager;
                    _context.SaveChanges();
                    return StatusCode(204, OldDept);
                }
                return BadRequest("Id Not Valid");

            }
            return BadRequest(ModelState);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteDepartment(int id)
        {
            Department? OldDept = _context.Departments.FirstOrDefault(d => d.Id == id);
            if (OldDept != null)
            {
                _context.Departments.Remove(OldDept);
                _context.SaveChanges();
                return StatusCode(204, OldDept);
            }
            return BadRequest("Id Not Valid");
        }
    }
}