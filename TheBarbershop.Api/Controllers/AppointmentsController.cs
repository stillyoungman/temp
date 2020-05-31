using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class AppointmentsController : AppBaseController
    {
        const string MasterInUnavailableForChosenTime = "1";
        public AppointmentsController(IDataContext c, IMapper m) : base(c, m) { }

        [Authorize(Policy = Policies.Client)]
        [HttpGet]
        public IActionResult Get()
        {
            var userId = HttpContext.GetUserId();
            return new JsonResult(dataContext.Set<Appointment>().Where(a => a.ClientId == userId).ProjectTo<AppointmentDto>(mapper.ConfigurationProvider));
        }

        [Authorize(Policy = Policies.Client)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NewAppointmentDto newAppointment)
        {
            //fetch master
            var master = dataContext.Set<Master>().Include(m => m.Appointments).SingleOrDefault(m => m.Id == newAppointment.MasterId);
            var service = dataContext.Set<Service>().SingleOrDefault(s => s.Id == newAppointment.ServiceId);
            if (master == null || service == null) return BadRequest();

            var desiredTime = newAppointment.StartTime.Value;
            var bookedTimes = master.Appointments.Select(a => a.StartTime);
            if (bookedTimes.Any(d => 
                d.Year == desiredTime.Year 
                && d.DayOfYear == desiredTime.DayOfYear 
                && d.Hour == desiredTime.Hour))
            {
                Response.Headers[Constants.InternalResponseCodeHeader] = MasterInUnavailableForChosenTime;
                return BadRequest("Master is busy at chosen time.");
            }

            var appointment = mapper.Map<Appointment>(newAppointment);
            appointment.ClientId = HttpContext.GetUserId();

            dataContext.Add(appointment);

            await dataContext.SaveChangesAsync();
            
            return Ok(mapper.Map<AppointmentDto>(appointment));
        }

        [Authorize(Policy = Policies.Client)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] NewAppointmentDto newAppointment)
        {
            var master = dataContext.Set<Master>().Include(m => m.Appointments).SingleOrDefault(m => m.Id == newAppointment.MasterId);
            var service = dataContext.Set<Service>().SingleOrDefault(s => s.Id == newAppointment.ServiceId);

            var userId = HttpContext.GetUserId();
            var originalObject = dataContext.Set<Appointment>().SingleOrDefault(a => a.Id == id && a.ClientId == userId);

            if (master == null || service == null || originalObject == null) return BadRequest();

            var desiredTime = newAppointment.StartTime.Value;
            var bookedTimes = master.Appointments.Where(a => a.Id != id).Select(a => a.StartTime);
            if (bookedTimes.Any(d =>
                d.Year == desiredTime.Year
                && d.DayOfYear == desiredTime.DayOfYear
                && d.Hour == desiredTime.Hour))
            {
                Response.Headers[Constants.InternalResponseCodeHeader] = MasterInUnavailableForChosenTime;
                return BadRequest();
            }

            mapper.Map(newAppointment, originalObject);
            dataContext.Update(originalObject);
            await dataContext.SaveChangesAsync();

            return Ok(mapper.Map<AppointmentDto>(originalObject));
        }
        
    }
}
