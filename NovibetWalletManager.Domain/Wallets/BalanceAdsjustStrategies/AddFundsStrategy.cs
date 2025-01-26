using NovibetWalletManager.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovibetWalletManager.Domain.Wallets.BalanceAdsjustStrategies
{
    public class AddFundsStrategy : IWalletBalanceAdjustmentStrategy
    {
        public decimal AdjustBalance(decimal currentBalance, decimal amount)
        {
            return currentBalance + amount;
        }
    }
}
