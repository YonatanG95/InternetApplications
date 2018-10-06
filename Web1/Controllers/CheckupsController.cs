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
    public class CheckupsController : Controller
    {
        private WebContext db = new WebContext();
        private ApplicationDbContext appDb = new ApplicationDbContext();

        // GET: Checkups
        public ActionResult Index()
        {
            return View(db.Checkups.ToList());
        }

        // GET: Checkups/Details/5
        public ActionResult Details(string id)
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

        // GET: Checkups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Checkups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Type,Result,Doctor_ID,Patient_ID")] Checkup checkup)
        {
            if (ModelState.IsValid)
            {
                db.Checkups.Add(checkup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(checkup);
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
            return View(checkup);
        }

        // POST: Checkups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Type,Result,Doctor_ID,Patient_ID")] Checkup checkup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(checkup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(checkup);
        }

        // GET: Checkups/Delete/5
        public ActionResult Delete(string id)
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

        // POST: Checkups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Checkup checkup = db.Checkups.Find(id);
            db.Checkups.Remove(checkup);
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


        // GET: Checkups/ShowUser
        public ActionResult ShowUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                List<Checkup> checkups = new List<Checkup>();
                string id = User.Identity.GetUserId();
                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appDb));
                var roles = userManager.GetRoles(User.Identity.GetUserId());
                if (roles[0] == "Doctor")
                {
                    foreach (Checkup checkup in db.Checkups.ToList())
                    {
                        if (checkup.Doctor_ID == id)
                        {
                            checkups.Add(checkup);
                        }
                    }
                }
                else
                {
                    foreach (Checkup checkup in db.Checkups.ToList())
                    {
                        if (checkup.Patient_ID == id)
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
