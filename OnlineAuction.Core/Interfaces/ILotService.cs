using OnlineAuction.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineAuction.Core.Interfaces
{
    public interface ILotService
    {
        Task<Lot> GetLotWithBidsAsync(int id);
        IEnumerable<Lot> GetActiveLotsByCategory(string category);

        Task CreateLotAsync(Lot lot);
        Task<Lot> UpdateLotAsync(int id, Lot lot);

        Task DeleteLotAsync(Lot lot);
    }
}
