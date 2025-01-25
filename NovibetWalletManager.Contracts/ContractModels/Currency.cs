using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NovibetWalletManager.Contracts.ContractModels
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Currency
    {
        EUR,
        USD,
        JPY,
        BGN,
        CZK,
        DKK,
        GBP,
        HUF,
        PLN,
        RON,
        SEK,
        CHF,
        ISK,
        NOK,
        TRY,
        AUD,
        BRL,
        CAD,
        CNY,
        HKD,
        IDR,
        ILS,
        INR,
        KRW,
        MXN,
        MYR,
        NZD,
        PHP,
        SGD,
        THB,
        ZAR
    }
}
