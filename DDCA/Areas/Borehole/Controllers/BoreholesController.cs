using DDCA.Models;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using DDCA.Areas.Borehole.ViewModels;
using DDCA.Infrastructure;

namespace DDCA.Areas.Borehole.Controllers
{
    public class BoreholesController : Controller
    {
        public List<Rig> Rigs { get; set; }
        public List<DrillMethod> DrillMethods { get; set; }
        public List<Staff> Staffs;
        public StrataDrop StrataDrop;
        public DropCasingTypes DropCasingTypes;

        public BoreholesController()
        {
            Rigs = Database.Session.Query<Rig>().ToList();
            DrillMethods = Database.Session.Query<DrillMethod>().ToList();
            StrataDrop = new StrataDrop();
            DropCasingTypes = new DropCasingTypes();
            Staffs = Database.Session.Query<Staff>().ToList();
        }

        // GET: Borehole/Boreholes
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult New()
        {
            return View();
        }

        private BoreholeWizard GetBorehole()
        {
            if (Session["borehole"] == null)
            {
                Session["borehole"] = new BoreholeWizard
                {
                    //Client = new Client(),
                    GeoSurvey = new GeoSurvey(),
                    //SurveyProfile = new SurveyProfile(),
                    Borehole = new Models.Borehole(),
                    MethodDrill = new MethodDrill(),
                    BoreDrillMethod = new BoreDrillMethod(),
                    StrataData1 = new Strata1(),
                    StrataData2 = new Strata2(),
                    StrataData3 = new Strata3(),
                    PumpTest = new PumpTest(),
                    Chemical = new Chemical(),
                    Physical = new Physical(),
                    LabAnalysis = new LabAnalysis()
                };
            }
            return (BoreholeWizard)Session["borehole"];
        }

        private void RemoveSurvey()
        {
            Session.Remove("borehole");
        }

        [HttpPost]
        public ActionResult GetBoreholeList()
        {
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortcolumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];

            List<BoreDrillMethod> Boreholes = Database.Session.Query<BoreDrillMethod>().ToList<BoreDrillMethod>();
            int totalRows = Boreholes.Count;


            if (!string.IsNullOrEmpty(searchValue))//filtering
            {
                Boreholes = Boreholes.Where(x => x.Borehole.BoreholeNo.ToLower().Contains(searchValue.ToLower()) || x.GeoSurvey.Village.ToLower().Contains(searchValue.ToLower()) || x.Borehole.Northings.ToLower().Contains(searchValue.ToLower()) || x.Borehole.Eastings.ToLower().Contains(searchValue.ToLower()) || x.Borehole.StartDate.ToString().Contains(searchValue.ToLower()) || x.Borehole.EndDate.ToString().Contains(searchValue.ToLower()) || x.Borehole.FinishDepth.ToLower().Contains(searchValue.ToLower()) || x.Borehole.Diameter.ToString().Contains(searchValue.ToLower()) || x.Borehole.AquiferDepth.ToLower().Contains(searchValue.ToLower())).ToList<BoreDrillMethod>();
            }

            int totalrowsAfterFiltering = Boreholes.Count;
            //sorting
            Boreholes = Boreholes.OrderBy(sortcolumnName + " " + sortDirection).ToList<BoreDrillMethod>();

            //paging
            Boreholes = Boreholes.Skip(start).Take(length).ToList<BoreDrillMethod>();
            return Json(new { data = Boreholes, draw = Request["draw"], recordsTotal = totalRows, recordsFiltered = totalrowsAfterFiltering }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetSurveyList()
        {
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortcolumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];

            List<GeoSurvey> Surveys = Database.Session.Query<GeoSurvey>().Where(x => x.Status == "U").ToList<GeoSurvey>();
            int totalRows = Surveys.Count;
            

            if (!string.IsNullOrEmpty(searchValue))//filtering
            {
                Surveys = Surveys.Where(x => x.Client.Name.ToLower().Contains(searchValue.ToLower()) || x.Region.Name.ToLower().Contains(searchValue.ToLower()) || x.District.Name.ToLower().Contains(searchValue.ToLower()) || x.Village.ToLower().Contains(searchValue.ToLower()) || x.StartDate.ToString().Contains(searchValue.ToLower()) || x.EndDate.ToString().Contains(searchValue.ToLower()) || x.Staff.Name.ToLower().Contains(searchValue.ToLower()) || x.Cost.ToString().Contains(searchValue.ToLower()) || x.SiteRecommendation.ToLower().Contains(searchValue.ToLower())).ToList<GeoSurvey>();
            }

            int totalrowsAfterFiltering = Surveys.Count;
            //sorting
            Surveys = Surveys.OrderBy(sortcolumnName + " " + sortDirection).ToList<GeoSurvey>();

            //paging
            Surveys = Surveys.Skip(start).Take(length).ToList<GeoSurvey>();
            return Json(new { data = Surveys, draw = Request["draw"], recordsTotal = totalRows, recordsFiltered = totalrowsAfterFiltering }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BoreholeNew(int Id)
        {
            ViewBag.RigList = new SelectList(Rigs, "Id", "RigType");
            ViewBag.DrillMethodList = new SelectList(DrillMethods, "Id", "Name");

            var geossurvey = Database.Session.Load<GeoSurvey>(Id);

            BoreholeWizard obj = GetBorehole();

            obj.GeoSurvey = geossurvey;



            return View();
        }


        [HttpPost]
        public ActionResult BoreholeDrillMethod(BoreholeNew data, string prevBtn, string nextBtn)
        {
           
            if (nextBtn != null)
            {
                if (ModelState.IsValid)
                {
                    ViewBag.VesPoints = new SelectList(StrataDrop.StrPoints.ToList(), "Id", "Number");

                    BoreholeWizard obj = GetBorehole();

                    obj.Borehole.BoreholeNo = data.BoreholeNumber;
                    obj.Borehole.Diameter = data.DrilledDiameter;
                    obj.Borehole.FinishDepth = data.DepthOnFinish;
                    obj.Borehole.Northings = data.Northings;
                    obj.Borehole.Eastings = data.Eastings;
                    obj.Borehole.StartDate = data.StartDate;
                    obj.Borehole.EndDate = data.CompleteDate;

                    obj.MethodDrill.Id = Convert.ToInt32(data.DrillingMethod);
                    obj.MethodDrill.DepthTo = data.DrillDepth;
                    obj.MethodDrill.RigId = Convert.ToInt32(data.RigMachine);




                    return PartialView("BorestrataBorehole");
                }
            }
            return PartialView();
        }

        [HttpPost]
        public ActionResult BorestrataBorehole(BoresrataNew data, string prevBtn, string nextBtn)
        {

            ViewBag.VesPoints = new SelectList(StrataDrop.StrPoints.ToList(), "Id", "Number");
            if (nextBtn != null)
            {
                if (ModelState.IsValid)
                {

                    ViewBag.Staff = new SelectList(Staffs, "Id", "Name");
                    BoreholeWizard obj = GetBorehole();

                    obj.Borehole.Formation = data.Formation;
                    obj.Borehole.AquiferDepth = data.StruckDepth;

                    obj.StrataData1 = data.Strata1;
                    obj.StrataData2 = data.Strata2;
                    obj.StrataData3 = data.Strata3;


                    return PartialView("PumpTestingBorehole");
                }
            }
            return PartialView();
        }


        [HttpPost]
        public ActionResult PumpTestingBorehole(PumpTestingNew data, string prevBtn, string nextBtn)
        {

            if (nextBtn != null)
            {
                if (ModelState.IsValid)
                {
                    ViewBag.CasingTypes = new SelectList(DropCasingTypes.CasingTypes.ToList(), "Id", "Name");
                    BoreholeWizard obj = GetBorehole();

                    obj.PumpTest.Staff = Staffs.Where(x => x.Id == Convert.ToInt32(data.Tester)).FirstOrDefault<Staff>();
                    obj.PumpTest.YieldRate = data.YieldRate;
                    obj.PumpTest.DrwDownDepth = data.DrawDownDepth;
                    obj.PumpTest.StaticWaterLvlDepth = data.SWLDepth;
                    obj.PumpTest.AirDiameter = data.AirLiftSizeDiameter;
                    obj.PumpTest.AirDepth = data.AirLiftSizeDepth;
                    obj.PumpTest.CylinderDiameter = data.CylinderDiameter;
                    obj.PumpTest.CylinderDepth = data.CylinderDepth;
                    obj.PumpTest.SubmissiblePumpSize = data.SubmissiblePumpsize;
                    obj.PumpTest.SubmissibleHeight = data.SubmissibleDepth;
                    obj.PumpTest.Results = data.PumpResults;

                    return PartialView("MaterialBorehole");
                }
            }
            return PartialView();
        }

        [HttpPost]
        public ActionResult MaterialBorehole(BoreholeMaterialNew data, string prevBtn, string nextBtn)
        {

            if (nextBtn != null)
            {
                if (ModelState.IsValid)
                {
                    BoreholeWizard obj = GetBorehole();

                    obj.Borehole.CasingDiameter = data.CasingDiameter;
                    obj.Borehole.CasingHeight = data.CasingHeight;
                    obj.Borehole.CasingType = data.CasingType;
                    obj.Borehole.CasingBottom = data.BottomPlug;
                    obj.Borehole.CasingTopper = data.TopProtection;
                    obj.Borehole.BackFillHeight = data.BackfillHeight;
                    obj.Borehole.BackFillMaterial = data.BackfillMaterial;
                    obj.Borehole.BackFillAvgSize = data.BackfillAvgSize;
                    obj.Borehole.BackFillMethod2 = data.OtherBackfillMethod;
                    obj.Borehole.ScreenDiameter = data.ScreenDiameter;
                    obj.Borehole.ScreenHeight = data.ScreenHeight;
                    obj.Borehole.ScreenType = data.ScreenType;
                    obj.Borehole.GravelType = data.GravelType;
                    obj.Borehole.GravelType = data.GravelType;
                    obj.Borehole.GravelAvgSize = data.GravelAvgSize;
                    obj.Borehole.GravelFrom = data.GravelFrom;
                    obj.Borehole.GravelTo = data.GravelTo;
                  

                    return PartialView("ChemicalBorehole");
                }
            }
            return PartialView();
        }


        [HttpPost]
        public ActionResult ChemicalBorehole(ChemicalNew data, string prevBtn, string nextBtn)
        {

            if (nextBtn != null)
            {
                if (ModelState.IsValid)
                {
                    BoreholeWizard obj = GetBorehole();

                    obj.Chemical.Hardness = data.Hardness;
                    obj.Chemical.Manganese = data.Manganese;
                    obj.Chemical.OrthoPhosphate = data.OrthoPhosphate;
                    obj.Chemical.Iron = data.Iron;
                    obj.Chemical.NitrateNitrogen = data.NitrateNitrogen;
                    obj.Chemical.Carbonate = data.Carbonate;
                    obj.Chemical.Phenophthalein = data.Phenophthalein;
                    obj.Chemical.NonCarbonate = data.NonCarbonate;
                    obj.Chemical.Sodium = data.Sodium;
                    obj.Chemical.Cadmium = data.Cadmium;
                    obj.Chemical.Calcium = data.Calcium;
                    obj.Chemical.Chloride = data.Chloride;
                    obj.Chemical.Fluoride = data.Fluoride;
                    obj.Chemical.TotalNitrogen = data.TotalNitrogen;
                    obj.Chemical.Chromium = data.Chromium;
                    obj.Chemical.Mercury = data.Mercury;
                    obj.Chemical.Zinc = data.Zinc;
                    obj.Chemical.Potassium = data.Potassium;
                    obj.Chemical.Lead = data.Lead;
                    obj.Chemical.Magnesium = data.Magnesium;
                    obj.Chemical.Copper = data.Copper;
                    obj.Chemical.Sulphate = data.Sulphate;
                    obj.Chemical.AmmonicalNitrogen = data.AmmonicalNitrogen;
                    obj.Chemical.NitriteNitrogen = data.NitriteNitrogen;
                    obj.Chemical.OrganicNitrogen = data.OrganicNitrogen;
                    obj.Chemical.Phosphorus = data.Phosphorus;
                    obj.Chemical.Permanganate = data.Permanganate;
                    obj.Chemical.Alkalinity = data.Alkalinity;
                    obj.Chemical.Bod = data.BOD;

                    return PartialView("PhysicalLabBorehole");
                }
            }
            return PartialView();
        }

        [HttpPost]
        public ActionResult PhysicalLabBorehole(PhysicalLabNew data, string prevBtn, string nextBtn)
        {

            if (nextBtn != null)
            {
                if (ModelState.IsValid)
                {
                    ViewBag.ActionMethod = "New";

                    BoreholeWizard obj = GetBorehole();

                    obj.Physical.Colour = data.Color;
                    obj.Physical.Turbidity = data.Turbidity;
                    obj.Physical.Odour = data.Odour;
                    obj.Physical.SettleableMatter = data.SettleableMatter;
                    obj.Physical.PH = data.PH;
                    obj.Physical.Taste = data.Taste;
                    obj.Physical.Conductivity = data.Conductivity;
                    obj.Physical.FiltrateResidue = data.FiltrateResidue;
                    obj.Physical.NonFiltrateResidue = data.NonFiltrateResidue;
                    obj.Physical.VolatileFixedResidue = data.VolatileFixedResidue;
                    obj.LabAnalysis.LabName = data.LabName;
                    obj.LabAnalysis.CollectedDate = data.CollectedDate;
                    obj.LabAnalysis.AnalysisDate = data.AnalysisDate;
                    obj.LabAnalysis.Remarks = data.Remarks;
                    obj.LabAnalysis.Recommend = data.Recommend;

                    obj.Borehole.UncasedDepth = "";

                    var borehole = obj.Borehole;
                    var geosurvey = Database.Session.Load<GeoSurvey>(obj.GeoSurvey.Id);

                    Database.Session.Save(borehole);

                    var rig = Database.Session.Load<Rig>(obj.MethodDrill.RigId);
                    var drillmethod = Database.Session.Load<DrillMethod>(obj.MethodDrill.Id);

                    BoreDrillMethod boreDrillMethod = new BoreDrillMethod();
                    boreDrillMethod.GeoSurvey = geosurvey;
                    boreDrillMethod.Rig = rig;
                    boreDrillMethod.Borehole = borehole;
                    Database.Session.Save(boreDrillMethod);

                    DrillingType drillingType = new DrillingType();
                    drillingType.DrillMethod = drillmethod;
                    drillingType.DrillDepth = obj.MethodDrill.DepthTo;
                    drillingType.BoreDrillMethod = boreDrillMethod;
                    Database.Session.Save(drillingType);

                    BoreStrata boreStrata1 = new BoreStrata();
                    BoreStrata boreStrata2 = new BoreStrata();
                    BoreStrata boreStrata3 = new BoreStrata();

                    boreStrata1.StrataName = obj.StrataData1.Name;
                    boreStrata1.RangeFrom = obj.StrataData1.DepthFrom;
                    boreStrata1.RangeTo = obj.StrataData1.DepthTo;
                    boreStrata1.Borehole = borehole;
                    Database.Session.Save(boreStrata1);

                    boreStrata2.StrataName = obj.StrataData2.Name;
                    boreStrata2.RangeFrom = obj.StrataData2.DepthFrom;
                    boreStrata2.RangeTo = obj.StrataData2.DepthTo;
                    boreStrata2.Borehole = borehole;
                    Database.Session.Save(boreStrata2);

                    boreStrata3.StrataName = obj.StrataData3.Name;
                    boreStrata3.RangeFrom = obj.StrataData3.DepthFrom;
                    boreStrata3.RangeTo = obj.StrataData3.DepthTo;
                    boreStrata3.Borehole = borehole;
                    Database.Session.Save(boreStrata3);

                    var pumpTest = obj.PumpTest;
                    pumpTest.Borehole = borehole;
                    Database.Session.Save(pumpTest);

                    var labanalysis = obj.LabAnalysis;
                    labanalysis.PumpTest = pumpTest;
                    Database.Session.Save(labanalysis);

                    var physical = obj.Physical;
                    physical.LabAnalysis = labanalysis;
                    Database.Session.Save(physical);

                    var chemical = obj.Chemical;
                    chemical.LabAnalysis = labanalysis;
                    Database.Session.Save(chemical);

                    geosurvey.Status = "R";
                    Database.Session.SaveOrUpdate(geosurvey);

                    RemoveSurvey();

                    return PartialView("Success");
                }
            }
            return PartialView();
        }


        public ActionResult EditBorehole(int Id)
        {
            var boreDrillMethod = Database.Session.Load<BoreDrillMethod>(Id);

            var drillingType = Database.Session.Query<DrillingType>().Where(x => x.BoreDrillMethod.Id == Id).FirstOrDefault<DrillingType>();

            BoreholeWizard obj = GetBorehole();

            obj.BoreDrillMethod.Id = boreDrillMethod.Id;

            obj.GeoSurvey.Id = boreDrillMethod.GeoSurvey.Id;

            var borehole = boreDrillMethod.Borehole;


            ViewBag.DrillMethodEdit = new SelectList(DrillMethods, "Id", "Name", drillingType.DrillMethod.Id);
            ViewBag.Rig11 = new SelectList(Rigs, "Id", "RigType", boreDrillMethod.Rig.Id);

            if (borehole == null)
                return HttpNotFound();

            var model = new BoreholeEdit();

            model.BoreholeId = borehole.Id;
            model.BoreholeNumber = borehole.BoreholeNo;
            model.DrilledDiameter = borehole.Diameter;
            model.DepthOnFinish = borehole.FinishDepth;
            model.Northings = borehole.Northings;
            model.Eastings = borehole.Eastings;
            model.StartDate = borehole.StartDate;
            model.CompleteDate = borehole.EndDate;
            model.DrillDepth = drillingType.DrillDepth;
            model.RigMachine = boreDrillMethod.Rig.RigType;



            return View(model);
        }


        [HttpPost]
        public ActionResult BoreholeDrillMethodEdit(BoreholeEdit data, string prevBtn, string nextBtn)
        {


            if (nextBtn != null)
            {
               

                if (ModelState.IsValid)
                {
                    ViewBag.VesPoints = new SelectList(StrataDrop.StrPoints.ToList(), "Id", "Number");

                    BoreholeWizard obj = GetBorehole();

                    obj.Borehole.Id = data.BoreholeId;
                    obj.Borehole.BoreholeNo = data.BoreholeNumber;
                    obj.Borehole.Diameter = data.DrilledDiameter;
                    obj.Borehole.FinishDepth = data.DepthOnFinish;
                    obj.Borehole.Northings = data.Northings;
                    obj.Borehole.Eastings = data.Eastings;
                    obj.Borehole.StartDate = data.StartDate;
                    obj.Borehole.EndDate = data.CompleteDate;
                    obj.MethodDrill.DepthTo = data.DrillDepth;
                    obj.MethodDrill.RigId = Convert.ToInt32(data.RigMachine);
                    obj.MethodDrill.Id = Convert.ToInt32(data.DrillingMethod);


                    BorestrataEdit model = new BorestrataEdit();

                    var borestrata = Database.Session.Query<BoreStrata>().Where(b => b.Borehole.Id == data.BoreholeId).ToList<BoreStrata>();
                   


                    model.Strata1.Name = borestrata[0].StrataName;
                    model.Strata1.DepthFrom = borestrata[0].RangeFrom;
                    model.Strata1.DepthTo = borestrata[0].RangeTo;
                    model.Strata1.Id = borestrata[0].Id;

                    model.Strata2.Name = borestrata[1].StrataName;
                    model.Strata2.DepthFrom = borestrata[1].RangeFrom;
                    model.Strata2.DepthTo = borestrata[1].RangeTo;
                    model.Strata2.Id = borestrata[1].Id;

                    model.Strata3.Name = borestrata[2].StrataName;
                    model.Strata3.DepthFrom = borestrata[2].RangeFrom;
                    model.Strata3.DepthTo = borestrata[2].RangeTo;
                    model.Strata3.Id = borestrata[2].Id;

                    model.Formation = borestrata[0].Borehole.Formation;
                    model.StruckDepth = borestrata[0].Borehole.AquiferDepth;

                    //obj.StrataData1 = model.Strata1;
                    //obj.StrataData2 = model.Strata2;
                    //obj.StrataData3 = model.Strata3;


                    return PartialView("BorestrataBoreholeEdit", model);
                }
            }
            return PartialView();
        }


        [HttpPost]
        public ActionResult BorestrataBoreholeEdit(BorestrataEdit data, string prevBtn, string nextBtn)
        {


            if (nextBtn != null)
            {


                if (ModelState.IsValid)
                {
                    BoreholeWizard obj = GetBorehole();

                    obj.StrataData1 = data.Strata1;
                    obj.StrataData2 = data.Strata2;
                    obj.StrataData3 = data.Strata3;
                    obj.Borehole.Formation = data.Formation;
                    obj.Borehole.AquiferDepth = data.StruckDepth;


                    var pumptest = Database.Session.Query<PumpTest>().Where(x => x.Borehole.Id == obj.Borehole.Id).FirstOrDefault<PumpTest>();

                    PumpTestingEdit pump = new PumpTestingEdit();

                    ViewBag.watu = new SelectList(Staffs, "Id", "Name", pumptest.Staff.Id.ToString());

                    obj.PumpTest.Id = pumptest.Id;

                    pump.Id = pumptest.Id;
                    pump.YieldRate = pumptest.YieldRate;
                    pump.DrawDownDepth = pumptest.DrwDownDepth;
                    pump.SWLDepth = pumptest.StaticWaterLvlDepth;
                    pump.AirLiftSizeDiameter = pumptest.AirDiameter;
                    pump.AirLiftSizeDepth = pumptest.AirDepth;
                    pump.CylinderDiameter = pumptest.CylinderDiameter;
                    pump.CylinderDepth = pumptest.CylinderDepth;
                    pump.SubmissiblePumpsize = pumptest.SubmissiblePumpSize;
                    pump.SubmissibleDepth = pumptest.SubmissibleHeight;
                    pump.Tester = pumptest.Staff.Name;
                    pump.PumpResults = pumptest.Results;

                    return PartialView("PumpTestingBoreholeEdit", pump);
                }
            }
            return PartialView();
        }

        [HttpPost]
        public ActionResult PumpTestingBoreholeEdit(PumpTestingEdit data, string prevBtn, string nextBtn)
        {


            if (nextBtn != null)
            {

                if (ModelState.IsValid)
                {
                    BoreholeWizard obj = GetBorehole();

                    
                    PumpTestingEdit pump = new PumpTestingEdit();

                    obj.PumpTest.YieldRate = data.YieldRate;
                    obj.PumpTest.DrwDownDepth = data.DrawDownDepth;
                    obj.PumpTest.StaticWaterLvlDepth = data.SWLDepth;
                    obj.PumpTest.AirDiameter = data.AirLiftSizeDiameter;
                    obj.PumpTest.AirDepth = data.AirLiftSizeDepth;
                    obj.PumpTest.CylinderDiameter = data.CylinderDiameter;
                    obj.PumpTest.CylinderDepth = data.CylinderDepth;
                    obj.PumpTest.SubmissiblePumpSize = data.SubmissiblePumpsize;
                    obj.PumpTest.SubmissibleHeight = data.SubmissibleDepth;
                    obj.PumpTest.Staff.Id =Convert.ToInt32(data.Tester);
                    obj.PumpTest.Results = data.PumpResults;


                    var borehole = Database.Session.Query<Models.Borehole>().Where(x => x.Id == obj.Borehole.Id).FirstOrDefault<Models.Borehole>();

                    BoreholeMaterialEdit borematerial = new BoreholeMaterialEdit();

                    borematerial.CasingDiameter = borehole.CasingDiameter;
                    borematerial.CasingHeight = borehole.CasingHeight;
                    //borematerial.CasingType = borehole.CasingType;
                    //borematerial.BottomPlug = borehole.CasingBottom;
                    //borematerial.TopProtection = borehole.CasingTopper;
                    borematerial.BackfillHeight = borehole.BackFillHeight;
                    borematerial.BackfillMaterial = borehole.BackFillMaterial;
                    borematerial.BackfillAvgSize = borehole.BackFillAvgSize;
                    borematerial.OtherBackfillMethod = borehole.BackFillMethod2;
                    borematerial.ScreenDiameter = borehole.ScreenDiameter;
                    borematerial.ScreenHeight = borehole.ScreenHeight;
                    //borematerial.ScreenType = borehole.ScreenType;
                    borematerial.GravelType = borehole.GravelType;
                    borematerial.GravelAvgSize = borehole.GravelAvgSize;
                    borematerial.GravelFrom = borehole.GravelFrom;
                    borematerial.GravelTo = borehole.GravelTo;

                    ViewBag.CasingTypes = new SelectList(DropCasingTypes.CasingTypes.ToList(), "Id", "Name");


                    return PartialView("BoreholeMaterialEdit", borematerial);
                }
            }
            return PartialView();
        }


        [HttpPost]
        public ActionResult BoreholeMaterialEdit(BoreholeMaterialEdit data, string prevBtn, string nextBtn)
        {

            if (nextBtn != null)
            {
                if (ModelState.IsValid)
                {
                    BoreholeWizard obj = GetBorehole();

                    obj.Borehole.CasingDiameter = data.CasingDiameter;
                    obj.Borehole.CasingHeight =   data.CasingHeight;
                    obj.Borehole.CasingType =     data.CasingType;
                    obj.Borehole.CasingBottom =   data.BottomPlug;
                    obj.Borehole.CasingTopper =   data.TopProtection;
                    obj.Borehole.BackFillHeight = data.BackfillHeight;
                    obj.Borehole.BackFillMaterial =  data.BackfillMaterial;
                    obj.Borehole.BackFillAvgSize = data.BackfillAvgSize;
                    obj.Borehole.BackFillMethod2 = data.OtherBackfillMethod;
                    obj.Borehole.ScreenDiameter =  data.ScreenDiameter;
                    obj.Borehole.ScreenHeight =    data.ScreenHeight;
                    obj.Borehole.ScreenType =      data.ScreenType;
                    obj.Borehole.GravelType =      data.GravelType;
                    obj.Borehole.GravelAvgSize =   data.GravelAvgSize;
                    obj.Borehole.GravelFrom =      data.GravelFrom;
                    obj.Borehole.GravelTo =        data.GravelTo;


                    var lab = Database.Session.Query<LabAnalysis>().Where(x => x.PumpTest.Id == obj.PumpTest.Id).FirstOrDefault<LabAnalysis>();

                    obj.LabAnalysis.Id = lab.Id;

                    var chemical = Database.Session.Query<Chemical>().Where(x => x.LabAnalysis == lab).FirstOrDefault<Chemical>();

                    obj.Chemical.Id = chemical.Id;

                    ChemicalEdit chmcl = new ChemicalEdit();

                    chmcl.Hardness = chemical.Hardness;
                    chmcl.Manganese = chemical.Manganese;
                    chmcl.OrthoPhosphate = chemical.OrthoPhosphate;
                    chmcl.Iron = chemical.Iron;
                    chmcl.NitrateNitrogen = chemical.NitrateNitrogen;
                    chmcl.Carbonate = chemical.Carbonate;
                    chmcl.Phenophthalein = chemical.Phenophthalein;
                    chmcl.NonCarbonate = chemical.NonCarbonate;
                    chmcl.Sodium = chemical.Sodium;
                    chmcl.Cadmium = chemical.Cadmium;
                    chmcl.Calcium = chemical.Calcium;
                    chmcl.Chloride = chemical.Chloride;
                    chmcl.Fluoride = chemical.Fluoride;
                    chmcl.TotalNitrogen = chemical.TotalNitrogen;
                    chmcl.Chromium = chemical.Chromium;
                    chmcl.Mercury = chemical.Mercury;
                    chmcl.Zinc = chemical.Zinc;
                    chmcl.Potassium = chemical.Potassium;
                    chmcl.Lead = chemical.Lead;
                    chmcl.Magnesium = chemical.Magnesium;
                    chmcl.Copper = chemical.Copper;
                    chmcl.Sulphate = chemical.Sulphate;
                    chmcl.AmmonicalNitrogen = chemical.AmmonicalNitrogen;
                    chmcl.NitriteNitrogen = chemical.NitriteNitrogen;
                    chmcl.OrganicNitrogen = chemical.OrganicNitrogen;
                    chmcl.Phosphorus = chemical.Phosphorus;
                    chmcl.Permanganate = chemical.Permanganate;
                    chmcl.Alkalinity = chemical.Alkalinity;
                    chmcl.BOD = chemical.Bod;

                    return PartialView("ChemicalBoreholeEdit", chmcl);
                }
            }
            return PartialView();
        }

        [HttpPost]
        public ActionResult ChemicalBoreholeEdit(ChemicalEdit data, string prevBtn, string nextBtn)
        {

            if (nextBtn != null)
            {
                if (ModelState.IsValid)
                {
                    BoreholeWizard obj = GetBorehole();


                    obj.Chemical.Hardness =       data.Hardness;
                    obj.Chemical.Manganese =      data.Manganese;
                    obj.Chemical.OrthoPhosphate = data.OrthoPhosphate;
                    obj.Chemical.Iron =           data.Iron;
                    obj.Chemical.NitrateNitrogen =data.NitrateNitrogen;
                    obj.Chemical.Carbonate =      data.Carbonate;
                    obj.Chemical.Phenophthalein = data.Phenophthalein;
                    obj.Chemical.NonCarbonate =   data.NonCarbonate;
                    obj.Chemical.Sodium =         data.Sodium;
                    obj.Chemical.Cadmium =        data.Cadmium;
                    obj.Chemical.Calcium =        data.Calcium;
                    obj.Chemical.Chloride =       data.Chloride;
                    obj.Chemical.Fluoride =       data.Fluoride;
                    obj.Chemical.TotalNitrogen =  data.TotalNitrogen;
                    obj.Chemical.Chromium =       data.Chromium;
                    obj.Chemical.Mercury =        data.Mercury;
                    obj.Chemical.Zinc =           data.Zinc;
                    obj.Chemical.Potassium =      data.Potassium;
                    obj.Chemical.Lead =           data.Lead;
                    obj.Chemical.Magnesium =      data.Magnesium;
                    obj.Chemical.Copper =         data.Copper;
                    obj.Chemical.Sulphate =       data.Sulphate;
                  obj.Chemical.AmmonicalNitrogen= data.AmmonicalNitrogen;
                    obj.Chemical.NitriteNitrogen =data.NitriteNitrogen;
                    obj.Chemical.OrganicNitrogen =data.OrganicNitrogen;
                    obj.Chemical.Phosphorus =     data.Phosphorus;
                    obj.Chemical.Permanganate =   data.Permanganate;
                    obj.Chemical.Alkalinity =     data.Alkalinity;
                    obj.Chemical.Bod =            data.BOD;


                    var physical = Database.Session.Query<Physical>().Where(x => x.Id == obj.LabAnalysis.Id).FirstOrDefault<Physical>();
                    var lab = Database.Session.Query<LabAnalysis>().Where(x => x.Id == obj.LabAnalysis.Id).FirstOrDefault<LabAnalysis>();

                    obj.Physical.Id = physical.Id;

                    PhysicalLabEdit physicalLab = new PhysicalLabEdit();

                    physicalLab.Color = physical.Colour;
                    physicalLab.Turbidity = physical.Turbidity;
                    physicalLab.Odour = physical.Odour;
                    physicalLab.SettleableMatter = physical.SettleableMatter;
                    physicalLab.PH = physical.PH;
                    physicalLab.Taste = physical.Taste;
                    physicalLab.Conductivity = physical.Conductivity;
                    physicalLab.FiltrateResidue = physical.FiltrateResidue;
                    physicalLab.NonFiltrateResidue = physical.NonFiltrateResidue;
                    physicalLab.VolatileFixedResidue = physical.VolatileFixedResidue;
                    physicalLab.LabName = lab.LabName;
                    physicalLab.CollectedDate = lab.CollectedDate;
                    physicalLab.AnalysisDate = lab.AnalysisDate;
                    physicalLab.Remarks = lab.Remarks;
                    physicalLab.Recommend = lab.Recommend;


                    return PartialView("PhysicalLabBoreholeEdit", physicalLab);
                }
            }
            return PartialView();
        }


        [HttpPost]
        public ActionResult PhysicalLabBoreholeEdit(PhysicalLabEdit data, string prevBtn, string nextBtn)
        {

            if (nextBtn != null)
            {
                if (ModelState.IsValid)
                {
                    BoreholeWizard obj = GetBorehole();


                    ViewBag.ActionMethod = "Edit";


                    obj.Physical.Colour = data.Color;
                    obj.Physical.Turbidity = data.Turbidity;
                    obj.Physical.Odour = data.Odour;
                    obj.Physical.SettleableMatter = data.SettleableMatter;
                    obj.Physical.PH = data.PH;
                    obj.Physical.Taste = data.Taste;
                    obj.Physical.Conductivity = data.Conductivity;
                    obj.Physical.FiltrateResidue = data.FiltrateResidue;
                    obj.Physical.NonFiltrateResidue = data.NonFiltrateResidue;
                    obj.Physical.VolatileFixedResidue = data.VolatileFixedResidue;
                    obj.LabAnalysis.LabName = data.LabName;
                    obj.LabAnalysis.CollectedDate = data.CollectedDate;
                    obj.LabAnalysis.AnalysisDate = data.AnalysisDate;
                    obj.LabAnalysis.Remarks = data.Remarks;
                    obj.LabAnalysis.Recommend = data.Recommend;

                    obj.Borehole.UncasedDepth = "";

                    var borehole = obj.Borehole;
                    var geosurvey = Database.Session.Load<GeoSurvey>(obj.GeoSurvey.Id);

                    Database.Session.SaveOrUpdate(borehole);

                    var rig = Database.Session.Load<Rig>(obj.MethodDrill.RigId);
                    var drillmethod = Database.Session.Load<DrillMethod>(obj.MethodDrill.Id);

                    var boreDrillMethod = Database.Session.Load<BoreDrillMethod>(obj.BoreDrillMethod.Id);
                    boreDrillMethod.GeoSurvey = geosurvey;
                    boreDrillMethod.Rig = rig;
                    boreDrillMethod.Borehole = borehole;
                    Database.Session.SaveOrUpdate(boreDrillMethod);


                    var drillingType = Database.Session.Load<DrillingType>(obj.BoreDrillMethod.Id);
                    drillingType.DrillMethod = drillmethod;
                    drillingType.DrillDepth = obj.MethodDrill.DepthTo;
                    drillingType.BoreDrillMethod = boreDrillMethod;
                    Database.Session.SaveOrUpdate(drillingType);

                    BoreStrata boreStrata1 = new BoreStrata();
                    BoreStrata boreStrata2 = new BoreStrata();
                    BoreStrata boreStrata3 = new BoreStrata();

                    boreStrata1.StrataName = obj.StrataData1.Name;
                    boreStrata1.RangeFrom = obj.StrataData1.DepthFrom;
                    boreStrata1.RangeTo = obj.StrataData1.DepthTo;
                    boreStrata1.Borehole = borehole;
                    boreStrata1.Id = obj.StrataData1.Id;
                    Database.Session.SaveOrUpdate(boreStrata1);

                    boreStrata2.StrataName = obj.StrataData2.Name;
                    boreStrata2.RangeFrom = obj.StrataData2.DepthFrom;
                    boreStrata2.RangeTo = obj.StrataData2.DepthTo;
                    boreStrata2.Borehole = borehole;
                    boreStrata2.Id = obj.StrataData2.Id;
                    Database.Session.SaveOrUpdate(boreStrata2);

                    boreStrata3.StrataName = obj.StrataData3.Name;
                    boreStrata3.RangeFrom = obj.StrataData3.DepthFrom;
                    boreStrata3.RangeTo = obj.StrataData3.DepthTo;
                    boreStrata3.Borehole = borehole;
                    boreStrata3.Id = obj.StrataData3.Id;
                    Database.Session.SaveOrUpdate(boreStrata3);

                    var pumpTest = obj.PumpTest;
                    pumpTest.Borehole = borehole;
                    Database.Session.SaveOrUpdate(pumpTest);

                    var labanalysis = obj.LabAnalysis;
                    labanalysis.PumpTest = pumpTest;
                    Database.Session.SaveOrUpdate(labanalysis);

                    var physical = obj.Physical;
                    physical.LabAnalysis = labanalysis;
                    Database.Session.SaveOrUpdate(physical);

                    var chemical = obj.Chemical;
                    chemical.LabAnalysis = labanalysis;
                    Database.Session.SaveOrUpdate(chemical);

                    geosurvey.Status = "R";
                    Database.Session.SaveOrUpdate(geosurvey);

                    RemoveSurvey();


                    return PartialView("Success");
                }
            }
            return PartialView();
        }


        public ActionResult Show(int Id)
        {
            var boreDrillMethod = Database.Session.Load<BoreDrillMethod>(Id);
            var drillingType = Database.Session.Query<DrillingType>().Where(x => x.BoreDrillMethod.Id == Id).FirstOrDefault<DrillingType>();

            BoreholeWizard obj = GetBorehole();

            obj.GeoSurvey.Id = boreDrillMethod.GeoSurvey.Id;
            obj.Borehole.Id = boreDrillMethod.Borehole.Id;
            var borehole = boreDrillMethod.Borehole;


            if (borehole == null)
                return HttpNotFound();

            var model = new BoreBorestrataView();

            model.boreholeNew.BoreholeId = borehole.Id;
            model.boreholeNew.BoreholeNumber = borehole.BoreholeNo;
            model.boreholeNew.DrilledDiameter = borehole.Diameter;
            model.boreholeNew.DepthOnFinish = borehole.FinishDepth;
            model.boreholeNew.Northings = borehole.Northings;
            model.boreholeNew.Eastings = borehole.Eastings;
            model.boreholeNew.StartDate = borehole.StartDate;
            model.boreholeNew.CompleteDate = borehole.EndDate;
            model.boreholeNew.DrillDepth = drillingType.DrillDepth;
            model.boreholeNew.DrillingMethod = drillingType.DrillMethod.Name;
            model.boreholeNew.RigMachine = boreDrillMethod.Rig.RigType;

            var borestrata = Database.Session.Query<BoreStrata>().Where(b => b.Borehole.Id == borehole.Id).ToList<BoreStrata>();

            model.boresrataNew.Strata1.Name = borestrata[0].StrataName;
            model.boresrataNew.Strata1.DepthFrom = borestrata[0].RangeFrom;
            model.boresrataNew.Strata1.DepthTo = borestrata[0].RangeTo;
  
                 
            model.boresrataNew.Strata2.Name = borestrata[1].StrataName;
            model.boresrataNew.Strata2.DepthFrom = borestrata[1].RangeFrom;
            model.boresrataNew.Strata2.DepthTo = borestrata[1].RangeTo;

            model.boresrataNew.Strata3.Name = borestrata[2].StrataName;
            model.boresrataNew.Strata3.DepthFrom = borestrata[2].RangeFrom;
            model.boresrataNew.Strata3.DepthTo = borestrata[2].RangeTo;

            model.boresrataNew.Formation = borestrata[0].Borehole.Formation;
            model.boresrataNew.StruckDepth = borestrata[0].Borehole.AquiferDepth;


            return View(model);
        }

        [HttpPost]
        public ActionResult BoreBorestrataShow(PhysicalLabEdit data, string prevBtn, string nextBtn)
        {

            if (nextBtn != null)
            {
               
                    BoreholeWizard obj = GetBorehole();

                var pumptest = Database.Session.Query<PumpTest>().Where(x => x.Borehole.Id == obj.Borehole.Id).FirstOrDefault<PumpTest>();
                var borehole = Database.Session.Query<Models.Borehole>().Where(x => x.Id == obj.Borehole.Id).FirstOrDefault<Models.Borehole>();

                obj.PumpTest.Id = pumptest.Id;

                PumpMaterialView pump = new PumpMaterialView();

                pump.PumpTestingNew.YieldRate = pumptest.YieldRate;
                pump.PumpTestingNew.DrawDownDepth = pumptest.DrwDownDepth;
                pump.PumpTestingNew.SWLDepth = pumptest.StaticWaterLvlDepth;
                pump.PumpTestingNew.AirLiftSizeDiameter = pumptest.AirDiameter;
                pump.PumpTestingNew.AirLiftSizeDepth = pumptest.AirDepth;
                pump.PumpTestingNew.CylinderDiameter = pumptest.CylinderDiameter;
                pump.PumpTestingNew.CylinderDepth = pumptest.CylinderDepth;
                pump.PumpTestingNew.SubmissiblePumpsize = pumptest.SubmissiblePumpSize;
                pump.PumpTestingNew.SubmissibleDepth = pumptest.SubmissibleHeight;
                pump.PumpTestingNew.Tester = pumptest.Staff.Name;
                pump.PumpTestingNew.PumpResults = pumptest.Results;
                pump.Formation = borehole.Formation;
                pump.StruckDepth = borehole.AquiferDepth;


                pump.BoreholeMaterialNew.CasingDiameter = borehole.CasingDiameter;
                pump.BoreholeMaterialNew.CasingHeight = borehole.CasingHeight;
                pump.BoreholeMaterialNew.CasingType = borehole.CasingType;
                pump.BoreholeMaterialNew.BottomPlug = borehole.CasingBottom;
                pump.BoreholeMaterialNew.TopProtection = borehole.CasingTopper;
                pump.BoreholeMaterialNew.BackfillHeight = borehole.BackFillHeight;
                pump.BoreholeMaterialNew.BackfillMaterial = borehole.BackFillMaterial;
                pump.BoreholeMaterialNew.BackfillAvgSize = borehole.BackFillAvgSize;
                pump.BoreholeMaterialNew.OtherBackfillMethod = borehole.BackFillMethod2;
                pump.BoreholeMaterialNew.ScreenDiameter = borehole.ScreenDiameter;
                pump.BoreholeMaterialNew.ScreenHeight = borehole.ScreenHeight;
                pump.BoreholeMaterialNew.ScreenType = borehole.ScreenType;
                pump.BoreholeMaterialNew.GravelType = borehole.GravelType;
                pump.BoreholeMaterialNew.GravelAvgSize = borehole.GravelAvgSize;
                pump.BoreholeMaterialNew.GravelFrom = borehole.GravelFrom;
                pump.BoreholeMaterialNew.GravelTo = borehole.GravelTo;
            
                return PartialView("PumpMaterialShow", pump);
               
            }
            return PartialView();
        }

        [HttpPost]
        public ActionResult ChemicalPhysicalLabShow(PhysicalLabEdit data, string prevBtn, string nextBtn)
        {

            if (nextBtn != null)
            {
                BoreholeWizard obj = GetBorehole();

                var pumptest = Database.Session.Query<PumpTest>().Where(x => x.Borehole.Id == obj.Borehole.Id).FirstOrDefault<PumpTest>();

                var lab = Database.Session.Query<LabAnalysis>().Where(x => x.PumpTest.Id == pumptest.Id).FirstOrDefault<LabAnalysis>();

                var physical = Database.Session.Query<Physical>().Where(x => x.Id == lab.Id).FirstOrDefault<Physical>();

                var chemical = Database.Session.Query<Chemical>().Where(x => x.LabAnalysis.Id == lab.Id).FirstOrDefault<Chemical>();

                ChemicalPhysicalLabView chmcl1 = new ChemicalPhysicalLabView();

                chmcl1.ChemicalNew.Hardness = chemical.Hardness;
                chmcl1.ChemicalNew.Manganese = chemical.Manganese;
                chmcl1.ChemicalNew.OrthoPhosphate = chemical.OrthoPhosphate;
                chmcl1.ChemicalNew.Iron = chemical.Iron;
                chmcl1.ChemicalNew.NitrateNitrogen = chemical.NitrateNitrogen;
                chmcl1.ChemicalNew.Carbonate = chemical.Carbonate;
                chmcl1.ChemicalNew.Phenophthalein = chemical.Phenophthalein;
                chmcl1.ChemicalNew.NonCarbonate = chemical.NonCarbonate;
                chmcl1.ChemicalNew.Sodium = chemical.Sodium;
                chmcl1.ChemicalNew.Cadmium = chemical.Cadmium;
                chmcl1.ChemicalNew.Calcium = chemical.Calcium;
                chmcl1.ChemicalNew.Chloride = chemical.Chloride;
                chmcl1.ChemicalNew.Fluoride = chemical.Fluoride;
                chmcl1.ChemicalNew.TotalNitrogen = chemical.TotalNitrogen;
                chmcl1.ChemicalNew.Chromium = chemical.Chromium;
                chmcl1.ChemicalNew.Mercury = chemical.Mercury;
                chmcl1.ChemicalNew.Zinc = chemical.Zinc;
                chmcl1.ChemicalNew.Potassium = chemical.Potassium;
                chmcl1.ChemicalNew.Lead = chemical.Lead;
                chmcl1.ChemicalNew.Magnesium = chemical.Magnesium;
                chmcl1.ChemicalNew.Copper = chemical.Copper;
                chmcl1.ChemicalNew.Sulphate = chemical.Sulphate;
                chmcl1.ChemicalNew.AmmonicalNitrogen = chemical.AmmonicalNitrogen;
                chmcl1.ChemicalNew.NitriteNitrogen = chemical.NitriteNitrogen;
                chmcl1.ChemicalNew.OrganicNitrogen = chemical.OrganicNitrogen;
                chmcl1.ChemicalNew.Phosphorus = chemical.Phosphorus;
                chmcl1.ChemicalNew.Permanganate = chemical.Permanganate;
                chmcl1.ChemicalNew.Alkalinity = chemical.Alkalinity;
                chmcl1.ChemicalNew.BOD = chemical.Bod;

                chmcl1.PhysicalLabNew.Color = physical.Colour;
                chmcl1.PhysicalLabNew.Turbidity = physical.Turbidity;
                chmcl1.PhysicalLabNew.Odour = physical.Odour;
                chmcl1.PhysicalLabNew.SettleableMatter = physical.SettleableMatter;
                chmcl1.PhysicalLabNew.PH = physical.PH;
                chmcl1.PhysicalLabNew.Taste = physical.Taste;
                chmcl1.PhysicalLabNew.Conductivity = physical.Conductivity;
                chmcl1.PhysicalLabNew.FiltrateResidue = physical.FiltrateResidue;
                chmcl1.PhysicalLabNew.NonFiltrateResidue = physical.NonFiltrateResidue;
                chmcl1.PhysicalLabNew.VolatileFixedResidue = physical.VolatileFixedResidue;
                chmcl1.PhysicalLabNew.LabName = lab.LabName;
                chmcl1.PhysicalLabNew.CollectedDate = lab.CollectedDate;
                chmcl1.PhysicalLabNew.AnalysisDate = lab.AnalysisDate;
                chmcl1.PhysicalLabNew.Remarks = lab.Remarks;
                chmcl1.PhysicalLabNew.Recommend = lab.Recommend;

                return PartialView("ChemicalPhysicalLabShow", chmcl1);

            }

                return PartialView();
        }


        [HttpPost]
        public ActionResult Delete(int Id)
        {

            var boreDrillMethod = Database.Session.Load<BoreDrillMethod>(Id);

            var drillingType = Database.Session.Query<DrillingType>().Where(x => x.BoreDrillMethod.Id == Id).FirstOrDefault<DrillingType>();

            if (boreDrillMethod == null)
            {
                return HttpNotFound();
            }

            var borehole = Database.Session.Query<Models.Borehole>().Where(x => x.Id == boreDrillMethod.Borehole.Id).FirstOrDefault<Models.Borehole>();

            //var boredrillMethd = Database.Session.Query<BoreDrillMethod>().Where(x => x.Borehole.Id == borehole.Id).FirstOrDefault<BoreDrillMethod>();

            var borestrata = Database.Session.Query<BoreStrata>().Where(x => x.Borehole.Id == borehole.Id).FirstOrDefault<BoreStrata>();

            var pumptest = Database.Session.Query<PumpTest>().Where(x => x.Borehole.Id == borehole.Id).FirstOrDefault<PumpTest>();

            var labanalysis = Database.Session.Query<LabAnalysis>().Where(x => x.PumpTest.Id == pumptest.Id).FirstOrDefault<LabAnalysis>();

            var physical = Database.Session.Query<Physical>().Where(x => x.LabAnalysis.Id == labanalysis.Id).FirstOrDefault<Physical>();

            var chemical = Database.Session.Query<Chemical>().Where(x => x.LabAnalysis.Id == labanalysis.Id).FirstOrDefault<Chemical>();

            Database.Session.Delete(chemical);
            Database.Session.Delete(physical);
            Database.Session.Delete(labanalysis);
            Database.Session.Delete(pumptest);
            Database.Session.Delete(borestrata);
            //Database.Session.Delete(drillingType);
           // Database.Session.Delete(boreDrillMethod);
            Database.Session.Delete(borehole);


            return Json(new { success = true, message = "Deleted  Successfully" }, JsonRequestBehavior.AllowGet);

        }

    }


}