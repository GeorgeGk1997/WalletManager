using NovibetWalletManager.Application.Common.Interfaces;
using NovibetWalletManager.Domain.Common.Interfaces;
using NovibetWalletManager.Domain.Wallets.BalanceAdsjustStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovibetWalletManager.Application.Common.Factories
{
    public class BalanceAdjustmentStrategyFactory : IBalanceAdjustmentStrategyFactory
    {
        public IWalletBalanceAdjustmentStrategy GetStrategy(string strategyName)
        {
            return strategyName.ToLower().Trim() switch
            {
                "addfundsstrategy" => new AddFundsStrategy(),
                "subtractfundsstrategy" => new SubtractFundsStrategy(),
                "forcesubtractfundsstrategy" => new ForceSubtractFundsStrategy(),
                _ => throw new InvalidOperationException("Strategy not found!")
            };
        }
    }
}
