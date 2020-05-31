using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheBarbershop.Api.Models.Containers
{
    public class NewPhoneValueObject
    {
        [Required]
        [RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}")]
        public string Value { get; set; }
    }
}
