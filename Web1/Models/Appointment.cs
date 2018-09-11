using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web1.Models
{
    public class Appointment
    {
        public string ID { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
        public DateTime Date_Time { get; set; }
        public bool IsAvaliable { get; set; }
    }
}