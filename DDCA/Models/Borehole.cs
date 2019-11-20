using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDCA.Models
{
    public class Borehole
    {
        public virtual int Id { get; set; } 
        public virtual string BoreholeNo { get; set; }
        public virtual string CasingDiameter { get; set; }
        public virtual string CasingHeight { get; set; }
        public virtual string CasingType { get; set; }
        public virtual string ScreenDiameter { get; set; }
        public virtual string ScreenHeight { get; set; }
        public virtual string ScreenType { get; set; }
        public virtual string CasingTopper { get; set; }
        public virtual string CasingBottom { get; set; }
        public virtual string UncasedDepth { get; set; }
        public virtual string BackFillHeight { get; set; }
        public virtual string BackFillAvgSize { get; set; }
        public virtual string BackFillMethod2 { get; set; }
        public virtual string BackFillMaterial { get; set; }
        public virtual string GravelType { get; set; }
        public virtual string GravelAvgSize { get; set; }
        public virtual string GravelFrom { get; set; }
        public virtual string GravelTo { get; set; }
        public virtual string AquiferDepth { get; set; }
        public virtual string Formation { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; } 
        public virtual string FinishDepth { get; set; }
        public virtual string Diameter { get; set; }
        public virtual string Eastings { get; set; }
        public virtual string Northings { get; set; }

    }

    public class BoreholeMap: ClassMapping<Borehole>
    {
        public BoreholeMap()
        {
            Table("borehole");
            Id(x => x.Id, x => x.Generator(Generators.Identity));

            Property(x => x.BoreholeNo, x => x.NotNullable(true));
            Property(x => x.CasingDiameter, x => x.NotNullable(true));
            Property(x => x.CasingHeight, x => x.NotNullable(true));
            Property(x => x.CasingType, x => x.NotNullable(true));
            Property(x => x.ScreenDiameter, x => x.NotNullable(true));
            Property(x => x.ScreenHeight, x => x.NotNullable(true));
            Property(x => x.ScreenType, x => x.NotNullable(true));
            Property(x => x.CasingTopper, x => x.NotNullable(true));
            Property(x => x.CasingBottom, x => x.NotNullable(true));
            Property(x => x.UncasedDepth, x => x.NotNullable(true));
            Property(x => x.BackFillHeight, x => x.NotNullable(true));
            Property(x => x.BackFillMaterial, x => x.NotNullable(true));
            Property(x => x.BackFillAvgSize, x => x.NotNullable(true));
            Property(x => x.BackFillMethod2, x => x.NotNullable(true));
            Property(x => x.GravelType, x => x.NotNullable(true));
            Property(x => x.GravelAvgSize, x => x.NotNullable(true));
            Property(x => x.GravelFrom, x => x.NotNullable(true));
            Property(x => x.GravelTo, x => x.NotNullable(true));
            Property(x => x.AquiferDepth, x => x.NotNullable(true));
            Property(x => x.Formation, x => x.NotNullable(true));
            Property(x => x.StartDate, x => x.NotNullable(true));
            Property(x => x.EndDate, x => x.NotNullable(true));
            Property(x => x.FinishDepth, x => x.NotNullable(true));
            Property(x => x.Diameter, x => x.NotNullable(true));
            Property(x => x.Eastings, x => x.NotNullable(true));
            Property(x => x.Northings, x => x.NotNullable(true));
        }
    }
}