using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBarbershop.Api.Models.Containers;
using TheBarbershop.Api.Services;

namespace TheBarbershop.Api.Controllers
{
    [Route("api/[controller]")]
    public class PostsController: ControllerBase
    {
        [Authorize]
        [HttpGet]
        public IEnumerable<PostDto> Get()
        {
            return new[]
            {
                new PostDto
                {
                    Id = 1,
                    Title = "Заголовок 1",
                    Text = "Если ты это видишь, то ты офигеть какой красавчик"
                },
                new PostDto
                {
                    Id = 2,
                    Title = "Заголовок 2",
                    Text = "Не ссы прорвемся :)"
                }
            };
        }

        [Authorize(Policy = Policies.Admin)]
        [HttpPost]
        public IActionResult Create()
        {
            throw new NotImplementedException();
        }

        [Authorize(Policy = Policies.Admin)]
        [HttpPut]
        public IActionResult Update()
        {
            throw new NotImplementedException();
        }
    }
}
