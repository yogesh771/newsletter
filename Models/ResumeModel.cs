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

 
    
}