using DDCA.Areas.Survey.ViewModels;
using DDCA.Infrastructure;
using DDCA.Models;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;

namespace DDCA.Areas.Survey.Controllers
{
    [SelectedTab("surveys")]
    public class SurveysController : Controller
    {
        public List<Region>  Regions;
        public List<District> Districts;
        public List<Staff> Staffs;
        public SurveyorDrop SurveyorDrop;
        public DropVs DropVs;


        public SurveysController()
        {
            Regions = Database.Session.Query<Region>().ToList();
            Districts = Database.Session.Query<District>().ToList();
            Staffs = Database.Session.Query<Staff>().ToList();
            SurveyorDrop = new SurveyorDrop();
            DropVs = new DropVs();
        }


        public ActionResult Index()
        {  
            return View();
        }

        public ActionResult Show(int Id)
        {
            var geosurvey = Database.Session.Load<GeoSurvey>(Id);
            var surveyProfile = Database.Session.Query<SurveyProfile>().Where(x => x.GeoSurvey.Id == Id).ToList<SurveyProfile>();

            var model = new SurveyView();

            //model.SurveyId = data.surveyId;

            model.Client.Name = geosurvey.Client.Name;
            model.Client.Address = geosurvey.Client.Address;
            model.Client.Region = geosurvey.Client.Region.Name;
            model.Client.Phone = geosurvey.Client.Phone;
            model.Client.Email = geosurvey.Client.Email;


            model.Region = geosurvey.Region.Name;
            model.District = geosurvey.District.Name;
            model.Village = geosurvey.Village;
            model.SurveyorType = geosurvey.SurveyType;
            model.Cost = geosurvey.Cost;
            model.SurveyorName = geosurvey.Staff.Name;
            model.StartDate = geosurvey.StartDate;
            model.EndDate = geosurvey.EndDate;
            model.SiteRecommendation = geosurvey.SiteRecommendation;


            model.ProfileEditor1.Id = surveyProfile[0].Id;
            model.ProfileEditor1.VESPoints = surveyProfile[0].VesPoint.ToString();
            model.ProfileEditor1.Northing = surveyProfile[0].Northing;
            model.ProfileEditor1.Easting = surveyProfile[0].Easting;
            model.ProfileEditor1.Elevation = surveyProfile[0].Elevation;
            model.ProfileEditor1.SurveyMethod = surveyProfile[0].SurveyMethod;
            model.ProfileEditor1.Recommend = surveyProfile[0].Recommend;


            model.ProfileEditor2.Id = surveyProfile[1].Id;
            model.ProfileEditor2.VESPoints = surveyProfile[1].VesPoint.ToString();
            model.ProfileEditor2.Northing = surveyProfile[1].Northing;
            model.ProfileEditor2.Easting = surveyProfile[1].Easting;
            model.ProfileEditor2.Elevation = surveyProfile[1].Elevation;
            model.ProfileEditor2.SurveyMethod = surveyProfile[1].SurveyMethod;
            model.ProfileEditor2.Recommend = surveyProfile[1].Recommend;

            model.ProfileEditor3.Id = surveyProfile[2].Id;
            model.ProfileEditor3.VESPoints = surveyProfile[2].VesPoint.ToString();
            model.ProfileEditor3.Northing = surveyProfile[2].Northing;
            model.ProfileEditor3.Easting = surveyProfile[2].Easting;
            model.ProfileEditor3.Elevation = surveyProfile[2].Elevation;
            model.ProfileEditor3.SurveyMethod = surveyProfile[2].SurveyMethod;
            model.ProfileEditor3.Recommend = surveyProfile[2].Recommend;

            return View(model);
        }

        public ActionResult EditSurvey(int Id)
        {
            var geosurvey = Database.Session.Load<GeoSurvey>(Id);

            ViewBag.CLientRegions = new SelectList(Regions, "Id", "Name", geosurvey.Client.Region.Id.ToString());
            ViewBag.GeosurveyRegions = new SelectList(Regions, "Id", "Name", geosurvey.Region.Id.ToString());
            ViewBag.DistrictLists = new SelectList(GetDistrict(geosurvey.Region.Id), "Id", "Name", geosurvey.District.Id.ToString());
            ViewBag.Staff = new SelectList(Staffs, "Id", "Name", geosurvey.Staff.Id);
            //ViewBag.SurveyTypes1 = new SelectList(SurveyorDrop.surveyorTypes.ToList(), "Id", "Name", SurveyTypeId(surveyProfile[0].SurveyMethod));
            //ViewBag.SurveyTypes2 = new SelectList(SurveyorDrop.surveyorTypes.ToList(), "Id", "Name", SurveyTypeId(surveyProfile[1].SurveyMethod));
            //ViewBag.SurveyTypes3 = new SelectList(SurveyorDrop.surveyorTypes.ToList(), "Id", "Name", SurveyTypeId(surveyProfile[2].SurveyMethod));

            if (geosurvey == null)
                return HttpNotFound();

            var model = new SurveyEdit();

            model.surveyId = geosurvey.Id;
            model.Client.Name = geosurvey.Client.Name;
            model.Client.Address = geosurvey.Client.Address;
            //model.Client.Region = geosurvey.Client.Region.Name;
            model.Client.Phone = geosurvey.Client.Phone;
            model.Client.Email = geosurvey.Client.Email;

            ViewBag.Village = geosurvey.Village;

            //model.Region1 = geosurvey.Region.Name;
            //model.District = geosurvey.District.Name;
            model.Village = geosurvey.Village;
            model.SurveyorType = geosurvey.SurveyType;
            model.Cost = geosurvey.Cost;
            //model.SurveyorName = geosurvey.Staff.Name;
            model.StartDate = geosurvey.StartDate;
            model.EndDate = geosurvey.EndDate;
            model.SiteRecommendation = geosurvey.SiteRecommendation;

            return View(model);
        }

        [HttpPost]
        public ActionResult EditSurveyClient(SurveyEdit data, string prevBtn, string nextBtn)
        {
            //var geosurvey = Database.Session.Load<GeoSurvey>(Id);
            //List<SurveyProfile> surveyProfile = new List<SurveyProfile>();
            var surveyProfile = Database.Session.Query<SurveyProfile>().Where(x => x.GeoSurvey.Id == data.surveyId).ToList<SurveyProfile>();
            ViewBag.SurveyTypes1 = new SelectList(SurveyorDrop.surveyorTypes.ToList(), "Id", "Name", SurveyTypeId(surveyProfile[0].SurveyMethod));
            ViewBag.SurveyTypes2 = new SelectList(SurveyorDrop.surveyorTypes.ToList(), "Id", "Name", SurveyTypeId(surveyProfile[1].SurveyMethod));
            ViewBag.SurveyTypes3 = new SelectList(SurveyorDrop.surveyorTypes.ToList(), "Id", "Name", SurveyTypeId(surveyProfile[2].SurveyMethod));


            var model = new SurveyProfileEdit();

            model.SurveyId = data.surveyId;

            model.ProfileEditor1.Id = surveyProfile[0].Id;
            model.ProfileEditor1.VESPoints = surveyProfile[0].VesPoint.ToString();
            model.ProfileEditor1.Northing = surveyProfile[0].Northing;
            model.ProfileEditor1.Easting = surveyProfile[0].Easting;
            model.ProfileEditor1.Elevation = surveyProfile[0].Elevation ;
            //model.profileeditor1.surveymethod = surveyprofile[0].surveymethod;
            model.ProfileEditor1.Recommend = surveyProfile[0].Recommend;

            model.ProfileEditor1.SiteRecommendation = data.SiteRecommendation;


            model.ProfileEditor2.Id = surveyProfile[1].Id;
            model.ProfileEditor2.VESPoints = surveyProfile[1].VesPoint.ToString();
            model.ProfileEditor2.Northing = surveyProfile[1].Northing;
            model.ProfileEditor2.Easting = surveyProfile[1].Easting;
            model.ProfileEditor2.Elevation = surveyProfile[1].Elevation;
            //model.profileeditor1.surveymethod = surveyprofile[0].surveymethod;
            model.ProfileEditor2.Recommend = surveyProfile[1].Recommend;

            model.ProfileEditor3.Id = surveyProfile[2].Id;
            model.ProfileEditor3.VESPoints = surveyProfile[2].VesPoint.ToString();
            model.ProfileEditor3.Northing = surveyProfile[2].Northing;
            model.ProfileEditor3.Easting = surveyProfile[2].Easting;
            model.ProfileEditor3.Elevation = surveyProfile[2].Elevation;
            //model.profileeditor1.surveymethod = surveyprofile[0].surveymethod;
            model.ProfileEditor3.Recommend = surveyProfile[2].Recommend;

            SurveyWizard obj = GetSurvey();

            if (nextBtn != null)
            {
                ViewBag.VesPoints = new SelectList(DropVs.VsPoints.ToList(), "Id", "Number");
                ViewBag.SurveyTypes = new SelectList(SurveyorDrop.surveyorTypes.ToList(), "Id", "Name");

                ViewBag.Village = data.Village;


                var client = new Client();
                var geosurvey = new GeoSurvey();

                int regionClientId = Convert.ToInt32(data.Client.Region);
                int regionGeosurveyId = Convert.ToInt32(data.Region);
                int geosurveyDistrictId = Convert.ToInt32(data.District);
                int geosurveyStaffId = Convert.ToInt32(data.SurveyorName);

                if (geosurveyStaffId == 0)
                    geosurveyStaffId = 5;

                client.Name = data.Client.Name;
                client.Address = data.Client.Address;
                client.Region = Regions.Where(x => x.Id == regionClientId).FirstOrDefault<Region>();
                client.Phone = data.Client.Phone;
                client.Email = data.Client.Email;

                geosurvey.Region = Regions.Where(x => x.Id == regionGeosurveyId).FirstOrDefault<Region>();
                geosurvey.District = Districts.Where(x => x.Id == geosurveyDistrictId).FirstOrDefault<District>();
                geosurvey.Village = data.Village;
                geosurvey.SurveyType = data.SurveyorType;
                geosurvey.Cost = data.Cost;
                geosurvey.Staff = Staffs.Where(x => x.Id == geosurveyStaffId).FirstOrDefault<Staff>();
                geosurvey.StartDate = data.StartDate;
                geosurvey.EndDate = data.EndDate;

                obj.Client = client;
                obj.GeoSurvey = geosurvey;

                return PartialView("SurveyProfileEdit", model);

            }


                return PartialView();
        }

        [HttpPost]
        public ActionResult SurveyProfileEdit(SurveyProfileEdit data, string prevBtn, string nextBtn)
        {
            SurveyWizard obj = GetSurvey();
            ViewBag.Village = obj.GeoSurvey.Village;
            ViewBag.VesPoints = new SelectList(DropVs.VsPoints.ToList(), "Id", "Number");
            ViewBag.SurveyTypes = new SelectList(SurveyorDrop.surveyorTypes.ToList(), "Id", "Name");

            var geosurvey1 = Database.Session.Load<GeoSurvey>(data.SurveyId);

            if (nextBtn != null)
            {

                if (ModelState.IsValid)
                {
                    var surveyProfile1 = Database.Session.Load<SurveyProfile>(data.ProfileEditor1.Id);
                    var surveyProfile2 = Database.Session.Load<SurveyProfile>(data.ProfileEditor2.Id);
                    var surveyProfile3 = Database.Session.Load<SurveyProfile>(data.ProfileEditor3.Id);

                    int surveyMethod1 = Convert.ToInt32(data.ProfileEditor1.SurveyMethod);
                    int surveyMethod2 = Convert.ToInt32(data.ProfileEditor2.SurveyMethod);
                    int surveyMethod3 = Convert.ToInt32(data.ProfileEditor3.SurveyMethod);

                    surveyProfile1.VesPoint = Convert.ToInt32(data.ProfileEditor1.VESPoints);
                    surveyProfile1.Northing = data.ProfileEditor1.Northing;
                    surveyProfile1.Easting = data.ProfileEditor1.Easting;
                    surveyProfile1.Elevation = data.ProfileEditor1.Elevation;
                    surveyProfile1.SurveyMethod = SurveyTypeName(surveyMethod1);
                    surveyProfile1.Recommend = data.ProfileEditor1.Recommend;

                    surveyProfile2.VesPoint = Convert.ToInt32(data.ProfileEditor2.VESPoints);
                    surveyProfile2.Northing = data.ProfileEditor2.Northing;
                    surveyProfile2.Easting = data.ProfileEditor2.Easting;
                    surveyProfile2.Elevation = data.ProfileEditor2.Elevation;
                    surveyProfile2.SurveyMethod = SurveyTypeName(surveyMethod2);
                    surveyProfile2.Recommend = data.ProfileEditor2.Recommend;

                    surveyProfile3.VesPoint = Convert.ToInt32(data.ProfileEditor3.VESPoints);
                    surveyProfile3.Northing = data.ProfileEditor3.Northing;
                    surveyProfile3.Easting = data.ProfileEditor3.Easting;
                    surveyProfile3.Elevation = data.ProfileEditor3.Elevation;
                    surveyProfile3.SurveyMethod = SurveyTypeName(surveyMethod3);
                    surveyProfile3.Recommend = data.ProfileEditor3.Recommend;

                    obj.GeoSurvey.SiteRecommendation = data.ProfileEditor1.SiteRecommendation;

                    var client = obj.Client;
                    var geosurvey = obj.GeoSurvey;

                    geosurvey1.Client.Name = client.Name;
                    geosurvey1.Client.Address = client.Address;
                    geosurvey1.Client.Phone = client.Phone;
                    geosurvey1.Client.Email = client.Email;
                    geosurvey1.Client.Region = client.Region;

                    geosurvey1.Region = geosurvey.Region;
                    geosurvey1.District = geosurvey.District;
                    geosurvey1.Village = geosurvey.Village;
                    geosurvey1.SurveyType = geosurvey.SurveyType;
                    geosurvey1.Cost = geosurvey.Cost;
                    geosurvey1.Staff = geosurvey.Staff;
                    geosurvey1.StartDate = geosurvey.StartDate;
                    geosurvey1.EndDate = geosurvey.EndDate;
                    geosurvey1.SiteRecommendation = geosurvey.SiteRecommendation;

                   
                    Database.Session.Update(geosurvey1);
                    Database.Session.Update(surveyProfile1);
                    Database.Session.Update(surveyProfile2);
                    Database.Session.Update(surveyProfile3);

                    ViewBag.ActionMethod = "Edit";

                    RemoveSurvey();
                    return PartialView("Success");
                }
            }
            return PartialView();
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

        public ActionResult New()
        {

             //var regions = Database.Session.Query<Region>().ToList();
            //var staff = Database.Session.Query<Staff>().ToList();
            ViewBag.RegionList1 = new SelectList(Regions, "Id", "Name");
            ViewBag.RegionList2 = new SelectList(Regions, "Id", "Name");
            ViewBag.Staff = new SelectList(Staffs, "Id", "Name");
            return View();
        }

        public ActionResult GetDistrictList(int RId)
        {
            var districtList = Districts.Where(d => d.Region.Id == RId).ToList();
            ViewBag.districtLists = new SelectList(districtList, "Id", "Name");

            return PartialView("_DistrictDisplay");
        }

        public List<District> GetDistrict(int RId)
        {
            var districtList = Districts.Where(d => d.Region.Id == RId).ToList();
            return districtList;
        }

            private SurveyWizard GetSurvey()
        {
            if (Session["survey"] == null)
            {
                Session["survey"] = new SurveyWizard
                {
                    Client = new Client(),
                    GeoSurvey = new GeoSurvey(),
                    SurveyProfile = new SurveyProfile()
                };
            }
            return (SurveyWizard)Session["survey"];
        }

        private void RemoveSurvey()
        {
            Session.Remove("survey");
        }

        [HttpPost]
        public ActionResult ClientSurvey(SurveysNew data, string prevBtn, string nextBtn)
        {
            //var regions = Database.Session.Query<Region>().ToList();
            ViewBag.RegionList1 = new SelectList(Regions, "Id", "Name");
            ViewBag.RegionList2 = new SelectList(Regions, "Id", "Name");

            //data.SurveyorType = "DDCA";

            //var points = new DropVs();
            //var surveyTypes = new SurveyorDrop();
            if (nextBtn != null)
            {
                if (ModelState.IsValid)
                {
                    ViewBag.VesPoints = new SelectList(DropVs.VsPoints.ToList(), "Id", "Number");
                    ViewBag.SurveyTypes = new SelectList(SurveyorDrop.surveyorTypes.ToList(), "Id", "Name");
                    ViewBag.Village = data.Village;
                    SurveyWizard obj = GetSurvey();

                    //var regionClient = new Region();
                    //var regionGeosurvey = new Region();
                    //var geosurveyDistrict = new District();
                    var client = new Client();
                    var geosurvey = new GeoSurvey();
                    //var geosurveyStaff = new Staff();

                    int regionClientId = Convert.ToInt32(data.Client.Region);
                    int regionGeosurveyId = Convert.ToInt32(data.Region);
                    int geosurveyDistrictId = Convert.ToInt32(data.District);
                     //geosurveyDistrict.Region = regionGeosurvey;
                    int geosurveyStaffId = Convert.ToInt32(data.SurveyorName);

                    if (geosurveyStaffId == 0)
                        geosurveyStaffId = 5;

                    client.Name = data.Client.Name;
                    client.Address = data.Client.Address;
                    client.Region = Regions.Where(x => x.Id == regionClientId).FirstOrDefault<Region>();
                    client.Phone = data.Client.Phone;
                    client.Email = data.Client.Email;

                    geosurvey.Region = Regions.Where(x => x.Id == regionGeosurveyId).FirstOrDefault<Region>();
                    geosurvey.District = Districts.Where(x => x.Id == geosurveyDistrictId).FirstOrDefault<District>();
                    geosurvey.Village = data.Village;
                    geosurvey.SurveyType = data.SurveyorType;
                    geosurvey.Cost = data.Cost;
                    geosurvey.Staff = Staffs.Where(x => x.Id == geosurveyStaffId).FirstOrDefault<Staff>();
                    geosurvey.StartDate = data.StartDate;
                    geosurvey.EndDate = data.EndDate;
                    geosurvey.Status = "U";

                    obj.Client = client;
                    obj.GeoSurvey = geosurvey;

                    return PartialView("SurveyProfile");
                }
            }
            return PartialView();
        }

        [HttpPost]
        public ActionResult SurveyProfile(SurveyProfileInfo data, string prevBtn, string nextBtn)
        {
            var points = new DropVs();
            //var surveyTypes = new SurveyorDrop();
            SurveyWizard obj = GetSurvey();
            ViewBag.Village = obj.GeoSurvey.Village;
            if (prevBtn != null)
            {
                //var regions = Database.Session.Query<Region>().ToList();
                //ViewBag.RegionList1 = new SelectList(regions, "Id", "Name");
                //var selectList = new SelectList(regions, "Id", "Name");
                //ViewBag.RegionList1 = SetSelectedValue(selectList, obj.Client.Region.Name);
                //ViewBag.RegionList2 = SetSelectedValue(selectList, obj.GeoSurvey.Region.Name);
                //ViewBag.district = GetDistrictList(Convert.ToInt32(obj.GeoSurvey.Region.Name));

                //SurveysNew sn = new SurveysNew();
                //sn.Client.Name = obj.Client.Name;
                //sn.Client.Address = obj.Client.Address;
                //sn.Client.Region =  obj.Client.Region;
                //sn.Client.Phone = obj.Client.Phone;
                //sn.Client.Email = obj.Client.Phone;
                //sn.Region = obj.GeoSurvey.Region;
                //sn.District = obj.GeoSurvey.District.Name;
                //sn.SurveyorType = obj.GeoSurvey.SurveyType;
                //sn.Cost = obj.GeoSurvey.Cost;
                //sn.SurveyorName = obj.GeoSurvey.Staff.Name;
               //sn.StartDate = obj.GeoSurvey.StartDate;
                //sn.EndDate = obj.GeoSurvey.EndDate;
                
                return PartialView("ClientSurvey");
            }
            if (nextBtn != null)
            {
                ViewBag.VesPoints = new SelectList(DropVs.VsPoints.ToList(), "Id", "Number");
                ViewBag.SurveyTypes = new SelectList(SurveyorDrop.surveyorTypes.ToList(), "Id", "Name");
                

                if (ModelState.IsValid)
                {
                    var surveyProfile1 = new SurveyProfile();
                    var surveyProfile2 = new SurveyProfile();
                    var surveyProfile3 = new SurveyProfile();

                    int surveyMethod1 = Convert.ToInt32(data.ProfileEditor1.SurveyMethod);
                    int surveyMethod2 = Convert.ToInt32(data.ProfileEditor2.SurveyMethod);
                    int surveyMethod3 = Convert.ToInt32(data.ProfileEditor3.SurveyMethod);

                    surveyProfile1.VesPoint = Convert.ToInt32(data.ProfileEditor1.VESPoints);
                    surveyProfile1.Northing = data.ProfileEditor1.Northing;
                    surveyProfile1.Easting = data.ProfileEditor1.Easting;
                    surveyProfile1.Elevation = data.ProfileEditor1.Elevation;
                    surveyProfile1.SurveyMethod = SurveyTypeName(surveyMethod1);
                    surveyProfile1.Recommend = data.ProfileEditor1.Recommend;

                    surveyProfile2.VesPoint = Convert.ToInt32(data.ProfileEditor2.VESPoints);
                    surveyProfile2.Northing = data.ProfileEditor2.Northing;
                    surveyProfile2.Easting = data.ProfileEditor2.Easting;
                    surveyProfile2.Elevation = data.ProfileEditor2.Elevation;
                    surveyProfile2.SurveyMethod = SurveyTypeName(surveyMethod2);
                    surveyProfile2.Recommend = data.ProfileEditor2.Recommend;

                    surveyProfile3.VesPoint = Convert.ToInt32(data.ProfileEditor3.VESPoints);
                    surveyProfile3.Northing = data.ProfileEditor3.Northing;
                    surveyProfile3.Easting = data.ProfileEditor3.Easting;
                    surveyProfile3.Elevation = data.ProfileEditor3.Elevation;
                    surveyProfile3.SurveyMethod = SurveyTypeName(surveyMethod3);
                    surveyProfile3.Recommend = data.ProfileEditor3.Recommend;

                    obj.GeoSurvey.SiteRecommendation = data.ProfileEditor1.SiteRecommendation;

                    var client = obj.Client;
                    var geosurvey = obj.GeoSurvey;

                    client.AddDate = DateTime.UtcNow;
                    Database.Session.Save(client);
                    geosurvey.Client = client;

                    Database.Session.Save(geosurvey);

                    surveyProfile1.Resistivity = "123"; 
                    surveyProfile1.GeoSurvey = geosurvey;
                    Database.Session.Save(surveyProfile1);

                    surveyProfile2.Resistivity = "123";
                    surveyProfile2.GeoSurvey = geosurvey;
                    Database.Session.Save(surveyProfile2);

                    surveyProfile3.Resistivity = "123";
                    surveyProfile3.GeoSurvey = geosurvey;
                    Database.Session.Save(surveyProfile3);

                    ViewBag.ActionMethod = "New";

                    RemoveSurvey();
                    return PartialView("Success");
                }
            }
            return PartialView();
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {

            var geosurvey = Database.Session.Load<GeoSurvey>(Id);

            if(geosurvey == null)
            {
                return HttpNotFound();
            }

            var surveyProfile = Database.Session.Query<SurveyProfile>().Where(x => x.GeoSurvey.Id == Id).ToList<SurveyProfile>();

            Database.Session.Delete(surveyProfile[0]);
            Database.Session.Delete(surveyProfile[1]);
            Database.Session.Delete(surveyProfile[2]);
            Database.Session.Delete(geosurvey);


            return Json(new { success = true, message = "Deleted  Successfully" }, JsonRequestBehavior.AllowGet);
            
        }

        public static SelectList SetSelectedValue(SelectList list, string value)
        {
            if (value != null)
            {
                var selected = list.Where(x => x.Text == value).First();
                selected.Selected = true;
                return list;
            }
            return list;
        }

        public string SurveyTypeName(int id)
        {
            var surveyType = SurveyorDrop.surveyorTypes.Where(x => x.Id == id).FirstOrDefault<SurveyorType>();
            return surveyType.Name;
        }


        public int SurveyTypeId(string name)
        {
            var surveyType = SurveyorDrop.surveyorTypes.Where(x => x.Name == name).FirstOrDefault<SurveyorType>();
            return surveyType.Id;
        }

    }
}