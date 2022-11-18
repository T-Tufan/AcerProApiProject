using DataAccess.Contexts;
using DataAccess.IRepositories;
using Entities.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly Context _context;

        public Repository(Context context)
        {
            _context = context;
        }
        public async Task Add(T entity)
        {
           await _context.Set<T>().AddAsync(entity);
        }

        public void Delete(object id)
        {
            var deletedEntity = _context.Set<T>().Find(int.Parse(id.ToString()));
            _context.Set<T>().Remove(deletedEntity);
        }

        public async Task<List<T>> GetAll()
        {
            var a =  await _context.Set<T>().AsNoTracking().ToListAsync();
            return a;
        }

        public async Task<T> GetByFilter(System.Linq.Expressions.Expression<Func<T, bool>> filter, bool asNoTracking = false)
        {
            return asNoTracking ? await _context.Set<T>().SingleOrDefaultAsync(filter) : await _context.Set<T>().AsNoTracking().SingleOrDefaultAsync(filter);
        }

        public async Task<T> GetById(object id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public IQueryable<T> GetQuery()
        {
            return _context.Set<T>().AsQueryable();
        }

        public void Update(T entity)
        {
            var updated = _context.Set<T>().Find(entity.Id);
            _context.Entry(updated).CurrentValues.SetValues(entity);
            //_context.Set<T>().Update(entity); tüm colonları değiştirir.
        }
    }
}
