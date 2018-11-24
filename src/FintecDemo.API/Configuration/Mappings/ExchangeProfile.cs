using AutoMapper;
using FintecDemo.API.Entities;
using FintecDemo.API.Models;

namespace FintecDemo.API.Configuration.Mappings
{
    public class ExchangeProfile : Profile
    {
        public ExchangeProfile()
        {
            CreateMap<Exchange, ExchangeDetailsModel>();
            CreateMap<Exchange, ExchangeListModel>();
            CreateMap<ExchangeCreateModel, Exchange>();
            CreateMap<ExchangeUpdateModel, Exchange>();
        }
    }
}
