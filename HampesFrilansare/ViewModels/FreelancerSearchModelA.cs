﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HampesFrilansare.Models;

namespace HampesFrilansare.ViewModels
{
    public class FreelancerSearchModelA
    {
        public string category { get; set; }
        public int compID { get; set; }
        public int skillID { get; set; }
        public List<FreelancerSearchModel> freelancers { get; set; }
    }
}