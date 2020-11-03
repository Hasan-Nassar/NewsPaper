using System.Threading.Tasks;

namespace User.Persistence.Interfaces
{
    public interface IUnitOfWork
    {
        IBaseRepository<Core.Entity.User> UserRepository { get; }
        Task CompleteAsync();
    }
}