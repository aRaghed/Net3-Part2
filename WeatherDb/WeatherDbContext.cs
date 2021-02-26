using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using WeatherDb.Abstract;
using WeatherDb.Model;

namespace WeatherDb
{
    public class WeatherDbContext : DbContext, IWeatherDbContext
    {
        public DbSet<WeatherForecast> Forecasts { get; set; }

        //You could either inject a LoggerFactory (will use Serilog and everything else that is configured by default)
        private readonly ILoggerFactory loggerFactory;

        private ILogger<WeatherDbContext> logger;

        //Or you could create your own LoggerFactory
        //public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>
        //    builder
        //        .AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information)
        //        .AddConsole());

        public WeatherDbContext(DbContextOptions<WeatherDbContext> options, ILoggerFactory loggerFactory)
         : base(options) => this.loggerFactory = loggerFactory;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLoggerFactory(MyLoggerFactory);
            optionsBuilder.UseLoggerFactory(loggerFactory);
            logger = loggerFactory.CreateLogger<WeatherDbContext>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<WeatherForecast>().Property("Summary").HasMaxLength(200);

            //Add any specific database design (Fluent API) here or add them in the model using attributes
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class, IPOCOClass
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

        public Task EnsureDbExists(bool seed = false)
        {
            var migrations = Database.GetPendingMigrations();

            if (migrations.Any())
            {
                logger.LogInformation("Applying {migrations} migrations to database", migrations.Count());
                Database.Migrate();
            }

            if (seed)
            {
                SeedSampleData();
            }

            return Task.CompletedTask;
        }

        public Task SeedSampleData()
        {
            logger.LogInformation("Seeding sample data");
            if (File.Exists("sampledata.json"))
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    NumberHandling = JsonNumberHandling.AllowReadingFromString
                };
                IEnumerable<WeatherForecast> forecasts = JsonSerializer.Deserialize<IEnumerable<WeatherForecast>>(File.ReadAllText("sampledata.json"), options);
                foreach (var forecast in forecasts)
                {
                    Forecasts.Add(forecast);
                    SaveChanges();
                }
            }
            return Task.CompletedTask;
        }
    }
}