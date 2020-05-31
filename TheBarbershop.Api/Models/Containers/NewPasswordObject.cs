using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheBarbershop.Api.Models.Containers
{
    public class NewPasswordObject
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        [MinLength(6)]
        public string NewPassword { get; set; }
    }
}
