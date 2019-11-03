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
            var freeprof = (from free in db.Freelancer
                            join resume in db.Resume on free.resumeID equals resume.resumeID
                            join comp in db.Competence on resume.resumeID equals comp.resumeID
                            join skill in db.Skill on comp.competenceID equals skill.competenceID
                            join lang in db.Language on resume.resumeID equals lang.resumeID

                            select new
                            {
                                free.freelancerID,
                                free.firstname,
                                free.lastname,
                                free.address,
                                free.phonenumber,
                                free.dateofbirth,
                                free.nationality,
                                free.email,
                                comp.competenceID,
                                comp.name,
                                skillname = skill.name,
                                skillcompid = skill.competenceID,
                                comp.category,
                                skill.rating
                            });
            FreelancerProfileViewModel model = new FreelancerProfileViewModel();
            var frees = freeprof.Where(x => x.freelancerID.Equals(id)).Distinct();
            bool duplicate = false;
            foreach (var free in frees)
            {
                duplicate = false;
                model.id = free.freelancerID;
                model.firstname = free.firstname;
                model.lastname = free.lastname;
                model.address = free.address;
                model.phonenumber = free.phonenumber;
                model.birtdate = free.dateofbirth.ToString();
                model.nationality = free.nationality;
                model.email = free.email;
                    foreach(var c in model.competences)
                    {
                        if(c.competenceID == free.competenceID)
                    {
                        duplicate = true;
                    }
                    }
                if (!duplicate)
                {
                    model.competences.Add(new Competence() { category = free.category, name = free.name, competenceID = free.competenceID });

                }
                model.skills.Add(new Skill() { name = free.skillname, rating = free.rating, competenceID = free.skillcompid});
            }
            return model;
        }
    }
}