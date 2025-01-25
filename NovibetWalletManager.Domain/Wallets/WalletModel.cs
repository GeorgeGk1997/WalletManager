using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NovibetWalletManager.Domain.Wallets
{
    public class WalletModel
    {
        public Guid Id { get; private set;  }
        public decimal? Balance { get; private set; }
        public CurrencyCode CurrencyCode { get; private set; } = null!;

        public WalletModel(decimal balance, CurrencyCode currencyCode, Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
            Balance = balance;
            CurrencyCode = currencyCode;
        }

        private WalletModel()
        {
            //created for .netcore entity framework to be accessed by reflection 
        }
    }
}
