using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using User.Persistence.Context;
using User.Persistence.Interfaces;

namespace User.Persistence.Repositories
{
    public class BaseRepository<TEntity>: IBaseRepository <TEntity> where TEntity : class
    {
        
        private readonly UserDbContext _context = null;
        private readonly DbSet<TEntity> table = null;
     
        public BaseRepository(UserDbContext context) 
        {
            _context = context;
            table = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate = null, string Includes = null)
        {
            if(predicate != null)
            {
                var query = table.Where(predicate);
                if(Includes !=null)
                {
                    foreach (var str in Includes.Split(","))
                        query = query.Include(str).AsQueryable();
                }
                return await query.ToListAsync();
            }
            return await table.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllIncluded(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] Includes)
        {
            var query = table.Where(predicate);
            foreach (var Include in Includes)
            {
                query = query.Include(Include);
            }
            return await query.ToListAsync();
        }

      

        public async Task<TEntity> GetById(object id)
        { 
            return await table.FindAsync(id);
        }

        public void Add(TEntity obj)
        { 
            table.Add(obj); 
        }

        public void Update(TEntity obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public TEntity Delete(TEntity existing)
        {
            table.Remove(existing);
            return existing;
        }
        
        
    }
}