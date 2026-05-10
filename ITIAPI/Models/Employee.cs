using System.ComponentModel.DataAnnotations.Schema;

namespace ITIAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Salary { get; set; } = 0;
        public string Address { get; set; } = null!;

        public string Phone { get; set; } = null!;

        [ForeignKey("Department")]
        public int DeptID { get; set; }

        public Department Department { get; set; } = null!;


    }
}
