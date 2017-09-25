using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace NewLetter.Models
{
    [MetadataType(typeof(qendidateListValidation))]
    public partial class qendidateList
    {
        public int qenCategory { get; set; }
       public long jobid { get; set; }
        public string city1 { get; set; }


    }
    //[MetadataType(typeof(qenrefvalidation))]
    //public partial class qenReference
    //{

    //}
    [MetadataType(typeof(qengradutionvalidtion))]
    public partial class qendidateGraduation
    {


    }

    [MetadataType(typeof(qenpostGradutionvalidtion))]
    public partial class qendidatePGraduation
    {


    }
    
    [MetadataType(typeof(EmpDetails))]
    public partial class qenEmpDetail
    {


    }

    [MetadataType(typeof(ValidateSecondary))]
    public partial class qenSecondary
    {

    }

    [MetadataType(typeof(HCValidateSecondary))]
    public partial class qenHigherSecondary
    {

    }
    public class ValidateSecondary
    {
        [RegularExpression("^[0-9]*$", ErrorMessage = "invalid number")]
        [Required(ErrorMessage = "please enter passing year")]
        public int? secondaryPassYear { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "invalid number")]
        [Required(ErrorMessage = "please enter percentage")]
        public int? secondaryPercentage { get; set; }
        [Required(ErrorMessage = "please enter school name")]
        public string schoolName { get; set; }
        [Required(ErrorMessage = "please enter board name")]
        public string secondaryBoard { get; set; }
        [Required(ErrorMessage = "please enter subjects")]
        public string secondarySubjects { get; set; }
    }
    public class HCValidateSecondary
    {
        [RegularExpression("^[0-9]*$", ErrorMessage = "invalid number")]
        [Required(ErrorMessage = "please enter passing year")]
        public int? hSecondaryPassYear { get; set; }
        [Required(ErrorMessage = "please enter percentage")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "invalid number")]
        public int? hSecondaryPercentage { get; set; }
        [Required(ErrorMessage = "please enter school name")]
        public string schoolName { get; set; }
        [Required(ErrorMessage = "please enter board name")]
        public string hSecondaryBoard { get; set; }
        [Required(ErrorMessage = "please enter subjects")]
        public string hSecondarySubjects { get; set; }
    }
    public class EmpDetails
    {
        [Required(ErrorMessage = "please enter company name")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "please enter employment start date")]
        public System.Nullable<System.DateTime> qenEmpFrom { get; set; }
        [Required(ErrorMessage = "please enter job description")]
        public string jobDescription { get; set; }
        [Required(ErrorMessage = "please enter employment end date")]
        public System.Nullable<System.DateTime> qenEmpTo { get; set; }
        [Required(ErrorMessage = "please enter salary")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "invalid salary detail")]
        public System.Nullable<double> qenSalary { get; set; }
        [Required(ErrorMessage = "please enter your postition")]
        public string qenPosition { get; set; }
    }
    public class EmployerModel
    {
        public EmployerModel()
        {
            employers = new List<qenEmpDetail>();
        }
        public long qenid { get; set; }
        public List<qenEmpDetail> employers { get; set; }
    }
    public class qengradutionvalidtion
    {
        [Required(ErrorMessage = "please enter univercity name")]
        public string collegeUniversity { get; set; }

        [Required(ErrorMessage = "please enter college name")]
        public string collegeName { get; set;}

        [Required(ErrorMessage = "please enter course name")]
        public string courseName { get; set; }

        [Required(ErrorMessage = "please enter subjects name")]
        public string subjects { get; set; } 

        [RegularExpression("^[0-9]*$", ErrorMessage = "invalid number")]
        [Required(ErrorMessage = "please enter percentage ")]
        public long gradPercentage { get; set; }

        [Required(ErrorMessage = "please enter course field")]
        public string courseField { get; set; }

        [Required(ErrorMessage = "please enter passing year")]
        public string YearPassing { get; set; }
    }

    public class qenpostGradutionvalidtion
    {
        [Required(ErrorMessage = "please enter univercity name")]
        public string collegeUniversity { get; set; }

        [Required(ErrorMessage = "please enter college name")]
        public string collegeName { get; set; }

        [Required(ErrorMessage = "please enter course name")]
        public string courseName { get; set; }

        [Required(ErrorMessage = "please enter subjects name")]
        public string subjects { get; set; }

        [Required(ErrorMessage = "please enter percentage ")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "please enter percentage")]
        public long pGradPercentage { get; set; }

        [Required(ErrorMessage = "please enter course field")]
        public string courseField { get; set; }

        [Required(ErrorMessage = "please enter passing year")]
        public string YearPassing { get; set; }
    }
        
    public class qenrefvalidation
    { 

        [Required(ErrorMessage = "please enter refrence name")]
        public string qenRefName1 { get; set; }

        [Required(ErrorMessage = "please enter company name")]
        public string qenRefCompany1 { get; set; }
        //[Required(ErrorMessage = "please enter postion")]
        public string qenRefPosition1 { get; set; }
        [Required(ErrorMessage = "please enter mobile number")]
        [RegularExpression(@"^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[789]\d{9}$", ErrorMessage = "Invalid phone number")]
        public System.Nullable<double> qenRefPhone1 { get; set; }
        [Required(ErrorMessage = "please enter email ")]        
        [EmailAddress]
        public string qenRefEmail1 { get; set; }
        [Required(ErrorMessage = "please enter refrence name")]
        public string qenRefName2 { get; set; }

        [Required(ErrorMessage = "please enter company name")]        
        public string qenRefCompany2 { get; set; }

        [RegularExpression(@"^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[789]\d{9}$", ErrorMessage = "Invalid phone number")]
        [Required(ErrorMessage = "please enter mobile number")]            
        public System.Nullable<double> qenRefPhone2 { get; set; }
        [Required(ErrorMessage = "please enter email ")]        
        [EmailAddress]
        public string qenRefEmail2 { get; set; }
        //[Required(ErrorMessage = "please enter postion")]
        public string qenRefPosition2 { get; set; }
        public int? qenid { get; set; }
        //[Required(ErrorMessage = "Please accept terms and conditions")]
        public string acceptterms { get; set; }
    }
    public class qendidateListValidation
    {
        public long qenID { get; set; }
        [Required(ErrorMessage = "please enter your name")]
        public string qenName { get; set; }
        [Required(ErrorMessage = "please enter your address")]
        public string qenAddress { get; set; }
        
        [Required(ErrorMessage = "please enter your email")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+‌​)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        public string qenEmail { get; set; }
        public string qenLinkdInUrl { get; set; }
        [Required(ErrorMessage = "please enter your phone number")]
        [RegularExpression(@"^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[789]\d{9}$", ErrorMessage = "Invalid phone number")]
        public System.Nullable<long> qenPhone { get; set; }
        public string qenNationality { get; set; }
        public string qenPassport { get; set; }
        
    }

    public class AcademicModel
    {

        public qendidateGraduation graduation { get; set; }
        public qendidatePGraduation pgraduation { get; set; }
        public qenHigherSecondary hsecondary { get; set; }
        public qenSecondary secondary { get; set; }
        public int? qenid { get; set; }
        public AcademicModel()
        {
            graduation = new qendidateGraduation();
            pgraduation = new qendidatePGraduation();
            hsecondary = new qenHigherSecondary();
            secondary = new qenSecondary();
        }

    }
    public class UrlAttribute : ValidationAttribute
    {
        public UrlAttribute() { }

        public override bool IsValid(object value)
        {
            //may want more here for https, etc
            Regex regex = new Regex(@"(http://)?(www\.)?\w+\.(com|net|edu|org)");

            if (value == null) return false;

            if (!regex.IsMatch(value.ToString())) return false;

            return true;
        }
    }


    public class qenSkillName
    {
        [Required(ErrorMessage = "please enter your skill name")]
        public string skillName { get; set; }
        public int skillID { get; set; }
        [Required(ErrorMessage = "please enter year of exp")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "invalid number")]
        public int yearOfExp { get; set; }
        public int qenID { get; set; }
        public int qenSkillsID { get; set; }
    }

    public class login
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "please enter your email")]
        [EmailAddress]
        public string qenEmail { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "please enter your email")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Invalid")]
        public string password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class job_posting_Title
    {
        public long jobID { get; set; }

        [StringLength(200, MinimumLength = 10, ErrorMessage = "200 charectar max")]
        [Required(ErrorMessage = "please enter job title")]
        [Display(Name = "Job Title")]
        public string jobTitle { get; set; }

        [AllowHtml]
        [Display(Name = "Job Description")]
        [StringLength(5000, MinimumLength = 10, ErrorMessage = "5000 charectar max")]
        [Required(ErrorMessage = "job description required")]
        public string jobDescription { get; set; }

        [Display(Name = "Openings")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "numeric allowed only")]
        [Required(ErrorMessage = "no of openings required")]
        public int openings { get; set; }


        [Display(Name = "Experience(year)")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "numeric allowed only")]
        [Required(ErrorMessage = "year of experience required")]
        public int experience { get; set; }

        [Display(Name = "Salary")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "numeric allowed only")]
        [Required(ErrorMessage = "salary (gross salary) required")]
        public int salary { get; set; }

        [Display(Name = "Industry")]
        [Required(ErrorMessage = "please select industry")]
        public int industry { get; set; }


        [Display(Name = "Currency")]
        [Required(ErrorMessage = "please select currency")]
        public string currency { get; set; }

        [Display(Name = "Location")]
        [Required(ErrorMessage = "location required")]
        public string city { get; set; }

        [Display(Name = "Job Type")]
        [Required(ErrorMessage = "job type required")]
        public int employmentType { get; set; }


        [Display(Name = "Unit")]
        [Required(ErrorMessage = "salary unit required")]
        public string Unit { get; set; }

        [Display(Name = "Salry visible to employee")]
        public bool salaryVisibleToEmployee { get; set; }


    }


    public class requiredSkills
    {
        [Display(Name = "Minimum education")]
        public string EducationReq { get; set; }

        [Display(Name = "Technical skills")]
        public string skills { get; set; }

        public long jobID { get; set; }
    }
    public class company_
    {
        [StringLength(100, MinimumLength = 3, ErrorMessage = "100 charectar max")]
        [Display(Name = "Company Name")]
        [Required(ErrorMessage = "Company name required")]
        public string companyName { get; set; }

        [AllowHtml]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "500 charectar max")]
        [Display(Name = "About Company")]
        [Required(ErrorMessage = "About company is required")]
        public string aboutCompany { get; set; }


        [StringLength(200, MinimumLength = 3, ErrorMessage = "200 charectar max")]
        [Display(Name = "Company Website")]
        [Url]
        public string companyWebsite { get; set; }

        [StringLength(30, MinimumLength = 5, ErrorMessage = "30 charectar max")]
        [Display(Name = "Contact person")]
        [Required(ErrorMessage = "Contact person required")]
        public string contactPerson { get; set; }

        [Required(ErrorMessage = "please enter contact person number")]
        [RegularExpression(@"^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[789]\d{9}$", ErrorMessage = "Invalid phone number")]
        [Display(Name = "Contact Number")]
        public string contactNumber { get; set; }
        [AllowHtml]
        [Display(Name = "Additional Information")]
        public string otherinformation { get; set; }

        [Display(Name = "Resume received on mail")]
        [EmailAddress]
        [Required(ErrorMessage = "Email ID required ")]
        public string receiveMailAt { get; set; }

        public long jobID { get; set; }
        public string companyLogo { get; set; }
    }


    public class empRegistration
    {
        [Required(ErrorMessage = "please enter company name")]
        public string companyName { get; set; }

        [Remote("isUserNameAvialable", "companyDetails", HttpMethod = "POST", ErrorMessage = "User name already exists. Please enter a different user name.")]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "please enter your email")]
        [EmailAddress]
        public string Email { get; set; }

        public int cityID { get; set; }

        public string website { get; set; }

        public int companyIndustry { get; set; }
        public int employerTypeID { get; set; }
        public string password { get; set; }
        public long employerID { get; set; }


        public HttpPostedFileBase files { get; set; }
    }

    public class candidateRegistration
    {
        [Required(ErrorMessage = "please enter your full name")]
        public string candidateName { get; set; }

        [Remote("isUserNameAvialable", "companyDetails", HttpMethod = "POST", ErrorMessage = "User name already exists. Please enter a different user name.")]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "please enter your email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "please enter contact person number")]
        [RegularExpression(@"^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[789]\d{9}$", ErrorMessage = "Invalid phone number")]
        [Display(Name = "Contact Number")]
        public long candidatePhone { get; set; }

        
        public string password { get; set; }
        public long candidateID { get; set; }

    }

    [MetadataType(typeof(qendidatePHDValidation))]
    public partial class qendidatePHD
    {
    }
    public class qendidatePHDValidation
    {
        [Required(ErrorMessage = "please enter certificate name")]
        public string courseField { get; set; }

       
        [Required(ErrorMessage = "please enter athourity name")]       
        public string collegeUniversity { get; set; }

        [Required(ErrorMessage = "please enter certificate number")]
        public long phdTitle { get; set; }

        [Required(ErrorMessage = "please enter start date")]        
        public DateTime phdStart { get; set; }

        [Required(ErrorMessage = "please enter end date")]      
        public DateTime phdEnd { get; set; }

    }



    public class eforgotPassword
    {
        //[Remote("isUserNameAvialable", "companyDetails", HttpMethod = "POST", ErrorMessage = "User name already exists. Please enter a different user name.")]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "please enter your email")]
        [EmailAddress]
        public string Email { get; set; }

        public string password { get; set; }
    }
    public class qenforgotPassword
    {
        //[Remote("isUserNameAvialable", "companyDetails", HttpMethod = "POST", ErrorMessage = "User name already exists. Please enter a different user name.")]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "please enter your email")]
        [EmailAddress]
        public string Email { get; set; }

        public string password { get; set; }
    }

    public class AuxoDashBoard
    {
        public string JobID { get; set; }
        public string jobTitle { get; set; }
        public int orionCV { get; set; }
        public int orionCVC1 { get; set; }
        public int orionCVC2 { get; set; }
        public int orionCVC3 { get; set; }

        public int orionCOMM { get; set; }
        public int orionPASS { get; set; }
        public int orionMEET { get; set; }
        public int shortList { get; set; }

    }
    public class mailReceivedInterested
    {
        public string qenName { get; set; }
        public bool mailReceivedjobChancgeInterested { get; set; }
        public long qenID { get; set; }
        public string qenEmail { get; set; }
        public bool selectedcandidate { get; set; }
    }

    public class mailReceivedtestSchedule
    {
        public string qenName { get; set; }
        public long qenID { get; set; }
        public bool mailReceivedscheduled { get; set; }
        public string qenEmail { get; set; }
        public int testScheduleCountInt { get; set; }
        public long jobID { get; set; }
        public DateTime testScheduledDateTime { get; set; }
        public int matchSkills { get; set; }
    }

}