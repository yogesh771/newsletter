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
    
    public partial class ProfilePerformance
    {
        public long performanceID { get; set; }
        public long qenID { get; set; }
        public long EmployerID { get; set; }
        public Nullable<System.DateTime> ViewedDate { get; set; }
        public Nullable<System.DateTime> Downloaded { get; set; }
        public Nullable<System.DateTime> Contacted { get; set; }
        public System.DateTime dataIsCreated { get; set; }
    
        public virtual EmployerDetail EmployerDetail { get; set; }
        public virtual qendidateList qendidateList { get; set; }
    }
}
