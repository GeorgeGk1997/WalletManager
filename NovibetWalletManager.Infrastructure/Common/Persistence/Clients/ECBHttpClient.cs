using NovibetWalletManager.Infrastructure.Common.Persistence.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovibetWalletManager.Infrastructure.Common.Persistence.Clients
{
    public class ECBHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly ECBResponseParser _parser;

        public ECBHttpClient(HttpClient httpClient, ECBResponseParser parser)
        {
            _httpClient = httpClient; 
            _parser = parser;
        }

        public async Task<Domain.CurrenciesRates.CurrenciesRates> GetCurrencyRatesAsync()
        {
            var url = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();
            var stringResp = await response.Content.ReadAsStringAsync();

            return _parser.ParseCurrenciesRates(stringResp);
        }
    }
}
