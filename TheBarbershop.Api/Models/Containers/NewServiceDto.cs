using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static TheBarbershop.Core.Models.Service;

namespace TheBarbershop.Api.Models.Containers
{
    public class NewServiceDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        [Range((int)ServiceSex.Female, (int)ServiceSex.Male)]
        public ServiceSex Sex { get; set; }
    }
}
