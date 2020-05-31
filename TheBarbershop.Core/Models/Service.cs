using System;
using System.Collections.Generic;
using System.Text;

namespace TheBarbershop.Core.Models
{
    public partial class Service: Entity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public ServiceSex Sex { get; set; }
    }
}
