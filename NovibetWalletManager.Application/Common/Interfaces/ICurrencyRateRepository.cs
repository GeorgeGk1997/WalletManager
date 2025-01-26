using NovibetWalletManager.Domain.CurrenciesRates;
using NovibetWalletManager.Domain.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovibetWalletManager.Application.Common.Interfaces
{
    public interface ICurrencyRateRepository
    {
        Task UpdateCurrencyRatesOnDbAsync(CurrenciesRates currenciesRates, int batchSize=10);

        Task<decimal?> GetRateFromCurrencyDbAsync(CurrencyCode currency);
    }
}
