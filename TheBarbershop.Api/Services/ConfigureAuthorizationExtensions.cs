using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBarbershop.Api.Models;
using TheBarbershop.Core.Models;

namespace TheBarbershop.Api.Services
{
    public static class ConfigureAuthorizationExtensions
    {
        public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
        {
            string secret = services.BuildServiceProvider().GetRequiredService<IOptions<ApplicationConfiguration>>().Value.TokenSecret; 
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
                    OnAuthenticationFailed = (context) =>
                    {

                        return Task.CompletedTask;
                    }, 
                    OnTokenValidated = (context) =>
                    {
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
    }
}
