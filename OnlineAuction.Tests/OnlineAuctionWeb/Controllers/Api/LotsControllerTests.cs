using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineAuction.Core.Interfaces;
using OnlineAuction.Web.Controllers.Api;
using System.Threading.Tasks;
using Xunit;

namespace OnlineAuction.Tests.OnlineAuctionWeb.Controllers.Api
{
    public class LotsControllerTests
    {
        [Fact]
        public async Task GetLotReturnsNotFoundResultWhenLotNotFound()
        {
            var mockLotService = new Mock<ILotService>();
            var controller = new LotsController(null, mockLotService.Object, null);

            var result = await controller.GetLotWithBids(-1);

            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public async Task GetLotReturnsBadRequestResultWhenIdIsNull()
        {
            var mockLotService = new Mock<ILotService>();
            var controller = new LotsController(null, mockLotService.Object, null);
            var result = await controller.CreateLot(null);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
