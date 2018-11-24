using AutoMapper;
using FintecDemo.API.Entities;
using FintecDemo.API.Models.Stock;

namespace FintecDemo.API.Configuration.Mappings
{
    public class StockProfile : Profile
    {
        public StockProfile()
        {
            CreateMap<Stock, StockDetailsModel>();
            CreateMap<Stock, StockListModel>();
        }
    }
}