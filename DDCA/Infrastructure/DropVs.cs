using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDCA.Infrastructure
{
    public class DropVs
    {
        public List<VESPoint> VsPoints { get; set; }

        public DropVs()
        {
            VsPoints = new List<VESPoint>();

            for (int i = 1; i <= 10; i++)
            {
                VESPoint point = new VESPoint();
                point.Id = i;
                point.Number = i;

                VsPoints.Add(point);
            }
        }
    }
}