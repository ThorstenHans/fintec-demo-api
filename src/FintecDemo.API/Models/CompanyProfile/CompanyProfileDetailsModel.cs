using System;

namespace FintecDemo.API.Models.CompanyProfile
{
    public class CompanyProfileDetailsModel: CompanyProfileListModel
    {
        public string About { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public DateTime LastModified { get; set; }
    }
}
