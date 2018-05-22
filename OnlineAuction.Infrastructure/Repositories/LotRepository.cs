using Microsoft.EntityFrameworkCore;
using OnlineAuction.Core.Entities;
using OnlineAuction.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineAuction.Infrastructure.Repositories
{
    public class LotRepository : Repository<Lot>, ILotRepository
    {
        public LotRepository(OnlineAuctionDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Lot> GetLotWithBidsAsync(int id)
        {
            return await _dbContext.Lots
                .Where(l => l.LotID == id)
                .Include(l => l.Bids)
                .FirstOrDefaultAsync();
        }

        public IEnumerable<Lot> GetActiveUserLotsWithBids(string id)
        {
            return _dbContext.Lots
                .Where(l => l.AppUserID == id && l.LotState.LotStateName == "Active")
                .Include(l => l.Bids)
                .AsEnumerable();
        }

        public IEnumerable<Lot> GetSoldUserLotsWithBids(string id)
        {
            return _dbContext.Lots
                .Where(l => l.AppUserID == id && l.LotState.LotStateName == "Sold")
                .Include(l => l.Bids)
                .AsEnumerable();
        }

        public IEnumerable<Lot> GetActiveLotsWithBidsByName(string name)
        {
            return _dbContext.Lots
                .Where(l => l.LotName.Contains(name) && l.LotState.LotStateName == "Active")
                .Include(l => l.Bids)
                .AsEnumerable();
        }
    }
}
