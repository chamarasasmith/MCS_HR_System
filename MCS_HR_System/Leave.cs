//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MCS_HR_System
{
    using System;
    using System.Collections.Generic;
    
    public partial class Leave
    {
        public int Leave_ID { get; set; }
        public string Emp_ID { get; set; }
        public Nullable<System.DateTime> L_Date { get; set; }
        public Nullable<int> LT_ID { get; set; }
        public string Status { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Leave_Type Leave_Type { get; set; }
    }
}
