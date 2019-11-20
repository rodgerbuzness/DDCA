using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDCA.Models
{
    public class Region
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Country { get; set; }
    }

    public class RegionMap : ClassMapping<Region>
    {
        public RegionMap()
        {
            Table("regions");

            Id(x => x.Id, x => x.Generator(Generators.Identity));
            Property(x => x.Name, x => x.NotNullable(true));
            Property(x => x.Country, x => x.NotNullable(false));
        }
    }
}