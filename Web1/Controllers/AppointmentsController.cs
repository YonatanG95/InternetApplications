using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Web1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Accord.MachineLearning;
using Accord.Imaging;
using Accord.Math.Distances;


namespace Web1.Controllers
{
    public class AppointmentsController : Controller
    {
        private WebContext db = new WebContext();
        private ApplicationDbContext appDb = new ApplicationDbContext();

        // GET: Appointments
        public ActionResult Index(string docID, string patID)
        {
            
            if (User.Identity.IsAuthenticated)
            {
                string cid = User.Identity.GetUserId();
                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appDb));
                var roles = userManager.GetRoles(cid);
                if (roles[0] == "Doctor")
                {
                    var apps = from a in db.Appointments
                               select a;
                    if (!String.IsNullOrEmpty(docID))
                    {
                        apps = apps.Where(s => s.Doctor_ID.Contains(docID));
                    }
                    if (!String.IsNullOrEmpty(patID))
                    {
                        apps = apps.Where(s => s.Patient_ID.Contains(patID));
                    }
                    //if (!String.IsNullOrEmpty(dateTime))
                    //{
                    //    int ageInt = Int32.Parse(dateTime);
                    //    apps = patients.Where(s => s.Date_Time == dateTime);
                    //}
                    return View(apps.ToList());
                }
                else
                {
                    return View("AccessDenied");
                }
            }
            else
            {
                return View("NotLoggedIn");
            }
        }

        public int RecommendedDoctorsByAge()
        {
            
            //int amirCounter = 0, inonCounter = 0, orCounter = 0, maayanCounter = 0;
            // Create some sample learning data. In this data,
            // the first two instances belong to a class, the
            // four next belong to another class and the last
            // three to yet another.
            List<double[]> inputs = new List<double[]>();
            List<int> outputs = new List<int>();
            List<int> countersdoctors = new List<int>();
            int classcounter = 0;

            

            foreach (Doctor doctor in db.Doctors.ToList())
            {
                
                foreach (Appointment app in db.Appointments.ToList())
                {
                    
                    if (app.Doctor_ID == doctor.ID)
                    {
                        
                        outputs.Add(classcounter);
                        //amirCounter++;
                        Patient p = db.Patients.Find(app.Patient_ID);
                        inputs.Add(new double[] { (p.Age) });

                    }

                }

                classcounter++;
                //Patient p = db.Patients.Find(checkup.Patient_ID);


            }

          /*  foreach (Appointment app in db.Appointments.ToList())
            {

                if (app.Doctor_ID == "987654321")
                {
                    inonCounter++;
                    Patient p = db.Patients.Find(app.Patient_ID);
                    inputs.Add(new double[] { (p.Age) });

                }

                //Patient p = db.Patients.Find(checkup.Patient_ID);


            }

            foreach (Appointment app in db.Appointments.ToList())
            {

                if (app.Doctor_ID == "543216789")
                {
                    orCounter++;
                    Patient p = db.Patients.Find(app.Patient_ID);
                    inputs.Add(new double[] { (p.Age) });

                }

                //Patient p = db.Patients.Find(checkup.Patient_ID);


            }

            foreach (Appointment app in db.Appointments.ToList())
            {

                if (app.Doctor_ID == "098765432")
                {
                    maayanCounter++;
                    Patient p = db.Patients.Find(app.Patient_ID);
                    inputs.Add(new double[] { (p.Age) });

                }

                //Patient p = db.Patients.Find(checkup.Patient_ID);


            }*/


            double[][] inputs1 = inputs.ToArray();


            // The first two are from class Blood=0


            // The next four are from class C.T=1


            // The last three are from class MRI=2
            /*int totalCounters = amirCounter + inonCounter + orCounter + maayanCounter;

            for (int i = 0; i < totalCounters; i++)
            {
                if (amirCounter != 0)
                {
                    outputs.Add(0);
                    amirCounter--;

                }
                else if (inonCounter != 0)
                {
                    outputs.Add(1);
                    inonCounter--;
                }
                else if (orCounter != 0)
                {
                    outputs.Add(2);
                    orCounter--;
                }
                else if (maayanCounter != 0)
                {
                    outputs.Add(3);
                    maayanCounter--;
                }



            }*/

            int[] outputs1 = outputs.ToArray();

            // Now we will create the K-Nearest Neighbors algorithm. For this
            // example, we will be choosing k = 4. This means that, for a given
            // instance, its nearest 4 neighbors will be used to cast a decision.
            var knn = new KNearestNeighbors(k: 1, distance: new Manhattan());


            // We learn the algorithm:
            knn.Learn(inputs1, outputs1);

            // After the algorithm has been created, we can classify a new instance:
            //Enter age here and get doctor by class number
            int answer = knn.Decide(new double[] { 25 });
            return answer;
        }
        // GET: Appointments/Details/5
        public ActionResult Details(string id)
        {
            if (User.Identity.IsAuthenticated)
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
                string cid = User.Identity.GetUserId();
                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appDb));
                var roles = userManager.GetRoles(cid);
                if (roles[0] == "Doctor")
                {
                    return View(appointment);
                }
                else
                {
                    if (cid == appointment.Patient_ID)
                    {
                        return View(appointment);
                    }
                    else
                    {
                        return View("AccessDenied");
                    }
                }
            }
            else
            {
                return View("NotLoggedIn");
            }
        }

        // GET: Appointments/Create
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
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
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    string cid = User.Identity.GetUserId();
                    appointment.Patient_ID = cid;
                    appointment.Patient = db.Patients.Find(cid);
                    db.Appointments.Add(appointment);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                    
                }
                return View(appointment);
            }
            else
            {
                return View("NotLoggedIn");
            }
            
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
            if (User.Identity.IsAuthenticated)
            {
                string cid = User.Identity.GetUserId();
                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appDb));
                var roles = userManager.GetRoles(cid);
                if (roles[0] == "Doctor")
                {
                    return View(appointment);
                }
                else
                {
                    if (cid == appointment.Patient_ID)
                    {
                        return View(appointment);
                    }
                    else
                    {
                        return View("AccessDenied");
                    }
                }
            }
            else
            {
                return View("NotLoggedIn");
            }
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Date_Time,IsAvaliable,Doctor_ID,Patient_ID")] Appointment appointment)
        {
            if (User.Identity.IsAuthenticated)
            {
                string cid = User.Identity.GetUserId();
                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appDb));
                var roles = userManager.GetRoles(cid);
                if (roles[0] == "Doctor")
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(appointment).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View(appointment);
                }
                else
                {
                    if (cid == appointment.Patient_ID)
                    {
                        if (ModelState.IsValid)
                        {
                            appointment.Patient = db.Patients.Find(cid);
                            db.Entry(appointment).State = EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        return View(appointment);
                    }
                    else
                    {
                        return View("AccessDenied");
                    }
                }
            }
            else
            {
                return View("NotLoggedIn");
            }
        }

        // GET: Appointments/Delete/5
        public ActionResult Delete(string id)
        {
            if (User.Identity.IsAuthenticated)
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
                string cid = User.Identity.GetUserId();
                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appDb));
                var roles = userManager.GetRoles(cid);
                if (roles[0] == "Doctor")
                {
                    return View(appointment);
                }
                else
                {
                    if (cid == appointment.Patient_ID)
                    {
                        return View(appointment);
                    }
                    else
                    {
                        return View("AccessDenied");
                    }
                }
            }
            else
            {
                return View("NotLoggedIn");
            }
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (User.Identity.IsAuthenticated)
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
                string cid = User.Identity.GetUserId();
                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appDb));
                var roles = userManager.GetRoles(cid);
                if (roles[0] == "Doctor")
                {
                    db.Appointments.Remove(appointment);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    if (cid == appointment.Patient_ID)
                    {
                        if (DateTime.Compare(DateTime.Now, appointment.Date_Time) < 0)
                        {
                            db.Appointments.Remove(appointment);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["msg"] = "Cannot delete this appointment because it already happened.";
                            return View(appointment);
                        }
                    }
                    else
                    {
                        return View("AccessDenied");
                    }
                }
            }
            else
            {
                return View("NotLoggedIn");
            }
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

        // GET: Appointments/ShowUserAppointments
        public ActionResult ShowUserAppointments()
        {
            if (User.Identity.IsAuthenticated)
            {
                List<Appointment> appointments = new List<Appointment>();
                string cid = User.Identity.GetUserId();
                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appDb));
                var roles = userManager.GetRoles(cid);
                if (roles[0] == "Doctor")
                {
                    foreach (Appointment appointment in db.Appointments.ToList())
                    {
                        if (cid == appointment.Doctor_ID)
                        {
                            appointments.Add(appointment);
                        }
                    }
                }
                else
                {
                    foreach (Appointment appointment in db.Appointments.ToList())
                    {
                        if (cid == appointment.Patient_ID)
                        {
                            appointments.Add(appointment);
                        }
                    }
                }
                return View(appointments);
            }
            else
            {
                return View("NotLoggedIn");
            }
        }
    }
}
