using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDCA.Infrastructure
{
    public class StatusDrop
    {
        public List<Status> StatusList { get; set; }

        public StatusDrop()
        {
            StatusList = new List<Status>();

            var s1 = new Status
            {
                Id = 1,
                Name = "Workig"
            };

            var s2 = new Status
            {
                Id = 2,
                Name = "Not Working"
            };

            StatusList.Add(s1);
            StatusList.Add(s2);
        }
    }
}