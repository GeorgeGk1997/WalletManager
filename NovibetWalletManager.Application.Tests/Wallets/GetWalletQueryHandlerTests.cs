using Moq;
using NovibetWalletManager.Application.Common.Interfaces;
using NovibetWalletManager.Application.Services.Wallet.Queries;
using NovibetWalletManager.Domain.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NovibetWalletManager.Application.Tests.Wallets
{
    public class GetWalletQueryHandlerTests
    {
        private readonly Mock<IWalletRepository> _mockWalletRepository;
        private readonly Mock<ICurrencyRateRepository> _mockCurrencyRateRepository;

        private readonly GetWalletQueryHandler _handler;

        public GetWalletQueryHandlerTests()
        {
            _mockWalletRepository = new Mock<IWalletRepository>();
            _mockCurrencyRateRepository = new Mock<ICurrencyRateRepository>();

            _handler = new GetWalletQueryHandler(_mockWalletRepository.Object, _mockCurrencyRateRepository.Object);
        }

        [Fact]
        public async Task Handle_WalletNotFound_ReturnsNotFoundError()
        {
            // Arrange
            var walletId = Guid.NewGuid();
            var currencyCode = new CurrencyCode("EUR", 0);
            var query = new GetWalletQuery(walletId, currencyCode);

            _mockWalletRepository.Setup(repo => repo.GetWalletByIdAsync(walletId))
                .ReturnsAsync((WalletModel?)null);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsError);
            Assert.Equal("Wallet with this id is not found", result.FirstError.Description);
        }

        [Fact]
        public async Task Handle_WalletCurrencyMatches_ReturnsWallet()
        {
            // Arrange
            var walletId = Guid.NewGuid();
            var currencyCode = new CurrencyCode("EUR", 0);
            var wallet = new WalletModel(100m, currencyCode, walletId);
            var query = new GetWalletQuery(walletId, currencyCode);

            _mockWalletRepository.Setup(repo => repo.GetWalletByIdAsync(walletId))
                .ReturnsAsync(wallet);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsError);
            Assert.Equal(wallet, result.Value);
        }



    }
}
