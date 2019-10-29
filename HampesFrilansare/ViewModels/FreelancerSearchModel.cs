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
        public int skillID { get; set; }

        public string firstname { get; set; }
        public string lastname { get; set; }

        public virtual Competence Competence { get; set; }
        public Nullable<int> rating { get; set; }
        public string name { get; set; }



    }
}