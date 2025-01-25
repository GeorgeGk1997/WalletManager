using NovibetWalletManager.Domain.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovibetWalletManager.Domain.CurrenciesRates
{
    public class CurrencyAndRate
    {
        public CurrencyCode CurrencyCode { get; set; } = null!;
        public decimal Rate { get; set; }

    }
}
