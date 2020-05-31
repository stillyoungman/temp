using System;
using System.Collections.Generic;
using System.Text;

namespace TheBarbershop.Core.Models
{
    public class Administrator: User
    {
        public Administrator()
        {
            Role = Enums.Role.Admin;
        }
        public string Login { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
