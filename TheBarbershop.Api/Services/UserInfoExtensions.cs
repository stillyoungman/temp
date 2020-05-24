using System;
using TheBarbershop.Core.Enums;
using TheBarbershop.Core.Models;

namespace TheBarbershop.Api.Services
{
    public static class UserInfoExtensions
    {
        private class UserInfoWrapper: IUserInfo
        {
            public string Subject { get; set; }
            public string Role { get; set; }
        }
        public static IUserInfo AsUserInfo(this User user)
        {
            return new UserInfoWrapper
            {
                Subject = user.Id.ToString(),
                Role = user.Role.ToPolicy()
            };
        }

        public static string ToPolicy(this Role role)
        {
            return role switch
            {
                Role.Admin => Policies.Admin,
                Role.Client => Policies.Client,
                Role.Master => Policies.Master,
                _ => throw new NotSupportedException("The role is not supported."),
            };
        }
    }
}
