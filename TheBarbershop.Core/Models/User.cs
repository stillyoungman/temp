using System;
using System.Collections.Generic;
using System.Text;
using TheBarbershop.Core.Enums;

namespace TheBarbershop.Core.Models
{
    public abstract class User : Entity
    {
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public Role Role { get; set; }
    }

}
