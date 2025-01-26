using ErrorOr;
using MediatR;
using NovibetWalletManager.Domain.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovibetWalletManager.Application.Services.Wallet.Commands.CreateWallet
{
    //public record CreateWalletCmd(decimal Balance, CurrencyCode Currency) :
    //    IRequest<ErrorOr<WalletModel>>;

    public record CreateWalletCmd(decimal Balance) :
        IRequest<ErrorOr<WalletModel>>;
}
