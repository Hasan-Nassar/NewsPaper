using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Author.Persistence.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        
        Task<TEntity> GetById(object id);
        void Add(TEntity obj);
        void Update(TEntity obj);
        
        
    }
}