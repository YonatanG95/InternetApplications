using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using Web1.Models;

namespace Web1.Controllers
{
    public class DoctorsController : Controller
    {
        private WebContext db = new WebContext();

        // GET: Doctors/ListDoctors
        public ActionResult ListDoctors()
        {
            return View(db.Doctors.ToList());
        }

        // GET: Doctors/DoctorDetails
        public ActionResult DoctorDetails(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }


        // GET: Doctors/CreateDoctor
        public ActionResult CreateDoctor()
        {
            return View();
        }

        // POST: Doctors/CreateDoctor
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDoctor([Bind(Include = "DoctorID, Password, FirstName, LastName, Specialization")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                if (db.Doctors.Find(doctor.DoctorID) == null)
                {
                    db.Doctors.Add(doctor);
                    db.SaveChanges();
                    return RedirectToAction("ListDoctors");
                }
                else
                {
                    ModelState.AddModelError("DoctorID", "DoctorID already exists");
                }
            }
            return View(doctor);
        }

        // GET: Doctors/EditDoctor
        public ActionResult EditDoctor(string id)
        {
            if (id.ToString() == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/EditDoctor
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDoctor([Bind(Include = "DoctorID,Password, FirstName, LastName, Specialization")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doctor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListDoctors");
            }
            return View(doctor);
        }


        // GET: Doctors/DeleteDoctor
        public ActionResult DeleteDoctor(string id)
        {
            if (id.ToString() == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/DeleteDoctor
        [HttpPost, ActionName("DeleteDoctor")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Doctor doctor = db.Doctors.Find(id);
            db.Doctors.Remove(doctor);
            db.SaveChanges();
            return RedirectToAction("ListDoctors");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}