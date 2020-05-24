using System.ComponentModel.DataAnnotations;

namespace TheBarbershop.Api.Models
{
    public class LoginAdminDto
    {
        [Required]
        [MinLength(4)]
        public string Login { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
