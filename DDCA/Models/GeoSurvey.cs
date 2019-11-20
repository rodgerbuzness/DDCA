using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DDCA.Models
{
    public class GeoSurvey
    {
        public virtual int Id { get; set; }
        public virtual Client Client { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual string SurveyType { get; set; }
        public virtual Region Region { get; set; }
        public virtual District District { get; set; }
        public virtual string Village { get; set; }
        public virtual DateTime? StartDate { get; set; }
        public virtual DateTime? EndDate { get; set; }
        public virtual int Cost { get; set; }
        public virtual string Status { get; set; }
        public virtual string SiteRecommendation { get; set; }

        public GeoSurvey()
        {
            Staff = new Staff();
            Region = new Region();
            District = new District();
        }
    }


    public class GeosurveyMap : ClassMapping<GeoSurvey>
    {
        public GeosurveyMap()
        {
            Table("geosurvey");
            Id(x => x.Id, x =>
            {
                x.Generator(Generators.Identity);
                x.Column("site_id");
            });

            ManyToOne(x => x.Client, map =>
            {
                map.Column("client_id");
                map.NotNullable(true);
            });

            ManyToOne(x => x.Region, map =>
            {
                map.Column("region_id");
                map.NotNullable(true);
            });

            ManyToOne(x => x.District, map =>
            {
                map.Column("district_id");
                map.NotNullable(true);
            });


            ManyToOne(x => x.Staff, x =>
            {
                x.Column("staff_id");
                x.NotNullable(true);
            });

            Property(x => x.SurveyType, x =>
            {
                x.Column("survey_type");
                x.NotNullable(true);
            });

            
            //Property(x => x.Region, x => x.NotNullable(true));
            //Property(x => x.District, x => x.NotNullable(true));
            Property(x => x.Village, x => x.NotNullable(true));
            Property(x => x.StartDate, x => x.Column("start_date"));
            Property(x => x.EndDate, x => x.Column("end_date"));
            Property(x => x.Cost, x => x.NotNullable(false));
            Property(x => x.Status, x => x.NotNullable(false));
            Property(x => x.SiteRecommendation, x => x.Column("site_recommendation"));

        }
    }
}