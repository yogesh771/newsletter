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
    
    public partial class EmployerDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmployerDetail()
        {
            this.HiredCandidates = new HashSet<HiredCandidate>();
            this.InterViewerComments = new HashSet<InterViewerComment>();
            this.jobDetails = new HashSet<jobDetail>();
            this.jobDetails1 = new HashSet<jobDetail>();
            this.ProfilePerformances = new HashSet<ProfilePerformance>();
        }
    
        public long EmployerID { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string password { get; set; }
        public System.DateTime dataIsCreated { get; set; }
        public bool isActive { get; set; }
        public bool isDelete { get; set; }
        public long companyID { get; set; }
        public int roleID { get; set; }
        public System.DateTime dataIsUpdated { get; set; }
    
        public virtual companyDetail companyDetail { get; set; }
        public virtual EmployerDetail EmployerDetails1 { get; set; }
        public virtual EmployerDetail EmployerDetail1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HiredCandidate> HiredCandidates { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InterViewerComment> InterViewerComments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<jobDetail> jobDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<jobDetail> jobDetails1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProfilePerformance> ProfilePerformances { get; set; }
    }
}
