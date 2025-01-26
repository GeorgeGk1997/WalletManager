using NovibetWalletManager.Domain.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovibetWalletManager.Application.Common.Interfaces
{
    public interface IWalletRepository
    {
        Task AddWalletAsync(WalletModel wallet);
        Task<WalletModel> GetWalletByIdAsync(Guid id);
        Task UpdateBalanceOnDbAsync(Guid id, decimal newBalance);
    }
}
