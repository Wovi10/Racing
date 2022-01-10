using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Racing.DAL.Context;
using Racing.DAL.Repositories.Interface;

namespace Racing.DAL.Repositories
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DBRacingContext context;
        private readonly DbSet<TEntity> dbSet;

        protected GenericRepository(DBRacingContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual Task<List<TEntity>> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            return Task.Run(() =>
            {
                IQueryable<TEntity> query = dbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split(new char[] {','},
                             StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                Console.WriteLine(query.ToString());
                
                if (orderBy != null)
                {
                    return orderBy(query).ToList();
                }
                else
                {
                    return query.ToList();
                }
            });
        }

        public virtual Task<TEntity> GetOne(object id)
        {
            return Task.Run(() =>
            {
                var entity = dbSet.FindAsync(id).Result;
                var entityEntry = context.Entry(entity);
                entityEntry.State = EntityState.Detached;
                return entityEntry.Entity;
            });
        }

        public virtual ValueTask<EntityEntry<TEntity>> Insert(TEntity entity)
        {
            return dbSet.AddAsync(entity);
        }

        public virtual Task<TEntity> Delete(TEntity entityToDelete)
        {
            return Task.Run(() =>
            {
                if (context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    dbSet.Attach(entityToDelete);
                }

                var entityEntry = dbSet.Remove(entityToDelete);

                entityEntry.State = EntityState.Deleted;

                return entityEntry.Entity;
            });
        }

        public virtual Task<TEntity> Update(TEntity entityToUpdate)
        {
            return Task.Run(() =>
            {
                dbSet.Attach(entityToUpdate);

                var entryEntity = context.Entry(entityToUpdate);

                entryEntity.State = EntityState.Modified;

                return entryEntity.Entity;
            });
        }
    }
}