using HampesFrilansare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HampesFrilansare.ViewModels
{
    public class FreelancerProfileViewModel
    {
        public FreelancerProfileViewModel()
        {
            experiences = new List<Experience>();
            educations = new List<Education>();
            competences = new List<Competence>();
            skills = new List<Skill>();
        }
        public List<Experience> experiences { get; set; }
        public List<Education> educations { get; set; }
        public List<Competence> competences { get; set; }
        public List<Skill> skills { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string address { get; set; }
        public string phonenumber { get; set; }
        public string email { get; set; }
        public string nationality { get; set; }
        public string birtdate { get; set; }
        public string languages { get; set; }
    }
}