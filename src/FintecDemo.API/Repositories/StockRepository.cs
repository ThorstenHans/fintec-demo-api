using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FintecDemo.API.Database;
using FintecDemo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FintecDemo.API.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly FintecDbContext _context;

        public StockRepository(FintecDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Stock>> GetAllAsync()
        {
            return await _context
                .Stocks
                .ToListAsync();
        }

        public async Task<Stock> GetByIsinAsync(string isin)
        {
            return await _context
                .Stocks
                .FirstOrDefaultAsync(stock => stock.Isin.Equals(isin, StringComparison.InvariantCultureIgnoreCase));
        }

        public async Task<CompanyProfile> GetCompanyProfileByIsinAsync(string isin)
        {
            var foundStock = await _context
                .Stocks
                .Include(stock => stock.CompanyProfile)
                .FirstOrDefaultAsync(stock => stock.Isin.Equals(isin, StringComparison.InvariantCultureIgnoreCase));

            return foundStock?.CompanyProfile;
        }

        public async Task<bool> HasStockWithIsinAsync(string isin)
        {
            return await _context.Stocks.AnyAsync(stock => stock.Isin.Equals(isin, StringComparison.InvariantCultureIgnoreCase));
        }

        public async Task<Stock> CreateStockAsync(Stock newStock)
        {
            _context.Stocks.Add(newStock);
            await _context.SaveChangesAsync();
            return newStock;
        }

        public async Task DeleteStockAsync(Stock stockToDelete)
        {
            _context.Stocks.Remove(stockToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<Stock> UpdateStockAsync(Stock modifiedStock)
        {
            _context.Stocks.Update(modifiedStock);
            await _context.SaveChangesAsync();
            return modifiedStock;
        }
    }
}
