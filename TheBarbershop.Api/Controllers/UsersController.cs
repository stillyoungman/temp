using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBarbershop.Api.Models;
using TheBarbershop.Api.Models.Containers;
using TheBarbershop.Api.Services;
using TheBarbershop.Api.Utils;
using TheBarbershop.Core.Enums;
using TheBarbershop.Core.Infrastructure;
using TheBarbershop.Core.Models;
using TheBarbershop.Core.Utils;

namespace TheBarbershop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController: AppBaseController
    {
        private const string PhoneDuplication = "1";
        private const string UnableToDeleteOldUser = "2";

        private readonly TokenService tokenService;

        public UsersController(TokenService tokenService,
            IDataContext context,
            IMapper mapper): base(context, mapper)
        {
            this.tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewUserDto newUser)
        {
            if(dataContext.Set<Client>().Any(c => c.Phone == newUser.Phone))
            {
                Response.Headers[Constants.InternalResponseCodeHeader] = PhoneDuplication;
                return BadRequest();
            }

            var client = mapper.Map<Client>(newUser);
            client.PasswordHash = new PasswordHash(newUser.Password).ToBase64();
            client.Role = Core.Enums.Role.Client;

            using (dataContext)
            {
                client = dataContext.Add(client);
                await dataContext.SaveChangesAsync();
            }
                
            return Ok(tokenService.CreateTokenObject(client));
        }
        
        [HttpPost("auth")]
        public IActionResult Auth(LoginUserDto userDto)
        {
            var user = dataContext.Set<Client>().FirstOrDefault(u => u.Phone == userDto.Phone);
            if(user == null || !PasswordHash.FromBase64(user.PasswordHash).Verify(userDto.Password))
            {
                return BadRequest();
            }

            return Ok(tokenService.CreateTokenObject(user));
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ValueObject input)
        {
            var user = dataContext.Set<Client>().FirstOrDefault(u => u.Phone == input.Value);
            if (user == null) return BadRequest();

            //make password from GUID
            var newPassword = Guid.NewGuid().ToString("N").Substring(22);
            user.PasswordHash = new PasswordHash(newPassword).ToBase64();
            dataContext.Update(user);
            await dataContext.SaveChangesAsync();

            return new JsonResult(new ValueObject { Value = newPassword });
        }


        [Authorize(Policy = Policies.Admin)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] long id, [FromBody] UpdateUserDto updateData)
        {
            if (updateData.RoleWasChanged)
            {
                switch (updateData.InitialRole)
                {
                    case Role.Client:
                        var client = dataContext.Set<Client>().Include(c => c.Appointments).SingleOrDefault(c => c.Id == id);
                        if (client == null) return NotFound();

                        dataContext.RemoveRange(client.Appointments);
                        var newMaster = mapper.Map<Master>(client);
                        newMaster.IsBlocked = updateData.IsBlocked;

                        try
                        {
                            dataContext.Remove(client);
                        }
                        catch
                        {
                            Response.Headers[Constants.InternalResponseCodeHeader] = UnableToDeleteOldUser;
                            return BadRequest();
                        }

                        dataContext.Add(newMaster);
                        break;
                    case Role.Master:
                        var master = dataContext.Set<Master>().Include(c => c.Appointments).SingleOrDefault(c => c.Id == id);
                        if (master == null) return NotFound();

                        dataContext.RemoveRange(master.Appointments);
                        var newClient = mapper.Map<Client>(master);
                        newClient.IsBlocked = updateData.IsBlocked;

                        try
                        {
                            dataContext.Remove(master);
                        }
                        catch
                        {
                            Response.Headers[Constants.InternalResponseCodeHeader] = UnableToDeleteOldUser;
                            return BadRequest();
                        }

                        dataContext.Add(newClient);
                        break;
                    default: return BadRequest();
                }
            } else
            {
                User u = null;
                switch (updateData.Role)
                {
                    case Role.Client:
                        u = dataContext.Set<Client>().SingleOrDefault(c => c.Id == id);
                        break;
                    case Role.Master:
                        u = dataContext.Set<Master>().SingleOrDefault(c => c.Id == id);
                        break;
                }
                if (u == null) return BadRequest();

                u.IsBlocked = updateData.IsBlocked;
                dataContext.Update(u);
            }

            await dataContext.SaveChangesAsync();

            return Ok();
        }

        [Authorize(Policy = Policies.Admin)]
        [HttpGet]
        public IActionResult GetUsers()
        {
            return new JsonResult(dataContext.Set<Client>().ProjectTo<UserDto>(mapper.ConfigurationProvider).ToArray()
                .Concat(dataContext.Set<Master>().ProjectTo<UserDto>(mapper.ConfigurationProvider).ToArray()));
        }
    }
}
