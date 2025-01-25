using ErrorOr;
using MediatR;
using NovibetWalletManager.Domain.CurrenciesRates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovibetWalletManager.Application.Services.ECB.Queries
{
    public record GetCurrenciesRatesQuery : IRequest<ErrorOr<CurrenciesRates>>;
}
