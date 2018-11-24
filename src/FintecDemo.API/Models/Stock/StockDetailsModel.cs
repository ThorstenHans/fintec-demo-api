using System;
using System.Collections.Generic;
using FintecDemo.API.Models.CompanyProfile;

namespace FintecDemo.API.Models.Stock
{
    public class StockDetailsModel: StockListModel
    {
        public DateTime LastModified { get; set; }
        public List<string> TradedAt { get; set; }
        public CompanyProfileDetailsModel CompanyProfile { get; }
    }
}