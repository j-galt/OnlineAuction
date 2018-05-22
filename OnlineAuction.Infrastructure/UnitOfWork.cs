using OnlineAuction.Core.Interfaces;
using System.Threading.Tasks;

namespace OnlineAuction.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OnlineAuctionDbContext _dbContext;

        public UnitOfWork(OnlineAuctionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CompleteAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
