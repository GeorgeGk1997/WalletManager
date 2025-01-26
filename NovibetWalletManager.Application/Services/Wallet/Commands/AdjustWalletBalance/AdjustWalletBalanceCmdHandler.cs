using ErrorOr;
using MediatR;
using NovibetWalletManager.Application.Common.Interfaces;
using NovibetWalletManager.Domain.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NovibetWalletManager.Application.Services.Wallet.Commands.AdjustWalletBalance
{
    public class AdjustWalletBalanceCmdHandler :
        IRequestHandler<AdjustWalletBalanceCmd, ErrorOr<WalletModel>>
    {
        private readonly ICurrencyRateRepository _currencyRateRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IBalanceAdjustmentStrategyFactory _balanceAdjustmentStrategyFactory;
        private readonly IUnitOfWork _unitOfWork;

        public AdjustWalletBalanceCmdHandler(
            IWalletRepository walletRepository, ICurrencyRateRepository currencyRateRepository,
            IBalanceAdjustmentStrategyFactory balanceAdjustmentStrategyFactory, IUnitOfWork unitOfWork
        )
        {
            _currencyRateRepository = currencyRateRepository;
            _walletRepository = walletRepository;
            _balanceAdjustmentStrategyFactory = balanceAdjustmentStrategyFactory;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<WalletModel>> Handle(AdjustWalletBalanceCmd request, CancellationToken cancellationToken)
        {
            var wallet = await _walletRepository.GetWalletByIdAsync(request.walletId);

            if(wallet is null)
            {
                return ErrorOr.Error.NotFound(description: "Wallet with this id is not found");
            }

            if (wallet.CurrencyCode == request.currency)
            {
                var newBalance = _balanceAdjustmentStrategyFactory
                                    .GetStrategy(request.strategy)
                                    .AdjustBalance((decimal)wallet.Balance!, request.amount);

                await _walletRepository.UpdateBalanceOnDbAsync(request.walletId, newBalance);
                await _unitOfWork.CommitChangesAsync();

                return new WalletModel(
                    newBalance,
                    request.currency,
                    wallet.Id
                );
            }
            else
            {
                var rate = await _currencyRateRepository.GetRateFromCurrencyDbAsync(request.currency);
                var amountToEuro = request.amount / rate;
                var newBalance = _balanceAdjustmentStrategyFactory
                                    .GetStrategy(request.strategy)
                                    .AdjustBalance((decimal)wallet.Balance!, (decimal)amountToEuro);

                await _walletRepository.UpdateBalanceOnDbAsync(request.walletId, newBalance);
                await _unitOfWork.CommitChangesAsync();

                return new WalletModel(
                    newBalance,
                    request.currency,
                    wallet.Id
                );
            }

        }
    }
}
