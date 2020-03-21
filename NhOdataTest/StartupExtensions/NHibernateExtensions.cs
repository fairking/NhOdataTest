using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using System.Reflection;

namespace NhOdataTest.StartupExtensions
{
    public static class NHibernateExtensions
    {
        public static IServiceCollection AddNHibernate(this IServiceCollection services, string connectionString)
        {
            var cfg = new Configuration().DataBaseIntegration(db =>
            {
                db.ConnectionString = connectionString;
                db.Dialect<NHibernate.Dialect.SQLiteDialect>();
            });
            cfg.SetProperty("show_sql", "true");
            cfg.SetNamingStrategy(ImprovedNamingStrategy.Instance);

            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());
            cfg.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());

            services.AddSingleton(cfg);
            services.AddSingleton(s => s.GetRequiredService<Configuration>().BuildSessionFactory());
            services.AddScoped(s => s.GetRequiredService<ISessionFactory>().OpenSession());

            return services;
        }
    }
}
