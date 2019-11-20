using DDCA.Models;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using DDCA.Areas.Report.DdcaReport;

namespace DDCA.Areas.Report.Controllers
{
    public class BorehoeReportController : Controller
    {
        // GET: Report/BorehoeReport
        public ActionResult BoreholeReport()
        {
            return View();
        }

        public ActionResult SurveyReport()
        {
            return View();
        }

        public ActionResult CarReport()
        {
            return View();
        }

        public ActionResult RigReport()
        {
            return View();
        }

        public ActionResult CmprsrReport()
        {
            return View();
        }

        public ActionResult WaterWellReport()
        {
            return View();
        }

        public ActionResult WaterQualityReport()
        {
            return View();
        }

        public ActionResult CompletionForm(int id)
        {
            WaterWellReportForm waterWellReportForm = new WaterWellReportForm();
            byte[] abytes = waterWellReportForm.PrepareReport(id);

            return File(abytes, "application/pdf");
        }

        public ActionResult WaterQuality(int id)
        {
            WaterQualityReport waterQualityReport = new WaterQualityReport();
            byte[] abytes = waterQualityReport.PrepareReport(id);

            return File(abytes, "application/pdf");
        }

        [HttpPost]
        public ActionResult GetCmprsrServiceList()
        {
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortcolumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];

            List<Models.MachineService> CarServices = Database.Session.Query<Models.MachineService>().Where(x => x.Type == "Compressor").ToList<Models.MachineService>();
            int totalRows = CarServices.Count;


            if (!string.IsNullOrEmpty(searchValue))//filtering
            {
                CarServices = CarServices.Where(x => x.JobDone.ToLower().Contains(searchValue.ToLower()) || x.RegNo.ToLower().Contains(searchValue.ToLower()) || x.JobDone.ToLower().Contains(searchValue.ToLower()) || x.MaterialCost.ToLower().Contains(searchValue.ToLower()) || x.LabourCost.ToLower().Contains(searchValue.ToLower()) || x.Staff.Name.ToLower().Contains(searchValue.ToLower()) || x.StartDate.ToString().Contains(searchValue.ToLower()) || x.EndDate.ToString().ToLower().Contains(searchValue.ToLower())).ToList<Models.MachineService>();
            }

            int totalrowsAfterFiltering = CarServices.Count;
            //sorting
            CarServices = CarServices.OrderBy(sortcolumnName + " " + sortDirection).ToList<Models.MachineService>();

            //paging
            CarServices = CarServices.Skip(start).Take(length).ToList<Models.MachineService>();
            return Json(new { data = CarServices, draw = Request["draw"], recordsTotal = totalRows, recordsFiltered = totalrowsAfterFiltering }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetRigServiceList()
        {
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortcolumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];

            List<Models.MachineService> CarServices = Database.Session.Query<Models.MachineService>().Where(x => x.Type == "Rig").ToList<Models.MachineService>();
            int totalRows = CarServices.Count;


            if (!string.IsNullOrEmpty(searchValue))//filtering
            {
                CarServices = CarServices.Where(x => x.JobDone.ToLower().Contains(searchValue.ToLower()) || x.RegNo.ToLower().Contains(searchValue.ToLower()) || x.JobDone.ToLower().Contains(searchValue.ToLower()) || x.MaterialCost.ToLower().Contains(searchValue.ToLower()) || x.LabourCost.ToLower().Contains(searchValue.ToLower()) || x.Staff.Name.ToLower().Contains(searchValue.ToLower()) || x.StartDate.ToString().Contains(searchValue.ToLower()) || x.EndDate.ToString().ToLower().Contains(searchValue.ToLower())).ToList<Models.MachineService>();
            }

            int totalrowsAfterFiltering = CarServices.Count;
            //sorting
            CarServices = CarServices.OrderBy(sortcolumnName + " " + sortDirection).ToList<Models.MachineService>();

            //paging
            CarServices = CarServices.Skip(start).Take(length).ToList<Models.MachineService>();
            return Json(new { data = CarServices, draw = Request["draw"], recordsTotal = totalRows, recordsFiltered = totalrowsAfterFiltering }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult GetCarServiceList()
        {
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortcolumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];

            List<Models.MachineService> CarServices = Database.Session.Query<Models.MachineService>().Where(x => x.Type == "Car").ToList<Models.MachineService>();
            int totalRows = CarServices.Count;


            if (!string.IsNullOrEmpty(searchValue))//filtering
            {
                CarServices = CarServices.Where(x => x.JobDone.ToLower().Contains(searchValue.ToLower()) || x.RegNo.ToLower().Contains(searchValue.ToLower()) || x.JobDone.ToLower().Contains(searchValue.ToLower()) || x.MaterialCost.ToLower().Contains(searchValue.ToLower()) || x.LabourCost.ToLower().Contains(searchValue.ToLower()) || x.Staff.Name.ToLower().Contains(searchValue.ToLower()) || x.StartDate.ToString().Contains(searchValue.ToLower()) || x.EndDate.ToString().ToLower().Contains(searchValue.ToLower())).ToList<Models.MachineService>();
            }

            int totalrowsAfterFiltering = CarServices.Count;
            //sorting
            CarServices = CarServices.OrderBy(sortcolumnName + " " + sortDirection).ToList<Models.MachineService>();

            //paging
            CarServices = CarServices.Skip(start).Take(length).ToList<Models.MachineService>();
            return Json(new { data = CarServices, draw = Request["draw"], recordsTotal = totalRows, recordsFiltered = totalrowsAfterFiltering }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult GetSurveyList()
        {
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortcolumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];

            List<GeoSurvey> Surveys = Database.Session.Query<GeoSurvey>().ToList<GeoSurvey>();
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
    }
}