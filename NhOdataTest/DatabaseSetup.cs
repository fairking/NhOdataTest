using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NhOdataTest.Entities;
using System;
using System.Linq;

namespace NhOdataTest
{
    public static class DatabaseSetup
    {
        public static IHost EnsureDatabaseCreated(this IHost host)
        {
            Exception error = null;

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var cfg = services.GetRequiredService<Configuration>();
                var session = services.GetRequiredService<ISession>();
                var logger = services.GetRequiredService<ILogger<Program>>();

                logger.LogInformation($"EnsureDatabaseCreated ASPNETCORE_ENVIRONMENT: {System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}");

                // Ensure Created
                try
                {
                    new SchemaExport(cfg).Execute(false, true, false);
                }
                catch (Exception ex)
                {
                    logger.LogCritical(ex, "Error whilst creating database");
                    error = new Exception("Error whilst creating database", ex);
                }

                // Seed database
                try
                {
                    if (!session.Query<WeatherForecast>().Any())
                    {
                        using (var tran = session.BeginTransaction())
                        {
                            try
                            {
                                var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

                                var rng = new Random();

                                var entities = Enumerable.Range(1, 7).Select(index => new WeatherForecast
                                {
                                    Date = DateTime.Now.AddDays(index),
                                    TemperatureC = rng.Next(-20, 55),
                                    Summary = summaries[rng.Next(summaries.Length)]
                                }).ToArray();

                                foreach (var entity in entities)
                                {
                                    session.SaveOrUpdate(entity);
                                }

                                tran.Commit();
                            }
                            catch
                            {
                                tran.Rollback();
                                throw;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.LogCritical(ex, "Error whilst seeding database");
                    error = new Exception("Error whilst seeding database", ex);
                }
            }

            if (error != null)
                throw error;

            return host;
        }
    }
}
