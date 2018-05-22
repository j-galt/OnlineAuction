using Moq;
using OnlineAuction.Core.Exceptions;
using OnlineAuction.Core.Interfaces;
using OnlineAuction.Core.Services;
using System;
using Xunit;

namespace OnlineAuction.Tests.OnlineAuctionCore.Services
{
    public class LotServiceTests
    {
        private int _invalidID = -1;
        private Mock<ILotRepository> _mockLotRepo;

        public LotServiceTests()
        {
            _mockLotRepo = new Mock<ILotRepository>();
        }

        [Fact]
        public async void ThrowsGivenInvalidLottId()
        {
            var lotService = new LotService(_mockLotRepo.Object, null, null, null);

            await Assert.ThrowsAsync<LotNotFoundException>(async () =>
                await lotService.GetLotWithBidsAsync(_invalidID));
        }

        [Fact]
        public async void ThrowsGivenNullLotObj()
        {
            var lotService = new LotService(null, null, null, null);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await lotService.UpdateLotAsync(1, null));
        }
    }
}
