using DDCA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DDCA.Areas.Machine.ViewModel
{
    public class NewCar
    {
        [Required]
        [Display(Name ="Registration Number")]
        public string RegNo { get; set; }

        [Required]
        [Display(Name ="Chasis Number")]
        public string ChasisNo { get; set; }

        [Required]
        [Display(Name = "Engine Number")]
        public string EngineNo { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name ="Driver Name")]
        public string DriverName { get; set; }

        [Required]
        [Display(Name = "Bought Date")]
        public DateTime BoughtDate { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string Remarks { get; set; }

    }

    public class EditCar
    {
        public int CarId { get; set; }
        public int MachineId { get; set; }
        [Required]
        [Display(Name = "Registration Number")]
        public string RegNo { get; set; }

        [Required]
        [Display(Name = "Chasis Number")]
        public string ChasisNo { get; set; }

        [Required]
        [Display(Name = "Engine Number")]
        public string EngineNo { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Driver Name")]
        public string DriverName { get; set; }

        [Required]
        [Display(Name = "Bought Date")]
        public DateTime BoughtDate { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string Remarks { get; set; }
    }

    public class RigNew
    {
        [Required]
        [Display(Name = "Registration Number")]
        public string RegNo { get; set; }

        [Required]
        [Display(Name = "Rig Type")]
        public string RigType { get; set; }

        [Required]
        [Display(Name = "Rig State")]
        public string RigState { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Region { get; set; }

        [Required]
        public string District { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Incharge Name")]
        public string DriverName { get; set; }

        [Required]
        [Display(Name = "Bought Date")]
        public DateTime BoughtDate { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string Remarks { get; set; }
    }

    public class RigEdit
    {
        public int RigId { get; set; }
        public int MachineId { get; set; }
        [Required]
        [Display(Name = "Registration Number")]
        public string RegNo { get; set; }

        [Required]
        [Display(Name = "Rig Type")]
        public string RigType { get; set; }

        [Required]
        [Display(Name = "Rig State")]
        public string RigState { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Region { get; set; }

        [Required]
        public string District { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Incharge Name")]
        public string DriverName { get; set; }

        [Required]
        [Display(Name = "Bought Date")]
        public DateTime BoughtDate { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string Remarks { get; set; }
    }

    public class ComprsrNew 
    {
        [Required]
        [Display(Name = "Registration Number")]
        public string RegNo { get; set; }

        [Required]
        [Display(Name = "Compressor Type")]
        public string ComprsrType { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Region { get; set; }

        [Required]
        public string District { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Incharge Name")]
        public string DriverName { get; set; }

        [Required]
        [Display(Name = "Bought Date")]
        public DateTime BoughtDate { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string Remarks { get; set; }
    }

    public class ComprsrEdit
    {
        public int ComprsrId { get; set; }
        public int MachineId { get; set; }
        [Required]
        [Display(Name = "Registration Number")]
        public string RegNo { get; set; }

        [Required]
        [Display(Name = "Compressor Type")]
        public string ComprsrType { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Region { get; set; }

        [Required]
        public string District { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Incharge Name")]
        public string DriverName { get; set; }

        [Required]
        [Display(Name = "Bought Date")]
        public DateTime BoughtDate { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string Remarks { get; set; }
    }
}