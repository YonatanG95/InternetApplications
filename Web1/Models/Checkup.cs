using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web1.Models
{
    public class Checkup
    {
        public string CheckupID;
        public virtual Patient Patient { get; set; }

        public virtual Doctor Doctor { get; set; }

        public string type { get; set; }

        public bool result { get; set; }
    }
}