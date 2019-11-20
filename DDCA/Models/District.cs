using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDCA.Models
{
    public class District
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Country { get; set; }
        public virtual Region Region { get; set; }
    }

    public class DistrictMap : ClassMapping<District>
    {
        public DistrictMap()
        {
            Table("districts");

            Id(x => x.Id, x => x.Generator(Generators.Identity));
            Property(x => x.Name, x => x.NotNullable(true));
            Property(x => x.Name, x => x.NotNullable(false));

            ManyToOne(x => x.Region, x =>
            {
                x.Column("region_id");
                x.NotNullable(true);
            });

        }
    }
}