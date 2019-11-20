using DDCA.Models;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using DDCA.Areas.MachineService.ViewModels;

namespace DDCA.Areas.MachineService.Controllers
{
    public class MachineServicesController : Controller
    {
        public List<Staff> Staffs;

        public MachineServicesController()
        {
            Staffs = Database.Session.Query<Staff>().ToList();
        }

        // GET: MachineService/MachineServices
        public ActionResult IndexCar()
        {
            return View();
        }

        public ActionResult ServiceNewCar()
        {
            return View();
        }

        public ActionResult ServiceCarNew(int id)
        {

            var Car = Database.Session.Query<Car>().Where(x => x.Id == id).FirstOrDefault<Car>();
            ViewBag.CarNo = Car.CarNo;
            CarServiceNew carServiceNew = new CarServiceNew();
            carServiceNew.CarId = id;
            carServiceNew.RegNo = Car.CarNo;

            ViewBag.watu = new SelectList(Staffs, "Id", "Name");

            return View(carServiceNew);
        }

        public ActionResult MachineServiceCar(CarServiceNew carServiceNew)
        {
            if (ModelState.IsValid)
            {
                var machine = Database.Session.Query<Models.Machine>().Where(x => x.CarId == carServiceNew.CarId).FirstOrDefault<Models.Machine>();

                Models.MachineService service = new Models.MachineService();
                service.Machine = machine;
                service.Staff.Id = Convert.ToInt32(carServiceNew.InchargeName);
                service.MaterialCost = carServiceNew.MaterialCost;
                service.LabourCost = carServiceNew.LabourCost;
                service.StartDate = carServiceNew.StartDate;
                service.EndDate = carServiceNew.EndDate;
                service.JobDone = carServiceNew.JobDone;
                service.RegNo = carServiceNew.RegNo;
                service.Type = "Car";

                Database.Session.Save(service);

                ViewBag.ActionMethod = "New";

                return PartialView("SuccessCar");
            }
            

            return PartialView();
        }

        public ActionResult CarEdit(int id)
        {
            var carService = Database.Session.Query<Models.MachineService>().Where(x => x.Id == id).FirstOrDefault<Models.MachineService>();

            if (carService == null)
                return HttpNotFound();

            CarServiceNew carServiceEdit = new CarServiceNew();

            carServiceEdit.CarId = carService.Machine.Id;
            carServiceEdit.RegNo = carService.RegNo;
            ViewBag.watu = new SelectList(Staffs, "Id", "Name", carService.Staff.Id.ToString());
            carServiceEdit.StartDate = carService.StartDate;
            carServiceEdit.EndDate = carService.EndDate;
            carServiceEdit.LabourCost = carService.LabourCost;
            carServiceEdit.MaterialCost = carService.MaterialCost;
            carServiceEdit.JobDone = carService.JobDone;
            carServiceEdit.Id = carService.Id;


            ViewBag.CarNo = carService.RegNo;

            return View(carServiceEdit);
        }

        [HttpPost]
        public ActionResult MachineServiceCarEdit(CarServiceNew carServiceEdit)
        {
            if (ModelState.IsValid)
            {
                Models.MachineService service = new Models.MachineService();

                service.Id = carServiceEdit.Id;
                service.Staff.Id = Convert.ToInt32(carServiceEdit.InchargeName);
                service.MaterialCost = carServiceEdit.MaterialCost;
                service.LabourCost = carServiceEdit.LabourCost;
                service.StartDate = carServiceEdit.StartDate;
                service.EndDate = carServiceEdit.EndDate;
                service.JobDone = carServiceEdit.JobDone;
                service.Machine.Id = carServiceEdit.CarId;
                service.RegNo = carServiceEdit.RegNo;
                service.Type = "Car";

                Database.Session.SaveOrUpdate(service);

                ViewBag.ActionMethod = "Edit";

                return PartialView("SuccessCar");
            }

            return PartialView();
        }

        public ActionResult ShowCar(int id)
        {
            var carService = Database.Session.Query<Models.MachineService>().Where(x => x.Id == id).FirstOrDefault<Models.MachineService>();

            CarServiceNew carView = new CarServiceNew();

            carView.InchargeName = carService.Staff.Name;
            carView.MaterialCost = carService.MaterialCost;
            carView.LabourCost = carService.LabourCost;
            carView.StartDate = carService.StartDate;
            carView.EndDate = carService.EndDate;
            carView.JobDone = carService.JobDone;
            carView.RegNo = carService.RegNo;

            return View(carView);
        }

        public ActionResult ServiceNewRig()
        {
            return View();
        }

        public ActionResult ServiceRigNew(int id)
        {

            var rig = Database.Session.Query<Rig>().Where(x => x.Id == id).FirstOrDefault<Rig>();
            ViewBag.RigNo = rig.RigNo;
            RigServiceNew rigServiceNew = new RigServiceNew();
            rigServiceNew.RigId = id;
            rigServiceNew.RegNo = rig.RigNo;

            ViewBag.watu = new SelectList(Staffs, "Id", "Name");

            return View(rigServiceNew);
        }

        public ActionResult MachineServiceRig(RigServiceNew rigServiceNew)
        {
            if (ModelState.IsValid)
            {
                var machine = Database.Session.Query<Models.Machine>().Where(x => x.RigId == rigServiceNew.RigId).FirstOrDefault<Models.Machine>();

                Models.MachineService service = new Models.MachineService();
                service.Machine = machine;
                service.Staff.Id = Convert.ToInt32(rigServiceNew.InchargeName);
                service.MaterialCost = rigServiceNew.MaterialCost;
                service.LabourCost = rigServiceNew.LabourCost;
                service.StartDate = rigServiceNew.StartDate;
                service.EndDate = rigServiceNew.EndDate;
                service.JobDone = rigServiceNew.JobDone;
                service.RegNo = rigServiceNew.RegNo;
                service.Type = "Rig";

                Database.Session.Save(service);

                ViewBag.ActionMethod = "New";

                return PartialView("SuccessRig");
            }


            return PartialView();
        }

        public ActionResult IndexRig()
        {
            return View();
        }

        public ActionResult RigEdit(int id)
        {
            var rigService = Database.Session.Query<Models.MachineService>().Where(x => x.Id == id).FirstOrDefault<Models.MachineService>();

            if (rigService == null)
                return HttpNotFound();

            RigServiceNew rigServiceEdit = new RigServiceNew();

            rigServiceEdit.RigId = rigService.Machine.Id;
            rigServiceEdit.RegNo = rigService.RegNo;
            ViewBag.watu = new SelectList(Staffs, "Id", "Name", rigService.Staff.Id.ToString());
            rigServiceEdit.StartDate = rigService.StartDate;
            rigServiceEdit.EndDate = rigService.EndDate;
            rigServiceEdit.LabourCost = rigService.LabourCost;
            rigServiceEdit.MaterialCost = rigService.MaterialCost;
            rigServiceEdit.JobDone = rigService.JobDone;
            rigServiceEdit.Id = rigService.Id;


            ViewBag.RigNo = rigService.RegNo;

            return View(rigServiceEdit);
        }

        [HttpPost]
        public ActionResult MachineServiceRigEdit(RigServiceNew rigServiceEdit)
        {
            if (ModelState.IsValid)
            {
                Models.MachineService service = new Models.MachineService();

                service.Id = rigServiceEdit.Id;
                service.Staff.Id = Convert.ToInt32(rigServiceEdit.InchargeName);
                service.MaterialCost = rigServiceEdit.MaterialCost;
                service.LabourCost = rigServiceEdit.LabourCost;
                service.StartDate = rigServiceEdit.StartDate;
                service.EndDate = rigServiceEdit.EndDate;
                service.JobDone = rigServiceEdit.JobDone;
                service.Machine.Id = rigServiceEdit.RigId;
                service.RegNo = rigServiceEdit.RegNo;
                service.Type = "Rig";

                Database.Session.SaveOrUpdate(service);

                ViewBag.ActionMethod = "Edit";

                return PartialView("SuccessRig");
            }

            return PartialView();
        }

        public ActionResult ShowRig(int id) 
        {
            var rigService = Database.Session.Query<Models.MachineService>().Where(x => x.Id == id).FirstOrDefault<Models.MachineService>();

            RigServiceNew rigView = new RigServiceNew();

            rigView.InchargeName = rigService.Staff.Name;
            rigView.MaterialCost = rigService.MaterialCost;
            rigView.LabourCost = rigService.LabourCost;
            rigView.StartDate = rigService.StartDate;
            rigView.EndDate = rigService.EndDate;
            rigView.JobDone = rigService.JobDone;
            rigView.RegNo = rigService.RegNo;

            return View(rigView);
        }

        public ActionResult ServiceNewCompressor()
        {
            return View();
        }

        public ActionResult ServiceCompressorNew(int id)
        {

            var cmprsr = Database.Session.Query<Compressor>().Where(x => x.Id == id).FirstOrDefault<Compressor>();

            ViewBag.ComprsrNo = cmprsr.CompressorNo;
            CmprsrServiceNew CmprsrServiceNew = new CmprsrServiceNew();
            CmprsrServiceNew.CmprsrId = id;
            CmprsrServiceNew.RegNo = cmprsr.CompressorNo;

            ViewBag.watu = new SelectList(Staffs, "Id", "Name");

            return View(CmprsrServiceNew);
        }

        public ActionResult MachineServiceCmprsr(CmprsrServiceNew cmprsrServiceNew)
        {
            if (ModelState.IsValid)
            {
                var machine = Database.Session.Query<Models.Machine>().Where(x => x.CompressorId == cmprsrServiceNew.CmprsrId).FirstOrDefault<Models.Machine>();

                Models.MachineService service = new Models.MachineService();
                service.Machine = machine;
                service.Staff.Id = Convert.ToInt32(cmprsrServiceNew.InchargeName);
                service.MaterialCost = cmprsrServiceNew.MaterialCost;
                service.LabourCost = cmprsrServiceNew.LabourCost;
                service.StartDate = cmprsrServiceNew.StartDate;
                service.EndDate = cmprsrServiceNew.EndDate;
                service.JobDone = cmprsrServiceNew.JobDone;
                service.RegNo = cmprsrServiceNew.RegNo;
                service.Type = "Compressor";

                Database.Session.Save(service);

                ViewBag.ActionMethod = "New";

                return PartialView("SuccessCmprsr");
            }

            return PartialView();
        }

        public ActionResult IndexCmprsr()
        {
            return View();
        }

        public ActionResult CmprsrEdit(int id)
        {
            var cmprsrService = Database.Session.Query<Models.MachineService>().Where(x => x.Id == id).FirstOrDefault<Models.MachineService>();

            if (cmprsrService == null)
                return HttpNotFound();

            CmprsrServiceNew cmprsrServiceEdit = new CmprsrServiceNew();

            cmprsrServiceEdit.CmprsrId = cmprsrService.Machine.Id;
            cmprsrServiceEdit.RegNo = cmprsrService.RegNo;
            ViewBag.watu = new SelectList(Staffs, "Id", "Name", cmprsrService.Staff.Id.ToString());
            cmprsrServiceEdit.StartDate = cmprsrService.StartDate;
            cmprsrServiceEdit.EndDate = cmprsrService.EndDate;
            cmprsrServiceEdit.LabourCost = cmprsrService.LabourCost;
            cmprsrServiceEdit.MaterialCost = cmprsrService.MaterialCost;
            cmprsrServiceEdit.JobDone = cmprsrService.JobDone;
            cmprsrServiceEdit.Id = cmprsrService.Id;


            ViewBag.ComprsrNo = cmprsrService.RegNo;

            return View(cmprsrServiceEdit);
        }

        [HttpPost]
        public ActionResult MachineServiceCmprsrEdit(CmprsrServiceNew cmprsrServiceEdit)
        {
            if (ModelState.IsValid)
            {
                Models.MachineService service = new Models.MachineService();

                service.Id = cmprsrServiceEdit.Id;
                service.Staff.Id = Convert.ToInt32(cmprsrServiceEdit.InchargeName);
                service.MaterialCost = cmprsrServiceEdit.MaterialCost;
                service.LabourCost = cmprsrServiceEdit.LabourCost;
                service.StartDate = cmprsrServiceEdit.StartDate;
                service.EndDate = cmprsrServiceEdit.EndDate;
                service.JobDone = cmprsrServiceEdit.JobDone;
                service.Machine.Id = cmprsrServiceEdit.CmprsrId;
                service.RegNo = cmprsrServiceEdit.RegNo;
                service.Type = "Compressor";

                Database.Session.SaveOrUpdate(service);

                ViewBag.ActionMethod = "Edit";

                return PartialView("SuccessCmprsr");
            }

            return PartialView();
        }

        public ActionResult ShowCmprsr(int id)
        {
            var cmprsrService = Database.Session.Query<Models.MachineService>().Where(x => x.Id == id).FirstOrDefault<Models.MachineService>();

            CmprsrServiceNew cmprsrView = new CmprsrServiceNew();

            cmprsrView.InchargeName = cmprsrService.Staff.Name;
            cmprsrView.MaterialCost = cmprsrService.MaterialCost;
            cmprsrView.LabourCost = cmprsrService.LabourCost;
            cmprsrView.StartDate = cmprsrService.StartDate;
            cmprsrView.EndDate = cmprsrService.EndDate;
            cmprsrView.JobDone = cmprsrService.JobDone;
            cmprsrView.RegNo = cmprsrService.RegNo;

            return View(cmprsrView);
        }

        [HttpPost]
        public ActionResult GetComprsrList()
        {
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortcolumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];

            List<Compressor> Compresrs = Database.Session.Query<Compressor>().ToList<Compressor>();
            int totalRows = Compresrs.Count;


            if (!string.IsNullOrEmpty(searchValue))//filtering
            {
                Compresrs = Compresrs.Where(x => x.CompressorNo.ToLower().Contains(searchValue.ToLower()) || x.Model.ToLower().Contains(searchValue.ToLower()) || x.Region.Name.ToLower().Contains(searchValue.ToLower()) || x.District.Name.ToLower().Contains(searchValue.ToLower()) || x.CompressorType.ToLower().Contains(searchValue.ToLower())).ToList<Compressor>();
            }

            int totalrowsAfterFiltering = Compresrs.Count;
            //sorting
            Compresrs = Compresrs.OrderBy(sortcolumnName + " " + sortDirection).ToList<Compressor>();

            //paging
            Compresrs = Compresrs.Skip(start).Take(length).ToList<Compressor>();
            return Json(new { data = Compresrs, draw = Request["draw"], recordsTotal = totalRows, recordsFiltered = totalrowsAfterFiltering }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetCarList()
        {
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortcolumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];

            List<Car> Cars = Database.Session.Query<Car>().ToList<Car>();
            int totalRows = Cars.Count;


            if (!string.IsNullOrEmpty(searchValue))//filtering
            {
                Cars = Cars.Where(x => x.CarNo.ToLower().Contains(searchValue.ToLower()) || x.Model.ToLower().Contains(searchValue.ToLower()) || x.Chasis.ToLower().Contains(searchValue.ToLower()) || x.Engine.ToLower().Contains(searchValue.ToLower())).ToList<Car>();
            }

            int totalrowsAfterFiltering = Cars.Count;
            //sorting
            Cars = Cars.OrderBy(sortcolumnName + " " + sortDirection).ToList<Car>();

            //paging
            Cars = Cars.Skip(start).Take(length).ToList<Car>();
            return Json(new { data = Cars, draw = Request["draw"], recordsTotal = totalRows, recordsFiltered = totalrowsAfterFiltering }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetRigList()
        {
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortcolumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];

            List<Rig> Rigs = Database.Session.Query<Rig>().ToList<Rig>();
            int totalRows = Rigs.Count;


            if (!string.IsNullOrEmpty(searchValue))//filtering
            {
                Rigs = Rigs.Where(x => x.RigNo.ToLower().Contains(searchValue.ToLower()) || x.Model.ToLower().Contains(searchValue.ToLower()) || x.Region.Name.ToLower().Contains(searchValue.ToLower()) || x.District.Name.ToLower().Contains(searchValue.ToLower()) || x.RigType.ToLower().Contains(searchValue.ToLower()) || x.RigState.ToLower().Contains(searchValue.ToLower())).ToList<Rig>();
            }

            int totalrowsAfterFiltering = Rigs.Count;
            //sorting
            Rigs = Rigs.OrderBy(sortcolumnName + " " + sortDirection).ToList<Rig>();

            //paging
            Rigs = Rigs.Skip(start).Take(length).ToList<Rig>();
            return Json(new { data = Rigs, draw = Request["draw"], recordsTotal = totalRows, recordsFiltered = totalrowsAfterFiltering }, JsonRequestBehavior.AllowGet);
        }

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

        public ActionResult DeleteCar(int id)
        {
            var carService = Database.Session.Query<Models.MachineService>().Where(x => x.Id == id).FirstOrDefault<Models.MachineService>();

            if (carService != null)
                Database.Session.Delete(carService);

            return Json(new { success = true, message = "Deleted  Successfully" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteRig(int id)
        {
            var rigService = Database.Session.Query<Models.MachineService>().Where(x => x.Id == id).FirstOrDefault<Models.MachineService>();

            if (rigService != null)
                Database.Session.Delete(rigService);

            return Json(new { success = true, message = "Deleted  Successfully" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteCmprsr(int id)
        {
            var cmprsrService = Database.Session.Query<Models.MachineService>().Where(x => x.Id == id).FirstOrDefault<Models.MachineService>();

            if (cmprsrService != null)
                Database.Session.Delete(cmprsrService);

            return Json(new { success = true, message = "Deleted  Successfully" }, JsonRequestBehavior.AllowGet);
        }

    }
}