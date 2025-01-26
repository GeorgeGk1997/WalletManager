using NovibetWalletManager.Application.Common.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovibetWalletManager.Infrastructure.CurrenciesRates
{
    public class CurrencyAndRateRepository : ICurrencyRateRepository
    {

        private readonly string _connectionString;

        public CurrencyAndRateRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task UpdateCurrencyRatesOnDbAsync(Domain.CurrenciesRates.CurrenciesRates currenciesRates)
        {
           
            var sql = @"
                INSERT INTO currency_rates (currency, rate, date)
                VALUES (@Currency, @Rate, @Date)
                ON CONFLICT (currency, date)
                DO UPDATE SET 
                    rate = EXCLUDED.rate,
                    date = EXCLUDED.date;
            ";

            var strr = "Server=localhost;Database=WalletManagement;Port=5432;Username=postgres;Password=postgres";
            await using var connection = new NpgsqlConnection(strr);
            await connection.OpenAsync();

            await using var transaction = await connection.BeginTransactionAsync();
            try
            {
                DateTime date = DateTime.Parse(currenciesRates.Date);
                foreach (var currencyAndRate in currenciesRates.Rates)
                {
                    await using var command = new NpgsqlCommand(sql, connection, transaction);
                    command.Parameters.AddWithValue("Currency", currencyAndRate.CurrencyCode.Name);
                    command.Parameters.AddWithValue("Rate", currencyAndRate.Rate);
                    command.Parameters.AddWithValue("Date", date);

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
