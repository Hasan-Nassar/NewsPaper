using System.Threading.Tasks;
using User.Persistence.Context;
using User.Persistence.Interfaces;

namespace User.Persistence.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        public  readonly UserDbContext _context;

        public IBaseRepository<Core.Entity.User> UserRepository { get; }
        
        
        public UnitOfWork(UserDbContext context)
        {
           
            UserRepository = new BaseRepository<Core.Entity.User>(context);
            _context = context;
        }

       

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}