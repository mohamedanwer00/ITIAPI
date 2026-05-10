using System.ComponentModel.DataAnnotations;

namespace ITIAPI.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }

        public string? Manager { get; set; }

        public List<Employee> Employee { get; set; }

    }
}
