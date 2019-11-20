using System.Web.Mvc;

namespace DDCA.Areas.MachineService
{
    public class MachineServiceAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MachineService";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MachineService_default",
                "MachineService/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}