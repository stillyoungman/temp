using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheBarbershop.Api.Models.Containers
{
    public class UpdateUserNameObject
    {
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string SecondName { get; set; }
    }
}
