using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NovibetWalletManager.Application.Common.Interfaces;
using NovibetWalletManager.Domain.CurrenciesRates;
using NovibetWalletManager.Domain.Wallets;
using NovibetWalletManager.Infrastructure.Common.Persistence.Configs;
using Npgsql;
using Quartz.Xml.JobSchedulingData20;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace NovibetWalletManager.Infrastructure.CurrenciesRates
{
    public class CurrencyAndRateRepository : ICurrencyRateRepository
    {

        private readonly string _connectionString;
        private readonly ICacheService _cacheService;
        private readonly IConfiguration _configuration;

        public CurrencyAndRateRepository(IOptions<DatabaseConfig> config,
            ICacheService cacheService, IConfiguration configuration)
        {
            _connectionString = config.Value.NovibetWalletManagerDb;
            _cacheService = cacheService;
            _configuration = configuration;
        }

        public async Task<decimal?> GetRateFromCurrencyDbAsync(CurrencyCode currency)
        {
            const string sqlQuery  = "SELECT rate FROM currency_rates WHERE currency = @currency";

            await using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                await using (var command = new NpgsqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@currency", currency.Name);

                    var result = await command.ExecuteScalarAsync();

                    // Return the result if not null, otherwise return null
                    return result != DBNull.Value ? (decimal?)result : null;
                }
            }
        }

        public async Task UpdateCurrencyRatesOnDbAsync(Domain.CurrenciesRates.CurrenciesRates currenciesRates, int batchSize = 10)
        {

            var sql = @"
                INSERT INTO currency_rates (currency, rate, date)
                VALUES {0}
                ON CONFLICT (currency, date)
                DO UPDATE SET 
                    rate = EXCLUDED.rate,
                    date = EXCLUDED.date;
            ";

            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var transaction = await connection.BeginTransactionAsync();
            try
            {
                DateTime date = DateTime.Parse(currenciesRates.Date);

                // split data on batches
                var rateChunks = currenciesRates.Rates.Chunk(batchSize);

                foreach (var chunk in rateChunks)
                {
                    var parameterValues = new List<string>();
                    var parameters = new List<NpgsqlParameter>();
                    int parameterIndex = 0;

                    foreach (var currencyAndRate in chunk)
                    {
                        parameterValues.Add($"(@Currency{parameterIndex}, @Rate{parameterIndex}, @Date{parameterIndex})");

                        parameters.Add(new NpgsqlParameter($"Currency{parameterIndex}", currencyAndRate.CurrencyCode.Name));
                        parameters.Add(new NpgsqlParameter($"Rate{parameterIndex}", currencyAndRate.Rate));
                        parameters.Add(new NpgsqlParameter($"Date{parameterIndex}", date));

                        parameterIndex++;
                    }

                    var valuesClause = string.Join(", ", parameterValues);
                    var finalSql = string.Format(sql, valuesClause);

                    await using var command = new NpgsqlCommand(finalSql, connection, transaction);
                    command.Parameters.AddRange(parameters.ToArray());
                    await command.ExecuteNonQueryAsync();
                }

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<CurrencyRateModelForDb>> GetRatesDbAsync()
        {
            const string sqlQuery = "SELECT * FROM currency_rates";

            var currencyRates = new List<CurrencyRateModelForDb>();

            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new NpgsqlCommand(sqlQuery, connection);
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var currencyRate = new CurrencyRateModelForDb
                {
                    Currency = reader["currency"].ToString()!,
                    Rate = Convert.ToDecimal(reader["rate"]),
                    Date = (string)Convert.ToString(reader["date"])!
                };

                currencyRates.Add(currencyRate);
            }

            return currencyRates;
        }

        public async Task<IEnumerable<CurrencyRateModelForDb>> GetCurrencyRatesRedisDbAsync()
        {
            var cacheKey = _configuration["Redis:CacheKey"];

            //check 1st the redis cache
            var cachedRates = await _cacheService.GetAsync<IEnumerable<CurrencyRateModelForDb>>(
                    (string)cacheKey!);

            if(cachedRates is not null)
            {
                return cachedRates;
            }

            //if redis dont have the cached the CaR

            //fetch from db
            var ratesFromDB = await GetRatesDbAsync();

            //cache the results
            await _cacheService.SetAsync(cacheKey, ratesFromDB, TimeSpan.FromMinutes(5));

            return ratesFromDB;

        }

        public async Task<decimal> GetRateFromCurrencyRedisDbAsync(CurrencyCode currency)
        {
            var cacheKey = _configuration["Redis:CacheKey"];

            // Check Redis cache
            var cachedRates = await _cacheService.GetAsync<IEnumerable<CurrencyRateModelForDb>>(cacheKey);

            if (cachedRates is not null)
            {
                // Find the rate for the specific currency in the cached data
                var c = currency.Name;
                var cachedRate = cachedRates.FirstOrDefault(r => r.Currency.Equals(currency.Name));
                if (cachedRate is not null)
                {
                    return cachedRate.Rate; // Return the rate directly from the cache
                }
            }

            
            // Return the specific rate from the database
            var rate = await GetRateFromCurrencyDbAsync(currency);
            return (decimal)rate;
        }
    }
}
