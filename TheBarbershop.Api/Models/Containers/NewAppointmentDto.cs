using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheBarbershop.Api.Models.Containers
{
    public class NewAppointmentDto
    {
        [Required]
        public long ServiceId { get; set; }

        [Required]
        public long MasterId { get; set; }

        [Required]
        public DateTime? StartTime { get; set; }

        [MaxLength(255)]
        public string Comment { get; set; }
    }
}
