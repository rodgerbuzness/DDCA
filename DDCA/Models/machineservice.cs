using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDCA.Models
{
    public class MachineService
    {
        public virtual int Id { get; set; }
        public virtual Machine Machine { get; set; }
        public virtual string JobDone { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual string RegNo { get; set; }
        public virtual string MaterialCost { get; set; }
        public virtual string LabourCost { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual string Type { get; set; }

        public MachineService()
        {
            Machine = new Machine();
            Staff = new Staff();
        }

    }

    public class MachineServiceMap : ClassMapping<MachineService>
    {
        public MachineServiceMap()
        {
            Table("machineservice");
            Id(x => x.Id, x => x.Generator(Generators.Identity));

            ManyToOne(x => x.Machine, x =>
            {
                x.Column("machine_id");
                x.NotNullable(true);
            });

            ManyToOne(x => x.Staff, x =>
            {
                x.Column("staff_id");
                x.NotNullable(true);
            });

            Property(x => x.JobDone, x => x.NotNullable(true));
            Property(x => x.MaterialCost, x => x.NotNullable(true));
            Property(x => x.LabourCost, x => x.NotNullable(true));
            Property(x => x.StartDate, x => x.NotNullable(true));
            Property(x => x.EndDate, x => x.NotNullable(true));
            Property(x => x.Type, x => x.NotNullable(true));
            Property(x => x.RegNo, x => x.NotNullable(true));
        }
    }
}