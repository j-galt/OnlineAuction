using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineAuction.Core.Entities;
using OnlineAuction.Core.Interfaces;
using OnlineAuction.Web.ViewModels.Lot;

namespace OnlineAuction.Web.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class ManagerController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILotRepository _lotRepository;

        public ManagerController(ILotRepository lotRepository, IMapper mapper)
        {
            _lotRepository = lotRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var lots = _lotRepository.GetAll();
            var lotSumViewModels = _mapper.Map<IEnumerable<Lot>, IEnumerable<LotSummaryViewModel>>(lots);
            return View(lotSumViewModels);
        }
    }
}
