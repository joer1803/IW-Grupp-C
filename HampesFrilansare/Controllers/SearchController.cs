using HampesFrilansare.Models;
using HampesFrilansare.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HampesFrilansare.Controllers
{
    public class SearchController : Controller
    {
        hampesfrilansdbEntities db = new hampesfrilansdbEntities();

        // GET: Search
        public ViewResult SearchFreelancer(string search)
        {
            var sfreelist = new List<FreelancerSearchModel>();
            var freelancers = db.Freelancer.ToList();
            var skills = db.Skill.ToList();
            var resumes = db.Resume.ToList();
            foreach(var f in freelancers)
            {
                foreach(var r in resumes)
                {
                    foreach(var s in skills)
                    {
                        var sfree = new FreelancerSearchModel()
                        {
                            firstname = f.firstname,
                            lastname = f.lastname,
                            freelancerID = f.freelancerID,
                            resumeIDs
                            resumeID.Add(resumes.Where(x => x.freelancerID.Equals(f.freelancerID)))

                        };
                    }
                }
                
            }
        }
    }
}