using System;
using System.Collections.Generic;
using System.Text;

namespace TheBarbershop.Core.Models
{
    public class Service: Entity
    {
        public string Name { get; set; }
        public double Cost { get; set; }
        public ServiceSex Sex { get; set; }

        public enum ServiceSex
        {
            Undefined = 0,
            Female = 1,
            Male = 2,
            Any = 3
        }
    }
}
