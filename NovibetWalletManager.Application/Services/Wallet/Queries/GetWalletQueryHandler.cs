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
        private readonly ICurrencyRateRepository _currencyRateRepository;

        public GetWalletQueryHandler(IWalletRepository walletRepository, ICurrencyRateRepository currencyRateRepository)
        {
            _walletRepository = walletRepository; 
            _currencyRateRepository = currencyRateRepository;
        }

        public async Task<ErrorOr<WalletModel>> Handle(GetWalletQuery request, CancellationToken cancellationToken)
        {
            var wallet = await _walletRepository.GetWalletByIdAsync(request.WalletId);

            if(wallet is null)
            {
                return Error.NotFound(description: "Wallet with this id is not found");
            }

            if (wallet.CurrencyCode == request.Currency)
            {
                return wallet;
            }
            else
            {
                //var rate = await _currencyRateRepository.GetRateFromCurrencyDbAsync(request.Currency);
                var rate = await _currencyRateRepository.GetRateFromCurrencyRedisDbAsync(request.Currency);

                //return rate is null ?
                //    Error.NotFound(description: "Conversion Rates isnt found!") :

                return new WalletModel(
                    (decimal)wallet.Balance! * (decimal)rate!,
                    request.Currency,
                    wallet.Id
                );
            }

        }
    }
}
