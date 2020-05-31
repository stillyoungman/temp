using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TheBarbershop.Core.Enums;

namespace TheBarbershop.Api.Models.Containers
{
    public class UpdateUserDto
    {
        public bool IsBlocked { get; set; }

        [Required]
        public Role Role { get; set; }

        [Required]
        public Role InitialRole { get; set; }

        public bool RoleWasChanged => Role != InitialRole;
    }
}
