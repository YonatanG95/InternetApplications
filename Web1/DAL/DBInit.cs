using System.Collections.Generic;
using Web1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


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
                new Doctor{ID="123456123", FirstName="Amir", LastName="Mizrahi", Specialization="Ophthalmologist"},
                new Doctor{ID="987654321", FirstName="Inon", LastName="Weber", Specialization="Neurologist"},
                new Doctor{ID="543216789", FirstName="Or", LastName="Chech", Specialization="Cardiologist"},
                new Doctor{ID="098765432", FirstName="Maayan", LastName="Katz", Specialization="Orthopedist"},             
                new Doctor{ID="456456456", FirstName="Itay", LastName="Menachem", Specialization="Surgeon"},
                new Doctor{ID="123451234", FirstName="Sharon", LastName="Vaksman", Specialization="Otolaryngologist"}
            };
            doctors.ForEach(d => context.Doctors.Add(d));
            context.SaveChanges();

            //Initialize patients
            var patients = new List<Patient>
            {
                new Patient{ID="444444123", FirstName="Yosef", LastName="Yaakov", Gender="M", Address="Hatikva 6", City="Ramla", Age=23, Latitude=31.92923, Longitude=34.8823322},
                new Patient{ID="123456789", FirstName="Ofir", LastName="Glozman", Gender="M", Address="Lean 2", City="Ramat Gan", Age=22, Latitude=32.0837731, Longitude=34.810031},
                new Patient{ID="853980142", FirstName="Tal", LastName="Hakimi", Gender="F", Address="Hashalom 2", City="Tel Aviv", Age=54, Latitude=32.072110, Longitude=34.794950},
                new Patient{ID="769324011", FirstName="Chen", LastName="Cohen", Gender="F", Address="Haprahim 5", City="Ashdod", Age=67, Latitude=31.788590, Longitude=34.653840},
                new Patient{ID="111908089", FirstName="Sagi", LastName="Zabirov", Gender="M", Address="Gefen 10", City="Haifa", Age=4, Latitude=32.817280, Longitude=34.988762},
                new Patient{ID="098098098", FirstName="Sapir", LastName="Shem Tov", Gender="F", Address="Hanasi 48", City="Hadera", Age=66, Latitude=32.437410, Longitude=34.925620},
                new Patient{ID="111222111", FirstName="Gil", LastName="Levy", Gender="M", Address="Sela 15", City="Atlit", Age=55, Latitude=32.688960, Longitude=34.941720},
                new Patient{ID="555444555", FirstName="Doron", LastName="Alpert", Gender="M", Address="Gardom 55", City="Rishon Lezion", Age=27, Latitude=31.961020, Longitude=34.801620},
                new Patient{ID="666888777", FirstName="Dror", LastName="Nitzan", Gender="M", Address="Moshe Sharet 32", City="Ofaqim", Age=84, Latitude=31.309490, Longitude=34.620579},
                new Patient{ID="437689143", FirstName="Bar", LastName="Kaplansky", Gender="F", Address="Agur 7", City="Eilat", Age=22, Latitude=29.550360, Longitude=34.952278},
                new Patient{ID="757354609", FirstName="Eden", LastName="Braha", Gender="F", Address="Moshe Dayan 16", City="Or Yehuda", Age=16, Latitude=32.027531, Longitude=34.860470},
                new Patient{ID="012768445", FirstName="Tom", LastName="Rot", Gender="M", Address="Maayan 27", City="Givataim", Age=10, Latitude=32.070810, Longitude=34.812880},
                new Patient{ID="224887556", FirstName="Asaf", LastName="Shimon", Gender="M", Address="Herzel 121", City="Ramat Gan", Age=65, Latitude=32.083550, Longitude=34.815500},
                new Patient{ID="336227227", FirstName="Yuval", LastName="Roter", Gender="M", Address="Shmuel 46", City="Netania", Age=45, Latitude=32.329370, Longitude=34.856540},
                new Patient{ID="888888444", FirstName="Nimrod", LastName="Cohen", Gender="M", Address="Weitzman 84", City="Or Akiva", Age=42, Latitude=32.510170, Longitude=34.919730},
                new Patient{ID="343343000", FirstName="Guy", LastName="Roimi", Gender="M", Address="Rotshild 67", City="Petah Tikva", Age=49, Latitude=32.089870, Longitude=34.880451},
                new Patient{ID="090121121", FirstName="Yaniv", LastName="Hary", Gender="M", Address="Nesiey Israel 98", City="Carmiel", Age=51, Latitude=32.908090, Longitude=35.293080},
                new Patient{ID="667788998", FirstName="Bar", LastName="Chen", Gender="F", Address="David 18", City="Qiryat Shemona", Age=76, Latitude=33.212260, Longitude=35.569400},
                new Patient{ID="345098112", FirstName="Matan", LastName="Vered", Gender="M", Address="Itzhak 3", City="Beer Sheva", Age=33, Latitude=31.243870, Longitude=34.793991},
                new Patient{ID="324348756", FirstName="Nathan", LastName="Ifrah", Gender="M", Address="Herzel 23", City="Dimona", Age=35, Latitude=31.067470, Longitude=35.031600}

            };
            patients.ForEach(p => context.Patients.Add(p));
            context.SaveChanges();

            var checkups = new List<Checkup>
            {
                new Checkup{ ID="1111", Result=true, Type="Blood", Patient_ID="324348756"},
                new Checkup{ ID="1112", Result=false, Type="Urine", Patient_ID="343343000"},
                new Checkup{ ID="1113", Result=true, Type="Urine", Patient_ID="667788998"},
                new Checkup{ ID="1114", Result=true, Type="Blood",  Patient_ID="888888444"},
                new Checkup{ ID="1115", Result=false, Type="Blood", Patient_ID="090121121"},
                new Checkup{ ID="1116", Result=false, Type="Urine", Patient_ID="444444123"},
                new Checkup{ ID="1117", Result=false, Type="Eyes", Patient_ID="123456789"},
                new Checkup{ ID="1118", Result=true, Type="Blood", Patient_ID="111222111"},
                new Checkup{ ID="1119", Result=true, Type="Blood", Patient_ID="769324011"},
                new Checkup{ ID="1120", Result=false, Type="Eyes", Patient_ID="757354609"},
                new Checkup{ ID="1121", Result=false, Type="Blood", Patient_ID="757354609"},
                new Checkup{ ID="1122", Result=true, Type="Blood", Patient_ID="555444555"},
                new Checkup{ ID="1123", Result=false, Type="Urine", Patient_ID="012768445"},
                new Checkup{ ID="1124", Result=false, Type="Blood", Patient_ID="012768445"},
                new Checkup{ ID="1125", Result=true, Type="Blood", Patient_ID="111908089"},
                new Checkup{ ID="1126", Result=false, Type="Heart", Patient_ID="437689143"},
                new Checkup{ ID="1127", Result=false, Type="Blood", Patient_ID="336227227"},
                new Checkup{ ID="1128", Result=false, Type="Urine", Patient_ID="224887556"},
                new Checkup{ ID="1129", Result=true, Type="Urine", Patient_ID="111908089"},
                new Checkup{ ID="1130", Result=false, Type="Blood", Patient_ID="666888777"},
                new Checkup{ ID="1131", Result=true, Type="Heart", Patient_ID="437689143"},
                new Checkup{ ID="1132", Result=false, Type="Eyes", Patient_ID="343343000"}
            };
        
            checkups.ForEach(c => context.Checkups.Add(c));
            context.SaveChanges();

            var aptms = new List<Appointment>
            {
                new Appointment{ID = "0001", Doctor_ID="123456123", Patient_ID="111908089", Date_Time=new System.DateTime(2018, 11, 29, 12, 0, 0)},
                new Appointment{ID = "0002", Doctor_ID="987654321", Patient_ID="437689143", Date_Time=new System.DateTime(2018, 12, 15, 11, 0, 0)},
                new Appointment{ID = "0003", Doctor_ID="543216789", Patient_ID="343343000", Date_Time=new System.DateTime(2018, 11, 15, 13, 0, 0)},
                new Appointment{ID = "0004", Doctor_ID="098765432", Patient_ID="224887556", Date_Time=new System.DateTime(2018, 11, 3, 14, 0, 0)},
                new Appointment{ID = "0005", Doctor_ID="456456456", Patient_ID="555444555", Date_Time=new System.DateTime(2018, 11, 7, 9, 0, 0)},
                new Appointment{ID = "0006", Doctor_ID="123451234", Patient_ID="343343000", Date_Time=new System.DateTime(2018, 12, 11, 10, 0, 0)},
                new Appointment{ID = "0007", Doctor_ID="123456123", Patient_ID="888888444", Date_Time=new System.DateTime(2018, 11, 6, 11, 0, 0)},
                new Appointment{ID = "0008", Doctor_ID="456456456", Patient_ID="090121121", Date_Time=new System.DateTime(2018, 11, 29, 11, 0, 0)},
                new Appointment{ID = "0009", Doctor_ID="098765432", Patient_ID="667788998", Date_Time=new System.DateTime(2018, 11, 18, 14, 0, 0)},
                new Appointment{ID = "0010", Doctor_ID="098765432", Patient_ID="757354609", Date_Time=new System.DateTime(2018, 12, 23, 15, 0, 0)},
                new Appointment{ID = "0011", Doctor_ID="456456456", Patient_ID="123456789", Date_Time=new System.DateTime(2018, 11, 25, 16, 0, 0)},
                new Appointment{ID = "0012", Doctor_ID="123456123", Patient_ID="444444123", Date_Time=new System.DateTime(2018, 12, 22, 11, 0, 0)},
                new Appointment{ID = "0013", Doctor_ID="543216789", Patient_ID="667788998", Date_Time=new System.DateTime(2018, 11, 29, 10, 0, 0)},
                new Appointment{ID = "0014", Doctor_ID="123456123", Patient_ID="123456789", Date_Time=new System.DateTime(2018, 11, 21, 16, 0, 0)},
                new Appointment{ID = "0015", Doctor_ID="543216789", Patient_ID="769324011", Date_Time=new System.DateTime(2018, 11, 30, 11, 0, 0)},
                new Appointment{ID = "0016", Doctor_ID="123451234", Patient_ID="343343000", Date_Time=new System.DateTime(2018, 12, 11, 9, 0, 0)},
                new Appointment{ID = "0017", Doctor_ID="123456123", Patient_ID="111908089", Date_Time=new System.DateTime(2018, 11, 11, 12, 0, 0)},
                new Appointment{ID = "0018", Doctor_ID="123456123", Patient_ID="343343000", Date_Time=new System.DateTime(2018, 11, 19, 12, 0, 0)},
            };

            aptms.ForEach(a => context.Appointments.Add(a));
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
            ApplicationUser doctorUser2 = new ApplicationUser { UserName = "orc@gmail.com", Email = "orc@gmail.com", Id = "543216789" };
            userManager.Create(doctorUser2, "Aa123456!");
            userManager.AddToRole(doctorUser2.Id, "Doctor");

            ApplicationUser patientUser1 = new ApplicationUser { UserName = "yosefy@gmail.com", Email = "yosefy@gmail.com", Id = "444444123" };
            userManager.Create(patientUser1, "Aa123456!");
            userManager.AddToRole(patientUser1.Id, "Patient");
            appContext.SaveChanges();
            ApplicationUser patientUser2 = new ApplicationUser { UserName = "ofir@gmail.com", Email = "ofir@gmail.com", Id = "123456789" };
            userManager.Create(patientUser2, "Aa123456!");
            userManager.AddToRole(patientUser2.Id, "Patient");
            ApplicationUser patientUser3 = new ApplicationUser { UserName = "yaniv@gmail.com", Email = "yaniv@gmail.com", Id = "090121121" };
            userManager.Create(patientUser3, "Aa123456!");
            userManager.AddToRole(patientUser3.Id, "Patient");


            appContext.SaveChanges();
            context.SaveChanges();
        }
    }
}