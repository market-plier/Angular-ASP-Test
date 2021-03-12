using System;
using System.Linq;
using System.Linq.Expressions;

namespace Repository
{
    public interface IRepositoryBase<T> where T:class
    {

        public IQueryable<T> FindAll();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        public void Create(T entity);
        public void Update(T entity) ;
        public void Delete(T entity) ;

    }
}