using Microsoft.Extensions.Configuration;
using NovibetWalletManager.Application.Common.Interfaces;
using NovibetWalletManager.Application.Services.ECB.Commands;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovibetWalletManager.Infrastructure.Common.Persistence.Jobs
{
    public class UpdateRatesJob : IJob
    {

        private readonly UpdateCurrencyRatesCommand _updateCurrencyRatesCommand;
        private readonly ICurrencyRateRepository _currencyRateRepository;
        private readonly ICacheService _cacheService;
        private readonly IConfiguration _configuration;

        public UpdateRatesJob(UpdateCurrencyRatesCommand updateCurrencyRatesCommand,
            ICurrencyRateRepository currencyRateRepository,
            ICacheService cacheService,
            IConfiguration configuration)
        {
            _updateCurrencyRatesCommand = updateCurrencyRatesCommand;
            _currencyRateRepository = currencyRateRepository;
            _cacheService = cacheService;
            _configuration = configuration;
        }


        public async Task Execute(IJobExecutionContext context)
        {
            //update db
            await _updateCurrencyRatesCommand.UpdateRatesAsync();

            //refresh cache
            var updatedRates = await _currencyRateRepository.GetCurrencyRatesRedisDbAsync();
            var cacheKey = _configuration["Redis:CacheKey"];
            await _cacheService.SetAsync(cacheKey, updatedRates, TimeSpan.FromMinutes(5));
        }
    }
}
