using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HampesFrilansare.Models;

namespace HampesFrilansare.ViewModels
{
    public class FreelancerSearchModelA
    {
        private List<string> Categories()
        {
            List<string> cats = new List<string>();
            var categories = (from comps in db.Competence
                              select new { category = comps.category });
            return cats = categories.Distinct().Select(x => x.category.ToString()).ToList();
        }
    }
}