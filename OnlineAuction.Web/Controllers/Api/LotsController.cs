using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineAuction.Core.Entities;
using OnlineAuction.Core.Interfaces;
using OnlineAuction.Web.ViewModels.Lot;

namespace OnlineAuction.Web.Controllers.Api
{
    /// <summary>
    /// The Controller for AJAX operations.
    /// </summary>
    /// <remarks>
    /// JQUERY code is down below views in the 'scripts' section.
    /// </remarks>
    [Route("api/[controller]")]
    public class LotsController : Controller
    {
        private readonly ILotService _lotService;
        private readonly ILotRepository _lotRepository;
        private readonly IMapper _maper;

        public LotsController(IMapper maper, ILotService lotService, ILotRepository lotRepository)
        {
            _lotRepository = lotRepository;
            _lotService = lotService;
            _maper = maper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLotWithBids(int id)
        {
            var lot = await _lotService.GetLotWithBidsAsync(id);

            if (lot == null)
                return NotFound();

            var lotViewModel = _maper.Map<Lot, LotDetailsViewModel>(lot);
            lotViewModel.TotalBids = lotViewModel.Bids.Count();
            lotViewModel.Duration = (lot.EndTime - DateTime.Now);
            return Ok(lotViewModel);
        }

        [HttpGet]
        public IActionResult List()
        {
            var lots = _lotRepository.GetAll();
            var lotSummaryViewModels = _maper.Map<IEnumerable<Lot>, IEnumerable<LotSummaryViewModel>>(lots);
            return Ok(lotSummaryViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLot([FromBody] LotCreateViewModel lotViewModel)
        {
            if (lotViewModel == null)
                return BadRequest(ModelState);

            AddValidationErrors(lotViewModel);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var lot = _maper.Map<LotCreateViewModel, Lot>(lotViewModel);
            lot.AppUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _lotService.CreateLotAsync(lot);

            return Ok(lot);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLot(int id, [FromBody] LotCreateViewModel lotViewModel)
        {
            if (lotViewModel == null)
                return BadRequest(ModelState);

            AddValidationErrors(lotViewModel);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedLot = _maper.Map<LotCreateViewModel, Lot>(lotViewModel);
            var result = await _lotService.UpdateLotAsync(id, updatedLot);

            if (result == null)
                return NotFound();

            return Ok(_maper.Map<Lot, LotCreateViewModel>(result));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLot(int id)
        {
            var lot = await _lotRepository.GetByIdAsync(id);

            if (lot == null)
                return NotFound();

            await _lotService.DeleteLotAsync(lot);

            return Ok(id);
        }

        private void AddValidationErrors(LotCreateViewModel lotViewModel)
        {
            if (lotViewModel.EndTime < DateTime.Now)
                ModelState.AddModelError("EndTime", "Invalid end time.");

            if (lotViewModel.MinBid == 0)
                ModelState.AddModelError("MinBid", "Minimum bid amount must be grater than 0.");

            if (lotViewModel.StartPrice <= 0)
                ModelState.AddModelError("StartPrice", "Start price must be grater than 0.");
        }
    }
}
