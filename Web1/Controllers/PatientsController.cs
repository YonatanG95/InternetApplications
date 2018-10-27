using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using System;
using Accord.MachineLearning;
using Accord.Imaging;
using Accord.Math.Distances;

namespace Web1.Controllers
{
    public class PatientsController : Controller
    {
        private WebContext db = new WebContext();
        private ApplicationDbContext appDb = new ApplicationDbContext();
        
        // GET: Patients
        public ActionResult Index(string fname, string lname, string age)
        {
            
            if (User.Identity.IsAuthenticated)
            {
                
                string cid = User.Identity.GetUserId();
                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appDb));
                var roles = userManager.GetRoles(cid);
                if (roles[0] == "Doctor")
                {
                    //return View(db.Patients.ToList());
                    var patients = from p in db.Patients
                                   select p;
                    if (!String.IsNullOrEmpty(fname))
                    {
                        patients = patients.Where(s => s.FirstName.Contains(fname));
                    }
                    if (!String.IsNullOrEmpty(lname))
                    {
                        patients = patients.Where(s => s.LastName.Contains(lname));
                    }
                    if (!String.IsNullOrEmpty(age))
                    {
                        int ageInt = Int32.Parse(age);
                        patients = patients.Where(s => s.Age == ageInt);
                    }
                    return View(patients.ToList());
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

        // GET: Patients/Details/5
        public ActionResult Details(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                string cid = User.Identity.GetUserId();
                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appDb));
                var roles = userManager.GetRoles(cid);
                if (roles[0] == "Doctor")
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
                else
                {
                    if (cid == id)
                    {
                        Patient patient = db.Patients.Find(id);
                        if (patient == null)
                        {
                            return HttpNotFound();
                        }
                        return View(patient);
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

        // GET: Patients/Create
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                string cid = User.Identity.GetUserId();
                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appDb));
                var roles = userManager.GetRoles(cid);
                if (roles[0] == "Doctor")
                {
                    return View();
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

        // POST: Patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,Age,Gender,Longitude,Latitude,City,Address")] Patient patient)
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
                        db.Patients.Add(patient);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                    return View(patient);
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

        // GET: Patients/Edit/5
        public ActionResult Edit(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                string cid = User.Identity.GetUserId();
                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appDb));
                var roles = userManager.GetRoles(cid);
                if (roles[0] == "Doctor")
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
                else
                {
                    if (cid == id)
                    {
                        Patient patient = db.Patients.Find(id);
                        if (patient == null)
                        {
                            return HttpNotFound();
                        }
                        return View(patient);
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

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Age,Gender,Longitude,Latitude,City,Address")] Patient patient)
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
                        db.Entry(patient).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View(patient);
                }
                else
                {
                    if (cid == patient.ID)
                    {
                        if (ModelState.IsValid)
                        {
                            db.Entry(patient).State = EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        return View(patient);
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

        // GET: Patients/Delete/5
        public ActionResult Delete(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                string cid = User.Identity.GetUserId();
                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appDb));
                var roles = userManager.GetRoles(cid);
                if (roles[0] == "Doctor")
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

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                string cid = User.Identity.GetUserId();
                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appDb));
                var roles = userManager.GetRoles(cid);
                if (roles[0] == "Doctor")
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Patient patient = db.Patients.Find(id);
                    db.Patients.Remove(patient);
                    db.SaveChanges();
                    if (ModelState.IsValid)
                    {
                        var user = userManager.FindById(id);
                        if (user == null)
                        {
                            return RedirectToAction("Index");
                        }

                        // Log user off if current user is being deleted
                        if (cid == id)
                        {
                            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                        }
                        var logins = user.Logins;
                        var rolesForUser =  userManager.GetRoles(id);

                        using (var transaction = appDb.Database.BeginTransaction())
                        {
                            foreach (var login in logins.ToList())
                            {
                                userManager.RemoveLogin(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
                            }

                            if (rolesForUser.Count() > 0)
                            {
                                foreach (var item in rolesForUser.ToList())
                                {
                                    var result = userManager.RemoveFromRole(user.Id, item);
                                }
                            }

                            userManager.Delete(user);
                            transaction.Commit();
                        }
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View();
                    }

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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Patients/Search
        public ActionResult Search()
        {
            if (User.Identity.IsAuthenticated)
            {
                string cid = User.Identity.GetUserId();
                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appDb));
                var roles = userManager.GetRoles(cid);
                if (roles[0] == "Doctor")
                {
                    return View();
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

        // POST: Patients/Search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Search(string firstName, int age, string city)
        {
            if (User.Identity.IsAuthenticated)
            {
                string cid = User.Identity.GetUserId();
                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appDb));
                var roles = userManager.GetRoles(cid);
                if (roles[0] == "Doctor")
                {
                    List<Patient> foundPatients = new List<Patient>();
                    List<Patient> patients = db.Patients.ToList();
                    foreach(Patient patient in patients)
                    {
                        if (firstName != null && !(firstName.Contains(patient.FirstName)))
                            continue;

                        // Add age default value 0 in view
                        if (age != 0 && age != patient.Age)
                            continue;
                        if (city != null && !(city.Contains(patient.City)))
                            continue;
                        foundPatients.Add(patient);
                    }
                    return View(foundPatients);
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

        [HttpPost, ActionName("RecommendedCheckupbyAge")]
        public int RecommendedCheckupbyAge(int age)
        {
            //int bloodCounter = 0, ctCounter = 0, MRICounter = 0, fecesCounter = 0, urineCounter = 0;
            // Create some sample learning data. In this data,
            // the first two instances belong to a class, the
            // four next belong to another class and the last
            // three to yet another.
            List<double[]> inputs = new List<double[]>();
            List<int> outputs = new List<int>();
            List<string> types = new List<string>();
            int classcounter=0;
            foreach (Checkup checkup in db.Checkups.ToList())
            {
                if (!types.Contains(checkup.Type))
                {
                    types.Add(checkup.Type);
                }

            }
            foreach (string type in types)
            {

                foreach (Checkup checkup in db.Checkups.ToList())
                {
                    if (checkup.Result)
                    {
                        if (checkup.Type == type)
                        {
                            outputs.Add(classcounter);
                            Patient p = db.Patients.Find(checkup.Patient_ID);
                            inputs.Add(new double[] { (p.Age) });

                        }

                        //Patient p = db.Patients.Find(checkup.Patient_ID);


                    }
                   
                }
                classcounter++;
            }
            /*foreach (Checkup checkup in db.Checkups.ToList())
            {
                if (checkup.Result)
                {
                    if (checkup.Type == "C.T")
                    {
                        ctCounter++;
                        Patient p = db.Patients.Find(checkup.Patient_ID);
                        inputs.Add(new double[] { (p.Age) });
                    }

                    //Patient p = db.Patients.Find(checkup.Patient_ID);


                }
            }
            foreach (Checkup checkup in db.Checkups.ToList())
            {
                if (checkup.Result)
                {
                    if (checkup.Type == "MRI")
                    {
                        MRICounter++;
                        Patient p = db.Patients.Find(checkup.Patient_ID);
                        inputs.Add(new double[] { (p.Age) });
                    }

                    //Patient p = db.Patients.Find(checkup.Patient_ID);


                }
            }
            foreach (Checkup checkup in db.Checkups.ToList())
            {
                if (checkup.Result)
                {
                    if (checkup.Type == "Feces")
                    {
                        fecesCounter++;
                        Patient p = db.Patients.Find(checkup.Patient_ID);
                        inputs.Add(new double[] { (p.Age) });
                    }

                    //Patient p = db.Patients.Find(checkup.Patient_ID);


                }
            }

            foreach (Checkup checkup in db.Checkups.ToList())
            {
                if (checkup.Result)
                {
                    if (checkup.Type == "Urine")
                    {
                        urineCounter++;
                        Patient p = db.Patients.Find(checkup.Patient_ID);
                        inputs.Add(new double[] { (p.Age) });
                    }

                    //Patient p = db.Patients.Find(checkup.Patient_ID);


                }
            }*/
            double[][] inputs1 = inputs.ToArray();


            // The first two are from class Blood=0


            // The next four are from class C.T=1


            // The last three are from class MRI=2
            /*int totalCounters = bloodCounter + ctCounter + MRICounter + fecesCounter + urineCounter;

            for (int i = 0; i < totalCounters; i++)
            {
                if (bloodCounter != 0)
                {
                    outputs.Add(0);
                    bloodCounter--;

                }
                else if (ctCounter != 0)
                {
                    outputs.Add(1);
                    ctCounter--;
                }
                else if (MRICounter != 0)
                {
                    outputs.Add(2);
                    MRICounter--;
                }
                else if (fecesCounter != 0)
                {
                    outputs.Add(3);
                    fecesCounter--;
                }
                else if (urineCounter != 0)
                {
                    outputs.Add(4);
                    urineCounter--;
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
            int answer = knn.Decide(new double[] { age }); // answer will be 2.
            return answer;
        }

        [HttpPost, ActionName("RecommendedDocbyAge")]
        public int RecommendedDocbyAge(int age)
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
            int answer = knn.Decide(new double[] { age });
            return answer;
        }

    }
}
