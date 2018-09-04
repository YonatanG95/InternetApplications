using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web1.Models;

namespace Web1.Controllers
{
    public class HomeController : Controller
    {

        private WebContext db = new WebContext();

        public ActionResult Index()
        {
            InsertDoctor();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public void InsertDoctor(Doctor doctor)
        {
            if(ModelState.IsValid)
            {
                if (db.Doctors.Find(doctor.DoctorID) == null)
                {
                    db.Doctors.Add(doctor);
                    db.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError("DoctorID", "DoctorID already exists");
                }
            }
        }

        public void DeleteDoctor(String doctorID)
        {
                Doctor doctor = db.Doctors.Find(doctorID);
                if (doctor != null)
                {
                    db.Doctors.Remove(doctor);
                    db.SaveChanges();
                }
        }
     
        //TODO understand + finish
        public void EditDoctor(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                Doctor doc = db.Doctors.Find(doctor.DoctorID);
                if (doc != null)
                {
                    doc.FirstName = doctor.FirstName;
                
                }
                else
                {
                    ModelState.AddModelError("DoctorID", "DoctorID doesn't exist");
                }
            }
        }
    }
}