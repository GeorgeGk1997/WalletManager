using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using NovibetWalletManager.Domain.Wallets;
using NovibetWalletManager.Application.Common.Interfaces;

namespace NovibetWalletManager.Application.Services.Wallet.Commands.CreateWallet
{
    public class CreateWalletCmdHandler : IRequestHandler<CreateWalletCmd, ErrorOr<WalletModel>>
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateWalletCmdHandler(IWalletRepository walletRepository, IUnitOfWork unitOfWork)
        { 
            _walletRepository = walletRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<WalletModel>> Handle(CreateWalletCmd request, CancellationToken cancellationToken)
        {
            //create wallet
            var wallet = new WalletModel(
                request.Balance
            );

            //add it to db
            await _walletRepository.AddWalletAsync(wallet);
            await _unitOfWork.CommitChangesAsync();

            return wallet;
        }
    }
}
