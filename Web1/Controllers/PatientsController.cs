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
    public class PatientsController : Controller
    {
        private WebContext db = new WebContext();


        // GET: Patients/ListPatients
        public ActionResult ListPatients()
        {
            return View(db.Patients.ToList());
        }

        // GET: Patients/PatientDetails
        public ActionResult PatientDetails(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // GET: Patients/CreatePatient
        public ActionResult CreatePatient()
        {
            return View();
        }

        // POST: Patients/CreatePatient
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePatient([Bind(Include = "PatientID, Password, FirstName, LastName, Age, Longitude, Latitude, City, Address, Zip")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                if (db.Patients.Find(patient.PatientID) == null)
                {
                    db.Patients.Add(patient);
                    db.SaveChanges();
                    return RedirectToAction("ListPatients");
                }
                else
                {
                    ModelState.AddModelError("PatientID", "PatientID already exists");
                }
            }
            return View(patient);
        }

        // GET: Patients/DeletePatient
        public ActionResult DeletePatient(string id)
        {
            if (id.ToString() == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/DeletePatient
        [HttpPost, ActionName("DeletePatient")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Patient patient = db.Patients.Find(id);
            db.Patients.Remove(patient);
            db.SaveChanges();
            return RedirectToAction("ListPatients");
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