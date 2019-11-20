using DDCA.Areas.Borehole.ViewModels;
using DDCA.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDCA.Models
{
    public class BoreholeWizard
    {
        public GeoSurvey GeoSurvey { get; set; }
        public Borehole Borehole { get; set; }
        public MethodDrill MethodDrill { get; set; }
        public BoreDrillMethod BoreDrillMethod { get; set; }
        public Strata1 StrataData1 { get; set; }
        public Strata2 StrataData2 { get; set; }
        public Strata3 StrataData3 { get; set; }
        public PumpTest PumpTest { get; set; }
        public Chemical Chemical { get; set; }
        public Physical Physical { get; set; }
        public LabAnalysis LabAnalysis { get; set; }

    }
}