using NovibetWalletManager.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NovibetWalletManager.Application.Common.Interfaces;
using NovibetWalletManager.Application.Common.Factories;

namespace NovibetWalletManager.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssemblyContaining(
                    typeof(DependencyInjection)); //search application for IHandler Methods
            });

            services.AddScoped<IBalanceAdjustmentStrategyFactory, BalanceAdjustmentStrategyFactory>();

            return services;
        }
    }
}
