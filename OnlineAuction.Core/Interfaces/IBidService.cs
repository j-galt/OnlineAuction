using OnlineAuction.Core.Entities;
using OnlineAuction.Core.Services;
using System.Threading.Tasks;

namespace OnlineAuction.Core.Interfaces
{
    public interface IBidService
    {
        Task<BidResult> CreateBidAsync(Bid bid);
    }
}
