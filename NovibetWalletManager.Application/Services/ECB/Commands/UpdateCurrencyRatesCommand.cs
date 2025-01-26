using NovibetWalletManager.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovibetWalletManager.Application.Services.ECB.Commands
{
    public class UpdateCurrencyRatesCommand
    {
        private readonly ICurrencyRateRepository _currencyRateRepository;
        private readonly IECBClient _eCBClient;

        public UpdateCurrencyRatesCommand(ICurrencyRateRepository currencyRateRepository,
            IECBClient eCBClient)
        {
            _currencyRateRepository = currencyRateRepository;
            _eCBClient = eCBClient;
        }

        public async Task UpdateRatesAsync()
        {
            var currencyAndRates = await _eCBClient.GetCurrenciesRatesECB();

            await _currencyRateRepository.UpdateCurrencyRatesOnDbAsync(currencyAndRates);
        }


    }
}
