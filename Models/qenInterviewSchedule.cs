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
    
    public partial class qenInterviewSchedule
    {
        public long meetID { get; set; }
        public long JobID { get; set; }
        public long qenID { get; set; }
        public bool meetScheduledMailSent { get; set; }
        public System.DateTime meetMailSentDateTime { get; set; }
        public System.DateTime meetMailRepliedDateTime { get; set; }
        public Nullable<System.DateTime> meetScheduledDateTime { get; set; }
        public string meetPreferredMedium { get; set; }
        public int reScheduled { get; set; }
        public string reasonReScheduled { get; set; }
        public Nullable<System.DateTime> dataIsCreated { get; set; }
        public Nullable<System.DateTime> dataIsUpdated { get; set; }
        public bool meetScheduledMailRecieved { get; set; }
    
        public virtual jobDetail jobDetail { get; set; }
        public virtual qendidateList qendidateList { get; set; }
    }
}