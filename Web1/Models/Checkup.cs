using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web1.Models
{
    public class Checkup
    {
        public string ID { get; set; }

        public string Type { get; set; }

        public bool Result { get; set; }

        //public string Doctor_ID { get; set; }

        public string Patient_ID { get; set; }

        public virtual Patient Patient { get; set; }

        //public virtual Doctor Doctor { get; set; }
    }
}