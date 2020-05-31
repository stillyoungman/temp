using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBarbershop.Api.Models;
using TheBarbershop.Api.Models.Containers;
using TheBarbershop.Api.Services;
using TheBarbershop.Api.Utils;
using TheBarbershop.Core.Infrastructure;
using TheBarbershop.Core.Models;

namespace TheBarbershop.Api.Controllers
{
    [Authorize(Policy = Policies.Client)]
    [Route("api/[controller]")]
    public class ProfileController : AppBaseController
    {
        public ProfileController(IDataContext c, IMapper m) : base(c, m) { }

        [HttpPut("password")]
        public IActionResult UpdatePassword([FromBody] NewPasswordObject input)
        {
            //fetch user
            var user = this.GetClient(dataContext);

            //ensure password
            if(user == null || !PasswordHash.FromBase64(user.PasswordHash).Verify(input.OldPassword))
            {
                return BadRequest();
            }

            //create new
            user.PasswordHash = new PasswordHash(input.NewPassword).ToBase64();
            dataContext.Update(user);

            return Ok();
        }

        [HttpPut("name")]
        public async Task<IActionResult> UpdateName([FromBody] UpdateUserNameObject input)
        {
            var user = this.GetClient(dataContext);
            if (user == null) return BadRequest();

            mapper.Map(input, user);

            dataContext.Update(user);
            await dataContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("phone")]
        public async Task<IActionResult> UpdatePhone([FromBody] NewPhoneValueObject input)
        {
            var user = this.GetClient(dataContext);
            if (user == null) return BadRequest();

            user.Phone = input.Value;

            dataContext.Update(user);
            await dataContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public IActionResult GetInfo()
        {
            var user = this.GetClient(dataContext);
            if (user == null) return BadRequest();

            return new JsonResult(mapper.Map<ProfileInfoDto>(user));
        }
    }
}
