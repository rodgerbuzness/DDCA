using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDCA.Models
{
    public class LabAnalysis
    {
        public virtual int Id { get; set; }
        public virtual PumpTest PumpTest { get; set; }
        public virtual string LabName { get; set; }
        public virtual DateTime CollectedDate { get; set; }
        public virtual DateTime AnalysisDate { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string Recommend { get; set; }
    }

    public class LabAnalysisMap: ClassMapping<LabAnalysis>
    {
        public LabAnalysisMap()
        {
            Table("labanalysis");
            Id(x => x.Id, x => x.Generator(Generators.Identity));

            ManyToOne(x => x.PumpTest, x =>
            {
                x.Column("pumptest_id");
                x.NotNullable(true);
            });
            Property(x => x.LabName, x => x.NotNullable(true));
            Property(x => x.CollectedDate, x => x.NotNullable(true));
            Property(x => x.AnalysisDate, x => x.NotNullable(true));
            Property(x => x.Remarks, x => x.NotNullable(true));
            Property(x => x.Recommend, x => x.NotNullable(true));
        }
    }
}