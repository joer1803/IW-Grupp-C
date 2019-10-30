using HampesFrilansare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HampesFrilansare.ViewModels
{
    public class FreelancerSearchModel
    {
        public FreelancerSearchModel()
        {
            skillname = new List<string>();
            compname = new List<string>();
        }
        public int freelancerID { get; set; }
        public int skillID { get; set; }

        public string firstname { get; set; }
        public string lastname { get; set; }
        public string fullname => $"{firstname} {lastname}";

        public Nullable<int> rating { get; set; }
        public List<string> skillname { get; set; }
        public List<string> compname { get; set; }



    }
}