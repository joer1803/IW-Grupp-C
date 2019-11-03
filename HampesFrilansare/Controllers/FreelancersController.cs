using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HampesFrilansare.Models;
using HampesFrilansare.ViewModels;

namespace HampesFrilansare.Controllers
{
    public class FreelancersController : Controller
    {
        private hampesfrilansdbEntities db = new hampesfrilansdbEntities();

        // GET: Freelancers
        public ActionResult Index()
        {
            return View("Index", "_NavbarFreelancer", db.Freelancer.ToList());
        }

        // GET: Freelancers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Freelancer freelancer = db.Freelancer.Find(id);
            if (freelancer == null)
            {
                return HttpNotFound();
            }
            return View(freelancer);
        }

        // GET: Freelancers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Freelancers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "freelancerID,firstname,lastname,phonenumber,email,address,dateofbirth,birthplace,nationality")] Freelancer freelancer)
        {
            if (ModelState.IsValid)
            {
                db.Freelancer.Add(freelancer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(freelancer);
        }

        public ActionResult SignupFreelance()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignupFreelance([Bind(Include = "freelancerID, firstname, lastname, email")] Freelancer freelancer)
        {
            if (ModelState.IsValid)
            {
                db.Freelancer.Add(freelancer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(freelancer);
        }

        // GET: Freelancers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Freelancer freelancer = db.Freelancer.Find(id);
            if (freelancer == null)
            {
                return HttpNotFound();
            }
            return View(freelancer);
        }

        // POST: Freelancers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "freelancerID,firstname,lastname,phonenumber,email,address,dateofbirth,birthplace,nationality")] Freelancer freelancer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(freelancer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(freelancer);
        }

        // GET: Freelancers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Freelancer freelancer = db.Freelancer.Find(id);
            if (freelancer == null)
            {
                return HttpNotFound();
            }
            return View(freelancer);
        }

        // POST: Freelancers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Freelancer freelancer = db.Freelancer.Find(id);
            db.Freelancer.Remove(freelancer);
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

        public ActionResult FreelancerProfile()
        {
            return View(GetProfile(1));
        }

        public FreelancerProfileViewModel GetProfile(int id)
        {
            var profileView = new FreelancerProfileViewModel();

            profileView.freelancer = db.Freelancer.First(i => i.freelancerID == id);
            profileView.resume =
                db.Resume.First(i => i.resumeID == profileView.freelancer.resumeID);
            profileView.languages =
                db.Language.Where(i => i.resumeID == profileView.resume.resumeID).ToList();
            profileView.competences =
                db.Competence.Where(i => i.resumeID == profileView.resume.resumeID).ToList();
            foreach (var c in profileView.competences)
            {
                profileView.skills.AddRange(db.Skill.Where(i => i.competenceID == c.competenceID));
            }
            profileView.experiences =
                db.Experience.Where(i => i.resumeID == profileView.resume.resumeID).ToList();
            profileView.educations = 
                db.Education.Where(i => i.resumeID == profileView.resume.resumeID).ToList();
            profileView.licence = 
                db.Driverslicence.First(i => i.licenceID == profileView.resume.licenceID);

            return profileView;
        }
    }
}