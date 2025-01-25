using NovibetWalletManager.Contracts.ContractModels;
using NovibetWalletManager.Domain.Wallets;

namespace NovibetWalletManager.Api.Mappers
{
    public class CurrencyMapper
    {
        public static Currency ToEnum(CurrencyCode currencyCode)
        {
            return currencyCode.Name switch
            {
                "EUR" => Currency.EUR,
                "USD" => Currency.USD,
                "JPY" => Currency.JPY,
                "BGN" => Currency.BGN,
                "CZK" => Currency.CZK,
                "DKK" => Currency.DKK,
                "GBP" => Currency.GBP,
                "HUF" => Currency.HUF,
                "PLN" => Currency.PLN,
                "RON" => Currency.RON,
                "SEK" => Currency.SEK,
                "CHF" => Currency.CHF,
                "ISK" => Currency.ISK,
                "NOK" => Currency.NOK,
                "TRY" => Currency.TRY,
                "AUD" => Currency.AUD,
                "BRL" => Currency.BRL,
                "CAD" => Currency.CAD,
                "CNY" => Currency.CNY,
                "HKD" => Currency.HKD,
                "IDR" => Currency.IDR,
                "ILS" => Currency.ILS,
                "INR" => Currency.INR,
                "KRW" => Currency.KRW,
                "MXN" => Currency.MXN,
                "MYR" => Currency.MYR,
                "NZD" => Currency.NZD,
                "PHP" => Currency.PHP,
                "SGD" => Currency.SGD,
                "THB" => Currency.THB,
                "ZAR" => Currency.ZAR,
                _ => throw new InvalidOperationException($"Unsupported currency: {currencyCode.Name}")
            };
        }
    }
}
