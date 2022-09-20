using ASP.NetCore_MVC2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using ASP.NetCore_MVC2.ViewModels;
using Microsoft.Extensions.Hosting;
using ASP.NetCore_MVC2.Repository;
using Microsoft.AspNetCore.Authorization;

namespace ASP.NetCore_MVC2.Controllers
{
    [Authorize]
    public class InstructorController : Controller
    {
        IInstructorRepository IInstructorRepository;
        ICourseRepository ICourseRepository;
        IDepartmentRepository IDepartmentRepository;

        //DI
        public InstructorController(IInstructorRepository _IInstrepo,ICourseRepository _Crsrepo, IDepartmentRepository _Deptrepo)
        {
            IInstructorRepository = _IInstrepo;
            ICourseRepository = _Crsrepo;//new CourseRepository();
            IDepartmentRepository = _Deptrepo;//new DepartmentRepository();
            
        }
        public IActionResult New()
        {
            ViewData["CrsList"] = ICourseRepository.GetAll(); 
            ViewData["DeptList"] = IDepartmentRepository.GetAll(); 
            return View(new InstructorCreateViewModel());//view="New",Model=null
        }

        [HttpPost]//<form method="post">
        public IActionResult SaveNew(InstructorCreateViewModel InstVM)
        {

            if (ModelState.IsValid == true)
            {
                if (InstVM.Dept_Id != 0)
                {
                    if (InstVM.Crs_Id != 0)
                    {
                        IInstructorRepository.Insert(InstVM);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("Crs_Id", "Select Course");
                    }
                }
                else
                {
                    ModelState.AddModelError("Dept_Id", "Select Department");
                }

            }
            ViewData["CrsList"] = ICourseRepository.GetAll(); 
            ViewData["DeptList"] = IDepartmentRepository.GetAll(); 
            return View("New", InstVM);
        }
        public IActionResult Edit(int id)
        {
            Instructor instructor = IInstructorRepository.GetById(id); //Model
            InstructorCreateViewModel InstVM = new InstructorCreateViewModel();
            InstVM.Id = id;
            InstVM.Name = instructor.Name;
            InstVM.Address = instructor.Address;
            InstVM.Salary = instructor.Salary;
            InstVM.Photo = instructor.Image;
            // image
            InstVM.Dept_Id = instructor.Dept_Id;
            InstVM.Crs_Id = instructor.Crs_Id;
            ViewData["DeptList"] = IDepartmentRepository.GetAll(); 
            ViewData["CrsList"] = ICourseRepository.GetAll(); 
            return View(InstVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, InstructorCreateViewModel instEVM)
        {
            if (ModelState.IsValid == true)
            {
                if (instEVM.Dept_Id != 0)
                {
                    if (instEVM.Crs_Id != 0)
                    {
                        IInstructorRepository.Edit(id, instEVM);
                        return RedirectToAction("Index");
                        //save
                    }
                    else
                    {
                        ModelState.AddModelError("Crs_Id", "Select Course");
                    }
                }
                else
                {
                    ModelState.AddModelError("Dept_Id", "Select Department");
                }

            }

            //model 
            ViewData["DeptList"] = IDepartmentRepository.GetAll(); 
            ViewData["CrsList"] = ICourseRepository.GetAll(); 
            return View("Edit", instEVM); 
        }


        // Get: /Instructor/Delete/5
        public IActionResult Delete(int id)
        {
            Instructor instructor = IInstructorRepository.GetById(id); 
            return View(instructor);
        }
        public IActionResult ConfirmDelete(int id)
        {
            try 
            {
                IInstructorRepository.Delete(id);
                return RedirectToAction("Index");
            }
            catch(Exception )
            {
                return Content("Can't Delete This Record Because Related Other Table");
            }
        }
       
        public IActionResult Index()
        {
            /*List<Instructor> instructors = IInstructorRepository.GetAll();
            ViewData["CurrentFilter"] = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                var res= instructors.Where(s => s.Name.Contains(searchString)
                                       || s.Address.Contains(searchString));
            }*/
            return View(IInstructorRepository.GetAll()); 
        }

        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Instructor instructor = IInstructorRepository.GetById(id); 
            if (instructor == null)
            {
                return NotFound();
            }
            return View("Details",instructor);

            
        }
        public IActionResult GetCourse(int DeptId)
        {
            return Json(ICourseRepository.GetCourses(DeptId));
        }
    }
}
