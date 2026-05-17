
namespace ITIAPI.DTO
{
    public class DepartmentDetailsWithEmployeeName
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string ManagerName { get; set; } = null!;

        public List<string> EmployeesName { get; set; } = new();
    }
}