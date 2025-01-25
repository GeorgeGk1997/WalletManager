using NovibetWalletManager.Application.Common.Interfaces;
using NovibetWalletManager.Infrastructure.Common.Persistence.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovibetWalletManager.Infrastructure.CurrenciesRates
{
    public class ECBClient : IECBClient
    {
        private readonly ECBHttpClient _ecbHttpClient;

        public ECBClient(ECBHttpClient ecbHttpClient)
        {
            _ecbHttpClient = ecbHttpClient;
        }

        public async Task<Domain.CurrenciesRates.CurrenciesRates> GetCurrenciesRatesECB()
        {
            return await _ecbHttpClient.GetCurrencyRatesAsync();
        }
    }
}
