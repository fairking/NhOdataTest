using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;
using NhOdataTest.Entities;

namespace NhOdataTest.Mappings
{
    public class DewPointMapping : ClassMapping<DewPoint>
    {
        public DewPointMapping()
        {
            Id(x => x.Id, x =>
            {
                x.Generator(Generators.GuidComb);
                x.Type(NHibernateUtil.Guid);
            });
            Property(x => x.Time, x => { x.Type(NHibernateUtil.UtcDateTimeNoMs); });
            Property(x => x.Type, x => { x.Type<EnumStringType<DewPointTypeEnum>>(); });
            ManyToOne(x => x.Forecast, x => { x.Column("forecast_id"); x.NotNullable(true); x.ForeignKey("fk_forecast_id"); });
        }
    }
}
