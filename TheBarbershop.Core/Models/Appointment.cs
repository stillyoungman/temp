using System;
using System.Collections.Generic;
using System.Text;

namespace TheBarbershop.Core.Models
{
    public class Appointment : Entity
    {
        public long ServiceId { get; set; }
        public Service Service { get; set; }

        public long ClientId { get; set; }
        public Client Client { get; set; }

        public long? MasterId { get; set; }
        public Master Master { get; set; }

        public DateTime StartTime { get; set; }
        public string Comment { get; set; }
        public State CurrentState { get; set; }

        public enum State
        {
            Pending = 1,
            Approved,
            Done,
            Canceled
        }
    }
}
