using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDCA.Models
{
    public class Role
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }

    public class RoleMap : ClassMapping<Role>
    {
        public RoleMap()
        {
            Table("roles");

            Id(x => x.Id, x => x.Generator(Generators.Identity));

            Property(x => x.Name, x => x.NotNullable(true));
        }
    }
}