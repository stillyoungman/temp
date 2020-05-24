using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBarbershop.Api.Models;
using TheBarbershop.Api.Models.Containers;
using TheBarbershop.Core.Models;

namespace TheBarbershop.Api
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<NewAdminDto, Administrator>();
            CreateMap<NewUserDto, Client>()
                .ForMember(e => e.FirstName, m => m.MapFrom(e => e.Name));
        }
    }
}
