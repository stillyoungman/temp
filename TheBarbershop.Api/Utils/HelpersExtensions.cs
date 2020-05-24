using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBarbershop.Api.Services;
using TheBarbershop.Core.Models;

namespace TheBarbershop.Api.Utils
{
    public static class HelpersExtensions
    {
        public static object CreateTokenObject(this TokenService service, User user)
        {
            return new { token = service.IssueToken(user.AsUserInfo()) };
        }
    }
}
