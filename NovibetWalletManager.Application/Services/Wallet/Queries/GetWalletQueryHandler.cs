using ErrorOr;
using MediatR;
using NovibetWalletManager.Application.Common.Interfaces;
using NovibetWalletManager.Domain.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NovibetWalletManager.Application.Services.Wallet.Queries
{
    public class GetWalletQueryHandler : IRequestHandler<GetWalletQuery,
        ErrorOr<WalletModel>>
    {
        private readonly IWalletRepository _walletRepository;
        public GetWalletQueryHandler(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository; 
        }
        public async Task<ErrorOr<WalletModel>> Handle(GetWalletQuery request, CancellationToken cancellationToken)
        {
            var wallet = await _walletRepository.GetWalletByIdAsync(request.WalletId);

            return
                wallet is null ?
                Error.NotFound(description: "Wallet with this id is not found") :
                wallet;

        }
    }
}
