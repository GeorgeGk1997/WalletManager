using ErrorOr;
using MediatR;
using NovibetWalletManager.Domain.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovibetWalletManager.Application.Services.Wallet.Queries
{
    public record GetWalletQuery(Guid WalletId) :
         IRequest<ErrorOr<WalletModel>>;
}
