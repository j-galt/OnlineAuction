using OnlineAuction.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineAuction.Core.Interfaces
{
    public interface ILotRepository : IRepository<Lot>
    {
        Task<Lot> GetLotWithBidsAsync(int id);
        IEnumerable<Lot> GetActiveUserLotsWithBids(string id);
        IEnumerable<Lot> GetSoldUserLotsWithBids(string id);
        IEnumerable<Lot> GetActiveLotsWithBidsByName(string name);
    }
}
