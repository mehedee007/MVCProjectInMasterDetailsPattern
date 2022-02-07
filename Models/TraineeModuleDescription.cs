using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCProjectInMasterDetailsPattern.Models
{
    public class TraineeModuleDescription
    {
        public int ID { get; set; }
        public string ModuleName { get; set; }
        //Foreign Key 
        public int TraineeID { get; set; }
        public virtual Trainee Trainee { get; set; }

        public int ExternalMark { get; set; }
        [DataType(DataType.Date)]
        //[DisplayName("External Date")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ExternalDate { get; set; }
        public int EvidenceMark { get; set; }
        [DataType(DataType.Date)]
        //[DisplayName("Evidence Date")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EvidenceDate { get; set; }

        public bool IsPass { get; set; }

        //public bool PassStatus { get; set; }

        //The Plan is calculating the boolean field value based on the value provided on the External and Evidence
        //Mark fields through an if condtion
        //for instance if(ExternalMark >=30 && EvidenceMark >= 30){PassStatus = true}
        //Else{PassStatus = false}
    }
}