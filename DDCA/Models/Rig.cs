using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDCA.Models
{
    public class Rig
    {
        public virtual int Id { get; set; }
        public virtual string RigNo { get; set; }
        public virtual string RigType { get; set; }
        public virtual string Model { get; set; }
        public virtual string RigState { get; set; }
        public virtual Region Region { get; set; }
        public virtual District District { get; set; }

        public Rig()
        {
            Region = new Region();
            District = new District();
        }
    }

    public class RigMap: ClassMapping<Rig>
    {
        public RigMap()
        {
            Table("rigs");
            Id(x => x.Id, x => x.Generator(Generators.Identity));

            Property(x => x.RigNo, x => x.NotNullable(true));
            Property(x => x.RigType, x => x.NotNullable(true));
            Property(x => x.Model, x => x.NotNullable(true));
            Property(x => x.RigState, x => x.NotNullable(true));

            ManyToOne(x => x.Region, x =>
            {
                x.Column("region_id");
                x.NotNullable(true);
            });
            ManyToOne(x => x.District, x =>
            {
                x.Column("district_id");
                x.NotNullable(true);
            });
        }
    }
}