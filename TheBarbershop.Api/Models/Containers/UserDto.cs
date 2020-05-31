using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBarbershop.Core.Enums;

namespace TheBarbershop.Api.Models.Containers
{
    public class UserDto
    {
        public long Id { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }
        public Role Role { get; set; }
        public bool IsBlocked { get; set; }
    }
}
