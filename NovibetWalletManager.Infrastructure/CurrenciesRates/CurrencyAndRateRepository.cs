using Microsoft.Extensions.Options;
using NovibetWalletManager.Application.Common.Interfaces;
using NovibetWalletManager.Domain.Wallets;
using NovibetWalletManager.Infrastructure.Common.Persistence.Configs;
using Npgsql;
using Quartz.Xml.JobSchedulingData20;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace NovibetWalletManager.Infrastructure.CurrenciesRates
{
    public class CurrencyAndRateRepository : ICurrencyRateRepository
    {

        private readonly string _connectionString;

        //public CurrencyAndRateRepository(string connectionString)
        //{
        //    _connectionString = connectionString;
        //}

        public CurrencyAndRateRepository(IOptions<DatabaseConfig> config)
        {
            _connectionString = config.Value.NovibetWalletManagerDb;
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
                        parameters.Add(new NpgsqlParameter($"Rate{parameterIndex}", currencyAndRate.Rate+1));
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
    }
}
