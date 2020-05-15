using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NhOdataTest.Entities;

namespace NhOdataTest.Mappings
{
    public class WeatherForecastMapping : ClassMapping<WeatherForecast>
    {
        public WeatherForecastMapping()
        {
            Id(x => x.Id, x =>
            {
                x.Generator(Generators.GuidComb);
                x.Type(NHibernateUtil.Guid);
            });
            Property(x => x.Date, x => x.Type(NHibernateUtil.DateTime));
            Property(x => x.TemperatureC, x => x.Type(NHibernateUtil.Int32));
            Property(x => x.Summary, x => { x.Type(NHibernateUtil.String); x.Length(150); });
            ManyToOne(x => x.Town, x => { x.Column("town_id"); x.NotNullable(true); x.ForeignKey("fk_town_id"); });
        }
    }
}
