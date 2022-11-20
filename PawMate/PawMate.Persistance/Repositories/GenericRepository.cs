using PawMate.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Linq.Expressions;

namespace Persistence.Respositories
{
    public class GenericRepository<TEntity>: IGenericRepository<TEntity> where TEntity : class
    {
        protected ApplicationContext _applicationContext { get; set; }

        public GenericRepository(ApplicationContext context)
        {
            _applicationContext = context;
        }
        public IQueryable<TEntity> FindAll()
        {
            return _applicationContext.Set<TEntity>();
        }

        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression)
        {
            return _applicationContext.Set<TEntity>().Where(expression);
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            _applicationContext.Set<TEntity>().Add(entity);
            await _applicationContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {

            _applicationContext.Set<TEntity>().Update(entity);
            await _applicationContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            _applicationContext.Set<TEntity>().Remove(entity);
            await _applicationContext.SaveChangesAsync();
            return entity;
        }
    }
}
