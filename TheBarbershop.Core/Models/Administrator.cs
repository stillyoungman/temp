using System;
using System.Collections.Generic;
using System.Text;

namespace TheBarbershop.Core.Models
{
    public class Administrator: User
    {
        public string Login { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
