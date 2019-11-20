using DDCA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DDCA.Areas.Survey.ViewModels
{
    public class SurveysIndex
    {
        public IEnumerable<GeoSurvey> Surveys { get; set; }
    }

    public class ClientInfo
    {

        [Required]
        [Display(Name = "Client Name")]
        public string Name { get; set; }
        [Required, DataType(DataType.PostalCode)]
        [Display(Name = "Address Box")]
        public string Address { get; set; }
        [Required]
        public string Region { get; set; }
        [Required, DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public DateTime AddDate { get; set; }


    }

    public class SurveyProfileInfo
    {
        public ProfileEditor1 ProfileEditor1 { get; set; }
        public ProfileEditor2 ProfileEditor2 { get; set; }
        public ProfileEditor3 ProfileEditor3 { get; set; }
    }




    public class ProfileEditor1
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "VES point")]
        public string VESPoints { get; set; }
        [Required]
        public int Northing { get; set; }
        [Required]
        public int Easting { get; set; }
        [Required]
        public string Elevation { get; set; }
        [Required]
        [Display(Name = "Geoph Method")]
        public string SurveyMethod { get; set; }
        [Required]
        public string Recommend { get; set; }
        [Required]
        [Display(Name = "Site Recommendation")]
        public string SiteRecommendation { get; set; }
    }

    public class ProfileEditor2
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "VES point")]
        public string VESPoints { get; set; }
        [Required]
        public int Northing { get; set; }
        [Required]
        public int Easting { get; set; }
        [Required]
        public string Elevation { get; set; }
        [Required]
        [Display(Name = "Geoph Method")]
        public string SurveyMethod { get; set; }
        [Required]
        public string Recommend { get; set; }
        

    }

    public class ProfileEditor3
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "VES point")]
        public string VESPoints { get; set; }
        [Required]
        public int Northing { get; set; }
        [Required]
        public int Easting { get; set; }
        [Required]
        public string Elevation { get; set; }
        [Required]
        [Display(Name = "Geoph Method")]
        public string SurveyMethod { get; set; }
        [Required]
        public string Recommend { get; set; }
        //[Required]
        //[Display(Name = "Site Recommendation")]
        //public string SiteRecommendation { get; set; }

    }



    public class SurveysNew
    {
        [Required]
        [Display(Name = "Site Region")]
        public string Region { get; set; }

        [Required]
        [Display(Name = "Site District")]
        public string District { get; set; }

        [Required]
        [Display(Name = "Site Village/Area")]
        public string Village { get; set; }
    
        [Display(Name = "Surveyor")]
        public string SurveyorType { get; set; }
        
        [Display(Name = "Surveyor Name")]
        public string SurveyorName { get; set; }
        
        [DataType(DataType.DateTime)]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }
        
        [DataType(DataType.DateTime)]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Survey Cost")]
        public int Cost { get; set; }

        public ClientInfo Client { get; set; }
       

        public SurveysNew()
        {
            Client = new ClientInfo();
        }
    }

    public class SurveyEdit
    {
        public int surveyId { get; set; }

        [Required]
        [Display(Name = "Site Region")]
        public string Region { get; set; }

        [Required]
        [Display(Name = "Site District")]
        public string District { get; set; }

        [Required]
        [Display(Name = "Site Village/Area")]
        public string Village { get; set; }

        [Display(Name = "Surveyor")]
        public string SurveyorType { get; set; }

        [Display(Name = "Surveyor Name")]
        public string SurveyorName { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Survey Cost")]
        public int Cost { get; set; }

        [Required]
        public string SiteRecommendation { get; set; }

        public ClientInfo Client { get; set; }

        public SurveyEdit()
        {
            Client = new ClientInfo();
            //ProfileEditor1 = new ProfileEditor1();
            //ProfileEditor2 = new ProfileEditor2();
            //ProfileEditor3 = new ProfileEditor3();
        }

    }

    public class SurveyProfileEdit
    {
        public int SurveyId { get; set; }
        public ProfileEditor1 ProfileEditor1 { get; set; }
        public ProfileEditor2 ProfileEditor2 { get; set; }
        public ProfileEditor3 ProfileEditor3 { get; set; }

        public SurveyProfileEdit()
        {
            ProfileEditor1 = new ProfileEditor1();
            ProfileEditor2 = new ProfileEditor2();
            ProfileEditor3 = new ProfileEditor3();
        }
    }

    public class SurveyView
    {
        [Display(Name = "Site Region")]
        public string Region { get; set; }

        [Display(Name = "Site District")]
        public string District { get; set; }

        [Display(Name = "Site Village/Area")]
        public string Village { get; set; }

        [Display(Name = "Surveyor")]
        public string SurveyorType { get; set; }

        [Display(Name = "Surveyor Name")]
        public string SurveyorName { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Survey Cost")]
        public int Cost { get; set; }

        public string SiteRecommendation { get; set; }

        public ClientInfo Client { get; set; }

        public ProfileEditor1 ProfileEditor1 { get; set; }
        public ProfileEditor2 ProfileEditor2 { get; set; }
        public ProfileEditor3 ProfileEditor3 { get; set; }

        public SurveyView()
        {
            Client = new ClientInfo();
            ProfileEditor1 = new ProfileEditor1();
            ProfileEditor2 = new ProfileEditor2();
            ProfileEditor3 = new ProfileEditor3();
        }
    }
}