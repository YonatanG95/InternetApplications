using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web1.Models
{
    public class Doctor
    {
        public string ID { get; set; }
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialization { get; set; }
    
        public virtual ICollection<Appointments> Appointments { get; set; }
       
        public virtual ICollection<Checkups> Checkups { get; set; }
    }
}