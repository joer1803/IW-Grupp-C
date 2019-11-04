using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HampesFrilansare.Models;

namespace HampesFrilansare.ViewModels
{
    public class FreelancerSearchModelA
    {
        public List<string> searchcategories { get; set; }
        public List<FreelancerSearchModel> freelancers { get; set; }
        public List<SelectListItem> selectcategories { get; set; }
        public List<SelectListItem> selectskills { get; set; }
    }
}