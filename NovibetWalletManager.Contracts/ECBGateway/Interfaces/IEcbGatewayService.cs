using NovibetWalletManager.Contracts.ECBGateway.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovibetWalletManager.Contracts.ECBGateway.Interfaces
{
    public interface IEcbGatewayService
    {
        Task<DailyRatesDto> GetDailyRatesAsync();
    }
}
