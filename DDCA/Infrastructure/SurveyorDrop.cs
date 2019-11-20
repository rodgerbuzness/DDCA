using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDCA.Infrastructure
{
    public class SurveyorDrop
    {
        public List<SurveyorType> surveyorTypes { get; set; } 

        public SurveyorDrop()
        {
            surveyorTypes = new List<SurveyorType>();

            var s1 = new SurveyorType
            {
                Id = 1,
                Name = "Magnetic"
            };

            var s2 = new SurveyorType
            {
                Id = 2,
                Name = "Electrical"
            };

            surveyorTypes.Add(s1);
            surveyorTypes.Add(s2);
        }
    }
}