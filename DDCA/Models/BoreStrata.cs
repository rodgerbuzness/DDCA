using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDCA.Models
{
    public class BoreStrata
    {
        public virtual int Id { get; set; }
        public virtual Borehole Borehole { get; set; }
        public virtual string StrataName { get; set; }
        public virtual string RangeFrom { get; set; }
        public virtual string RangeTo { get; set; }

        public BoreStrata()
        {
            Borehole = new Borehole();
        }
    }

    public class BoreStrataMap: ClassMapping<BoreStrata>
    {
        public BoreStrataMap()
        {
            Table("borestrata");
            Id(x => x.Id, x => x.Generator(Generators.Identity));

            ManyToOne(x => x.Borehole, x =>
            {
                x.Column("borehole_id");
                x.NotNullable(true);
            });
            Property(x => x.StrataName, x => x.NotNullable(true));
            Property(x => x.RangeFrom, x => x.NotNullable(true));
            Property(x => x.RangeTo, x => x.NotNullable(true));
        }
    }
}