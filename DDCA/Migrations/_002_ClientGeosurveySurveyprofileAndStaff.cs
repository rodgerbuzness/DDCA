using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDCA.Migrations
{
    [Migration(2)]
    public class _002_ClientGeosurveySurveyprofileAndStaff : Migration
    {
        public override void Down()
        {
            Delete.Table("surveyprofile");
            Delete.Table("geosurvey");
            Delete.Table("staffs");
            Delete.Table("clients");
            Delete.Table("districts");
            Delete.Table("regions");
        }

        public override void Up()
        {
            Create.Table("regions")
               .WithColumn("id").AsInt32().PrimaryKey().Identity()
               .WithColumn("name").AsString(128)
               .WithColumn("country").AsString(128).Nullable();

            Create.Table("districts")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("region_id").AsInt32().ForeignKey("regions", "id")
                .WithColumn("name").AsString(128)
                .WithColumn("country").AsString(128).Nullable();


            Create.Table("clients")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("name").AsString(128)
                .WithColumn("address").AsString(128)
                .WithColumn("region_id").AsInt32().ForeignKey("regions", "id")
                .WithColumn("phone").AsString(128)
                .WithColumn("email").AsString(128)
                .WithColumn("added_date").AsDateTime().NotNullable();

            Create.Table("staffs")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("name").AsString(128);

            Create.Table("geosurvey")
                .WithColumn("site_id").AsInt32().PrimaryKey().Identity()
                .WithColumn("client_id").AsInt32().ForeignKey("clients", "id")
                .WithColumn("staff_id").AsInt32().ForeignKey("staffs", "id")
                .WithColumn("survey_type").AsString(128)
                .WithColumn("region_id").AsInt32().ForeignKey("regions", "id")
                .WithColumn("district_id").AsInt32().ForeignKey("districts", "id")
                .WithColumn("village").AsString(128)
                .WithColumn("start_date").AsDateTime().Nullable()
                .WithColumn("end_date").AsDateTime().Nullable()
                .WithColumn("cost").AsInt32()
                .WithColumn("site_recommendation").AsString(256);

            Create.Table("surveyprofile")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("site_id").AsInt32().ForeignKey("geosurvey", "site_id")
                .WithColumn("ves_point").AsInt32()
                .WithColumn("survey_method").AsString(128)
                .WithColumn("resistivity").AsString(128)
                .WithColumn("easting").AsInt32()
                .WithColumn("northing").AsInt32()
                .WithColumn("elevation").AsString(128)
                .WithColumn("recommend").AsString(256);

        }
    }
}