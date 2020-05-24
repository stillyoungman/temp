using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBarbershop.Api.Models.Containers
{
    public class PostDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
