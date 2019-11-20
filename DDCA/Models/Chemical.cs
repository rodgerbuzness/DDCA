using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDCA.Models
{
    public class Chemical
    {
        public virtual int Id { get; set; }
        public virtual LabAnalysis LabAnalysis { get; set; }
        public virtual string Alkalinity { get; set; }
        public virtual string  Hardness { get; set; }
        public virtual string Calcium { get; set; }
        public virtual string Phenophthalein { get; set; }
        public virtual string Carbonate { get; set; }
        public virtual string Magnesium { get; set; }
        public virtual string NonCarbonate { get; set; }
        public virtual string Sodium { get; set; }
        public virtual string Potassium { get; set; }
        public virtual string Cadmium { get; set; }
        public virtual string Chromium { get; set; }
        public virtual string Copper { get; set; }
        public virtual string Lead { get; set; }
        public virtual string Manganese { get; set; }
        public virtual string Mercury { get; set; }
        public virtual string Zinc { get; set; }
        public virtual string TotalNitrogen { get; set; }
        public virtual string NitriteNitrogen { get; set; }
        public virtual string NitrateNitrogen { get; set; }
        public virtual string AmmonicalNitrogen { get; set; }
        public virtual string OrganicNitrogen { get; set; }
        public virtual string Phosphorus { get; set; }
        public virtual string OrthoPhosphate { get; set; }
        public virtual string Sulphate { get; set; }
        public virtual string Chloride { get; set; }
        public virtual string Iron { get; set; }
        public virtual string Fluoride { get; set; }
        public virtual string Permanganate { get; set; }
        public virtual string Bod { get; set; }

    }

    public class ChemicalMap: ClassMapping<Chemical>
    {
        public ChemicalMap()
        {
            Table("chemical");
            Id(x => x.Id, x => x.Generator(Generators.Identity));

            ManyToOne(x => x.LabAnalysis, x =>
            {
                x.Column("labanalysis_id");
                x.NotNullable(true);
            });
            Property(x => x.Alkalinity, x => x.NotNullable(true));
            Property(x => x.Hardness, x => x.NotNullable(true));
            Property(x => x.Calcium, x => x.NotNullable(true));
            Property(x => x.Phenophthalein, x => x.NotNullable(true));
            Property(x => x.Carbonate, x => x.NotNullable(true));
            Property(x => x.Magnesium, x => x.NotNullable(true));
            Property(x => x.NonCarbonate, x => x.NotNullable(true));
            Property(x => x.Sodium, x => x.NotNullable(true));
            Property(x => x.Potassium, x => x.NotNullable(true));
            Property(x => x.Cadmium, x => x.NotNullable(true));
            Property(x => x.Chromium, x => x.NotNullable(true));
            Property(x => x.Copper, x => x.NotNullable(true));
            Property(x => x.Iron, x => x.NotNullable(true));
            Property(x => x.Lead, x => x.NotNullable(true));
            Property(x => x.Manganese, x => x.NotNullable(true));
            Property(x => x.Mercury, x => x.NotNullable(true));
            Property(x => x.Zinc, x => x.NotNullable(true));
            Property(x => x.TotalNitrogen, x => x.NotNullable(true));
            Property(x => x.NitriteNitrogen, x => x.NotNullable(true));
            Property(x => x.NitrateNitrogen, x => x.NotNullable(true));
            Property(x => x.AmmonicalNitrogen, x => x.NotNullable(true));
            Property(x => x.OrganicNitrogen, x => x.NotNullable(true));
            Property(x => x.Phosphorus, x => x.NotNullable(true));
            Property(x => x.OrthoPhosphate, x => x.NotNullable(true));
            Property(x => x.Sulphate, x => x.NotNullable(true));
            Property(x => x.Chloride, x => x.NotNullable(true));
            Property(x => x.Fluoride, x => x.NotNullable(true));
            Property(x => x.Permanganate, x => x.NotNullable(true));
            Property(x => x.Bod, x => x.NotNullable(true));
        }
    }
}