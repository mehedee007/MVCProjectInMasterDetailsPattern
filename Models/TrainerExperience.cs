using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProjectInMasterDetailsPattern.Models
{
    public class TrainerExperience
    {
        public int ID { get; set; }
        public string Designation { get; set; }
        public int TrainerID { get; set; }
        public virtual Trainer Trainer { get; set; }
        public string Institution { get; set; }
        public int ExperienceInYears { get; set; }
    }
}