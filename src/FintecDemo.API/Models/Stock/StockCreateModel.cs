using System.Collections.Generic;
using FintecDemo.API.Models.CompanyProfile;

namespace FintecDemo.API.Models.Stock
{
    public class StockCreateModel
    {
        public string Isin { get; set; }
        public List<string> TradedAt { get; set; }
        public CompanyProfileCreateModel CompanyProfile { get; set; }
    }
}
