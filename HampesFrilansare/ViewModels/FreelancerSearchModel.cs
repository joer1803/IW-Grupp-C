using HampesFrilansare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HampesFrilansare.ViewModels
{
    public class FreelancerSearchModel
    {
        public int freelancerID { get; set; }
        public List<int> resumeID { get; set; }

        public string firstname { get; set; }
        public string lastname { get; set; }
        List<Skill> skills { get; set; }




    }
}