using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DDCA.Areas.Borehole.ViewModels
{

    public class BoreholeNew
    {
        public int BoreholeId { get; set; }
        [Display(Name ="Borehole Number")]
        [Required]
        public string BoreholeNumber { get; set; }
        [Display(Name ="Rig Machine")]
        [Required]
        public string RigMachine { get; set; }
        [Display(Name = "Drilled Diameter")]
        [Required]
        public string DrilledDiameter { get; set; }
        [Display(Name = "Depth Of Finish")]
        [Required]
        public string DepthOnFinish { get; set; }
        [Required]
        public string Northings { get; set; }
        [Required]
        public string Eastings { get; set; }
        [Display(Name = "Start Date")]
        [Required]
        public DateTime StartDate { get; set; }
        [Display(Name = "Complete Date")]
        [Required]
        public DateTime CompleteDate { get; set; }
        [Display(Name = "Drilling Method")]
        [Required]
        public string DrillingMethod { get; set; }
        [Display(Name = "Depth To")]
        [Required]
        public string DrillDepth { get; set; }
    }

    public class Strata1
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Strata 1 Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "From")]
        public string DepthFrom { get; set; }
        [Required]
        [Display(Name = "To")]
        public string DepthTo { get; set; }

    }

    public class Strata2
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Strata 2 Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "From")]
        public string DepthFrom { get; set; }
        [Required]
        [Display(Name = "To")]
        public string DepthTo { get; set; }
    }

    public class Strata3
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Strata 3 Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "From")]
        public string DepthFrom { get; set; }
        [Required]
        [Display(Name = "To")]
        public string DepthTo { get; set; }
    }

    public class BoresrataNew
    {
        [Display(Name ="Strata Point")]
        [Required]
        public int StrataPoint { get; set; }
        public Strata1 Strata1 { get; set; }
        public Strata2 Strata2 { get; set; }
        public Strata3 Strata3 { get; set; }
        [Required]
        public string Formation { get; set; }
        [Required]
        [Display(Name ="Struck Depth")]
        public string StruckDepth { get; set; }


        public BoresrataNew()
        {
            Strata1 = new Strata1();
            Strata2 = new Strata2();
            Strata3 = new Strata3();
        }
    }

    public class PumpTestingNew
    {
        [Display(Name ="Tester Name")]
        [Required]
        public string Tester { get; set; }
        [Display(Name ="Pump Results")]
        [Required]
        public string PumpResults { get; set; }
        [Display(Name ="Yield Rate")]
        [Required]
        public string YieldRate { get; set; }
        [Display(Name = "Draw Dwn Depth")]
        [Required]
        public string DrawDownDepth { get; set; }
        [Display(Name = "SWL Depth")]
        [Required]
        public string SWLDepth { get; set; }
        [Display(Name = "Air Liftsize Diameter")]
        [Required]
        public string AirLiftSizeDiameter { get; set; }
        [Display(Name = "Air Liftsize Depth")]
        [Required]
        public string AirLiftSizeDepth { get; set; }
        [Display(Name = "Cylinder Diameter")]
        [Required]
        public string CylinderDiameter { get; set; }
        [Display(Name = "Cylinder Depth")]
        [Required]
        public string CylinderDepth { get; set; }
        [Display(Name = "Submissible Pumpsize")]
        [Required]
        public string SubmissiblePumpsize { get; set; }
        [Display(Name = "Submissible Depth")]
        [Required]
        public string SubmissibleDepth { get; set; }
    }

    public class BoreholeMaterialNew
    {
        [Display(Name ="Diameter")]
        [Required]
        public String CasingDiameter { get; set; }
        [Display(Name = "Height")]
        [Required]
        public String CasingHeight { get; set; }
        [Display(Name = "Casing Type")]
        [Required]
        public string CasingType { get; set; }
        [Display(Name = "Bottom Plug")]
        [Required]
        public string BottomPlug { get; set; }
        [Display(Name = "Top Protection")]
        [Required]
        public string TopProtection { get; set; }
        [Display(Name = "Backfill Height")]
        [Required]
        public string BackfillHeight { get; set; }
        [Display(Name = "Backfill Material")]
        [Required]
        public string BackfillMaterial { get; set; }
        [Display(Name = "Backfill Avg Size")]
        [Required]
        public string BackfillAvgSize { get; set; }
        [Display(Name = "Other Backfill Method")]
        [Required]
        public string OtherBackfillMethod { get; set; }
        [Display(Name = "Diameter")]
        [Required]
        public string ScreenDiameter { get; set; }
        [Display(Name = "Height")]
        [Required]
        public string ScreenHeight { get; set; }
        [Display(Name = "Screen Type")]
        [Required]
        public string ScreenType { get; set; }
        [Display(Name = "Gravel Type")]
        [Required]
        public string GravelType { get; set; }
        [Display(Name = "Gravel Avg Size")]
        [Required]
        public string GravelAvgSize { get; set; }
        [Display(Name = "Insert Range")]
        public string InsertRange { get; set; }
        [Display(Name = "From")]
        [Required]
        public string GravelFrom { get; set; }
        [Display(Name = "To")]
        [Required]
        public string GravelTo { get; set; }
    }

    public class ChemicalNew
    {
        [Display(Name = "Hardness CaCO3")]
        [Required]
        public string Hardness { get; set; }
        [Required]
        public string Manganese { get; set; }
        [Required]
        [Display(Name = "Ortho phosphate")]
        public string OrthoPhosphate { get; set; }
        [Display(Name = "Iron Fe")]
        [Required]
        public string Iron { get; set; }
        [Display(Name = "Nitrate Nitrogen")]
        [Required]
        public string NitrateNitrogen { get; set; }
        [Required]
        public string Carbonate { get; set; }
        [Required]
        public string Phenophthalein { get; set; }
        [Display(Name = "Non Carbonate")]
        [Required]
        public string NonCarbonate { get; set; }
        [Required]
        public string Sodium { get; set; }
        [Required]
        public string Cadmium { get; set; }
        [Required]
        [Display(Name = "Calcium Ca2+")]
        public string Calcium { get; set; }
        [Required]
        [Display(Name = "Chloride Cl")]
        public string Chloride { get; set; }
        [Required]
        public string Fluoride { get; set; }
        [Required]
        [Display(Name = "Total Nitrogen")]
        public string TotalNitrogen { get; set; }
        [Required]
        public string Chromium { get; set; }
        [Required]
        public string Mercury { get; set; }
        [Required]
        public string Zinc { get; set; }
        [Required]
        public string Potassium { get; set; }
        [Required]
        public string Lead { get; set; }
        [Required]
        [Display(Name = "Magnesium Mg2+")]
        public string Magnesium { get; set; }
        [Required]
        [Display(Name = "Copper Cu")]
        public string Copper { get; set; }
        [Required]
        [Display(Name = "Sulphate")]
        public string Sulphate { get; set; }
        [Required]
        [Display(Name = "Ammonical Nitrogen")]
        public string AmmonicalNitrogen { get; set; }
        [Required]
        [Display(Name = "Nitrite Nitrogen")]
        public string NitriteNitrogen { get; set; }
        [Required]
        [Display(Name = "Organic Nitrogen")]
        public string OrganicNitrogen { get; set; }
        [Required]
        public string Phosphorus { get; set; }
        [Required]
        public string Permanganate { get; set; }
        [Required]
        public string Alkalinity { get; set; }
        [Required]
        [Display(Name = "BOD (in 5 days)")]
        public string BOD { get; set; }
    }

    public class PhysicalLabNew
    {
        [Required]
        [Display(Name = "Color (In mg Pt/L)")]
        public string Color { get; set; }
        [Required]
        public string Turbidity { get; set; }
        [Required]
        public string Odour { get; set; }
        [Required]
        [Display(Name = "Settleable Matter(MI/L)")]
        public string SettleableMatter { get; set; }
        [Required]
        public string PH { get; set; }
        [Required]
        public string Taste { get; set; }
        [Required]
        public string Conductivity { get; set; }
        [Required]
        [Display(Name = "Filtrate Residue(TDS)")]
        public string FiltrateResidue { get; set; }
        [Required]
        [Display(Name = "NonFiltrate Residue")]
        public string NonFiltrateResidue { get; set; }
        [Required]
        [Display(Name = "Volatile Fixed Residue")]
        public string VolatileFixedResidue { get; set; }
        [Required]
        [Display(Name = "Laboratory Name")]
        public string LabName { get; set; }
        [Required]
        [Display(Name = "Collected Date")]
        public DateTime CollectedDate { get; set; }
        [Required]
        [Display(Name = "Analysis Date")]
        public DateTime AnalysisDate { get; set; }
        [Required]
        public string Remarks { get; set; }
        [Required]
        [Display(Name ="Recommendations")]
        public string Recommend { get; set; }
    }

    public class BoreholeEdit
    {
        public int BoreholeId { get; set; }

        [Display(Name = "Borehole Number")]
        [Required]
        public string BoreholeNumber { get; set; }
        [Display(Name = "Rig Machine")]
        [Required]
        public string RigMachine { get; set; }
        [Display(Name = "Drilled Diameter")]
        [Required]
        public string DrilledDiameter { get; set; }
        [Display(Name = "Depth Of Finish")]
        [Required]
        public string DepthOnFinish { get; set; }
        [Required]
        public string Northings { get; set; }
        [Required]
        public string Eastings { get; set; }
        [Display(Name = "Start Date")]
        [Required]
        public DateTime StartDate { get; set; }
        [Display(Name = "Complete Date")]
        [Required]
        public DateTime CompleteDate { get; set; }
        [Display(Name = "Drilling Method")]
        [Required]
        public string DrillingMethod { get; set; }
        [Display(Name = "Depth To")]
        [Required]
        public string DrillDepth { get; set; }
    }

    public class BorestrataEdit
    {
        [Display(Name = "Strata Point")]
        [Required]
        public int StrataPoint { get; set; }
        public Strata1 Strata1 { get; set; }
        public Strata2 Strata2 { get; set; }
        public Strata3 Strata3 { get; set; }
        [Required]
        public string Formation { get; set; }
        [Required]
        [Display(Name = "Struck Depth")]
        public string StruckDepth { get; set; }

        public BorestrataEdit()
        {
            Strata1 = new Strata1();
            Strata2 = new Strata2();
            Strata3 = new Strata3();
        }
    }


    public class PumpTestingEdit
    {
        public int Id { get; set; }
        [Display(Name = "Tester Name")]
        [Required]
        public string Tester { get; set; }
        [Display(Name = "Pump Results")]
        [Required]
        public string PumpResults { get; set; }
        [Display(Name = "Yield Rate")]
        [Required]
        public string YieldRate { get; set; }
        [Display(Name = "Draw Dwn Depth")]
        [Required]
        public string DrawDownDepth { get; set; }
        [Display(Name = "SWL Depth")]
        [Required]
        public string SWLDepth { get; set; }
        [Display(Name = "Air Liftsize Diameter")]
        [Required]
        public string AirLiftSizeDiameter { get; set; }
        [Display(Name = "Air Liftsize Depth")]
        [Required]
        public string AirLiftSizeDepth { get; set; }
        [Display(Name = "Cylinder Diameter")]
        [Required]
        public string CylinderDiameter { get; set; }
        [Display(Name = "Cylinder Depth")]
        [Required]
        public string CylinderDepth { get; set; }
        [Display(Name = "Submissible Pumpsize")]
        [Required]
        public string SubmissiblePumpsize { get; set; }
        [Display(Name = "Submissible Depth")]
        [Required]
        public string SubmissibleDepth { get; set; }
    }

    public class BoreholeMaterialEdit
    {
        [Display(Name = "Diameter")]
        [Required]
        public String CasingDiameter { get; set; }
        [Display(Name = "Height")]
        [Required]
        public String CasingHeight { get; set; }
        [Display(Name = "Casing Type")]
        [Required]
        public string CasingType { get; set; }
        [Display(Name = "Bottom Plug")]
        [Required]
        public string BottomPlug { get; set; }
        [Display(Name = "Top Protection")]
        [Required]
        public string TopProtection { get; set; }
        [Display(Name = "Backfill Height")]
        [Required]
        public string BackfillHeight { get; set; }
        [Display(Name = "Backfill Material")]
        [Required]
        public string BackfillMaterial { get; set; }
        [Display(Name = "Backfill Avg Size")]
        [Required]
        public string BackfillAvgSize { get; set; }
        [Display(Name = "Other Backfill Method")]
        [Required]
        public string OtherBackfillMethod { get; set; }
        [Display(Name = "Diameter")]
        [Required]
        public string ScreenDiameter { get; set; }
        [Display(Name = "Height")]
        [Required]
        public string ScreenHeight { get; set; }
        [Display(Name = "Screen Type")]
        [Required]
        public string ScreenType { get; set; }
        [Display(Name = "Gravel Type")]
        [Required]
        public string GravelType { get; set; }
        [Display(Name = "Gravel Avg Size")]
        [Required]
        public string GravelAvgSize { get; set; }
        [Display(Name = "Insert Range")]
        public string InsertRange { get; set; }
        [Display(Name = "From")]
        [Required]
        public string GravelFrom { get; set; }
        [Display(Name = "To")]
        [Required]
        public string GravelTo { get; set; }
    }

    public class ChemicalEdit
    {
        [Display(Name = "Hardness CaCO3")]
        [Required]
        public string Hardness { get; set; }
        [Required]
        public string Manganese { get; set; }
        [Required]
        [Display(Name = "Ortho phosphate")]
        public string OrthoPhosphate { get; set; }
        [Display(Name = "Iron Fe")]
        [Required]
        public string Iron { get; set; }
        [Display(Name = "Nitrate Nitrogen")]
        [Required]
        public string NitrateNitrogen { get; set; }
        [Required]
        public string Carbonate { get; set; }
        [Required]
        public string Phenophthalein { get; set; }
        [Display(Name = "Non Carbonate")]
        [Required]
        public string NonCarbonate { get; set; }
        [Required]
        public string Sodium { get; set; }
        [Required]
        public string Cadmium { get; set; }
        [Required]
        [Display(Name = "Calcium Ca2+")]
        public string Calcium { get; set; }
        [Required]
        [Display(Name = "Chloride Cl")]
        public string Chloride { get; set; }
        [Required]
        public string Fluoride { get; set; }
        [Required]
        [Display(Name = "Total Nitrogen")]
        public string TotalNitrogen { get; set; }
        [Required]
        public string Chromium { get; set; }
        [Required]
        public string Mercury { get; set; }
        [Required]
        public string Zinc { get; set; }
        [Required]
        public string Potassium { get; set; }
        [Required]
        public string Lead { get; set; }
        [Required]
        [Display(Name = "Magnesium Mg2+")]
        public string Magnesium { get; set; }
        [Required]
        [Display(Name = "Copper Cu")]
        public string Copper { get; set; }
        [Required]
        [Display(Name = "Sulphate")]
        public string Sulphate { get; set; }
        [Required]
        [Display(Name = "Ammonical Nitrogen")]
        public string AmmonicalNitrogen { get; set; }
        [Required]
        [Display(Name = "Nitrite Nitrogen")]
        public string NitriteNitrogen { get; set; }
        [Required]
        [Display(Name = "Organic Nitrogen")]
        public string OrganicNitrogen { get; set; }
        [Required]
        public string Phosphorus { get; set; }
        [Required]
        public string Permanganate { get; set; }
        [Required]
        public string Alkalinity { get; set; }
        [Required]
        [Display(Name = "BOD (in 5 days)")]
        public string BOD { get; set; }
    }

    public class PhysicalLabEdit
    {
        [Required]
        [Display(Name = "Color (In mg Pt/L)")]
        public string Color { get; set; }
        [Required]
        public string Turbidity { get; set; }
        [Required]
        public string Odour { get; set; }
        [Required]
        [Display(Name = "Settleable Matter(MI/L)")]
        public string SettleableMatter { get; set; }
        [Required]
        public string PH { get; set; }
        [Required]
        public string Taste { get; set; }
        [Required]
        public string Conductivity { get; set; }
        [Required]
        [Display(Name = "Filtrate Residue(TDS)")]
        public string FiltrateResidue { get; set; }
        [Required]
        [Display(Name = "NonFiltrate Residue")]
        public string NonFiltrateResidue { get; set; }
        [Required]
        [Display(Name = "Volatile Fixed Residue")]
        public string VolatileFixedResidue { get; set; }
        [Required]
        [Display(Name = "Laboratory Name")]
        public string LabName { get; set; }
        [Required]
        [Display(Name = "Collected Date")]
        public DateTime CollectedDate { get; set; }
        [Required]
        [Display(Name = "Analysis Date")]
        public DateTime AnalysisDate { get; set; }
        [Required]
        public string Remarks { get; set; }
        [Required]
        [Display(Name = "Recommendations")]
        public string Recommend { get; set; }
    }

    public class BoreBorestrataView
    {
        public BoreholeNew boreholeNew { get; set; }
        public BoresrataNew boresrataNew { get; set; }

        public BoreBorestrataView()
        {
            boresrataNew = new BoresrataNew();
            boreholeNew = new BoreholeNew();
        }
    }

    public class PumpMaterialView
    {
        public PumpTestingNew PumpTestingNew { get; set; }
        public BoreholeMaterialNew BoreholeMaterialNew { get; set; }
        public string Formation { get; set; }
        [Display(Name = "Struck Depth")]
        public string StruckDepth { get; set; }

        public PumpMaterialView()
        {
            PumpTestingNew = new PumpTestingNew();
            BoreholeMaterialNew = new BoreholeMaterialNew();
        }
    }

    public class ChemicalPhysicalLabView
    {
        public ChemicalNew ChemicalNew { get; set; }
        public PhysicalLabNew PhysicalLabNew { get; set; }

        public ChemicalPhysicalLabView()
        {
            ChemicalNew = new ChemicalNew();
            PhysicalLabNew = new PhysicalLabNew();
        }
    }
}