using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web1.Models
{
    public class Patient
    {
        public string PatientID { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public long Longitude { get; set; }
        public long Latitude { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
    }
}