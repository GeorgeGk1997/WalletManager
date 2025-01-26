using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovibetWalletManager.Domain.Common.Interfaces
{
    public interface IWalletBalanceAdjustmentStrategy
    {
        decimal AdjustBalance(decimal currentBalance, decimal amount);
    }
}
