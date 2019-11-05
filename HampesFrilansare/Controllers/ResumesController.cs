using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HampesFrilansare.Models;

namespace HampesFrilansare.Controllers
{
    public class ResumesController : Controller
    {
        private hampesfrilansdbEntities db = new hampesfrilansdbEntities();

        // GET: Resumes
        public ActionResult Index()
        {
            var resume = db.Resume.Include(r => r.Driverslicence);
            return View(resume.ToList());
        }

        // GET: Resumes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resume resume = db.Resume.Find(id);
            if (resume == null)
            {
                return HttpNotFound();
            }
            return View(resume);
        }

        // GET: Resumes/Create
        public ActionResult Create()
        {
            ViewBag.licenceID = new SelectList(db.Driverslicence, "licenceID", "type");
            return View();
        }

        // POST: Resumes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "resumeID,profile,coreability,licenceID")] Resume resume)
        {
            if (ModelState.IsValid)
            {
                db.Resume.Add(resume);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.licenceID = new SelectList(db.Driverslicence, "licenceID", "type", resume.licenceID);
            return View(resume);
        }

        // GET: Resumes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resume resume = db.Resume.Find(id);
            if (resume == null)
            {
                return HttpNotFound();
            }
            ViewBag.licenceID = new SelectList(db.Driverslicence, "licenceID", "type", resume.licenceID);
            return View(resume);
        }

        // POST: Resumes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "resumeID,profile,coreability,licenceID")] Resume resume)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resume).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.licenceID = new SelectList(db.Driverslicence, "licenceID", "type", resume.licenceID);
            return View(resume);
        }

        // GET: Resumes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resume resume = db.Resume.Find(id);
            if (resume == null)
            {
                return HttpNotFound();
            }
            return View(resume);
        }

        // POST: Resumes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Resume resume = db.Resume.Find(id);
            db.Resume.Remove(resume);
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
    }
}
