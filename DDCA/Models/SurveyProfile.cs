using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDCA.Models
{
    public class SurveyProfile
    {
        public virtual int Id { get; set; }
        public virtual GeoSurvey GeoSurvey { get; set; }
        public virtual int VesPoint { get; set; }
        public virtual string SurveyMethod { get; set; }
        public virtual string Resistivity { get; set; }
        public virtual int Easting { get; set; }
        public virtual int Northing { get; set; }
        public virtual string Elevation { get; set; }
        public virtual string Recommend { get; set; }
    }

    public class SurveyProfileMap : ClassMapping<SurveyProfile>
    {
        public SurveyProfileMap()
        {
            Table("surveyprofile");
            Id(x => x.Id, x => x.Generator(Generators.Identity));
            Property(x => x.SurveyMethod, x =>
            {
                x.Column("survey_method");
                x.NotNullable(true);
            });
            Property(x => x.VesPoint, x => 
            {
                x.Column("ves_point");
                x.NotNullable(true);

                });
            Property(x => x.Resistivity, x => x.NotNullable(true));
            Property(x => x.Easting, x => x.NotNullable(true));
            Property(x => x.Northing, x => x.NotNullable(true));
            Property(x => x.Elevation, x => x.NotNullable(true));
            Property(x => x.Recommend, x => x.NotNullable(true));

            ManyToOne(x => x.GeoSurvey, x =>
            {
                x.Column("site_id");
                x.NotNullable(true);
            });
        }
    }
}