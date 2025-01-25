using Microsoft.EntityFrameworkCore;
using NovibetWalletManager.Application.Common.Interfaces;
using NovibetWalletManager.Domain.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NovibetWalletManager.Infrastructure.Common.Persistence.DbContexts
{
    public class WalletManagementContext : DbContext, IUnitOfWork
    {
        public DbSet<WalletModel> Wallets { get; set; } = null!;

        public WalletManagementContext(DbContextOptions options) : base(options)
        { 
             
        }

        public async Task CommitChangesAsync()
        {
           await base.SaveChangesAsync();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
