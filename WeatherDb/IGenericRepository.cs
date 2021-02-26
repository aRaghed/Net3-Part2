using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using WeatherDb.Model;

namespace WeatherDb.Abstract
{
    public interface IGenericRepository<TEntity> where TEntity : class, IPOCOClass
    {
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
                                     Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                     string includeProperties = "");

        TEntity GetById(object id);

        void Insert(TEntity entity);

        Task InsertAsync(TEntity entity);

        void Update(TEntity entity);

        void DeleteById(object id);

        void Delete(TEntity entityToDelete);
    }
}