using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IRepository<T> where T : BaseEntity,new()
    {
        Task<List<T>> GetAll();
        Task<T> GetById(object id);
        //Bu projede kullanılmayacak.
        Task<T> GetByFilter(Expression<Func<T,bool>> filter, bool asNoTracking = false);
        Task Add(T entity);
        void Delete(object id);
        void Update(T entity);

        IQueryable<T> GetQuery();
    }
}
