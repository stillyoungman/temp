using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

            CreateMap<UpdateUserNameObject, Client>();
            CreateMap<Client, ProfileInfoDto>();
            CreateMap<NewServiceDto, Service>();

            CreateMap<Client, Master>()
                .Ignore(m => m.Role);
                
            CreateMap<Master, Client>()
                .Ignore(m => m.Role);

            CreateMap<NewAppointmentDto, Appointment>();
            CreateMap<Appointment, AppointmentDto>();

            CreateMap<Client, UserDto>();
            CreateMap<Master, UserDto>();
        }
    }

    internal static class MapperExt
    {
        public static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination, TMember>(this IMappingExpression<TSource, TDestination> mappingExpression, Expression<Func<TDestination, TMember>> destinationMember)
        {
            return mappingExpression.ForMember(destinationMember, opt => opt.Ignore());
        }
    }
}
