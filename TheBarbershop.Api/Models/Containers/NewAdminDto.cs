using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheBarbershop.Api.Models
{

    public class NewAdminDto: LoginAdminDto
    {
        [Required]
        public string Code { get; set; }
    }
}
