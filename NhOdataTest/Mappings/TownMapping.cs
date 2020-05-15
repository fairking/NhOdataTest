using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NhOdataTest.Entities;

namespace NhOdataTest.Mappings
{
    public class TownMapping : ClassMapping<Town>
    {
        public TownMapping()
        {
            Id(x => x.Id, x =>
            {
                x.Generator(Generators.GuidComb);
                x.Type(NHibernateUtil.Guid);
            });
            Property(x => x.Name, x => { x.Type(NHibernateUtil.String); x.Length(150); });
        }
    }
}
