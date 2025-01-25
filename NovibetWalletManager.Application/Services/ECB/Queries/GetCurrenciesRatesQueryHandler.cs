using ErrorOr;
using MediatR;
using NovibetWalletManager.Application.Common.Interfaces;
using NovibetWalletManager.Domain.CurrenciesRates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovibetWalletManager.Application.Services.ECB.Queries
{
    public class GetCurrenciesRatesQueryHandler : IRequestHandler<GetCurrenciesRatesQuery, ErrorOr<CurrenciesRates>>
    {
        private readonly IECBClient _eCBClient;

        public GetCurrenciesRatesQueryHandler(IECBClient eCBClient)
        {
            _eCBClient = eCBClient;
        }

        public async Task<ErrorOr<CurrenciesRates>> Handle(GetCurrenciesRatesQuery request, CancellationToken cancellationToken)
        {
            var currenciesRates = await _eCBClient.GetCurrenciesRatesECB();

            return
                currenciesRates is null ?
                    Error.Failure(description: "Currrencies Rated couldn't be fetched from ECB") :
                    currenciesRates;
        }
    }
}
