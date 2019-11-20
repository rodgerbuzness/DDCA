using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDCA.Models
{
    public class Compressor
    {
        public virtual int Id { get; set; }
        public virtual string CompressorNo { get; set; }
        public virtual string CompressorType { get; set; }
        public virtual string Model { get; set; }
        public virtual Region Region { get; set; }
        public virtual District District { get; set; }

        public Compressor()
        {
            Region = new Region();
            District = new District();
        }
    }

    public class CompressorMap : ClassMapping<Compressor>
    {
        public CompressorMap()
        {
            Table("compressor");
            Id(x => x.Id, x => x.Generator(Generators.Identity));

            Property(x => x.CompressorNo, x => x.NotNullable(true));
            Property(x => x.CompressorType, x => x.NotNullable(true));
            Property(x => x.Model, x => x.NotNullable(true));


            ManyToOne(x => x.Region, x =>
            {
                x.Column("region_id");
                x.NotNullable(true);
            });

            ManyToOne(x => x.District, x =>
            {
                x.Column("district_id");
                x.NotNullable(true);
            });
        }
    }

}