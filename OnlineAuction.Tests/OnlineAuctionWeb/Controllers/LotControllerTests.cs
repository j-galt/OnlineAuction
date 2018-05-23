using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineAuction.Core.Entities;
using OnlineAuction.Core.Interfaces;
using OnlineAuction.Web.Controllers;
using OnlineAuction.Web.ViewModels.Lot;
using System;
using System.Collections.Generic;
using Xunit;

namespace OnlineAuction.Tests.OnlineAuctionWeb.Controllers
{
    public class LotControllerTests
    {
        [Fact]
        public void ListReturnsAViewResultWithAListOfLotVM()
        {
            var mockLotService = new Mock<ILotService>();
            var mockMapper = new Mock<IMapper>();
            mockLotService.Setup(repo => repo.GetActiveLotsByCategory("")).Returns(GetTestLots());
            var controller = new LotController(mockMapper.Object, mockLotService.Object, null, null, null);

            var result = controller.List("");

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<LotsListViewModel>(viewResult.Model);
            Assert.Equal(GetTestLots().Count, model.PagingInfo.TotalItems);
        }

        public static List<Lot> GetTestLots()
        {
            return new List<Lot>()
            {
                new Lot { CategoryID = 1, Description = "Skirt", LotName = "Skirt", MinBid = 1, StartPrice = 100, LotStateID = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(7) },
                new Lot { CategoryID = 1, Description = "Trousers", LotName = "Trousers", MinBid = 100, StartPrice = 100, LotStateID = 2, StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(7) },
                new Lot { CategoryID = 1, Description = "Socks", LotName = "Socks", MinBid = 1, StartPrice = 100, LotStateID = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(4) },
                new Lot { CategoryID = 1, Description = "Gloves", LotName = "Gloves", MinBid = 1, StartPrice = 100, LotStateID = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(4) },
                new Lot { CategoryID = 1, Description = "Jacket", LotName = "Jacket", MinBid = 1, StartPrice = 100, LotStateID = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(1) },
                new Lot { CategoryID = 1, Description = "Shorts", LotName = "Shorts", MinBid = 1, StartPrice = 100, LotStateID = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(3) },
                new Lot { CategoryID = 1, Description = "Magic Socks", LotName = "Magic Socks", MinBid = 1000, StartPrice = 100, LotStateID = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(8) }
            };
        }
    }
}
