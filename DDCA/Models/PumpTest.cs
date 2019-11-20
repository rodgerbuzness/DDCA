using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDCA.Models
{
    public class PumpTest
    {
        public virtual int Id { get; set; }
        public virtual Borehole Borehole { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual string YieldRate { get; set; }
        public virtual string DrwDownDepth { get; set; }
        public virtual string StaticWaterLvlDepth { get; set; }
        public virtual string AirDiameter { get; set; }
        public virtual string AirDepth { get; set; }
        public virtual string CylinderDiameter { get; set; }
        public virtual string CylinderDepth { get; set; }
        public virtual string SubmissiblePumpSize { get; set; }
        public virtual string SubmissibleHeight { get; set; }
        public virtual string Results { get; set; }

        public PumpTest()
        {
            Staff = new Staff();
        }
    }

    public class PumpTestMap: ClassMapping<PumpTest>
    {
        public PumpTestMap()
        {
            Table("pumptest");
            Id(x => x.Id, x => x.Generator(Generators.Identity));

            ManyToOne(x => x.Borehole, x =>
            {
                x.Column("borehole_id");
                x.NotNullable(true);
            });
            ManyToOne(x => x.Staff, x =>
            {
                x.Column("staff_id");
                x.NotNullable(true);
            });
            Property(x => x.YieldRate, x => x.NotNullable(true));
            Property(x => x.DrwDownDepth, x => x.NotNullable(true));
            Property(x => x.StaticWaterLvlDepth, x => x.NotNullable(true));
            Property(x => x.AirDiameter, x => x.NotNullable(true));
            Property(x => x.AirDepth, x => x.NotNullable(true));
            Property(x => x.CylinderDiameter, x => x.NotNullable(true));
            Property(x => x.CylinderDepth, x => x.NotNullable(true));
            Property(x => x.SubmissiblePumpSize, x => x.NotNullable(true));
            Property(x => x.SubmissibleHeight, x => x.NotNullable(true));
            Property(x => x.Results, x => x.NotNullable(true));
        }
    }
}