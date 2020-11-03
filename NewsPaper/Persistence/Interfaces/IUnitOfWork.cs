using System.Threading.Tasks;
using Author.Core.Entity;
using Author.Persistence.Context;

namespace Author.Persistence.Interfaces
{
    public interface IUnitOfWork
    {
        IBaseRepository<Core.Entity.Author> AuthRepository { get; }
        Task CompleteAsync();
    }
}