using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web1.Models;

namespace Web1.DAL
{
    public class DBInit : System.Data.Entity.DropCreateDatabaseIfModelChanges<WebContext>
    {
        protected override void Seed(WebContext context)
        {
            //Initialize doctors
            var doctors = new List<Doctor>
            {
                new Doctor{ID="123456123", FirstName="Amir", LastName="Mizrahi", Password="123456", Specialization="Eyes"},
                new Doctor{ID="987654321", FirstName="Inon", LastName="Weber", Password="123456", Specialization="Family"}
            };
            doctors.ForEach(d => context.Doctors.Add(d));
            context.SaveChanges();

            //Initialize patients
            var patients = new List<Patient>
            {
                new Patient{ID="444444123", FirstName="Yosef", LastName="Yaakov", Password="123456", Address="Hatikva 6", City="Ramla", Age=23, Latitude=31.92923, Longitude=34.8823322, Zip="43254"},
                new Patient{ID="123456789", FirstName="Yonatan", LastName="Glozman", Password="123456", Address="Lean 2", City="Ramat Gan", Age=22, Latitude=32.0837731, Longitude=34.810031, Zip="12345"}
            };
            patients.ForEach(p => context.Patients.Add(p));
            context.SaveChanges();

            var checkups = new List<Checkup>
            {
                new Checkup{ ID="1234", Result=true, Type="Blood", Doctor_ID="987654321", Patient_ID="444444123"}
            };
            checkups.ForEach(c => context.Checkups.Add(c));
            context.SaveChanges();
        }
    }
}