using Microsoft.EntityFrameworkCore;
using OnlineAuction.Core.Entities;
using OnlineAuction.Core.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineAuction.Infrastructure.Repositories
{
    public class LotStateRepository : Repository<LotState>, ILotStateRepository
    {
        public LotStateRepository(OnlineAuctionDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<LotState> GetStateByNameAsync(string name)
        {
            return await _dbContext.LotStates
                .Where(ls => ls.LotStateName == name)
                .SingleOrDefaultAsync();
        }
    }
}
