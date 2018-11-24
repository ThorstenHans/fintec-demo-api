using System.Collections.Generic;
using System.Threading.Tasks;
using FintecDemo.API.Models.CompanyProfile;
using FintecDemo.API.Models.Stock;

namespace FintecDemo.API.Services
{
    public interface IStockService
    {
        Task<IEnumerable<StockListModel>> GetAllAsync();
        Task<StockDetailsModel> GetByIsinAsync(string isin);
        Task<CompanyProfileDetailsModel> GetCompanyProfileByIsinAsync(string isin);
        Task<StockDetailsModel> CreateStockAsync(StockCreateModel createModel);
        Task<bool> DeleteStockByIsinAsync(string isin);
    }
}