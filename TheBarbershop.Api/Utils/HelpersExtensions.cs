using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using System.Threading.Tasks;
using TheBarbershop.Api.Services;
using TheBarbershop.Core.Infrastructure;
using TheBarbershop.Core.Models;

namespace TheBarbershop.Api.Utils
{
    public static class HelpersExtensions
    {
        public static object CreateTokenObject(this TokenService service, User user)
        {
            return new { token = service.IssueToken(user.AsUserInfo()) };
        }

        public static long GetUserId(this HttpContext context) => long.Parse(context.User.FindFirst(ClaimTypes.NameIdentifier).Value);

        public static Client GetClient(this ControllerBase controllerBase, IDataContext context)
        {
            var userId = controllerBase.HttpContext.GetUserId();
            var user = context.Set<Client>().SingleOrDefault(c => c.Id == userId);
            return user;
        }
    }
}
