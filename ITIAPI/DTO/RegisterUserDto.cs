using System.ComponentModel.DataAnnotations;

namespace ITIAPI.DTO
{
    public class RegisterUserDto
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = null!;
        public string Email { get; set; }=null!;
    }
}
