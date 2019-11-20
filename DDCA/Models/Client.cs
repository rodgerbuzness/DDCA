using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDCA.Models
{
    public class Client
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
        public virtual Region Region { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Email { get; set; }
        public virtual DateTime AddDate { get; set; }

        public Client()
        {
            Region = new Region();
        }
    }

    public class ClientMap : ClassMapping<Client>
    {
        public ClientMap()
        {
            Table("clients");


            ManyToOne(x => x.Region, map =>
            {
                map.Column("region_id");
                map.NotNullable(true);
            });

            Id(x => x.Id, x => x.Generator(Generators.Identity));
            Property(x => x.Name, x => x.NotNullable(true));
            Property(x => x.Address, x => x.NotNullable(true));
            Property(x => x.Phone, x => x.NotNullable(true));
            Property(x => x.Email, x => x.NotNullable(true));
            Property(x => x.AddDate, x =>
            {
                x.Column("added_date");
                x.NotNullable(true);
            });
        }
    }
}