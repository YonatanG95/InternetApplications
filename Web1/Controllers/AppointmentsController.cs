using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace Web1.Controllers
{
    public class AppointmentsController : Controller
    {
        private WebContext db = new WebContext();
        private ApplicationDbContext appDb = new ApplicationDbContext();

        // GET: Appointments
        public ActionResult Index()
        {
            return View(db.Appointments.ToList());
        }

        // GET: Appointments/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: Appointments/PatientCreate
        public ActionResult PatientCreate()
        {
            if (User.Identity.IsAuthenticated)
            {
                string id = User.Identity.GetUserId();
                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appDb));
                var roles = userManager.GetRoles(User.Identity.GetUserId());
                if (roles[0] == "Patient")
                {
                    return View();
                }
                else
                {
                    // A doctor shouldn't be able to reach this page at all, throw an error at him
                    return View("Error");
                }
            } 
            else
            {
                return View("NotLoggedIn");
            }

        }

        // POST: Appointments/PatientCreate
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PatientCreate([Bind(Include = "ID,Date_Time,IsAvaliable,Doctor_ID,Patient_ID")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                appointment.Patient_ID = User.Identity.GetUserId();
                appointment.Patient = db.Patients.Find(appointment.Patient_ID);
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(appointment);
        }

        // GET: Appointments/DoctorCreate
        public ActionResult DoctorCreate()
        {
            if (User.Identity.IsAuthenticated)
            {
                string id = User.Identity.GetUserId();
                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appDb));
                var roles = userManager.GetRoles(User.Identity.GetUserId());
                if (roles[0] == "Doctor")
                {
                    return View();
                }
                else
                {
                    // A patient shouldn't be able to reach this page at all, throw an error at him
                    return View("Error");
                }
            }
            else
            {
                return View("NotLoggedIn");
            }

        }

        // POST: Appointments/DoctorCreate
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoctorCreate([Bind(Include = "ID,Date_Time,IsAvaliable,Doctor_ID,Patient_ID")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                appointment.Doctor_ID = User.Identity.GetUserId();
                appointment.Doctor = db.Doctors.Find(appointment.Doctor_ID);
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                string id = User.Identity.GetUserId();
                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appDb));
                var roles = userManager.GetRoles(User.Identity.GetUserId());
                if (roles[0] == "Doctor")
                {
                    return RedirectToAction("DoctorCreate");
                }
                else
                {
                    return RedirectToAction("PatientCreate");
                }
            }
            else
            {
                return View("NotLoggedIn");
            }
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Date_Time,IsAvaliable,Doctor_ID,Patient_ID")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Date_Time,IsAvaliable,Doctor_ID,Patient_ID")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Appointment appointment = db.Appointments.Find(id);
            db.Appointments.Remove(appointment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost, ActionName("CheckAvailableDate")]
        public string CheckAvailableDate(string doctorID, DateTime dateTime)
        {
            foreach (Appointment appointment in db.Appointments.ToList())
            {
                if (appointment.Doctor_ID == doctorID)
                {
                    if (appointment.Date_Time.Equals(dateTime))
                        return "booked";
                }
            }
            return "not booked";
        }
    }
}
