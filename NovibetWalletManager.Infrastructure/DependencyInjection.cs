using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NovibetWalletManager.Application.Common.Interfaces;
using NovibetWalletManager.Application.Services.ECB.Commands;
using NovibetWalletManager.Infrastructure.Common.Persistence.Caching;
using NovibetWalletManager.Infrastructure.Common.Persistence.Clients;
using NovibetWalletManager.Infrastructure.Common.Persistence.Configs;
using NovibetWalletManager.Infrastructure.Common.Persistence.DbContexts;
using NovibetWalletManager.Infrastructure.Common.Persistence.Jobs;
using NovibetWalletManager.Infrastructure.Common.Persistence.Parsers;
using NovibetWalletManager.Infrastructure.CurrenciesRates;
using NovibetWalletManager.Infrastructure.Wallets.Persistence;
using Quartz;
using Quartz.Simpl;
using StackExchange.Redis;

namespace NovibetWalletManager.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseConfig>(configuration.GetSection("ConnectionStrings"));

            services.AddDbContext<WalletManagementContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("NovibetWalletManagerDb")!,
                    b => b.MigrationsAssembly("NovibetWalletManager.Infrastructure")
                    )
            );
            services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<WalletManagementContext>());
            services.AddScoped<IWalletRepository, WalletsRepository>();
            services.AddScoped<ICurrencyRateRepository, CurrencyAndRateRepository>();

            //auto einai an den xreisimopoiiso to option pattern
            //services.AddScoped<ICurrencyRateRepository, CurrencyAndRateRepository>(
            //    provider => new CurrencyAndRateRepository(configuration.GetConnectionString("NovibetWalletManagerDb")!));
            
            services.AddScoped<IECBClient, ECBClient>();
            services.AddScoped<ECBHttpClient>();
            services.AddScoped<ECBResponseParser>();
            services.AddScoped<UpdateCurrencyRatesCommand>();
            services.AddScoped<UpdateRatesJob>();
            services.AddHttpClient();

            //Quartz configs
            services.AddQuartz(q =>
            {
                q.UseJobFactory<MicrosoftDependencyInjectionJobFactory>();

                var jobKey = new JobKey("UpdateRatesJob");

                q.AddJob<UpdateRatesJob>(opts => opts.WithIdentity(jobKey));
                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("UpdateRatesTrigger")
                    .WithSimpleSchedule(x => x.WithIntervalInMinutes(1).RepeatForever()));
            });

            //some notes for me 
            //false quartz immediately terminates and stop any running jobs
            //true quartz wairs for any running job to finish before shutdown
            services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = false; // Graceful shutdown
            });

            //REDIS CONGIG
            var redisConnStr = configuration.GetSection("Redis:ConnectionString").Value;
            var redis = ConnectionMultiplexer.Connect((string)redisConnStr!);
            services.AddSingleton<IConnectionMultiplexer>(redis);
            services.AddSingleton<ICacheService, RedisCaching>();

            return services;
        }
    }
}
