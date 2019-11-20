using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDCA.Models
{
    public class Car
    {
        public virtual int Id { get; set; }
        public virtual string CarNo { get; set; }
        public virtual string Model { get; set; }
        public virtual string Engine { get; set; }
        public virtual string Chasis { get; set; }
    }

    public class CarMap : ClassMapping<Car>
    {
        public CarMap()
        {
            Table("car");
            Id(x => x.Id, x => x.Generator(Generators.Identity));

            Property(x => x.CarNo, x => x.NotNullable(true));
            Property(x => x.Model, x => x.NotNullable(true));
            Property(x => x.Engine, x => x.NotNullable(true));
            Property(x => x.Chasis, x => x.NotNullable(true));
        }
    }
}