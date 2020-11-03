using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using Author.Persistence.Context;
using Author.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Author.Persistence.Repositories
{
    public class BaseRepositories <TEntity>: IBaseRepository <TEntity> where TEntity : class
    {
        
        private readonly AuthorDbContext _context = null;
        private readonly DbSet<TEntity> table = null;
     
        public BaseRepositories(AuthorDbContext context) 
        {
            _context = context;
            table = context.Set<TEntity>();
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

        
        
        
    }
}