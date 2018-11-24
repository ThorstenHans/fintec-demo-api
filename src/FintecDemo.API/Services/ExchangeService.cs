using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FintecDemo.API.Entities;
using FintecDemo.API.Models;
using FintecDemo.API.Models.Stock;
using FintecDemo.API.Repositories;
using Microsoft.AspNetCore.JsonPatch;

namespace FintecDemo.API.Services
{
    public class ExchangeService : IExchangeService
    {
        private readonly IExchangeRepository _repository;
        private readonly IMapper _mapper;

        public ExchangeService(IExchangeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ExchangeDetailsModel> CreateAsync(ExchangeCreateModel exchange)
        {
            if (await _repository.HasExchangeWithShortcut(exchange.Shortcut))
            {
                throw new ArgumentException();
            }

            var exchangeEntity = _mapper.Map<Exchange>(exchange);
            var createdEntity = await _repository.CreateAsync(exchangeEntity);
            return _mapper.Map<ExchangeDetailsModel>(createdEntity);
        }

        public async Task<ExchangeDetailsModel> UpdateAsync(string shortcut, ExchangeUpdateModel exchange)
        {
            var existing = await _repository.GetByShortcutAsync(shortcut);
            if (existing == null)
            {
                return null;
            }

            _mapper.Map(exchange, existing);
            var updated = await _repository.UpdateAsync(existing);
            return _mapper.Map<ExchangeDetailsModel>(updated);
        }

        public async Task<bool> DeleteExchangeAsync(string shortcut)
        {
            var existing = await _repository.GetByShortcutAsync(shortcut);
            if (existing == null)
            {
                return false;
            }

            await _repository.DeleteAsync(existing);
            return true;
        }

        public async Task<ExchangeDetailsModel> PatchAsync(string shortcut, JsonPatchDocument<ExchangeUpdateModel> exchangePatch)
        {
            var existing = await _repository.GetByShortcutAsync(shortcut);
            if (existing == null)
            {
                return null;
            }

            var updateModel = _mapper.Map<ExchangeUpdateModel>(existing);
            exchangePatch.ApplyTo(updateModel);
            _mapper.Map(updateModel, existing);
            var updated = await _repository.UpdateAsync(existing);
            return _mapper.Map<ExchangeDetailsModel>(updated);
        }

        public async Task<IEnumerable<StockListModel>> GetAllStocksTradedAtExchangeAsync(string shortcut)
        {
            var existing = await _repository.GetByShortcutAsync(shortcut);
            if (existing == null)
            {
                return null;
            }

            var allStocks = await _repository.GetAllStocksByExchangeAsync(existing);
            return _mapper.Map<IEnumerable<StockListModel>>(allStocks);
        }

        public async Task<IEnumerable<ExchangeListModel>> GetAllAsync()
        {
            var exchanges = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ExchangeListModel>>(exchanges);
        }

        public async Task<ExchangeDetailsModel> GetByShortcutAsync(string shortcut)
        {
            if (string.IsNullOrWhiteSpace(shortcut))
            {
                return null;
            }

            var found = await _repository.GetByShortcutAsync(shortcut);
            return _mapper.Map<ExchangeDetailsModel>(found);
        }
    }
}
