using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDCA.Infrastructure
{
    public class StrataDrop
    {
        public List<StrataPoint> StrPoints { get; set; }

        public StrataDrop()
        {
            StrPoints = new List<StrataPoint>();

            for (int i = 1; i <= 10; i++)
            {
                StrataPoint point = new StrataPoint();
                point.Id = i;
                point.Number = i;

                StrPoints.Add(point);
            }
        }
    }
}