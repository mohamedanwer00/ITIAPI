namespace ITIAPI.DTO
{
    public class EmployeeDataWithDepartmentNameDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string Phone { get; set; } = null!;
        public decimal Salary { get; set; }

        public string DepartmentName { get; set; } = null!;
    }
}