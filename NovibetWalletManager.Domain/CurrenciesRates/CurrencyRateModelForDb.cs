using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovibetWalletManager.Domain.CurrenciesRates
{
    public class CurrencyRateModelForDb
    {
        public string Currency { get; set; } = null!;
        public decimal Rate { get; set; }
        public string Date { get; set; } = null!;
    }
}
