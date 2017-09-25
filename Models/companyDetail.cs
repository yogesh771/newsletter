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
    
    public partial class companyDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public companyDetail()
        {
            this.EmployerDetails = new HashSet<EmployerDetail>();
            this.jobDetails = new HashSet<jobDetail>();
        }
    
        public long companyID { get; set; }
        public string companyName { get; set; }
        public int cityID { get; set; }
        public string address { get; set; }
        public string website { get; set; }
        public int companyIndustry { get; set; }
        public string companyDescription { get; set; }
        public string gstNo { get; set; }
        public string tinNo { get; set; }
        public string ctsNo { get; set; }
        public string logo { get; set; }
        public System.DateTime dataIsCreated { get; set; }
        public bool isActive { get; set; }
        public Nullable<bool> isDeleted { get; set; }
        public Nullable<long> modifiedBy { get; set; }
        public System.DateTime dataIsUpdated { get; set; }
        public int employerTypeID { get; set; }
    
        public virtual city city { get; set; }
        public virtual employerType employerType { get; set; }
        public virtual industry industry { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployerDetail> EmployerDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<jobDetail> jobDetails { get; set; }
    }
}