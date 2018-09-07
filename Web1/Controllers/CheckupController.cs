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
    public class CheckupController : Controller
    {
        private WebContext db = new WebContext();

        // GET: Checkups/ListCheckups
        public ActionResult ListCheckups()
        {
            return View(db.Checkups.ToList());
        }

        // GET: Checkups/CheckupDetails
        public ActionResult CheckupDetails(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Checkup checkup = db.Checkups.Find(id);
            if (checkup == null)
            {
                return HttpNotFound();
            }
            return View(checkup);
        }


        // GET: Checkups/AddCheckup
        public ActionResult AddCheckup()
        {
            return View();
        }

        // POST: Checkups/AddCheckup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCheckup([Bind(Include = "CheckupID, DoctorID, PatientID, type, result")] Checkup checkup)
        {
            if (ModelState.IsValid)
            {
                if (db.Doctors.Find(checkup.Doctor_DoctorID.DoctorID) != null)
                {
                    if (db.Patients.Find(checkup.Patient_PatientID.PatientID) != null)
                    {
                        db.Checkups.Add(checkup);
                        db.SaveChanges();
                        return RedirectToAction("ListCheckups");
                    }
                    else
                    {
                        ModelState.AddModelError("PatientID", "PatientID does not exist");
                    }
                        
                }
                else
                {
                    ModelState.AddModelError("DoctorID", "DoctorID does not exist");
                }
            }
            return View(checkup);
        }


        // GET: Checkups/UpdateCheckup
        public ActionResult UpdateCheckup(string id)
        {
            if (id.ToString() == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Checkup checkup = db.Checkups.Find(id);
            if (checkup == null)
            {
                return HttpNotFound();
            }
            return View(checkup);
        }

        // POST: Checkups/UpdateCheckup
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCheckup([Bind(Include = "CheckupID, DoctorID, PatientID, type, result")] Checkup checkup)
        {
            if (ModelState.IsValid)
            {
                if (db.Doctors.Find(checkup.Doctor_DoctorID.DoctorID) != null)
                {
                    if (db.Patients.Find(checkup.Patient_PatientID.PatientID) != null)
                    {
                        db.Entry(checkup).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("ListCheckups");
                    }
                    else
                    {
                        ModelState.AddModelError("PatientID", "PatientID does not exist");
                    }

                }
                else
                {
                    ModelState.AddModelError("DoctorID", "DoctorID does not exist");
                }
            }
            return View(checkup);
        }

        // GET: Checkups/DeleteCheckup
        public ActionResult DeleteCheckup(string id)
        {
            if (id.ToString() == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Checkup checkup = db.Checkups.Find(id);
            if (checkup == null)
            {
                return HttpNotFound();
            }
            return View(checkup);
        }

        // POST: Checkups/DeleteCheckup
        [HttpPost, ActionName("DeleteCheckup")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Checkup checkup = db.Checkups.Find(id);
            db.Checkups.Remove(checkup);
            db.SaveChanges();
            return RedirectToAction("ListCheckups");
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