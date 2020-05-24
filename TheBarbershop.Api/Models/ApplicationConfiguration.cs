using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBarbershop.Api.Models
{
    public class ApplicationConfiguration
    {
        public string AdministratorRegistrationCode { get; set; }
        public string DatabaseConnectionString { get; set; }
        public string TokenSecret { get; set; }
    }
}
