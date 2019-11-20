using DDCA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate.Linq;
namespace DDCA.Controllers
{
    public class HomeController : Controller
    {
      

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetSurveyList()
        {

            var Surveys = Database.Session.Query<GeoSurvey>().ToList<GeoSurvey>();
            return Json(new { data = Surveys }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}