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

namespace Web1.Controllers
{
    public class PatientsController : Controller
    {
        private WebContext db = new WebContext();
        private ApplicationDbContext appDb = new ApplicationDbContext();

        // GET: Patients
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                string cid = User.Identity.GetUserId();
                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appDb));
                var roles = userManager.GetRoles(cid);
                if (roles[0] == "Doctor")
                {
                    return View(db.Patients.ToList());
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
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,Age,Longitude,Latitude,City,Address,Zip")] Patient patient)
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
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Age,Longitude,Latitude,City,Address,Zip")] Patient patient)
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

    }
}
