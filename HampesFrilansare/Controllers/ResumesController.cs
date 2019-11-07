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
            int freeID = db.Freelancer.First(x => x.resumeID == resume.resumeID).freelancerID;
            if (ModelState.IsValid)
            {
                db.Entry(resume).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("FreelancerProfile", "Freelancers",new { id = freeID });
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
        public ActionResult Education(int id)
        {
            Education ed = new Education()
            {
                resumeID = id
            };
            return View(ed);
        }
        //public ActionResult EditEducation([Bind(Include = "resumeID,profile,coreability,licenceID")] Education education)
        //{
        //    int freeID = db.Freelancer.First(x => x.resumeID == resume.resumeID).freelancerID;
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(resume).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("FreelancerProfile", "Freelancers", new { id = freeID });
        //    }
        //    ViewBag.licenceID = new SelectList(db.Driverslicence, "licenceID", "type", resume.licenceID);
        //    return View(resume);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEducation([Bind(Include = "resumeID ,school, startdate, enddate, subject, degree")] Education education)
        {
            int freeID = db.Freelancer.First(x => x.resumeID == education.resumeID).freelancerID;
            if (ModelState.IsValid)
            {
                db.Education.Add(education);
                db.SaveChanges();
                return RedirectToAction("FreelancerProfile", "Freelancers", new { id = freeID });
            }

            return View(education);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateExperience([Bind(Include = "resumeID ,name,role,startdate,enddate,duty")] Experience experience)
        {
            int freeID = db.Freelancer.First(x => x.resumeID == experience.resumeID).freelancerID;
            if (ModelState.IsValid)
            {
                db.Experience.Add(experience);
                db.SaveChanges();
                return RedirectToAction("FreelancerProfile", "Freelancers", new { id = freeID });
            }

            return View(experience);
        }
        public ActionResult Experience(int id)
        {
            Experience xp = new Experience()
            {
                resumeID = id
            };
            return View(xp);
        }
        public ActionResult Driverslicence(int id)
        {
            Session["id"] = id;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDriverslicence([Bind(Include = "licenceID, type")] Driverslicence licence)
        {
            int id = (int)Session["id"];
            int freeID = db.Freelancer.First(x => x.resumeID == id).freelancerID;
            Resume r = db.Resume.First(x => x.resumeID == id);
            if (ModelState.IsValid)
            {
                if(r.Driverslicence == null)
                {
                    r.Driverslicence = db.Driverslicence.Add(licence);
                    db.Entry(r).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    r.Driverslicence.type = licence.type;
                    db.Entry(r.Driverslicence).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("FreelancerProfile", "Freelancers", new { id = freeID });

            }

            return View(licence);
        }
        public ActionResult AddCompetence([Bind(Include = "competence, skill")] CompetenceSkillViewModel comp)
        {
            int freeID = db.Freelancer.First(x => x.resumeID == comp.competence.resumeID).freelancerID;
            if (ModelState.IsValid)
            {
                db.Competence.Add(comp.competence);
                db.SaveChanges();
                comp.skill.competenceID = comp.competence.competenceID;
                db.Skill.Add(comp.skill);
                db.SaveChanges();
                return RedirectToAction("FreelancerProfile", "Freelancers", new { id = freeID });
            }

            return View(comp);
        }
        public ActionResult AddLanguage([Bind(Include = "name, resumeID")] Language lang)
        {
            int freeID = db.Freelancer.First(x => x.resumeID == lang.resumeID).freelancerID;
            if (ModelState.IsValid)
            {
                db.Language.Add(lang);
                db.SaveChanges();
                return RedirectToAction("FreelancerProfile", "Freelancers", new { id = freeID });
            }

            return View(lang);
        }
        public ActionResult Language(int id)
        {
            Language l = new Language()
            {
                resumeID = id
            };
            return View(l);
        }

        public ActionResult CompetenceSkill(int id)
        {
            CompetenceSkillViewModel cvm = new CompetenceSkillViewModel();
            cvm.competence.resumeID = id;
            List<string> cats = db.Competence.Select(o => o.category).ToList();
            int count = 0;
            for (int i = 0; i < cats.Count; i++)
            {
                for (int j = 0; j < cats.Count; j++)
                {
                    if (cats[i] == cats[j])
                    {
                        count++;
                        if (count == 2)
                        {
                            cats.RemoveAt(i);
                            i = 0;
                            j = 0;
                        }
                    }
                    else if(cats[i] == null)
                    {
                        cats.RemoveAt(i);
                        i = 0;
                        j = 0;
                    }
                }
                count = 0;
            }
            List<string> ratings = new List<string>(){"1","2","3","4","5"};
            ViewBag.catlist = cats;
            ViewBag.ratinglist = ratings;
            return View(cvm);
        }
        public JsonResult GetComps(string category)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<string> comps = db.Competence.Where(x => x.category == category).Select(o => o.name).ToList();
            int count = 0;
            for (int i = 0; i < comps.Count; i++)
            {
                for (int j = 0; j < comps.Count; j++)
                {
                    if (comps[i] == comps[j])
                    {
                        count++;
                        if (count == 2)
                        {
                            comps.RemoveAt(i);
                            i = 0;
                            j = 0;
                        }
                    }
                }
                count = 0;
            }
            return Json(comps, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSkills(string compname)
        {
            db.Configuration.ProxyCreationEnabled = false;
            int id = db.Competence.First(o => o.name == compname).competenceID;
            List<string> skills = db.Skill.Where(x => x.competenceID == id).Select(y => y.name).ToList();

            return Json(skills, JsonRequestBehavior.AllowGet);
        }
    }
}
