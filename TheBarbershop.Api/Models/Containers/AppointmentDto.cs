using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static TheBarbershop.Core.Models.Appointment;

namespace TheBarbershop.Api.Models.Containers
{
    public class AppointmentDto
    {
        public long Id { get; set; }
        public long ServiceId { get; set; }
        public long ClientId { get; set; }
        public long? MasterId { get; set; }
        public DateTime StartTime { get; set; }
        public string Comment { get; set; }
        public State CurrentState { get; set; }
    }
}
