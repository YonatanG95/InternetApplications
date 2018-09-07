using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web1.Models
{
    public class Checkup
    {
        public string CheckupID { get; set; }
        public virtual Patient Patient_PatientID { get; set; }

        public virtual Doctor Doctor_DoctorID { get; set; }

        public string type { get; set; }

        public bool result { get; set; }
    }
}