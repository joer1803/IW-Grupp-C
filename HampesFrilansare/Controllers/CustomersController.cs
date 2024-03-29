﻿using System;
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
    public class CustomersController : Controller
    {
        private hampesfrilansdbEntities db = new hampesfrilansdbEntities();
        FreelancerSearchModelA freelancerSearchCat;

        // GET: Customers
        public ActionResult Index()
        {          
            return View("Index", "_NavbarCustomer", GetSearchFreeCategories());
        }

        public ActionResult PrintCV()
        {
            
            return View();
        }

        private FreelancerSearchModelA GetSearchFreeCategories()
        {

            freelancerSearchCat = new FreelancerSearchModelA();
            freelancerSearchCat.freelancers = GetFreelancerVM();
            return freelancerSearchCat;
        }
        private List<FreelancerSearchModel> GetFreelancerVM()
        {
            var sfree = (from free in db.Freelancer
                         join resume in db.Resume on
                         free.resumeID equals resume.resumeID
                         join comp in db.Competence on resume.resumeID
                         equals comp.resumeID
                         join skill in db.Skill on comp.competenceID
                         equals skill.competenceID
                         select new { free.freelancerID, free.firstname, free.lastname, comp.name, skillname = skill.name, comp.category, skill.rating });



            List<FreelancerSearchModel> freeVM = new List<FreelancerSearchModel>();
            foreach (var f in sfree)
            {
                bool duplicate = false;
                for (int i = 0; i < freeVM.Count; i++)
                {


                    if (freeVM[i].freelancerID.Equals(f.freelancerID))
                    {
                        
                        if (!freeVM[i].compname.Contains(f.name))
                        {
                            freeVM[i].compname += $", {f.name}";
                        }
                        freeVM[i].skillname += $", {f.skillname}" + "(" + f.rating + "/5)"; ;


                        duplicate = true;
                    }
                }
                if (!duplicate)
                {
                    FreelancerSearchModel model = new FreelancerSearchModel();
                    model.freelancerID = f.freelancerID;
                    model.firstname = f.firstname;
                    model.lastname = f.lastname;
                    model.compname = f.name;
                    model.skillname = f.skillname + "(" + f.rating + "/5)";
                    model.compcategory = f.category;

                    freeVM.Add(model);
                }

            }

            return freeVM;
        }
        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "customerID,businessname,firstname,lastname,phonenumber,email,address")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customer.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }
        public ActionResult SignupCustomer()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignupCustomer([Bind(Include = "customerID, businessname,email")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customer.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(customer);
            }
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "customerID,businessname,firstname,lastname,phonenumber,email,address")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customer.Find(id);
            db.Customer.Remove(customer);
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

        public ActionResult ProfileCustomer()
        {
            return View();
        }
        public ActionResult Resume()
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
