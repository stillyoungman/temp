using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBarbershop.Api.Models.Containers;
using TheBarbershop.Api.Services;
using TheBarbershop.Core.Infrastructure;
using TheBarbershop.Core.Models;

namespace TheBarbershop.Api.Controllers
{
    [Route("api/[controller]")]
    public class ServicesController : AppBaseController
    {
        public ServicesController(IDataContext c, IMapper m) : base(c, m) { }

        [Authorize]
        public IActionResult Get()
        {
            return new JsonResult(dataContext.Set<Service>());
        }

        [Authorize(Policy = Policies.Admin)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NewServiceDto service)
        {
            var result = dataContext.Add(mapper.Map<Service>(service));
            await dataContext.SaveChangesAsync();

            return Ok(result);
        }

        [Authorize(Policy = Policies.Admin)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] NewServiceDto updated)
        {
            var service = dataContext.Set<Service>().SingleOrDefault(s => s.Id == id);
            if (service == null) return BadRequest();

            mapper.Map(updated, service);

            dataContext.Update(service);
            await dataContext.SaveChangesAsync();

            return Ok(service);
        }

        [Authorize(Policy = Policies.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var service = dataContext.Set<Service>().SingleOrDefault(s => s.Id == id);
            if (service == null) return BadRequest();

            dataContext.Remove(service);
            await dataContext.SaveChangesAsync();

            return Ok();
        }
    }
}
