using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;


namespace Web1.DAL
{
    // Change DropCreateDatabaseIfModelChanges to DropCreateDatabaseAlways if you want to force the Seed function to trigger
    // You need to close the connection to the db if the db is currently in use error pops up, you do that by right clicking the db in server explorer>Close connection
    public class DBInit : System.Data.Entity.DropCreateDatabaseIfModelChanges<WebContext>
    {
        protected override void Seed(WebContext context)
        {
            //Initialize doctors
            var doctors = new List<Doctor>
            {
                new Doctor{ID="123456123", FirstName="Amir", LastName="Mizrahi", Password="123456", Specialization="Eyes"},
                new Doctor{ID="987654321", FirstName="Inon", LastName="Weber", Password="123456", Specialization="Neurologist"},
                new Doctor{ID="543216789", FirstName="Or", LastName="Chech", Password="123456", Specialization="Cardiologist"},
                new Doctor{ID="098765432", FirstName="Maayan", LastName="Katz", Password="123456", Specialization="Orthopedist"}             
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


            // Changes related to account management
            ApplicationDbContext appContext = new ApplicationDbContext();

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appContext));

            // Create doctor and patient roles
            IdentityRole patientRole = new IdentityRole("Patient");
            appContext.Roles.Add(patientRole);
            IdentityRole doctorRole = new IdentityRole("Doctor");
            appContext.Roles.Add(doctorRole);
            appContext.SaveChanges();

            // Create some users
            ApplicationUser doctorUser1 = new ApplicationUser { UserName = "amirm@gmail.com", Email = "amirm@gmail.com", Id = "123456123" };
            userManager.Create(doctorUser1, "Aa123456!");
            userManager.AddToRole(doctorUser1.Id, "Doctor");
            ApplicationUser patientUser1 = new ApplicationUser { UserName = "yosefy@gmail.com", Email = "yosefy@gmail.com", Id = "444444123" };
            userManager.Create(patientUser1, "Aa123456!");
            userManager.AddToRole(patientUser1.Id, "Patient");
            appContext.SaveChanges();

            var appUsers = new List<ApplicationUser>
            {
                
                new ApplicationUser { UserName = "inonw@gmail.com", Email = "inonw@gmail.com", Id = "987654321"},
                new ApplicationUser { UserName = "yosefy@gmail.com", Email = "yosefy@gmail.com", Id = "444444123"}
            };

            context.SaveChanges();
        }
    }
}