using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Racing.DAL.Repositories.Interface
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        public Task<List<TEntity>> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        public Task<TEntity> GetOne(object id);

        public ValueTask<EntityEntry<TEntity>> Insert(TEntity entity);

        public Task<TEntity> Delete(TEntity entityToDelete);

        public Task<TEntity> Update(TEntity entityToUpdate);
    }
}