using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBarbershop.Api.Models.Containers;
using TheBarbershop.Api.Services;
using TheBarbershop.Api.Utils;
using TheBarbershop.Core.Infrastructure;
using TheBarbershop.Core.Models;
using TheBarbershop.Core.Utils;

namespace TheBarbershop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController: ControllerBase
    {
        private const string PhoneDuplication = "1";

        private readonly TokenService tokenService;
        private readonly IDataContext context;
        private readonly IMapper mapper;

        public UsersController(TokenService tokenService,
            IDataContext context,
            IMapper mapper)
        {
            this.tokenService = tokenService;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewUserDto newUser)
        {
            if(context.Set<Client>().Any(c => c.Phone == newUser.Phone))
            {
                Response.Headers[Constants.InternalResponseCodeHeader] = PhoneDuplication;
                return BadRequest();
            }

            var client = mapper.Map<Client>(newUser);
            client.PasswordHash = new PasswordHash(newUser.Password).ToBase64();
            client.Role = Core.Enums.Role.Client;

            using (context)
            {
                client = context.Add(client);
                await context.SaveChangesAsync();
            }
                
            return Ok(tokenService.CreateTokenObject(client));
        }
        
        [HttpPost("auth")]
        public IActionResult Auth(LoginUserDto userDto)
        {
            var user = context.Set<Client>().FirstOrDefault(u => u.Phone == userDto.Phone);
            if(user == null || !PasswordHash.FromBase64(user.PasswordHash).Verify(userDto.Password))
            {
                return BadRequest();
            }

            return Ok(tokenService.CreateTokenObject(user));
        }
    }
}
