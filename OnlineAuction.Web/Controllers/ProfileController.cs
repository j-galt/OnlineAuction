using System.Collections.Generic;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineAuction.Core.Entities;
using OnlineAuction.Core.Interfaces;
using OnlineAuction.Web.ViewModels.Lot;

namespace OnlineAuction.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILotRepository _lotRepository;

        public ProfileController(ILotRepository lotRepository, IMapper mapper)
        {
            _lotRepository = lotRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var currentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userLots = _lotRepository.GetActiveUserLotsWithBids(currentUserID);

            var lotSumViewModels = _mapper.Map<IEnumerable<Lot>, IEnumerable<LotSummaryViewModel>>(userLots);

            return View("_UserProfilePartial", lotSumViewModels);
        }

        public IActionResult SoldLots()
        {
            var currentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userLots = _lotRepository.GetSoldUserLotsWithBids(currentUserID);

            var lotSumViewModels = _mapper.Map<IEnumerable<Lot>, IEnumerable<LotSummaryViewModel>>(userLots);

            return View("_UserProfilePartial", lotSumViewModels);
        }
    }
}
