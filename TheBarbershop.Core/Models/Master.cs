using System;
using System.Collections.Generic;
using System.Text;

namespace TheBarbershop.Core.Models
{
    public class Master: User
    {
        public string Phone { get; set; }
        public string FullName { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
