using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewLetter.Models
{
    public class ResumeModel
    {
        public qendidateList personainfo { get; set; }
        public AcademicModel educationinfo { get; set; }
        public qendidatePHD phdinfo { get; set; }
        public List<qenEmpDetail> employmentinfo { get; set; }
        public List<qenReference> refrences { get; set; }
    }

    public class companyRegistrationModel
    {
        public companyDetail companyInfo { get; set; }
        public EmployerDetail employerInfo { get; set; }

    }

    public class companyRegistrationEmailModel
    {
        public string from { get; set; }
        public string to { get; set; }

        public string subject { get; set; }
        public string body { get; set; }

    }
    public class candidateRegistratioEmailModel
    {
        public string from { get; set; }
        public string to { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
    }
   public class qendidateSearchmodel
    {
        
        public long qenid { get; set; }
        public string qenname { get; set; }
        public string qenexperience { get; set; }
        public string qenskills { get; set; }
        public double qensalary { get; set; }
        public int qencategory { get; set; }
        //public int qencategory2 { get; set; }
        //public int qencategory3 { get; set; }
        public int jobid { get; set; }
    }
    public class hiredCandidateList
    {
        public long qenID { get; set; }
        public string qenName { get; set; }
        public string jobtitle { get; set; }
        public string dateOfJoining { get; set; }
        public long salary { get; set; }
    }
    
}