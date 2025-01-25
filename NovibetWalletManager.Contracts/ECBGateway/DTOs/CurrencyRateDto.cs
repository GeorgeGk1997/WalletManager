using NovibetWalletManager.Contracts.ContractModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovibetWalletManager.Contracts.ECBGateway.DTOs
{
    public class CurrencyRateDto
    {
        public Currency Currency { get; set; }
        public decimal Rate { get; set; }
    }
}
