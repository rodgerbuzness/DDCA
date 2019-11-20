using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDCA.Models
{
    public class Physical
    {
        public virtual int Id { get; set; }
        public virtual LabAnalysis LabAnalysis { get; set; }
        public virtual string Colour { get; set; }
        public virtual string Turbidity { get; set; }
        public virtual string Odour { get; set; }
        public virtual string SettleableMatter { get; set; }
        public virtual string PH { get; set; }
        public virtual string Taste { get; set; }
        public virtual string Conductivity { get; set; }
        public virtual string FiltrateResidue { get; set; }
        public virtual string NonFiltrateResidue { get; set; }
        public virtual string VolatileFixedResidue { get; set; }
    }

    public class PhysicalMap: ClassMapping<Physical>
    {
        public PhysicalMap()
        {
            Table("physical");
            Id(x => x.Id, x => x.Generator(Generators.Identity));

            ManyToOne(x => x.LabAnalysis, x =>
            {
                x.Column("labanalysis_id");
                x.NotNullable(true);
            });
            Property(x => x.Colour, x => x.NotNullable(true));
            Property(x => x.Turbidity, x => x.NotNullable(true));
            Property(x => x.Odour, x => x.NotNullable(true));
            Property(x => x.SettleableMatter, x => x.NotNullable(true));
            Property(x => x.PH, x => x.NotNullable(true));
            Property(x => x.Taste, x => x.NotNullable(true));
            Property(x => x.Conductivity, x => x.NotNullable(true));
            Property(x => x.FiltrateResidue, x => x.NotNullable(true));
            Property(x => x.NonFiltrateResidue, x => x.NotNullable(true));
            Property(x => x.VolatileFixedResidue, x => x.NotNullable(true));
        }
    }
}