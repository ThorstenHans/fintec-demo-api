using System.Collections.Generic;
using System.Threading.Tasks;
using FintecDemo.API.Models;
using FintecDemo.API.Models.Stock;
using Microsoft.AspNetCore.JsonPatch;

namespace FintecDemo.API.Services
{
    public interface IExchangeService
    {
        Task<IEnumerable<ExchangeListModel>> GetAllAsync();
        Task<ExchangeDetailsModel> GetByShortcutAsync(string shortcut);
        Task<ExchangeDetailsModel> CreateAsync(ExchangeCreateModel exchange);
        Task<ExchangeDetailsModel> UpdateAsync(string shortcut, ExchangeUpdateModel exchange);
        Task<bool> DeleteExchangeAsync(string shortcut);
        Task<ExchangeDetailsModel> PatchAsync(string shortcut, JsonPatchDocument<ExchangeUpdateModel> exchangePatch);
        Task<IEnumerable<StockListModel>> GetAllStocksTradedAtExchangeAsync(string shortcut);
    }
}
