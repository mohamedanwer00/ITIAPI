using System.ComponentModel.DataAnnotations;

namespace ITIAPI.DTO
{
    public class LoginUserDto
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string Password { get; set; }=null!;
    }
}
