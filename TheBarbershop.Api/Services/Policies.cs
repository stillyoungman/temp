using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBarbershop.Core.Enums;

namespace TheBarbershop.Api.Services
{
    public class Policies
    {
        public const string Admin = "admin";
        public const string Client = "client";
        public const string Master = "master";

        public static AuthorizationPolicy AdminPolicy => new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser().RequireRole(Admin).Build();
        public static AuthorizationPolicy ClientPolicy => new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser().RequireRole(Client).Build();
        public static AuthorizationPolicy MasterPolicy => new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser().RequireRole(Master).Build();

    }
}
