using AutoMapper;
using FintecDemo.API.Entities;
using FintecDemo.API.Models.CompanyProfile;

namespace FintecDemo.API.Configuration.Mappings
{
    public class CompanyProfileProfile : Profile
    {
        public CompanyProfileProfile()
        {
            CreateMap<CompanyProfile, CompanyProfileDetailsModel>();
            CreateMap<CompanyProfile, CompanyProfileListModel>();
        }
    }
}