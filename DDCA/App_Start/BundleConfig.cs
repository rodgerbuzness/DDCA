using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace DDCA.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/datetime")
                .Include("~/Scripts/moment.js", "~/Scripts/bootstrap-datetimepicker.js"));

            bundles.Add(new StyleBundle("~/admin/styles")
                .Include("~/content/styles/bootstrap.css", "~/Content/bootstrap-datetimepicker.css")
                .Include("~/content/styles/admin.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new StyleBundle("~/styles")
                .Include("~/content/styles/bootstrap.css")
                .Include("~/content/styles/Site.css"));

            bundles.Add(new ScriptBundle("~/scripts/admin")
                .Include("~/scripts/jquery-3.3.1.js")
                .Include("~/Scripts/moment.js")
                .Include("~/Scripts/bootstrap-datetimepicker.js")
                .Include("~/scripts/jquery.validate.js")
                .Include("~/scripts/jquery.validate.unobtrusive.js")
                .Include("~/scripts/bootstrap.js")
                .Include("~/areas/admin/scripts/form.js")
                .Include("~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/scripts")
                .Include("~/scripts/jquery-3.3.1.js")
                .Include("~/scripts/jquery.validate.js")
                .Include("~/scripts/jquery.validate.unobtrusive.js")
                .Include("~/scripts/bootstrap.js")
                .Include("~/scripts/MenuPopup1.js"));
        }
    }
}