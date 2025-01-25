using NovibetWalletManager.Contracts.ContractModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovibetWalletManager.Contracts.Wallets
{
    public record CreateWalletResponse(Guid Id, decimal Balance, Currency Currency);
}
