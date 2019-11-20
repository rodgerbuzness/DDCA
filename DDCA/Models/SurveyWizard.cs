using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDCA.Models
{
    public class SurveyWizard
    {
        public Client Client { get; set; } 
        public GeoSurvey GeoSurvey { get; set; }
        public SurveyProfile SurveyProfile { get; set; }
    }
}