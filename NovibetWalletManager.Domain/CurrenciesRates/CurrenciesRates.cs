using NovibetWalletManager.Domain.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovibetWalletManager.Domain.CurrenciesRates
{
    public class CurrenciesRates
    {
        public string? Date { get; set; }
        
        public List<CurrencyAndRate> Rates { get; set; } =
            new List<CurrencyAndRate>()!;
    }
}
