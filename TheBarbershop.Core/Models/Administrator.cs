using System;
using System.Collections.Generic;
using System.Text;

namespace TheBarbershop.Core.Models
{
    public class Administrator: User
    {
        public ICollection<Post> Posts { get; set; }
    }
}
