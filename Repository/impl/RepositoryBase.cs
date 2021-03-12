using System;
using System.Linq;
using System.Linq.Expressions;
using Entities.Data;

namespace Repository.impl
{
    public class RepositoryBase<T>: IRepositoryBase<T> where T:class
    {
        protected OrderDbContext _orderDbContext;

        public RepositoryBase(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public IQueryable<T> FindAll() =>
            _orderDbContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            _orderDbContext.Set<T>().Where(expression);

        public void Create(T entity) => _orderDbContext.Set<T>().Add(entity);
        public void Update(T entity) => _orderDbContext.Set<T>().Update(entity);
        public void Delete(T entity) => _orderDbContext.Set<T>().Remove(entity);
    }
    
    
    
   
}