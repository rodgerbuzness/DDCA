using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDCA.Migrations
{
    [Migration(4)]
    public class _004_AddSurveyStatus : Migration
    {
        public override void Down()
        {
            Delete.Column("status").FromTable("geosurvey");
        }

        public override void Up()
        {
            Create.Column("status").OnTable("geosurvey").AsString(128).Nullable();
        }
    }
}