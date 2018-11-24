using System.Collections.Generic;
using System.Threading.Tasks;
using FintecDemo.API.Entities;

namespace FintecDemo.API.Repositories
{
    public interface IExchangeRepository
    {
        Task<IEnumerable<Exchange>> GetAllAsync();
        Task<Exchange> GetByShortcutAsync(string shortcut);
        Task<Exchange> CreateAsync(Exchange newExchange);
        Task<bool> HasExchangeWithShortcut(string shortcut);
        Task<Exchange> UpdateAsync(Exchange modifiedExchange);
        Task DeleteAsync(Exchange exchangeToDelete);
        Task<IEnumerable<Stock>> GetAllStocksByExchangeAsync(Exchange exchange);
    }
}
