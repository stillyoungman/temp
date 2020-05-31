using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TheBarbershop.Api.Models;
using TheBarbershop.Api.Utils;
using TheBarbershop.Core.Infrastructure;
using TheBarbershop.Core.Models;

namespace TheBarbershop.Api.Services
{
    public static class ConfigureAuthorizationExtensions
    {
        public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetRequiredService<ILogger<Startup>>();
            string secret = serviceProvider.GetRequiredService<IOptions<ApplicationConfiguration>>().Value.TokenSecret;
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                    //ClockSkew = TimeSpan.Zero
                };
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = (context) =>
                    {
                        var userId = long.Parse(context.Principal.FindFirst(ClaimTypes.NameIdentifier).Value);
                        var userRole = context.Principal.FindFirst(ClaimTypes.Role).Value;
                        var dataContext = context.HttpContext.RequestServices.GetRequiredService<IDataContext>();
                        if (!DoesUserExist(userId, userRole, dataContext)) {
                            context.Fail("User doesn't exist.");
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddAuthorization(config =>
            {
                config.AddPolicy(Policies.Admin, Policies.AdminPolicy);
                config.AddPolicy(Policies.Client, Policies.ClientPolicy);
                config.AddPolicy(Policies.Master, Policies.MasterPolicy);
            });

            return services;
        }

        private static bool DoesUserExist(long id, string role, IDataContext context)
        {
            switch (role)
            {
                case Policies.Admin: return context.Set<Administrator>().Any(a => a.Id == id);
                case Policies.Client: return context.Set<Client>().Any(c => c.Id == id);
                case Policies.Master: return context.Set<Master>().Any(c => c.Id == id);
                default: throw new NotSupportedException($"Role {role} isn't supported.");
            }
        }
    }
}
