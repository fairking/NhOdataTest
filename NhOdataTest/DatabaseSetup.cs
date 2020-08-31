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

                                var town1 = new Town("London");
                                session.SaveOrUpdate(town1);
                                var town2 = new Town("New York");
                                session.SaveOrUpdate(town2);
                                var town3 = new Town("Sidney");
                                session.SaveOrUpdate(town3);
                                var allTowns = new[] { town1, town2, town3 };

                                var entities = Enumerable.Range(1, 25).Select(index => new WeatherForecast(allTowns[rng.Next(allTowns.Length)])
                                {
                                    Date = DateTime.Now.AddDays(index),
                                    TemperatureC = rng.Next(-20, 55),
                                    Summary = summaries[rng.Next(summaries.Length)]
                                }).ToArray();

                                foreach (var entity in entities)
                                {
                                    session.SaveOrUpdate(entity);

                                    entity.DewPoints.Add(new DewPoint(entity, entity.Date.AddHours(3).ToUniversalTime(), DewPointTypeEnum.Low));
                                    entity.DewPoints.Add(new DewPoint(entity, entity.Date.AddHours(6).ToUniversalTime(), DewPointTypeEnum.Normal));
                                    entity.DewPoints.Add(new DewPoint(entity, entity.Date.AddHours(9).ToUniversalTime(), DewPointTypeEnum.High));
                                    foreach (var point in entity.DewPoints)
                                        session.SaveOrUpdate(point);
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

            // Break database
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var session = services.GetRequiredService<ISession>();

                using (var tran = session.BeginTransaction())
                {
                    // Breaking one of the enums
                    session
                        .CreateSQLQuery("update dew_point set type = 'Normal2' where type = 'Normal'")
                        .ExecuteUpdate();

                    tran.Commit();
                }
            }

            return host;
        }
    }
}
