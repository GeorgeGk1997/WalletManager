using NovibetWalletManager.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NovibetWalletManager.Domain.Wallets
{
    public class WalletModel
    {
        public Guid Id { get; private set;  }
        public decimal? Balance { get; set; }
        public CurrencyCode CurrencyCode { get; private set; } = null!;

        public WalletModel(decimal balance, CurrencyCode? currencyCode=null, Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
            Balance = balance;
            CurrencyCode = currencyCode ?? new CurrencyCode("EUR",0);
        }

        private WalletModel()
        {
            //created for .netcore entity framework to be accessed by reflection 
        }
    }
}
