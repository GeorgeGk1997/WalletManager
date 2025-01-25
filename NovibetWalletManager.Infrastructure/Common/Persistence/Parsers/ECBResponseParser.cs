using Ardalis.SmartEnum;
using NovibetWalletManager.Domain.Wallets;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CurrencyAndRate = NovibetWalletManager.Domain.CurrenciesRates.CurrencyAndRate;

namespace NovibetWalletManager.Infrastructure.Common.Persistence.Parsers
{
    public class ECBResponseParser
    {
        public Domain.CurrenciesRates.CurrenciesRates ParseCurrenciesRates(string xmlResponse)
        {
            var document = XDocument.Parse(xmlResponse);

            var cubeNode = document
                .Descendants()
                .FirstOrDefault(node => node.Name.LocalName == "Cube" && node.Attribute("time") != null);

            if (cubeNode == null)
                return null;

            var date = cubeNode.Attribute("time")?.Value;

            var currenciesAndRates = cubeNode
                .Elements()
                .Select(cube => new CurrencyAndRate
                {
                    CurrencyCode = SmartEnum<CurrencyCode>.FromName(cube.Attribute("currency")?.Value, ignoreCase:true),
                    Rate = decimal.Parse(cube.Attribute("rate")?.Value ?? "0", CultureInfo.InvariantCulture) // note to remember CultureInfo.InvariantCulture : ensures that the parser interprets floating point
                })
                .ToList();

            return new Domain.CurrenciesRates.CurrenciesRates
            {
                Date = date,
                Rates = currenciesAndRates
            };
        }
    }
}
