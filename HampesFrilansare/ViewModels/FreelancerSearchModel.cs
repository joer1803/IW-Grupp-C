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
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string fullname => $"{firstname} {lastname}";

        public Nullable<int> rating { get; set; }
        public string skillname { get; set; }
        public string compname { get; set; }
        public string compcategory { get; set; }



    }
}