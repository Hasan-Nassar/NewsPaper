using System.Threading.Tasks;
using Author.Persistence.Context;
using Author.Persistence.Interfaces;

namespace Author.Persistence.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        public  readonly AuthorDbContext _context;
     
        public IBaseRepository<Core.Entity.Author> AuthRepository { get; }
        
        public UnitOfWork(AuthorDbContext context)
        {
           
            AuthRepository = new BaseRepositories<Core.Entity.Author>(context);
            _context = context;
        }

       

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}