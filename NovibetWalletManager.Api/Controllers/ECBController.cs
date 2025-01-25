using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovibetWalletManager.Api.Mappers;
using NovibetWalletManager.Application.Services.ECB.Queries;
using NovibetWalletManager.Contracts.ECBGateway;
using NovibetWalletManager.Contracts.ECBGateway.DTOs;

namespace NovibetWalletManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ECBController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ECBController(IMediator mediator)
        {
             _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrenciesRateECB()
        {
            var currenciesRates = await _mediator.Send(new GetCurrenciesRatesQuery());

            return currenciesRates.MatchFirst<IActionResult>(
                curAndRates => (IActionResult) Ok( new GetCurrencyRateECBResponse(new DailyRatesDto
                {
                    Date = currenciesRates.Value.Date,
                    Rates = currenciesRates.Value.Rates
                            .Select(rate => new CurrencyRateDto
                            {
                                Currency = CurrencyMapper.ToEnum(rate.CurrencyCode),
                                Rate = rate.Rate
                            })
                            .ToList()
                })),

                error => (IActionResult)Problem()
            );
        }
    }
}
