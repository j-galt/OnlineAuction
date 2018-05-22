using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineAuction.Core.Entities;
using OnlineAuction.Core.Interfaces;
using OnlineAuction.Web.ViewModels;
using OnlineAuction.Web.ViewModels.Lot;

namespace OnlineAuction.Web.Controllers
{
    public class LotController : Controller
    {
        private readonly ILotRepository _lotRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly ILotService _lotService;
        private readonly IBidService _bidService;
        private readonly IMapper _maper;
        public int _pageSize = 6;

        public LotController(IMapper mapper, ILotService lotService, IBidService bidService, IRepository<Category> categoryRepository, ILotRepository lotRepository)
        {
            _lotRepository = lotRepository;
            _categoryRepository = categoryRepository;
            _bidService = bidService;
            _maper = mapper;
            _lotService = lotService;
        }

        public IActionResult List(string category, int lotPage = 1)
        {
            var lots = _lotService.GetActiveLotsByCategory(category);
            var lotsOnPage = lots.Skip((lotPage - 1) * _pageSize).Take(_pageSize);
            var lotViewModels = _maper.Map<IEnumerable<Lot>, IEnumerable<LotSummaryViewModel>>(lotsOnPage);

            return View(new LotsListViewModel
            {
                Lots = lotViewModels,
                PagingInfo = new PagingInfoViewModel
                {
                    CurrentPage = lotPage,
                    ItemsPerPage = _pageSize,
                    TotalItems = lots.Count()
                },
                CurrentCategory = category
            });
        }

        public IActionResult Search(string name, int lotPage = 1)
        {
            var lots = _lotRepository.GetActiveLotsWithBidsByName(name);
            var lotsOnPage = lots.Skip((lotPage - 1) * _pageSize).Take(_pageSize);
            var lotViewModels = _maper.Map<IEnumerable<Lot>, IEnumerable<LotSummaryViewModel>>(lotsOnPage);

            return View("List", new LotsListViewModel
            {
                Lots = lotViewModels,
                PagingInfo = new PagingInfoViewModel
                {
                    CurrentPage = lotPage,
                    ItemsPerPage = _pageSize,
                    TotalItems = lots.Count()
                }
            });
        }

        public IActionResult Details(int id)
        {
            ViewBag.Error = TempData["Error"];
            return View(id);
        }

        [Authorize]
        public IActionResult Create()
        {
            var categories = _categoryRepository.GetAll().Select(c => new SelectListItem
            {
                Value = c.CategoryID.ToString(),
                Text = c.CategoryName
            });

            return View(new LotCreateViewModel { Categories = categories });
        }

        [Authorize]
        public async Task<IActionResult> Update(int id)
        {
            var categories = _categoryRepository.GetAll().Select(c => new SelectListItem
            {
                Value = c.CategoryID.ToString(),
                Text = c.CategoryName
            });

            var lot = await _lotRepository.GetByIdAsync(id);
            var lotCreateVM = _maper.Map<Lot, LotCreateViewModel>(lot);
            lotCreateVM.Categories = categories;

            return View(lotCreateVM);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var lot = await _lotRepository.GetByIdAsync(id);
            await _lotService.DeleteLotAsync(lot);
            return RedirectToAction("Index", "Manager");
        }

        [HttpPost]
        public async Task<IActionResult> MakeBid(MakeBidViewModel makeBidViewModel)
        {
            if (!User.Identity.IsAuthenticated)
            {
                // Get the previous page URL.
                var prevUri = Request.Headers["Referer"].ToString();                
                var start = prevUri.IndexOf("Lot") - 1;
                var res = prevUri.Substring(start, (prevUri.Length - start));
                return RedirectToAction("LogIn", "Account", new { returnUrl = res });
            }

            var bid = _maper.Map<MakeBidViewModel, Bid>(makeBidViewModel);
            bid.AppUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bid.BidTime = DateTime.Now;
            var result = await _bidService.CreateBidAsync(bid);

            if (!result.Succeeded)
            {
                TempData["Error"] = result.Error;
            }

            return RedirectToAction("Details", new { id = makeBidViewModel.LotID });
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
