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

        public ActionResult SearchFreelancer()
        {

                return View(GetFreelancerVM());

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
                         select new { free.freelancerID, free.firstname, free.lastname, comp.name, skillname = skill.name, comp.category });
            
            

            List<FreelancerSearchModel> freeVM = new List<FreelancerSearchModel>();
            foreach (var f in sfree)
            {
                bool duplicate = false;
                for(int i=0; i<freeVM.Count; i++)
                {
                    if (freeVM[i].freelancerID.Equals(f.freelancerID))
                    {
                        if(freeVM[i].compname != f.name)
                        {
                            freeVM[i].compname += $", {f.name}";
                        }
                        freeVM[i].skillname += $", {f.skillname}";


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
                    model.skillname = f.skillname;
                    model.compcategory = f.category;

                    freeVM.Add(model);
                }
                
            }

            return freeVM;
        }
    }
}