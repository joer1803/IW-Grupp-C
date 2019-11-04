using HampesFrilansare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HampesFrilansare.ViewModels
{

    

    public class FreelancerResumeViewModel
    {
        public Freelancer freelancer { get; set; }
        public Resume resume { get; set; }
        public Education education { get; set; }
        public Driverslicence driverslicence { get; set; }
        public Experience experience { get; set; }
        public Skill skill { get; set; }
        public Competence competence { get; set; }
        public Language language { get; set; }

        public List<SelectListItem> selectcategoriesResume { get; set; }
        public List<SelectListItem> selectskillsResume { get; set; }
    }
}
