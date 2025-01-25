using Ardalis.SmartEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovibetWalletManager.Domain.Wallets
{
    public class CurrencyCode : SmartEnum<CurrencyCode>
    {
        public static readonly CurrencyCode EUR = new(nameof(EUR), 0);
        public static readonly CurrencyCode USD = new(nameof(USD), 1);
        public static readonly CurrencyCode JPY = new(nameof(JPY), 2);
        public static readonly CurrencyCode BGN = new(nameof(BGN), 3);
        public static readonly CurrencyCode CZK = new(nameof(CZK), 4);
        public static readonly CurrencyCode DKK = new(nameof(DKK), 5);
        public static readonly CurrencyCode GBP = new(nameof(GBP), 6);
        public static readonly CurrencyCode HUF = new(nameof(HUF), 7);
        public static readonly CurrencyCode PLN = new(nameof(PLN), 8);
        public static readonly CurrencyCode RON = new(nameof(RON), 9);
        public static readonly CurrencyCode SEK = new(nameof(SEK), 10);
        public static readonly CurrencyCode CHF = new(nameof(CHF), 11);
        public static readonly CurrencyCode ISK = new(nameof(ISK), 12);
        public static readonly CurrencyCode NOK = new(nameof(NOK), 13);
        public static readonly CurrencyCode TRY = new(nameof(TRY), 14);
        public static readonly CurrencyCode AUD = new(nameof(AUD), 15);
        public static readonly CurrencyCode BRL = new(nameof(BRL), 16);
        public static readonly CurrencyCode CAD = new(nameof(CAD), 17);
        public static readonly CurrencyCode CNY = new(nameof(CNY), 18);
        public static readonly CurrencyCode HKD = new(nameof(HKD), 19);
        public static readonly CurrencyCode IDR = new(nameof(IDR), 20);
        public static readonly CurrencyCode ILS = new(nameof(ILS), 21);
        public static readonly CurrencyCode INR = new(nameof(INR), 22);
        public static readonly CurrencyCode KRW = new(nameof(KRW), 23);
        public static readonly CurrencyCode MXN = new(nameof(MXN), 24);
        public static readonly CurrencyCode MYR = new(nameof(MYR), 25);
        public static readonly CurrencyCode NZD = new(nameof(NZD), 26);
        public static readonly CurrencyCode PHP = new(nameof(PHP), 27);
        public static readonly CurrencyCode SGD = new(nameof(SGD), 28);
        public static readonly CurrencyCode THB = new(nameof(THB), 29);
        public static readonly CurrencyCode ZAR = new(nameof(ZAR), 30);


        public CurrencyCode(string name, int value) : base(name, value)
        {

        }
    }
}
