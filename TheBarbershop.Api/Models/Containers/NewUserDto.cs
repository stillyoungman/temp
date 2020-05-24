using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheBarbershop.Api.Models.Containers
{
    public class LoginUserDto
    {
        [Required]
        [RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}")]
        public string Phone { get; set; }

        [Required]
        public string Password { get; set; }
    }
    public class NewUserDto: LoginUserDto
    {
        [Required]
        public string Name { get; set; }        
    }
}
