using OnlineAuction.Core.Entities;
using OnlineAuction.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineAuction.Core.Services
{
    public class LotService : ILotService
    {
        private readonly ILotRepository _lotRepository;
        private readonly ILotStateRepository _lotStateRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LotService(ILotRepository lotRepository, ILotStateRepository lotStateRepository, 
            IUnitOfWork unitOfWork, IRepository<Category> categoryRepository)
        {
            _lotRepository = lotRepository ?? throw new ArgumentNullException(nameof(lotRepository));
            _lotStateRepository = lotStateRepository ?? throw new ArgumentNullException(nameof(lotStateRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        /// <summary>
        /// Creates the lot and saves it to the database.
        /// </summary>
        /// <param name="lot"></param>
        public async Task CreateLotAsync(Lot lot)
        {
            if (lot == null) throw new ArgumentNullException(nameof(lot));

            await MarkLotAsActive(lot);            
            await _lotRepository.AddAsync(lot);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteLotAsync(Lot lot)
        {
            if (lot == null) throw new ArgumentNullException(nameof(lot));

            _lotRepository.Delete(lot);
            await _unitOfWork.CompleteAsync();
        }

        /// <summary>
        /// Gets the lot with bids checking the state and time left. 
        /// Actually the lot selling occures here by marking the lot as sold.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Lot domain object.
        /// </returns>
        public async Task<Lot> GetLotWithBidsAsync(int id)
        {
            var lot = await _lotRepository.GetLotWithBidsAsync(id);
            var activeLotState = await _lotStateRepository.GetStateByNameAsync("Active");

            // The second check is for preventing double marking sold lot.
            if (DateTime.Now > lot.EndTime && lot.LotStateID == activeLotState.LotStateID)
            {
                await MarkLotAsSold(lot);
                await _unitOfWork.CompleteAsync();
            }

            return lot;
        }

        /// <summary>
        /// Gets lots with active lot state by the category name. 
        /// </summary>
        /// <param name="category"></param>
        /// <returns>
        /// If parameters is null, returns all available lots.
        /// </returns>
        public IEnumerable<Lot> GetActiveLotsByCategory(string category)
        {
            if (category == null)
                return _lotRepository.Find(l => l.LotState.LotStateName == "Active");
            else
            {
                var categoryID = _categoryRepository
                    .Find(c => c.CategoryName == category)
                    .FirstOrDefault()
                    .CategoryID;

                return _lotRepository.Find(l => l.CategoryID == categoryID && l.LotState.LotStateName == "Active");
            }
        }

        public async Task<Lot> UpdateLotAsync(int id, Lot updatedLot)
        {
            if (updatedLot == null) throw new ArgumentNullException(nameof(updatedLot));

            var lotToUpdate = await _lotRepository.GetByIdAsync(id);
            if (lotToUpdate == null) throw new ArgumentNullException(nameof(lotToUpdate));

            lotToUpdate.LotName = updatedLot.LotName;
            lotToUpdate.Description = updatedLot.Description;
            lotToUpdate.EndTime = updatedLot.EndTime;
            lotToUpdate.CategoryID = updatedLot.CategoryID;
            lotToUpdate.MinBid = updatedLot.MinBid;
            lotToUpdate.StartPrice = updatedLot.StartPrice;
            lotToUpdate.StartTime = updatedLot.StartTime;

            await MarkLotAsActive(lotToUpdate);             
            await _unitOfWork.CompleteAsync();

            return lotToUpdate;
        }

        private async Task MarkLotAsActive(Lot lot)
        {
            var activeLotState = await _lotStateRepository.GetStateByNameAsync("Active");
            lot.LotStateID = activeLotState.LotStateID;
            lot.StartTime = DateTime.Now;
        }

        private async Task MarkLotAsSold(Lot lot)
        {
            var soldLotState = await _lotStateRepository.GetStateByNameAsync("Sold");
            lot.LotStateID = soldLotState.LotStateID;
        }
    }
}
