//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Appointments
    {
        public string ID { get; set; }
        public System.DateTime Date_Time { get; set; }
        public bool IsAvaliable { get; set; }
        public string Doctor_ID { get; set; }
        public string Patient_ID { get; set; }
    
        public virtual Doctors Doctors { get; set; }
        public virtual Patients Patients { get; set; }
    }
}
