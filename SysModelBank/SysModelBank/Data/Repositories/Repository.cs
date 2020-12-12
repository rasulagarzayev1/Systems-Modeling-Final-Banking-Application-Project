using Microsoft.EntityFrameworkCore;
using SysModelBank.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysModelBank.Data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        protected readonly SysModelBankDbContext DbContext;

        public Repository(SysModelBankDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected IQueryable<TEntity> Query() => DbContext.Set<TEntity>();

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await Query().ToListAsync();
        }

        public Task<TEntity> GetAsync(int id)
        {
            return Query().SingleOrDefaultAsync(x => id == x.Id);
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            DbContext.Add(entity);

            try
            {
                await DbContext.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                if (exception.InnerException != null)
                {
                    throw exception.InnerException;
                }

                throw;
            }

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            DbContext.Update(entity);

            try
            {
                await DbContext.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                if (exception.InnerException != null)
                {
                    throw exception.InnerException;
                }

                throw;
            }

            return entity;
        }

        public async Task<bool> RemoveAsync(TEntity entity)
        {
            DbContext.Remove(entity);

            try
            {
                await DbContext.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                if (exception.InnerException != null)
                {
                    throw exception.InnerException;
                }

                return false;
            }

            return true;
        }
    }
}
