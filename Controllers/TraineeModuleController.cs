using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data.Entity;
using MVCProjectInMasterDetailsPattern.Models;
namespace MVCProjectInMasterDetailsPattern.Controllers
{
    public class TraineeModuleController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: TrianeeModule
        public ActionResult Index()
        {
            var tm = db.TraineeModuleDescriptions.Include(t => t.Trainee);
            return View(tm.ToList());
        }
    }
}