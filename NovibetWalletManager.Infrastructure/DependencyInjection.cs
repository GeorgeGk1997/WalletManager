using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NovibetWalletManager.Application.Common.Interfaces;
using NovibetWalletManager.Infrastructure.Common.Persistence.DbContexts;
using NovibetWalletManager.Infrastructure.Wallets.Persistence;

namespace NovibetWalletManager.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<WalletManagementContext>(options =>
            options.UseNpgsql(
                "Host=localhost;Port=5432;Database=WalletManagement;Username=postgres;Password=postgres",
                b => b.MigrationsAssembly("NovibetWalletManager.Infrastructure")
                )
            );
            services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<WalletManagementContext>());
            services.AddScoped<IWalletRepository, WalletsRepository>();
            return services;
        }
    }
}
