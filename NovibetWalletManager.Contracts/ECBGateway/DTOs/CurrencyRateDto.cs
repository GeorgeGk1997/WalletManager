using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovibetWalletManager.Contracts.ECBGateway.DTOs
{
    public class CurrencyRateDto
    {
        public string Currency { get; set; }
        public decimal Rate { get; set; }
    }
}
