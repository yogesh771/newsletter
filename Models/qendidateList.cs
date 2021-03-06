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
    
    public partial class qendidateList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public qendidateList()
        {
            this.qendidateGraduations = new HashSet<qendidateGraduation>();
            this.qendidatePGraduations = new HashSet<qendidatePGraduation>();
            this.qendidatePHDs = new HashSet<qendidatePHD>();
            this.qenHigherSecondaries = new HashSet<qenHigherSecondary>();
            this.qenReferences = new HashSet<qenReference>();
            this.qenSecondaries = new HashSet<qenSecondary>();
            this.qenEmpDetails = new HashSet<qenEmpDetail>();
            this.qenSkills = new HashSet<qenSkill>();
        }
    
        public long qenID { get; set; }
        public string qenName { get; set; }
        public string qenEmail { get; set; }
        public string qenLinkdInUrl { get; set; }
        public string qenPhone { get; set; }
        public string qenNationality { get; set; }
        public string qenPassport { get; set; }
        public Nullable<System.DateTime> dataIsCreated { get; set; }
        public Nullable<System.DateTime> dataIsUpdated { get; set; }
        public int roleID { get; set; }
        public bool isDelete { get; set; }
        public bool isActive { get; set; }
        public string password { get; set; }
        public string qenResume { get; set; }
        public string qenCoverLetter { get; set; }
        public Nullable<bool> IsReferenceCleared { get; set; }
        public string qenImage { get; set; }
        public Nullable<int> genderID { get; set; }
        public string CareerObjective { get; set; }
        public Nullable<System.DateTime> dob { get; set; }
        public Nullable<bool> socialCheck { get; set; }
        public Nullable<double> latitude { get; set; }
        public Nullable<double> longitude { get; set; }
        public string City { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string streetNo { get; set; }
        public string qenAddress { get; set; }
        public string zipCode { get; set; }
    
        public virtual genderList genderList { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<qendidateGraduation> qendidateGraduations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<qendidatePGraduation> qendidatePGraduations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<qendidatePHD> qendidatePHDs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<qenHigherSecondary> qenHigherSecondaries { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<qenReference> qenReferences { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<qenSecondary> qenSecondaries { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<qenEmpDetail> qenEmpDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<qenSkill> qenSkills { get; set; }
    }
}
