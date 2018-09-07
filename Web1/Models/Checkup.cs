using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web1.Models
{
    public class Checkup
    {
        public string CheckupID { get; set; }

        [Association(name: "PatientCheckup", thisKey: "Patient_PatientID", otherKey: "PatientID", IsForeignKey = true)]
        public virtual Patient Patient_PatientID { get; set; }

        [Association(name: "DoctorCheckup", thisKey: "Doctor_DoctorID", otherKey: "DoctorID", IsForeignKey = true)]
        public virtual Doctor Doctor_DoctorID { get; set; }

        public string type { get; set; }

        public bool result { get; set; }
    }
}