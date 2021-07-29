using DataLayer;
using EFTutorial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EFTutorial.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            EFTutDBEntities db = new EFTutDBEntities();
            List<StudentModel> studentList = new List<StudentModel>();

            var students = db.tblStudents.ToList();
            foreach (var student in students)
            {
                StudentModel model = new StudentModel();
                model.ID = student.StudentID;
                model.FirstName = student.FirstName;
                model.LastName = student.LastName;
                model.Email = student.Email;
                model.Department = student.Department;

                studentList.Add(model);

            }

            return View(studentList);
        }

        public ActionResult CreateStudent()
        {
            StudentModel model = new StudentModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateStudent(StudentModel model)
        {
            using (EFTutDBEntities db = new EFTutDBEntities())
            {
                tblStudents student = new tblStudents();
                student.FirstName = model.FirstName;
                student.LastName = model.LastName;
                student.Email = model.Email;
                student.Department = model.Department;

                db.tblStudents.Add(student);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

        }

        public ActionResult UpdateStudent(int id)
        {
            StudentModel model = new StudentModel();
            using (EFTutDBEntities db = new EFTutDBEntities())
            {
                var student = db.tblStudents.FirstOrDefault(x => x.StudentID == id);
                if (student != null)
                {
                    model.ID = student.StudentID;
                    model.FirstName = student.FirstName;
                    model.LastName = student.LastName;
                    model.Email = student.Email;
                    model.Department = student.Department;
                }

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateStudent(StudentModel model)
        {
            using (EFTutDBEntities db = new EFTutDBEntities())
            {
                var student = db.tblStudents.FirstOrDefault(x => x.StudentID == model.ID);

                student.FirstName = model.FirstName;
                student.LastName = model.LastName;
                student.Email = model.Email;
                student.Department = model.Department;

                db.Entry(student).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

        }

        public ActionResult Details(int id)
        {
            StudentModel model = new StudentModel();

            using (EFTutDBEntities db = new EFTutDBEntities())
            {
                var student = db.tblStudents.FirstOrDefault(x => x.StudentID == id);
                if (student != null)
                {
                    model.ID = student.StudentID;
                    model.FirstName = student.FirstName;
                    model.LastName = student.LastName;
                    model.Email = student.Email;
                    model.Department = student.Department;
                }
            }
            return View(model);
        }
    }
}