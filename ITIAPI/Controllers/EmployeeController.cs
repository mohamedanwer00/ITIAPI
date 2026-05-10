using ITIAPI.DTO;
using ITIAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly ITIEntity _context;

    public EmployeeController(ITIEntity context)
    {
        _context = context;
    }

    //GET: api/Employee
    [HttpGet]
    public IActionResult GetAllEmployees()
    {
        List<Employee> employees = _context.Employees.Include(e => e.Department).ToList();
        return Ok(employees);
    }


    //// GET: api/Employee/5
    //[HttpGet("{id}")]
    //public async Task<IActionResult> GetOne(int id)
    //{
    //    var employee = await _context.Employees
    //        .Include(e => e.Department)
    //        .FirstOrDefaultAsync(e => e.Id == id);

    //    if (employee == null)
    //        return NotFound();

    //    return Ok(employee);
    //}


    [HttpGet("{id:int}", Name = "OneEmployeeRoute")]
    public IActionResult GetEmp(int id)
    {
        Employee Emp = _context.Employees.Include(s => s.Department)
            .FirstOrDefault(e => e.Id == id);
        EmployeeDataWithDepartmentNameDTO EmpDto = new EmployeeDataWithDepartmentNameDTO();
        EmpDto.DepartmentName = Emp.Department.Name;
        EmpDto.Name = Emp.Name;
        EmpDto.Id = Emp.Id;
        EmpDto.Address = Emp.Address;
        return Ok(EmpDto);
    }

    // POST: api/Employee
    [HttpPost]
    public async Task<IActionResult> Insert(Employee employee)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        return Ok(employee);
    }

    // PUT: api/Employee/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Employee updatedEmployee)
    {
        var employee = await _context.Employees.FindAsync(id);

        if (employee == null)
            return NotFound();

        employee.Name = updatedEmployee.Name;
        employee.Salary = updatedEmployee.Salary;
        employee.Address = updatedEmployee.Address;
        employee.Phone = updatedEmployee.Phone;
        employee.DeptID = updatedEmployee.DeptID;

        await _context.SaveChangesAsync();

        return Ok(employee);
    }

    // DELETE: api/Employee/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var employee = await _context.Employees.FindAsync(id);

        if (employee == null)
            return NotFound();

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Deleted successfully" });
    }
}