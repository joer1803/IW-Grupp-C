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
            languages = new List<Language>();
        }
        public List<Experience> experiences { get; set; }
        public List<Education> educations { get; set; }
        public List<Competence> competences { get; set; }
        public List<Skill> skills { get; set; }
        public Freelancer freelancer { get; set; }
        public Resume resume { get; set; }
        public List<Language> languages { get; set; }
        public Driverslicence licence { get; set; }
    }
}