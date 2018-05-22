using OnlineAuction.Core.Entities;
using OnlineAuction.Core.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineAuction.Core.Services
{
    public class BidService : IBidService
    {
        private readonly ILotStateRepository _lotStateRepository;
        private readonly IRepository<Lot> _lotRepository;
        private readonly IRepository<Bid> _bidRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BidService(IRepository<Bid> bidRepository, IUnitOfWork unitOfWork, 
            IRepository<Lot> lotRepository, ILotStateRepository lotStateRepository)
        {
            _lotStateRepository = lotStateRepository ?? throw new ArgumentNullException(nameof(lotStateRepository));
            _lotRepository = lotRepository ?? throw new ArgumentNullException(nameof(lotRepository));
            _bidRepository = bidRepository ?? throw new ArgumentNullException(nameof(bidRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        /// <summary>
        /// Creates the user bid.
        /// </summary>
        /// <param name="bid"></param>
        /// <returns>
        /// The BidResult object with success state and errors if occured.
        /// </returns>
        public async Task<BidResult> CreateBidAsync(Bid bid)
        {
            if (bid == null) throw new ArgumentNullException(nameof(bid));

            var lot = await _lotRepository.GetByIdAsync(bid.LotID);
            if (lot == null) throw new ArgumentNullException(nameof(lot));

            var soldLotState = await _lotStateRepository.GetStateByNameAsync("Sold");
            if (soldLotState == null) throw new ArgumentNullException(nameof(soldLotState));

            if (lot.LotStateID == soldLotState.LotStateID)
                return AddError("The lot has been sold.");               

            if (lot.Bids.Any())
            {
                var maxBid = lot.Bids.Max(b => b.Amount);

                if (bid.Amount < maxBid + lot.MinBid)
                    return AddError("Entered value doesn't match minimum bid (" + 
                        (maxBid + lot.MinBid) + ")! Don't be greedy ;)");
                else
                    await _bidRepository.AddAsync(bid);
            }
            else
            {
                if (bid.Amount < lot.StartPrice + lot.MinBid)
                    return AddError("Entered value doesn't match minimum bid (" +
                        (lot.StartPrice + lot.MinBid) + ")! Don't be greedy ;)");
                else
                    await _bidRepository.AddAsync(bid);
            }

            await _unitOfWork.CompleteAsync();
            return new BidResult { Succeeded = true };
        }

        private BidResult AddError(string error)
        {
            return new BidResult { Succeeded = false, Error = error };
        }
    }
}
