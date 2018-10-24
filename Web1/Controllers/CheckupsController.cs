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
    public class CheckupsController : Controller
    {
        private WebContext db = new WebContext();
        private ApplicationDbContext appDb = new ApplicationDbContext();

        // GET: Checkups
        public ActionResult Index()
        {
            
            if (User.Identity.IsAuthenticated)
            {
                string cid = User.Identity.GetUserId();
                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appDb));
                var roles = userManager.GetRoles(cid);
                if (roles[0] == "Doctor")
                {
                    return View(db.Checkups.ToList());
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

        // GET: Checkups/Details/5
        public ActionResult Details(string id)
        {
            if (User.Identity.IsAuthenticated)
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
                string cid = User.Identity.GetUserId();
                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appDb));
                var roles = userManager.GetRoles(cid);
                if (roles[0] == "Doctor")
                {
                    return View(checkup);
                }
                else
                {
                    if (cid == checkup.Patient_ID)
                    {
                        return View(checkup);
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

        public int RecommendedLabsbyAge()
        {
            int bloodCounter = 0, ctCounter = 0, MRICounter = 0, fecesCounter = 0, urineCounter = 0;
            // Create some sample learning data. In this data,
            // the first two instances belong to a class, the
            // four next belong to another class and the last
            // three to yet another.
            List<double[]> inputs = new List<double[]>();
            List<int> outputs = new List<int>();
            foreach (Checkup checkup in db.Checkups.ToList())
            {
                if (checkup.Result)
                {
                    if (checkup.Type == "Blood")
                    {
                        bloodCounter++;
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
            }
            double[][] inputs1 = inputs.ToArray();


            // The first two are from class Blood=0


            // The next four are from class C.T=1


            // The last three are from class MRI=2
            int totalCounters = bloodCounter + ctCounter + MRICounter + fecesCounter + urineCounter;

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


            }

            int[] outputs1 = outputs.ToArray();

            // Now we will create the K-Nearest Neighbors algorithm. For this
            // example, we will be choosing k = 4. This means that, for a given
            // instance, its nearest 4 neighbors will be used to cast a decision.
            var knn = new KNearestNeighbors(k: 1, distance: new Manhattan());


            // We learn the algorithm:
            knn.Learn(inputs1, outputs1);

            // After the algorithm has been created, we can classify a new instance:
            int answer = knn.Decide(new double[] { 20 }); // answer will be 2.
            return answer;
        }

        // GET: Checkups/Create
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

        // POST: Checkups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Type,Result,Patient_ID")] Checkup checkup)
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
                        db.Checkups.Add(checkup);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                    return View(checkup);
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

        // GET: Checkups/Edit/5
        public ActionResult Edit(string id)
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
            if (User.Identity.IsAuthenticated)
            {
                string cid = User.Identity.GetUserId();
                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appDb));
                var roles = userManager.GetRoles(cid);
                if (roles[0] == "Doctor")
                {
                    return View(checkup);
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

        // POST: Checkups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Type,Result,Patient_ID")] Checkup checkup)
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
                        db.Entry(checkup).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View(checkup);
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

        // GET: Checkups/Delete/5
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
                    Checkup checkup = db.Checkups.Find(id);
                    if (checkup == null)
                    {
                        return HttpNotFound();
                    }
                    return View(checkup);
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

        // POST: Checkups/Delete/5
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
                    Checkup checkup = db.Checkups.Find(id);
                    if (checkup == null)
                    {
                        return HttpNotFound();
                    }
                    db.Checkups.Remove(checkup);
                    db.SaveChanges();
                    return RedirectToAction("Index");
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


        // GET: Checkups/ShowUserCheckups
        public ActionResult ShowUserCheckups()
        {
            if (User.Identity.IsAuthenticated)
            {
                List<Checkup> checkups = new List<Checkup>();
                string cid = User.Identity.GetUserId();
                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appDb));
                var roles = userManager.GetRoles(cid);
                if (roles[0] == "Doctor")
                {
                    //foreach (Checkup checkup in db.Checkups.ToList())
                    //{
                    //    if (cid == checkup.Doctor_ID)
                    //    {
                    //        checkups.Add(checkup);
                    //    }
                    //}
                }
                else
                {
                    foreach (Checkup checkup in db.Checkups.ToList())
                    {
                        if (cid == checkup.Patient_ID)
                        {
                            checkups.Add(checkup);
                        }
                    }
                }
                return View(checkups);
            }
            else
            {
                return View("NotLoggedIn");
            }
        }
    }
}
