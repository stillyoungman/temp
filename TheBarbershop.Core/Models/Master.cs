using System;
using System.Collections.Generic;
using System.Text;

namespace TheBarbershop.Core.Models
{
    public class Master: User
    {
        public Master()
        {
            Role = Enums.Role.Master;
        }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string SecondName { get; set; }
        public ICollection<Appointment> Appointments { get; set; }

        public string FullName
        {
            get
            {
                if(SecondName != null)
                {
                    return MiddleName != null ? $"{FirstName} {MiddleName} {SecondName}" : $"{FirstName} {SecondName}";
                } else
                {
                    return FirstName;
                }
            }
        } 
    }
}
