using System;
using System.Threading.Tasks;

namespace OnlineAuction.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task CompleteAsync();
    }
}
