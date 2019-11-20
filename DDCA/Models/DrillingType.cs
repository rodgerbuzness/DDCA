using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDCA.Models
{
    public class DrillingType
    {
        public virtual int Id { get; set; }
        public virtual BoreDrillMethod BoreDrillMethod { get; set; }
        public virtual DrillMethod DrillMethod { get; set; }
        public virtual string DrillDepth { get; set; }

        public DrillingType()
        {
            BoreDrillMethod = new BoreDrillMethod();
            DrillMethod = new DrillMethod();
        }
    }

    public class DrillingTypeMap: ClassMapping<DrillingType>
    {
        public DrillingTypeMap()
        {
            Table("drillingtype");
            Id(x => x.Id, x => x.Generator(Generators.Identity));

            ManyToOne(x => x.BoreDrillMethod, x =>
            {
                x.Column("boredrmthd_id");
                x.NotNullable(true);
            });

            ManyToOne(x => x.DrillMethod, x =>
            {
                x.Column("drillmethod_id");
                x.NotNullable(true);
            });

            Property(x => x.DrillDepth, x => x.NotNullable(true));
        }
    }
}