using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web1.Models;

namespace Web1.DAL
{
    public class DBInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<WebContext>
    {
        protected override void Seed(WebContext context)
        {
            //Initialize doctors
            var doctors = new List<Doctor>
            {
                new Doctor{ID="123456", FirstName="Amir", LastName="Mizrahi", Password="123456", Specialization="Eyes"}
            };
            doctors.ForEach(d => context.Doctors.Add(d));
            context.SaveChanges();

            //Initialize patients
            var patients = new List<Patient>
            {
                new Patient{ID="444444", FirstName="Yosef", LastName="Yaakov", Password="123456", Address="Hatikva 6", City="Ramla", Age=23, Latitude=31.92923, Longitude=34.8823322, Zip="43254"}
            };
            patients.ForEach(p => context.Patients.Add(p));
            context.SaveChanges();

            //Initialize Checkups
            Patient pa = new Patient
            {
                ID = "654321",
                FirstName = "Yosef",
                LastName = "Yaakov",
                Password = "123456",
                Address = "Hatikva 6",
                City = "Ramla",
                Age = 23,
                Latitude = 31.92923,
                Longitude = 34.8823322,
                Zip = "43254"
            };

            Doctor doc = new Doctor { ID = "333333", FirstName = "Amir", LastName = "Mizrahi", Password = "123456", Specialization = "Eyes" };
            var checkups = new List<Checkup>
            {
                new Checkup{ID="111", Type="Blood", Result=true, Doctor=doc}
            };
            //checkups.ForEach(c => context.Checkups.Add(c));
            context.SaveChanges();
        }
    }
}