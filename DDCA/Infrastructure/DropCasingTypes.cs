using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDCA.Infrastructure
{
    public class DropCasingTypes
    {
        public List<CasingType> CasingTypes { get; set; }

        public DropCasingTypes()
        {
            CasingTypes = new List<CasingType>();

            var casing1 = new CasingType
            {
                Id = 1,
                Name = "Conductor Casing"
            };

            var casing2 = new CasingType
            {
                Id = 2,
                Name = "Surface Casing"
            };

            var casing3 = new CasingType
            {
                Id = 3,
                Name = "Intermediate Casing"
            };

            CasingTypes.Add(casing1);
            CasingTypes.Add(casing2);
            CasingTypes.Add(casing3);
        }
    }
}