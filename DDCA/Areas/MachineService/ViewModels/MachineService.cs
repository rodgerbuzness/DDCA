using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DDCA.Areas.MachineService.ViewModels
{
    public class CarServiceNew 
    {
        public string RegNo { get; set; }
        public int Id { get; set; }
        public int CarId { get; set; }
        [Required]
        [Display(Name ="Incharge Name")]
        public string InchargeName { get; set; }
        [Required]
        [Display(Name = "Material Cost")]
        public string MaterialCost { get; set; }
        [Required]
        [Display(Name = "Labour Cost")]
        public string LabourCost { get; set; }
        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        [Required]
        [Display(Name = "Job Done")]
        public string JobDone { get; set; }
    }

    public class RigServiceNew
    {
        public string RegNo { get; set; }
        public int Id { get; set; }
        public int RigId { get; set; }
        [Required]
        [Display(Name = "Incharge Name")]
        public string InchargeName { get; set; }
        [Required]
        [Display(Name = "Material Cost")]
        public string MaterialCost { get; set; }
        [Required]
        [Display(Name = "Labour Cost")]
        public string LabourCost { get; set; }
        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        [Required]
        [Display(Name = "Job Done")]
        public string JobDone { get; set; }
    }

    public class CmprsrServiceNew
    {
        public string RegNo { get; set; }
        public int Id { get; set; }
        public int CmprsrId { get; set; }
        [Required]
        [Display(Name = "Incharge Name")]
        public string InchargeName { get; set; }
        [Required]
        [Display(Name = "Material Cost")]
        public string MaterialCost { get; set; }
        [Required]
        [Display(Name = "Labour Cost")]
        public string LabourCost { get; set; }
        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        [Required]
        [Display(Name = "Job Done")]
        public string JobDone { get; set; }
    }
}
