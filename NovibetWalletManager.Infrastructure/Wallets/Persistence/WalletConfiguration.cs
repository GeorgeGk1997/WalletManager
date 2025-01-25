using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NovibetWalletManager.Domain.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace NovibetWalletManager.Infrastructure.Wallets.Persistence
{
    public class WalletConfiguration : IEntityTypeConfiguration<WalletModel>
    {
        public void Configure(EntityTypeBuilder<WalletModel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Balance)
                .HasColumnName("Balance");

            builder.Property(x => x.CurrencyCode)
                .HasConversion(
                    currencyCode => currencyCode.Value,
                    value => CurrencyCode.FromValue(value)
                );

            //string name for Currency code

            //builder.Property(x => x.CurrencyCode)
            //    .HasConversion(
            //        currencyCode => currencyCode.Name,
            //        value => CurrencyCode.FromName(value, false)
            //    );
        }
    }
}
