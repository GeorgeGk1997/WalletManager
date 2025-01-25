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
        SEK
    }
}
