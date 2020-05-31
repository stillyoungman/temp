using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBarbershop.Core.Infrastructure;

namespace TheBarbershop.Api.Controllers
{
    [ApiController]
    public class AppBaseController: ControllerBase
    {
        protected readonly IDataContext dataContext;
        protected readonly IMapper mapper;
        public AppBaseController(IDataContext context, IMapper mapper) => (this.dataContext, this.mapper) = (context, mapper);
    }
}
