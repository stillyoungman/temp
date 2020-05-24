using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;
using TheBarbershop.Api.Models;
using TheBarbershop.Api.Services;
using TheBarbershop.Api.Utils;
using TheBarbershop.Core.Infrastructure;
using TheBarbershop.Core.Models;
using TheBarbershop.Core.Utils;

namespace TheBarbershop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministratorsController : ControllerBase
    {
        private readonly ApplicationConfiguration config;
        private readonly IDataContext context;
        private readonly IMapper mapper;
        private readonly TokenService tokenService;

        private const string InvalidCodeCode = "1";
        private const string LoginDuplicationCode = "2";

        public AdministratorsController(
            IOptions<ApplicationConfiguration> options,
            IDataContext context,
            IMapper mapper,
            TokenService tokenService)
        {
            this.config = options.Value;
            this.context = context;
            this.mapper = mapper;
            this.tokenService = tokenService;
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] NewAdminDto input)
        {
            if (input.Code != config.AdministratorRegistrationCode)
            {
                Response.Headers[Constants.InternalResponseCodeHeader] = InvalidCodeCode;
                return BadRequest();
            }

            if (context.Set<Administrator>().Any(a => a.Login == input.Login))
            {
                Response.Headers[Constants.InternalResponseCodeHeader] = LoginDuplicationCode;
                return BadRequest();
            }

            Administrator a = null;

            using (context)
            {
                a = mapper.Map<Administrator>(input);
                a.Role = Core.Enums.Role.Admin;
                a.PasswordHash = new PasswordHash(input.Password).ToBase64();
                a = context.Add(a);
                await context.SaveChangesAsync();
            }

            return Ok(tokenService.CreateTokenObject(a));
        }

        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody] LoginAdminDto input)
        {
            var admin = context.Set<Administrator>().FirstOrDefault(a => a.Login == input.Login);
            if(admin == null || !PasswordHash.FromBase64(admin.PasswordHash).Verify(input.Password))
            {
                return BadRequest();
            }

            return Ok(tokenService.CreateTokenObject(admin));
        }
    }
}
