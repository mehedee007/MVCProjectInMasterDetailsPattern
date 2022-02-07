using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

using MVCProjectInMasterDetailsPattern.Models;
using System.IO;


namespace MVCProjectInMasterDetailsPattern.Controllers
{
    
    public class TraineeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Trainee
        public ActionResult Index()
        {
            var trainee = db.Trainees.Include(t => t.Course);
            return View(trainee.ToList());
        }

        public ActionResult Create()
        {
            Trainee trainee = new Trainee();
            trainee.TraineeModuleDescriptions.Add(new TraineeModuleDescription() { ID = 1 });
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName");
            return View(trainee);
        }

        [HttpPost]
        public ActionResult Create(Trainee trainee)
        {
            string fileName = Path.GetFileNameWithoutExtension(trainee.fileBase.FileName);
            string extention = Path.GetExtension(trainee.fileBase.FileName);

            fileName = fileName + extention;
            trainee.TraineeImage = "~/Images/Trainees/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Images/Trainees/"), fileName);
            trainee.fileBase.SaveAs(fileName);
            db.Trainees.Add(trainee);
            db.SaveChanges();
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName");
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            Trainee trainee = db.Trainees.Include(t => t.TraineeModuleDescriptions)
                .Where(tm => tm.TraineeID == id).FirstOrDefault();
            return View(trainee);
        }

        public ActionResult Delete(int id)
        {
            Trainee trainee = db.Trainees.Include(t => t.TraineeModuleDescriptions)
                .Where(tm => tm.TraineeID == id).FirstOrDefault();
            return View(trainee);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            Trainee trainee = db.Trainees.Find(id);
            db.Trainees.Remove(trainee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            Trainee trainee = db.Trainees.Include(t => t.TraineeModuleDescriptions)
               .Where(tm => tm.TraineeID == id).FirstOrDefault();
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", trainee.CourseID);
            Session["TraineeImage"] = trainee.TraineeImage;
            return View(trainee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Trainee trainee)
        {
            if (ModelState.IsValid)
            {
                if(trainee.fileBase != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(trainee.fileBase.FileName);
                    string extention = Path.GetExtension(trainee.fileBase.FileName);

                    fileName = fileName + extention;
                    trainee.TraineeImage = "~/Images/Trainees/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Images/Trainees/"), fileName);
                    trainee.fileBase.SaveAs(fileName);
                }
                Session["TraineeImage"] = trainee.TraineeImage;
                db.Entry(trainee).State = EntityState.Modified;
                foreach (var m in trainee.TraineeModuleDescriptions)
                {
                    db.Entry(m).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", trainee.CourseID);
            return RedirectToAction("Index");
        }


    }
}