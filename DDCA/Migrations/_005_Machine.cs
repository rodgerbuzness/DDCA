using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DDCA.Migrations
{
    [Migration(5)]
    public class _005_Machine : Migration
    {
        public override void Down()
        {
            Delete.Table("machineservice");
            Delete.Table("machine");
            Delete.Table("car");
            Delete.Table("compressor");
        }

        public override void Up()
        {
            Create.Table("car")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("carno").AsString(128)
                .WithColumn("model").AsString(128)
                .WithColumn("engine").AsString(128)
                .WithColumn("chasis").AsString(128);

            Create.Table("compressor")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("compressorno").AsString(128)
                .WithColumn("compressortype").AsString(128)
                .WithColumn("model").AsString(128)
                .WithColumn("region_id").AsInt32().ForeignKey("regions", "id").OnDelete(Rule.Cascade)
                .WithColumn("district_id").AsInt32().ForeignKey("districts", "id").OnDelete(Rule.Cascade);

            Create.Table("machine")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("rigid").AsInt32().Nullable()
                .WithColumn("compressorid").AsInt32().Nullable()
                .WithColumn("carid").AsInt32().Nullable()
                .WithColumn("staff_id").AsInt32().ForeignKey("staffs", "id").OnDelete(Rule.Cascade)
                .WithColumn("name").AsString(128)
                .WithColumn("status").AsString(128)
                .WithColumn("boughtdate").AsDateTime()
                .WithColumn("remarks").AsString(128);

            Create.Table("machineservice")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("machine_id").AsInt32().ForeignKey("machine", "id").OnDelete(Rule.Cascade)
                .WithColumn("jobdone").AsString(128)
                .WithColumn("staff_id").AsInt32().ForeignKey("staffs", "id").OnDelete(Rule.None)
                .WithColumn("materialcost").AsString(128)
                .WithColumn("type").AsString(128)
                 .WithColumn("regno").AsString(128)
                .WithColumn("labourcost").AsString(128)
                .WithColumn("startdate").AsDateTime()
                .WithColumn("enddate").AsDateTime();
                




        }
    }
}