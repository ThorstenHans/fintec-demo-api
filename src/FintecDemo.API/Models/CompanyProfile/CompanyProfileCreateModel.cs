using System;
using FintecDemo.API.Models.Person;

namespace FintecDemo.API.Models.CompanyProfile
{
    public class CompanyProfileCreateModel
    {
        public string Name { get; set; }
        public string About { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public PersonCreateModel Ceo { get; set; }
        
    }
}
