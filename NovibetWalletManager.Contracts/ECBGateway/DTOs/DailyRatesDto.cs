using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovibetWalletManager.Contracts.ECBGateway.DTOs
{
    public class DailyRatesDto
    {
        public string Date {  get; set; }
        public List<CurrencyRateDto> Rates { get; set; } = 
            new List<CurrencyRateDto>();
    }
}
