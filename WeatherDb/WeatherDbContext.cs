using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;

using WeatherDb.Abstract;
using WeatherDb.Model;

namespace WeatherDb
{
    public class WeatherDbContext : DbContext, IUnitOfWork
    {
        public DbSet<WeatherForecast> Forecasts { get; set; }

        //You could either inject a LoggerFactory (will use Serilog and everything else that is configured by default)
        private readonly ILoggerFactory loggerFactory;

        //Or you could create your own LoggerFactory
        //public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>
        //    builder
        //        .AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information)
        //        .AddConsole());

        public DatabaseFacade DatabaseAccessor => Database;

        public WeatherDbContext(DbContextOptions<WeatherDbContext> options/*, ILoggerFactory loggerFactory*/)
         : base(options) { }//=> this.loggerFactory = loggerFactory;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLoggerFactory(MyLoggerFactory);
            //optionsBuilder.UseLoggerFactory(loggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Add any specific database design (Fluent API) here or add them in the model using attributes
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (repositories.Keys.Contains(typeof(TEntity)) == true)
            {
                return repositories[typeof(TEntity)] as IGenericRepository<TEntity>;
            }

            IGenericRepository<TEntity> repo = new GenericRepository<TEntity>(this);

            repositories.Add(typeof(TEntity), repo);

            return repo;
        }

        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();
    }
}