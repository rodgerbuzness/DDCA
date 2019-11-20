using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DDCA.Migrations
{
    [Migration(3)]
    public class _003_borehole : Migration
    {
        public override void Down()
        {
            Delete.Table("chemical");
            Delete.Table("physical");
            Delete.Table("labanalysis");
            Delete.Table("pumptest");
            Delete.Table("borestrata");
            Delete.Table("drillingtype");
            Delete.Table("boredrillmethod");
            Delete.Table("drillmethod");
            Delete.Table("borehole");
            Delete.Table("rigs");
        }

        public override void Up()
        {
            Create.Table("borehole")
                 .WithColumn("id").AsInt32().PrimaryKey().Identity()
                 .WithColumn("boreholeno").AsString(128)
                 .WithColumn("casingdiameter").AsString(128)
                 .WithColumn("casingheight").AsString(128)
                 .WithColumn("casingtype").AsString(128)
                 .WithColumn("screendiameter").AsString(128)
                 .WithColumn("screenheight").AsString(128)
                 .WithColumn("screentype").AsString(128)
                 .WithColumn("casingtopper").AsString(128)
                 .WithColumn("casingbottom").AsString(128)
                 .WithColumn("uncaseddepth").AsString(128)
                 .WithColumn("backfillheight").AsString(128)
                 .WithColumn("backfillmaterial").AsString(128)
                 .WithColumn("backfillavgsize").AsString(128)
                 .WithColumn("backfillmethod2").AsString(128)
                 .WithColumn("graveltype").AsString(128)
                 .WithColumn("gravelavgsize").AsString(128)
                 .WithColumn("gravelfrom").AsString(128)
                 .WithColumn("gravelto").AsString(128)
                 .WithColumn("aquiferdepth").AsString(128)
                 .WithColumn("formation").AsString(128)
                 .WithColumn("startdate").AsDateTime().NotNullable()
                 .WithColumn("enddate").AsDateTime().NotNullable()
                 .WithColumn("finishdepth").AsString(128)
                 .WithColumn("diameter").AsString(128)
                 .WithColumn("eastings").AsString(128)
                 .WithColumn("northings").AsString(128);

            Create.Table("rigs")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("rigno").AsString(128)
                .WithColumn("rigtype").AsString(128)
                .WithColumn("model").AsString(128)
                .WithColumn("rigstate").AsString(128)
                .WithColumn("region_id").AsInt32().ForeignKey("regions", "id").OnDelete(Rule.Cascade)
                .WithColumn("district_id").AsInt32().ForeignKey("districts", "id").OnDelete(Rule.Cascade);

            Create.Table("drillmethod")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("name").AsString(128);

            Create.Table("boredrillmethod")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("borehole_Id").AsInt32().ForeignKey("borehole", "id").OnDelete(Rule.Cascade)
                .WithColumn("rig_id").AsInt32().ForeignKey("rigs", "id").OnDelete(Rule.Cascade)
                .WithColumn("site_id").AsInt32().ForeignKey("geosurvey", "site_id").OnDelete(Rule.Cascade);

            Create.Table("drillingtype")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("boredrmthd_id").AsInt32().ForeignKey("boredrillmethod", "id").OnDelete(Rule.Cascade)
                .WithColumn("drillmethod_id").AsInt32().ForeignKey("drillmethod", "id").OnDelete(Rule.Cascade)
                .WithColumn("drilldepth").AsString(128);

            Create.Table("borestrata")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("borehole_id").AsInt32().ForeignKey("borehole", "id").OnDelete(Rule.Cascade)
                .WithColumn("strataname").AsString(128)
                .WithColumn("rangefrom").AsString(128)
                .WithColumn("rangeto").AsString(128);

            Create.Table("pumptest")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("borehole_id").AsInt32().ForeignKey("borehole", "id").OnDelete(Rule.Cascade)
                .WithColumn("staff_id").AsInt32().ForeignKey("staffs", "id").OnDelete(Rule.Cascade)
                .WithColumn("yieldrate").AsString(128)
                .WithColumn("drwdowndepth").AsString(128)
                .WithColumn("staticwaterlvldepth").AsString(128)
                .WithColumn("airdiameter").AsString(128)
                .WithColumn("airdepth").AsString(128)
                .WithColumn("cylinderdiameter").AsString(128)
                .WithColumn("cylinderdepth").AsString(128)
                .WithColumn("submissiblepumpsize").AsString(128)
                .WithColumn("submissibleheight").AsString(128)
                .WithColumn("results").AsString(128);

            Create.Table("labanalysis")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("pumptest_id").AsInt32().ForeignKey("pumptest", "id").OnDelete(Rule.Cascade)
                .WithColumn("labname").AsString(128)
                .WithColumn("collecteddate").AsDateTime().NotNullable()
                .WithColumn("analysisdate").AsDateTime().NotNullable()
                .WithColumn("remarks").AsString(128)
                .WithColumn("recommend").AsString(128);

            Create.Table("physical")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("labanalysis_id").AsInt32().ForeignKey("labanalysis", "id").OnDelete(Rule.Cascade)
                .WithColumn("colour").AsString(128)
                .WithColumn("turbidity").AsString(128)
                .WithColumn("odour").AsString(128)
                .WithColumn("settleablematter").AsString(128)
                .WithColumn("ph").AsString(128)
                .WithColumn("taste").AsString(128)
                .WithColumn("conductivity").AsString(128)
                .WithColumn("filtrateresidue").AsString(128)
                .WithColumn("nonfiltrateresidue").AsString(128)
                .WithColumn("volatilefixedresidue").AsString(128);

            Create.Table("chemical")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("labanalysis_id").AsInt32().ForeignKey("labanalysis", "id").OnDelete(Rule.Cascade)
                .WithColumn("alkalinity").AsString(128)
                .WithColumn("hardness").AsString(128)
                .WithColumn("calcium").AsString(128)
                .WithColumn("phenophthalein").AsString(128)
                .WithColumn("carbonate").AsString(128)
                .WithColumn("magnesium").AsString(128)
                .WithColumn("noncarbonate").AsString(128)
                .WithColumn("sodium").AsString(128)
                .WithColumn("potassium").AsString(128)
                .WithColumn("cadmium").AsString(128)
                .WithColumn("chromium").AsString(128)
                .WithColumn("copper").AsString(128)
                .WithColumn("iron").AsString(128)
                .WithColumn("lead").AsString(128)
                .WithColumn("manganese").AsString(128)
                .WithColumn("mercury").AsString(128)
                .WithColumn("zinc").AsString(128)
                .WithColumn("totalnitrogen").AsString(128)
                .WithColumn("nitritenitrogen").AsString(128)
                .WithColumn("nitratenitrogen").AsString(128)
                .WithColumn("ammonicalnitrogen").AsString(128)
                .WithColumn("organicnitrogen").AsString(128)
                .WithColumn("phosphorus").AsString(128)
                .WithColumn("orthophosphate").AsString(128)
                .WithColumn("sulphate").AsString(128)
                .WithColumn("chloride").AsString(128)
                .WithColumn("fluoride").AsString(128)
                .WithColumn("permanganate").AsString(128)
                .WithColumn("bod").AsString(128);

        }
    }
}