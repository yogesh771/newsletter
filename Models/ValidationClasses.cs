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
        public string collegeName { get; set; }

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
        public string qenPhone { get; set; }
        public string qenNationality { get; set; }
        public string qenPassport { get; set; }


        [Required(ErrorMessage = "please enter your date of birth ")]
        public DateTime dob { get; set; }

        [Required(ErrorMessage = "please enter your city")]
        public string City { get; set; }
        


         [Required(ErrorMessage = "please enter your state")]
        public string state { get; set; }

        [Required(ErrorMessage = "please enter your country")]
        public string country { get; set; }

        [DataType(DataType.PostalCode)]
        [Required(ErrorMessage = "please enter your zipCode")]
        public string zipCode { get; set; }

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




    public class requiredSkills
    {
        [Display(Name = "Minimum education")]
        public string EducationReq { get; set; }

        [Display(Name = "Technical skills")]
        public string skills { get; set; }

        public long jobID { get; set; }
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


}