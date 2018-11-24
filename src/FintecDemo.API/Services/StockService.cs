using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FintecDemo.API.Entities;
using FintecDemo.API.Models.CompanyProfile;
using FintecDemo.API.Models.Stock;
using FintecDemo.API.Repositories;

namespace FintecDemo.API.Services
{
    public class StockService: IStockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;

        public StockService(IStockRepository stockRepository, IMapper mapper)
        {
            _stockRepository = stockRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<StockListModel>> GetAllAsync()
        {
            var allStocks = await _stockRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<StockListModel>>(allStocks);
        }

        public async Task<StockDetailsModel> GetByIsinAsync(string isin)
        {
            var found = await _stockRepository.GetByIsinAsync(isin);
            if (found == null)
            {
                return null;
            }
            return _mapper.Map<StockDetailsModel>(found);
        }

        public async Task<CompanyProfileDetailsModel> GetCompanyProfileByIsinAsync(string isin)
        {
            if (string.IsNullOrWhiteSpace(isin))
            {
                return null;
            }
            var companyProfile = await _stockRepository.GetCompanyProfileByIsinAsync(isin);
            if (companyProfile == null)
            {
                return null;
            }

            return _mapper.Map<CompanyProfileDetailsModel>(companyProfile);
        }

        public async Task<StockDetailsModel> CreateStockAsync(StockCreateModel createModel)
        {
            if (createModel == null)
            {
                throw new ArgumentNullException(nameof(createModel));
            }

            if (await _stockRepository.HasStockWithIsinAsync(createModel.Isin))
            {
                throw new ArgumentException();
            }

            var stockEntity = _mapper.Map<Stock>(createModel);
            var createdEntity = await _stockRepository.CreateStockAsync(stockEntity);
            return _mapper.Map<StockDetailsModel>(createdEntity);
            
        }

        public async Task<bool> DeleteStockByIsinAsync(string isin)
        {
            var existing = await _stockRepository.GetByIsinAsync(isin);
            if (existing == null)
            {
                return false;
            }

            await _stockRepository.DeleteStockAsync(existing);
            return true;
        }
    }
}
