//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NewLetter.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class HiredCandidate
    {
        public long hiredID { get; set; }
        public long jobID { get; set; }
        public long qenID { get; set; }
        public long EmployerID { get; set; }
        public System.DateTime joinningDate { get; set; }
        public int ExpectedSalaryMonthly { get; set; }
        public int ExpectedSalaryGross { get; set; }
        public string Designtion { get; set; }
        public string SpecialComment { get; set; }
    
        public virtual EmployerDetail EmployerDetail { get; set; }
        public virtual jobDetail jobDetail { get; set; }
        public virtual qendidateList qendidateList { get; set; }
    }
}
