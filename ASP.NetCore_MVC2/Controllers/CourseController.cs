using ASP.NetCore_MVC2.Models;
using ASP.NetCore_MVC2.Repository;
using ASP.NetCore_MVC2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.NetCore_MVC2.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
       // ITIEntity Context = new ITIEntity();
        ICourseRepository ICourseRepository;
        IDepartmentRepository IDepartmentRepository;
        //DI
        public CourseController(ICourseRepository _Crsrepo,IDepartmentRepository _Deptrepo)
        {
            ICourseRepository = _Crsrepo;//new CourseRepository();
            IDepartmentRepository = _Deptrepo;//new DepartmentRepository();
        }

        public IActionResult CheckDegree(int Degree,double MinDegree)
        {
            if(Degree > MinDegree)
                return Json(true);
            return Json(false);
        }
        public IActionResult Index()
        {
            return View(ICourseRepository.GetAll());//Context.Courses.Include(d => d.Department).ToList());

        }
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Course course = ICourseRepository.GetById(id); //Context.Courses.FirstOrDefault(x => x.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            return View("Details", course);
        }
        public IActionResult New()
        {
            ViewData["DeptList"] = IDepartmentRepository.GetAll();//Context.Departments.ToList();
            return View(new Course());//view="New",Model=null
        }

        [HttpPost]//<form method="post">
        [ValidateAntiForgeryToken]
        public IActionResult SaveNew(Course Crs)
        {

           // if (Crs.Name != null)
           if(ModelState.IsValid==true)
            {
                if(Crs.Dept_Id !=0)
                {
                    /*Context.Courses.Add(Crs);
                    Context.SaveChanges();*/
                    ICourseRepository.Insert(Crs);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Dept_Id", "Select Department");
                }
                

            }
            ViewData["DeptList"] = IDepartmentRepository.GetAll(); //Context.Departments.ToList();
            return View("New",Crs);
        }
        public IActionResult Edit(int id)
        {
            Course course = ICourseRepository.GetById(id);
            ViewData["DeptList"] = IDepartmentRepository.GetAll();
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Course newCrs)
        {
            /*Course oldCrs = ICourseRepository.GetById(id);
                //get old object
                oldCrs.Name = newCrs.Name;
                oldCrs.Degree = newCrs.Degree;
                oldCrs.MinDegree = newCrs.MinDegree;
                oldCrs.Dept_Id = newCrs.Dept_Id;*/
            if (ModelState.IsValid == true)
            {
                if (newCrs.Dept_Id != 0)
                {
                    ICourseRepository.Edit(id, newCrs);
                    return RedirectToAction("Index");
                    //save
                }
                else
                {
                    ModelState.AddModelError("Dept_Id", "Select Department");
                }
            }

            //model 
            ViewData["DeptList"] = IDepartmentRepository.GetAll(); //Context.Departments.ToList();
            return View("Edit", newCrs);
        }
        // Get: /Course/Delete/5
        public IActionResult Delete(int id)
        {
            Course course = ICourseRepository.GetById(id); //Context.Courses.FirstOrDefault(d => d.Id == id);
            return View(course);
        }
        public IActionResult ConfirmDelete(int id)
        {
            /*Course course = Context.Courses.FirstOrDefault(d => d.Id == id);
            Context.Courses.Remove(course);
            Context.SaveChanges();*/
            try
            {
                ICourseRepository.Delete(id);
                return RedirectToAction("Index");
            }
            catch(Exception )
            {
                return Content("Can't Delete This Record Because Related Other Table");
            }
            

            
        }


    }
}
