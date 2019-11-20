using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDCA.Models
{
    public class Machine
    {
        public virtual int Id { get; set; }
        public virtual int RigId { get; set; } 
        public virtual int CompressorId { get; set; }
        public virtual int CarId { get; set; }
        public virtual string Name { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual string Status { get; set; }
        public virtual DateTime BoughtDate { get; set; }
        public virtual string Remarks { get; set; }

        public Machine()
        {
            Staff = new Staff();
         
        }

    }

    public class MachineMap: ClassMapping<Machine>
    {
        public MachineMap()
        {
            Table("machine");
            Id(x => x.Id, x => x.Generator(Generators.Identity));


            ManyToOne(x => x.Staff, x =>
            {
                x.Column("staff_id");
                x.NotNullable(true);
            });

            Property(x => x.Name, x => x.NotNullable(true));
            Property(x => x.Status, x => x.NotNullable(false));
            Property(x => x.BoughtDate, x => x.NotNullable(true));
            Property(x => x.Remarks, x => x.NotNullable(true));
            Property(x => x.CarId, x => x.NotNullable(false));
            Property(x => x.CompressorId, x => x.NotNullable(false));
            Property(x => x.RigId, x => x.NotNullable(false));
        }
    }
}