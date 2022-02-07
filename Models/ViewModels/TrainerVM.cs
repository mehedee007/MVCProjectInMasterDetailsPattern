using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCProjectInMasterDetailsPattern.Models.ViewModels
{
    public class TrainerVM
    {
        public int TrainerID { get; set; }
        public string TrainerName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string TrainerContact { get; set; }
        public string TrainerEmail { get; set; }

        public bool IsActive { get; set; }
        public string TrainerImage { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageUrl { get; set; }

        public int CourseID { get; set; }
        public virtual Course Course { get; set; }

        public virtual IList<TrainerExperience> TrainerExperiences { get; set; } = new List<TrainerExperience>();
    }
}