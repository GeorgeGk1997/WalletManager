using NovibetWalletManager.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovibetWalletManager.Application.Common.Interfaces
{
    public interface IBalanceAdjustmentStrategyFactory
    {
        IWalletBalanceAdjustmentStrategy GetStrategy(string strategyName);
    }
}
