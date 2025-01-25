using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovibetWalletManager.Infrastructure.Gateways.Clients
{
    public class ECBHttpClient
    {
        private readonly HttpClient _httpClient;

        public ECBHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient; 
        }

        public async Task<string> GetCurrencyRatesAsync()
        {
            var url = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
