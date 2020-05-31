using static TheBarbershop.Core.Models.Service;

namespace TheBarbershop.Api.Models.Containers
{
    public class ServiceDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public ServiceSex Sex { get; set; }
    }
}
