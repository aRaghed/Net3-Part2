using Microsoft.EntityFrameworkCore.Infrastructure;

using System.Threading;
using System.Threading.Tasks;

namespace WeatherDb.Abstract
{
    public interface IUnitOfWork
    {
        //This interface should be implemented by your DbContext
        //It doesn't expose any DbSet or any other internals of the DbContext
        //This way the DbContext will act like a plain UnitOfWork with IGenericRepositories exposed
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;

        int SaveChanges();

        int SaveChanges(bool acceptAllChangesOnSuccess);

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        DatabaseFacade DatabaseAccessor { get; }
    }
}