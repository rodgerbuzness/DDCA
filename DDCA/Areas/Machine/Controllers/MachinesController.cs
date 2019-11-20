using DDCA.Areas.Machine.ViewModel;
using DDCA.Infrastructure;
using DDCA.Models;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;

namespace DDCA.Areas.Machine.Controllers
{
    public class MachinesController : Controller
    {
        public StatusDrop Statuses;
        public List<Staff> Staffs;
        public List<Region> Regions;
        public List<District> Districts;

        public MachinesController()
        {
            Statuses = new StatusDrop();
            Staffs = Database.Session.Query<Staff>().ToList();
            Regions = Database.Session.Query<Region>().ToList();
            Districts = Database.Session.Query<District>().ToList();
        }


        // GET: Machine/Machines
        public ActionResult Index()
        {
            return View();
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


        public ActionResult CarNew()
        {

            ViewBag.Statussss = new SelectList(Statuses.StatusList, "Id", "Name");
            ViewBag.watu = new SelectList(Staffs, "Id", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult CarNew(NewCar newCar)
        {
            ViewBag.Statussss = new SelectList(Statuses.StatusList, "Id", "Name");

            if (ModelState.IsValid)
            {

                var car = new Car();

                car.CarNo = newCar.RegNo;
                car.Chasis = newCar.ChasisNo;
                car.Engine = newCar.EngineNo;
                car.Model = newCar.Model;

                Database.Session.Save(car);

                var machineCar = new Models.Machine();

                machineCar.CarId = car.Id;
                machineCar.Staff.Id = Convert.ToInt32(newCar.DriverName);
                machineCar.BoughtDate = newCar.BoughtDate;
                machineCar.Status = StatusName(Convert.ToInt32(newCar.Status));
                machineCar.Remarks = newCar.Remarks;
                machineCar.Name = newCar.Name;

                Database.Session.Save(machineCar);

                ViewBag.ActionMethod = "New";

                return PartialView("Success");

            }

            return PartialView();
        }

        public ActionResult Editcar(int id)
        {
            var car = Database.Session.Load<Car>(id);

            var machineCar = Database.Session.Query<Models.Machine>().Where(x => x.CarId == id).FirstOrDefault<Models.Machine>();


            EditCar editCar = new EditCar();

            editCar.CarId = car.Id;
            editCar.MachineId = machineCar.Id;
            editCar.Name = machineCar.Name;
            editCar.RegNo = car.CarNo;
            editCar.ChasisNo = car.Chasis;
            editCar.EngineNo = car.Engine;
            editCar.Model = car.Model;
            //editCar.DriverName
            ViewBag.watu = new SelectList(Staffs, "Id", "Name", machineCar.Staff.Id);
            editCar.BoughtDate = machineCar.BoughtDate;
            ViewBag.Statussss = new SelectList(Statuses.StatusList, "Id", "Name", StatusId(machineCar.Status));
            editCar.Remarks = machineCar.Remarks;

            return View(editCar);
        }

        [HttpPost]
        public ActionResult EditcarVehicle(EditCar editCar)
        {
            if (ModelState.IsValid)
            {
                var car = new Car();

                car.Id = editCar.CarId;
                car.CarNo = editCar.RegNo;
                car.Chasis = editCar.ChasisNo;
                car.Engine = editCar.EngineNo;
                car.Model = editCar.Model;

                Database.Session.SaveOrUpdate(car);

                var machine = new Models.Machine();

                machine.Id = editCar.MachineId;
                machine.CarId = car.Id;
                machine.Staff.Id = Convert.ToInt32(editCar.DriverName);
                machine.BoughtDate = editCar.BoughtDate;
                machine.Status = StatusName(Convert.ToInt32(editCar.Status));
                machine.Remarks = editCar.Remarks;
                machine.Name = editCar.Name;

                Database.Session.SaveOrUpdate(machine);

                ViewBag.ActionMethod = "Edit";

                return PartialView("Success");
            }
            

            return PartialView();
        }

        public ActionResult Show(int id)
        {
            var car = Database.Session.Load<Car>(id);

            var machineCar = Database.Session.Query<Models.Machine>().Where(x => x.CarId == id).FirstOrDefault<Models.Machine>();

            EditCar editCar = new EditCar();

            editCar.CarId = car.Id;
            editCar.MachineId = machineCar.Id;
            editCar.Name = machineCar.Name;
            editCar.RegNo = car.CarNo;
            editCar.ChasisNo = car.Chasis;
            editCar.EngineNo = car.Engine;
            editCar.Model = car.Model;
            editCar.DriverName = machineCar.Staff.Name;
            //ViewBag.watu = new SelectList(Staffs, "Id", "Name", machineCar.Staff.Id);
            editCar.BoughtDate = machineCar.BoughtDate;
            //ViewBag.Statussss = new SelectList(Statuses.StatusList, "Id", "Name", StatusId(machineCar.Status));
            editCar.Status = machineCar.Status;
            editCar.Remarks = machineCar.Remarks;

            return View(editCar);
        }

        public ActionResult RigNew()
        {
            ViewBag.RegionList = new SelectList(Regions, "Id", "Name");
            ViewBag.Statussss = new SelectList(Statuses.StatusList, "Id", "Name");
            ViewBag.watu = new SelectList(Staffs, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult NewRigMachine(RigNew rigNew)
        {
            if (ModelState.IsValid)
            {

                var rig = new Rig();

                rig.RigNo =  rigNew.RegNo;
                rig.RigState = rigNew.RigState;
                rig.Model = rigNew.Model;
                rig.RigType =  rigNew.RigType;
                rig.Region.Id = Convert.ToInt32(rigNew.Region);
                rig.District.Id = Convert.ToInt32(rigNew.District);
                Database.Session.Save(rig);

                var machineRig = new Models.Machine();

                machineRig.RigId = rig.Id;
                machineRig.Staff.Id = Convert.ToInt32(rigNew.DriverName);
                machineRig.BoughtDate = rigNew.BoughtDate;
                machineRig.Status = StatusName(Convert.ToInt32(rigNew.Status));
                machineRig.Remarks = rigNew.Remarks;
                machineRig.Name = rigNew.Name;

                Database.Session.Save(machineRig);

                ViewBag.ActionMethod = "New";

                return PartialView("SuccessRig");

            }

            return PartialView();
        }

        public ActionResult IndexRig()
        {
            return View();
        }

        public ActionResult EditRig(int id)
        {
            var rig = Database.Session.Load<Rig>(id);

            var machineRig = Database.Session.Query<Models.Machine>().Where(x => x.RigId == id).FirstOrDefault<Models.Machine>();


            RigEdit editRig = new RigEdit();

            editRig.RigId = rig.Id;
            editRig.MachineId = machineRig.Id;
            editRig.Name = machineRig.Name;
            editRig.RegNo = rig.RigNo;
            editRig.RigType = rig.RigType;
            editRig.RigState = rig.RigState;
            editRig.Model = rig.Model;
            ViewBag.watu = new SelectList(Staffs, "Id", "Name", machineRig.Staff.Id);
            editRig.BoughtDate = machineRig.BoughtDate;
            ViewBag.Statussss = new SelectList(Statuses.StatusList, "Id", "Name", StatusId(machineRig.Status));
            editRig.Remarks = machineRig.Remarks;
            editRig.Region = rig.Region.Name;

            ViewBag.RegionList = new SelectList(Regions, "Id", "Name", rig.Region.Id.ToString());
            ViewBag.Dstrct = new SelectList(GetDistrict(rig.Region.Id), "Id", "Name", rig.District.Id.ToString());

            return View(editRig);
        }

        public ActionResult EditRigMachine(RigEdit rigEdit)
        {

            if (ModelState.IsValid)
            {
                var rig = new Rig();

               rig.Id = rigEdit.RigId;
               rig.RigNo = rigEdit.RegNo;
               rig.RigType = rigEdit.RigType;
               rig.RigState = rigEdit.RigState;
               rig.Model = rigEdit.Model;
                rig.Region.Id = Convert.ToInt32(rigEdit.Region);
                rig.District.Id = Convert.ToInt32(rigEdit.District);

                Database.Session.SaveOrUpdate(rig);

                var machine = new Models.Machine();

                machine.Id = rigEdit.MachineId;
                machine.RigId = rig.Id;
                machine.Staff.Id = Convert.ToInt32(rigEdit.DriverName);
                machine.BoughtDate = rigEdit.BoughtDate;
                machine.Status = StatusName(Convert.ToInt32(rigEdit.Status));
                machine.Remarks = rigEdit.Remarks;
                machine.Name = rigEdit.Name;

                Database.Session.SaveOrUpdate(machine);

                ViewBag.ActionMethod = "Edit";

                return PartialView("SuccessRig");
            }


            return PartialView();

        }

        public ActionResult ShowRig(int id)
        {
            var rig = Database.Session.Load<Rig>(id);

            var machineRig = Database.Session.Query<Models.Machine>().Where(x => x.RigId == id).FirstOrDefault<Models.Machine>();


            RigEdit editRig = new RigEdit();

            //editRig.RigId = rig.Id;
            //editRig.MachineId = machineRig.Id;
            editRig.Name = machineRig.Name;
            editRig.RegNo = rig.RigNo;
            editRig.RigType = rig.RigType;
            editRig.RigState = rig.RigState;
            editRig.Model = rig.Model;
            //ViewBag.watu = new SelectList(Staffs, "Id", "Name", machineRig.Staff.Id);
            editRig.BoughtDate = machineRig.BoughtDate;
            //ViewBag.Statussss = new SelectList(Statuses.StatusList, "Id", "Name", StatusId(machineRig.Status));
            editRig.Remarks = machineRig.Remarks;
            editRig.Region = rig.Region.Name;
            editRig.District = rig.District.Name;
            editRig.DriverName = machineRig.Staff.Name;
            editRig.Status = machineRig.Status;
            //ViewBag.RegionList = new SelectList(Regions, "Id", "Name", rig.Region.Id.ToString());
           //ViewBag.Dstrct = new SelectList(GetDistrict(rig.Region.Id), "Id", "Name", rig.District.Id.ToString());

            return View(editRig);
        }


        public ActionResult CompressorNew()
        {
            ViewBag.RegionList = new SelectList(Regions, "Id", "Name");
            ViewBag.Statussss = new SelectList(Statuses.StatusList, "Id", "Name");
            ViewBag.watu = new SelectList(Staffs, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult NewCompressorMachine(ComprsrNew comprsrNew)
        {
            if (ModelState.IsValid)
            {

                var comprsr = new Compressor();

                comprsr.CompressorNo = comprsrNew.RegNo;
                comprsr.Model = comprsrNew.Model;
                comprsr.CompressorType = comprsrNew.ComprsrType;
                comprsr.Region.Id = Convert.ToInt32(comprsrNew.Region);
                comprsr.District.Id = Convert.ToInt32(comprsrNew.District);
                Database.Session.Save(comprsr);

                var machineComprsr = new Models.Machine();

                machineComprsr.CompressorId = comprsr.Id;
                machineComprsr.Staff.Id = Convert.ToInt32(comprsrNew.DriverName);
                machineComprsr.BoughtDate = comprsrNew.BoughtDate;
                machineComprsr.Status = StatusName(Convert.ToInt32(comprsrNew.Status));
                machineComprsr.Remarks = comprsrNew.Remarks;
                machineComprsr.Name = comprsrNew.Name;

                Database.Session.Save(machineComprsr);

                ViewBag.ActionMethod = "New";

                return PartialView("SuccessComprsr");

            }

            return PartialView();
        }

        public ActionResult IndexComprsr()
        {
            return View();
        }

        public ActionResult CompressorEdit(int id)
        {
            var comprsr = Database.Session.Load<Compressor>(id);

            var machineCompresr = Database.Session.Query<Models.Machine>().Where(x => x.CompressorId == id).FirstOrDefault<Models.Machine>();


            ComprsrEdit editComprsr = new ComprsrEdit();

            editComprsr.ComprsrId = comprsr.Id;
            editComprsr.MachineId = machineCompresr.Id;
            editComprsr.Name = machineCompresr.Name;
            editComprsr.RegNo = comprsr.CompressorNo;
            editComprsr.ComprsrType = comprsr.CompressorType;
            editComprsr.Model = comprsr.Model;
            ViewBag.watu = new SelectList(Staffs, "Id", "Name", machineCompresr.Staff.Id);
            editComprsr.BoughtDate = machineCompresr.BoughtDate;
            ViewBag.Statussss = new SelectList(Statuses.StatusList, "Id", "Name", StatusId(machineCompresr.Status));
            editComprsr.Remarks = machineCompresr.Remarks;
            editComprsr.Region = comprsr.Region.Name;

            ViewBag.RegionList = new SelectList(Regions, "Id", "Name", comprsr.Region.Id.ToString());
            ViewBag.Dstrct = new SelectList(GetDistrict(comprsr.Region.Id), "Id", "Name", comprsr.District.Id.ToString());

            return View(editComprsr);
        }

        [HttpPost]
        public ActionResult EditComprsrMachine(ComprsrEdit comprsrEdit)
        {
            if (ModelState.IsValid)
            {
                var compressor = new Compressor();

                compressor.Id = comprsrEdit.ComprsrId;
                compressor.CompressorNo = comprsrEdit.RegNo;
                compressor.CompressorType = comprsrEdit.ComprsrType;
                compressor.Model = comprsrEdit.Model;
                compressor.Region.Id = Convert.ToInt32(comprsrEdit.Region);
                compressor.District.Id = Convert.ToInt32(comprsrEdit.District);

                Database.Session.SaveOrUpdate(compressor);

                var machine = new Models.Machine();

                machine.Id = comprsrEdit.MachineId;
                machine.CompressorId = comprsrEdit.ComprsrId;
                machine.Staff.Id = Convert.ToInt32(comprsrEdit.DriverName);
                machine.BoughtDate = comprsrEdit.BoughtDate;
                machine.Status = StatusName(Convert.ToInt32(comprsrEdit.Status));
                machine.Remarks = comprsrEdit.Remarks;
                machine.Name = comprsrEdit.Name;

                Database.Session.SaveOrUpdate(machine);

                ViewBag.ActionMethod = "Edit";

                return PartialView("SuccessComprsr");
            }

            return PartialView();
        }

        public ActionResult ShowCompressor(int id)
        {
            var compressor = Database.Session.Load<Compressor>(id);

            var machineCompressor = Database.Session.Query<Models.Machine>().Where(x => x.CompressorId == id).FirstOrDefault<Models.Machine>();


            ComprsrEdit editComprsr = new ComprsrEdit();

            //editRig.RigId = rig.Id;
            //editRig.MachineId = machineRig.Id;
           editComprsr.Name = machineCompressor.Name;
           editComprsr.RegNo = compressor.CompressorNo;
           editComprsr.ComprsrType = compressor.CompressorType;
           editComprsr.Model = compressor.Model;
            //ViewBag.watu = new SelectList(Staffs, "Id", "Name", machineRig.Staff.Id);
            editComprsr.BoughtDate = machineCompressor.BoughtDate;
            //ViewBag.Statussss = new SelectList(Statuses.StatusList, "Id", "Name", StatusId(machineRig.Status));
           editComprsr.Remarks = machineCompressor.Remarks;
           editComprsr.Region = compressor.Region.Name;
           editComprsr.District = compressor.District.Name;
           editComprsr.DriverName = machineCompressor.Staff.Name;
           editComprsr.Status = machineCompressor.Status;
            //ViewBag.RegionList = new SelectList(Regions, "Id", "Name", rig.Region.Id.ToString());
            //ViewBag.Dstrct = new SelectList(GetDistrict(rig.Region.Id), "Id", "Name", rig.District.Id.ToString());

            return View(editComprsr);
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

        public ActionResult Delete(int id)
        {
            var car = Database.Session.Load<Car>(id);

            var machineCar = Database.Session.Query<Models.Machine>().Where(x => x.CarId == id).FirstOrDefault<Models.Machine>();

            Database.Session.Delete(machineCar);
            Database.Session.Delete(car);

            return Json(new { success = true, message = "Deleted  Successfully" }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DeleteRig(int id)
        {
            var rig = Database.Session.Load<Rig>(id);

            var machineRig = Database.Session.Query<Models.Machine>().Where(x => x.RigId == id).FirstOrDefault<Models.Machine>();

            if(machineRig != null)
                Database.Session.Delete(machineRig);

            if (rig != null)
                Database.Session.Delete(rig);

            return Json(new { success = true, message = "Deleted  Successfully" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteComprsr(int id)
        {
            var compresr = Database.Session.Load<Compressor>(id);

            var machineComprsr = Database.Session.Query<Models.Machine>().Where(x => x.CompressorId == id).FirstOrDefault<Models.Machine>();

            if (machineComprsr != null)
                Database.Session.Delete(machineComprsr);

            if (compresr != null)
                Database.Session.Delete(compresr);

            return Json(new { success = true, message = "Deleted  Successfully" }, JsonRequestBehavior.AllowGet);
        }

        public string StatusName(int id)
        {
            var status = Statuses.StatusList.Where(x => x.Id == id).FirstOrDefault<Status>();
            return status.Name;
        }

        public int StatusId(string name)
        {
            var status = Statuses.StatusList.Where(x => x.Name == name).FirstOrDefault<Status>();
            return status.Id;
        }
    }
}