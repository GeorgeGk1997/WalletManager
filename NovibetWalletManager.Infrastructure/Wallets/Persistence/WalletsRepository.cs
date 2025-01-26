using NovibetWalletManager.Application.Common.Interfaces;
using NovibetWalletManager.Domain.Wallets;
using NovibetWalletManager.Infrastructure.Common.Persistence.DbContexts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NovibetWalletManager.Infrastructure.Wallets.Persistence
{
    public class WalletsRepository : IWalletRepository
    {
        private readonly WalletManagementContext _dbContext;


        public WalletsRepository(WalletManagementContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddWalletAsync(WalletModel wallet)
        {
            await _dbContext.Wallets.AddAsync(wallet);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<WalletModel?> GetWalletByIdAsync(Guid id)
        {
            return await _dbContext.Wallets.FindAsync(id);
        }

        public async Task UpdateBalanceOnDbAsync(Guid id, decimal newBalance)
        {
            var wallet = await GetWalletByIdAsync(id);
            if (wallet == null)
            {
                throw new Exception($"Wallet with ID {id} not found.");
            }

            wallet.Balance = newBalance;

            await _dbContext.SaveChangesAsync();
        }

    }
}
