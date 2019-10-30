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
        public List<FreelancerSearchModel> SearchFreelancer()
        {
            var sfree = (from free in db.Freelancer
                         join resume in db.Resume on
                         free.resumeID equals resume.resumeID
                         join comp in db.Competence on resume.resumeID
                         equals comp.resumeID
                         join skill in db.Skill on comp.competenceID
                         equals skill.competenceID
                         select new { free.firstname, free.lastname, comp.name, skillname = skill.name });
            List<FreelancerSearchModel> freeVM = new List<FreelancerSearchModel>();
            foreach (var f in sfree)
            {
                FreelancerSearchModel model = new FreelancerSearchModel();
                model.firstname = f.firstname;
                model.lastname = f.lastname;
                model.compname.Add(f.name);
                model.skillname.Add(f.skillname);
                freeVM.Add(model);
            }

            return freeVM;
        }
    }
}