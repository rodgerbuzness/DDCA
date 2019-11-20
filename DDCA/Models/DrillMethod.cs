using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDCA.Models
{
    public class DrillMethod
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }

    public class DrillMethodMapp: ClassMapping<DrillMethod>
    {
        public DrillMethodMapp()
        {
            Table("drillmethod");
            Id(x => x.Id, x => x.Generator(Generators.Identity));

            Property(x => x.Name, x => x.NotNullable(true));
        }
    }
}