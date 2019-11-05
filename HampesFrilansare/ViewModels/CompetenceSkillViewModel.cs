using HampesFrilansare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HampesFrilansare.ViewModels
{
    public class CompetenceSkillViewModel
    {
        public Competence competence { get; set; }
        public Skill skill { get; set; }
        public int compID { get; set; }
        public string compCategory { get; set; }
    }
}