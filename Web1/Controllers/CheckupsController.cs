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
                    List<Checkup> checkups = new List<Checkup>();
                    foreach (Checkup checkup in db.Checkups.ToList())
                    {
                        if (cid == checkup.Patient_ID)
                        {
                            checkups.Add(checkup);
                        }
                    }
                    return View(checkups);
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
                        if (db.Checkups.Find(checkup.ID) != null)
                        {
                            TempData["msg"] = "Checkup ID already in use.";
                            return View("Error");
                        }
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



        // REDACTED - Now functionality is in Index, left in comment for good measure
        // GET: Checkups/ShowUserCheckups
        /*
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
        */
    }
}
