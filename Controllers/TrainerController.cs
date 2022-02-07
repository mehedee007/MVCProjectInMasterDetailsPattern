using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data.Entity;
using MVCProjectInMasterDetailsPattern.Models.ViewModels;

using MVCProjectInMasterDetailsPattern.Models;
using System.IO;

namespace MVCProjectInMasterDetailsPattern.Controllers
{
    public class TrainerController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Trainer
        public ActionResult Index()
        {
            var trainer = db.Trainers.Include(t => t.Course);
            return View(trainer.ToList());
        }

        public ActionResult Create()
        {
            TrainerVM trainer = new TrainerVM();
            trainer.TrainerExperiences.Add(new TrainerExperience() { ID = 1 });
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName");
            return PartialView(trainer);
        }
        [HttpPost]
        public ActionResult Create(TrainerVM tVm)
        {
            Trainer trainer = new Trainer();
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(tVm.ImageUrl.FileName);
                string extention = Path.GetExtension(tVm.ImageUrl.FileName);
                fileName = fileName + extention;
                tVm.TrainerImage = "~/Images/Trainers/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Images/Trainers/"), fileName);
                tVm.ImageUrl.SaveAs(fileName);

                trainer.TrainerName = tVm.TrainerName;
                trainer.DateOfBirth = tVm.DateOfBirth;
                trainer.TrainerContact = tVm.TrainerContact;
                trainer.TrainerEmail = tVm.TrainerEmail;
                trainer.IsActive = tVm.IsActive;
                trainer.CourseID = tVm.CourseID;
                trainer.TrainerImage = tVm.TrainerImage;
                db.Trainers.Add(trainer);
                db.SaveChanges();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName");
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            Trainer tr = db.Trainers.SingleOrDefault(t => t.TrainerID == id);
            TrainerVM tVm = new TrainerVM();
            tVm.TrainerName = tr.TrainerName;
            tVm.DateOfBirth = tr.DateOfBirth;
            tVm.TrainerContact = tr.TrainerContact;
            tVm.TrainerEmail = tr.TrainerEmail;
            tVm.IsActive = tr.IsActive;
            tVm.CourseID = tr.CourseID;
            //tVm.TrainerImage = Session["TrainerImage"].ToString();
            //Session["TrainerImage"] = tr.TrainerImage;
            tVm.TrainerImage = tr.TrainerImage;


            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", tr.CourseID);
            return PartialView(tVm);
        }
        [HttpPost]
        public ActionResult Edit(TrainerVM tvm, int id)
        {
            Trainer tr = db.Trainers.Find(id);
            if (ModelState.IsValid)
            {
                //if(tvm.ImageUrl == null)
                //{
                //    tr.TrainerImage = tvm.TrainerImage;
                //}
                //else
                //{
                    string fileName = Path.GetFileNameWithoutExtension(tvm.ImageUrl.FileName);
                    string extention = Path.GetExtension(tvm.ImageUrl.FileName);
                    fileName = fileName + extention;
                    tvm.TrainerImage = "~/Images/Trainers/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Images/Trainers/"), fileName);
                    tvm.ImageUrl.SaveAs(fileName);
                    //tr.TrainerImage = tvm.TrainerImage;
                //}

               tr.TrainerName = tvm.TrainerName;
               tr.DateOfBirth = tvm.DateOfBirth;
               tr.TrainerContact = tvm.TrainerContact;
               tr.TrainerEmail = tvm.TrainerEmail;
               tr.IsActive = tvm.IsActive;
               tr.CourseID = tvm.CourseID;
               tr.TrainerImage = tvm.TrainerImage;

                db.Entry(tr).State = EntityState.Modified;
                db.SaveChanges();
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", tr.CourseID);
            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id)
        {
            Trainer tr = db.Trainers.SingleOrDefault(t => t.TrainerID == id);
            TrainerVM tVm = new TrainerVM();
            tVm.TrainerName = tr.TrainerName;
            tVm.DateOfBirth = tr.DateOfBirth;
            tVm.TrainerContact = tr.TrainerContact;
            tVm.TrainerEmail = tr.TrainerEmail;
            tVm.IsActive = tr.IsActive;
            tVm.CourseID = tr.CourseID;
            tVm.TrainerImage = tr.TrainerImage;
            return PartialView(tVm);
        }

        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            Trainer tr = db.Trainers.Find(id);
            if (tr != null)
            {
                db.Trainers.Remove(tr);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            Trainer tr = db.Trainers.SingleOrDefault(t => t.TrainerID == id);
            TrainerVM tVm = new TrainerVM();
            tVm.TrainerName = tr.TrainerName;
            tVm.DateOfBirth = tr.DateOfBirth;
            tVm.TrainerContact = tr.TrainerContact;
            tVm.TrainerEmail = tr.TrainerEmail;
            tVm.IsActive = tr.IsActive;
            tVm.CourseID = tr.CourseID;
            tVm.TrainerImage = tr.TrainerImage;
            return PartialView(tVm);
        }
    }
}