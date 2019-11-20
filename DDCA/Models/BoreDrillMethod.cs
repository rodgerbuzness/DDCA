using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDCA.Models
{
    public class BoreDrillMethod
    {
        public virtual int Id { get; set; }
        public virtual Borehole Borehole { get; set; }
        public virtual Rig Rig { get; set; }
        public virtual GeoSurvey GeoSurvey { get; set; }

        public BoreDrillMethod()
        {
            Borehole = new Borehole();
            Rig = new Rig();
            GeoSurvey = new GeoSurvey();
        }
    }

    public class BoreDrillMethodMap: ClassMapping<BoreDrillMethod>
    {
        public BoreDrillMethodMap()
        {
            Table("boredrillmethod");
            Id(x => x.Id, x => x.Generator(Generators.Identity));

            ManyToOne(x => x.Borehole, x =>
            {
                x.Column("borehole_id");
                x.NotNullable(true);
            });

            ManyToOne(x => x.Rig, x =>
            {
                x.Column("rig_id");
                x.NotNullable(true);
            });

            ManyToOne(x => x.GeoSurvey, x =>
            {
                x.Column("site_id");
                x.NotNullable(true);
            });
        }
    }
}