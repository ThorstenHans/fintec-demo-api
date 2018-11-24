using System.Collections.Generic;
using System.Threading.Tasks;
using FintecDemo.API.Entities;

namespace FintecDemo.API.Repositories
{
    public interface IStockRepository
    {
        Task<IEnumerable<Stock>> GetAllAsync();
        Task<Stock> GetByIsinAsync(string isin);
        Task<CompanyProfile> GetCompanyProfileByIsinAsync(string isin);
        Task<bool> HasStockWithIsinAsync(string isin);
        Task<Stock> CreateStockAsync(Stock newStock);
        Task DeleteStockAsync(Stock stockToDelete);
        Task<Stock> UpdateStockAsync(Stock modifiedStock);
    }
}