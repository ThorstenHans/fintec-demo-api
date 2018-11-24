using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FintecDemo.API.Database;
using FintecDemo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FintecDemo.API.Repositories
{
    public class ExchangeRepository : IExchangeRepository
    {
        private readonly FintecDbContext _context;

        public ExchangeRepository(FintecDbContext context)
        {
            _context = context;
        }

        public async Task<bool> HasExchangeWithShortcut(string shortcut)
        {
            return await _context.Exchanges.AnyAsync(exchange =>
                exchange.Shortcut.Equals(shortcut, StringComparison.InvariantCultureIgnoreCase));
        }

        public async Task<Exchange> UpdateAsync(Exchange modifiedExchange)
        {
            _context.Exchanges.Update(modifiedExchange);
            await _context.SaveChangesAsync();
            return modifiedExchange;
        }

        public async Task DeleteAsync(Exchange exchangeToDelete)
        {
            _context.Exchanges.Remove(exchangeToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Stock>> GetAllStocksByExchangeAsync(Exchange exchange)
        {
            if (exchange == null)
            {
                return null;
            }
            //TODO: implement this async
            return Enumerable.Empty<Stock>();
        }

        public async Task<IEnumerable<Exchange>> GetAllAsync()
        {
            return await _context.Exchanges.ToListAsync();
        }

        public async Task<Exchange> GetByShortcutAsync(string shortcut)
        {
            return await _context.Exchanges.FirstOrDefaultAsync(exchange =>
                exchange.Shortcut.Equals(shortcut, StringComparison.InvariantCultureIgnoreCase));
        }

        public async Task<Exchange> CreateAsync(Exchange newExchange)
        {
            _context.Exchanges.Add(newExchange);
            await _context.SaveChangesAsync();
            return newExchange;
        }
    }
}
