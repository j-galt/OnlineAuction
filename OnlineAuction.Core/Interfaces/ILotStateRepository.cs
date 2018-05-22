using OnlineAuction.Core.Entities;
using System.Threading.Tasks;

namespace OnlineAuction.Core.Interfaces
{
    public interface ILotStateRepository
    {
        Task<LotState> GetStateByNameAsync(string name);
    }
}
