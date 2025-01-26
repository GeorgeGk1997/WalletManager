using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovibetWalletManager.Application.Services;
using NovibetWalletManager.Application.Services.Wallet.Commands.CreateWallet;
using NovibetWalletManager.Application.Services.Wallet.Queries;
using NovibetWalletManager.Contracts.ContractModels;
using NovibetWalletManager.Contracts.Wallets;
using DomainCurrencyCode = NovibetWalletManager.Domain.Wallets.CurrencyCode;

namespace NovibetWalletManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletsController : ControllerBase
    {
        private readonly IMediator _mediator;


        public WalletsController(IMediator mediator )
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWallet(CreateWalletRequest request)
        {
            var cmd = new CreateWalletCmd(
                request.Balance
            ); 

            var createWalletResult = await _mediator.Send( cmd );

            return createWalletResult.MatchFirst(

                wallet => Ok(new CreateWalletResponse(
                    createWalletResult.Value.Id, request.Balance,
                    (Currency)Enum.Parse(typeof(Currency), createWalletResult.Value.CurrencyCode.Name, true))),

                error => Problem()
            );
        }



        [HttpGet("{walletId:guid}")]
        public async Task<IActionResult> GetWallet(Guid walletId)
        {
            var query = new GetWalletQuery(
                walletId
            );

            var getWalletResult = await _mediator.Send(query);
           

            return getWalletResult.MatchFirst(

                wallet => Ok(new CreateWalletResponse(
                    getWalletResult.Value.Id,
                    getWalletResult.Value.Balance == null? 0m:(decimal) getWalletResult.Value.Balance,
                    Enum.Parse<Currency>(getWalletResult.Value.CurrencyCode.Name))),

                error => Problem()
            );
        }
    }
}
