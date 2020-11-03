using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace User.Persistence.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate = null, string included = null);
        Task<IEnumerable<TEntity>> GetAllIncluded(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] Includes);
        Task<TEntity> GetById(object id);
        void Add(TEntity obj);
        void Update(TEntity obj);
        TEntity Delete(TEntity id);
        
    }
}