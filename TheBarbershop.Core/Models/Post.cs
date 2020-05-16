using System;
using System.Collections.Generic;
using System.Text;

namespace TheBarbershop.Core.Models
{
    public class Post: Entity
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
