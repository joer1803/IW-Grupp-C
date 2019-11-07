﻿using HampesFrilansare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HampesFrilansare.ViewModels
{
    public class CompetenceSkillViewModel
    {
        public CompetenceSkillViewModel()
        {
            competence = new Competence();
            skill = new Skill();
        }
        public Competence competence { get; set; }
        public Skill skill { get; set; }
    }
}