using ErrorOr;
using MediatR;
using NovibetWalletManager.Domain.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovibetWalletManager.Application.Services.Wallet.Commands.AdjustWalletBalance
{
    public record AdjustWalletBalanceCmd(
        Guid walletId, decimal amount, CurrencyCode currency, string strategy
    ):IRequest<ErrorOr<WalletModel>>;
}
