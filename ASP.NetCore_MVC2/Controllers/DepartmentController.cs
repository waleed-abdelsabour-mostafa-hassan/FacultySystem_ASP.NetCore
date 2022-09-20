using ASP.NetCore_MVC2.Models;
using ASP.NetCore_MVC2.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.NetCore_MVC2.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        //DIP
        //IStudentRepository studentRepository;//Tigh Couple ==>lossly couple
        IDepartmentRepository departmentRepository;
        //DI
        public DepartmentController(/*IStudentRepository _stdRepo,*/ IDepartmentRepository _Deptrepo)
        {
            // studentRepository = _stdRepo;//new StudentMockREspotory();
            departmentRepository = _Deptrepo;//new DepartmentRepository();
        }
        public IActionResult Index()
        {
            return View(departmentRepository.GetAll());//Context.Departments.ToList());

        }
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Department department = departmentRepository.GetById(id); //Context.Departments.FirstOrDefault(x => x.Id == id);
            if (department == null)
            {
                return NotFound();
            }
            return View("Details", department);
        }
        public IActionResult New()
        {
            return View(new Department());//view="New",Model=null
        }

        [HttpPost]//<form method="post">
        [ValidateAntiForgeryToken]
        public IActionResult SaveNew(Department Dept)
        {

            //if (Dept.Name != null)
            if (ModelState.IsValid == true)
            {
                /*Context.Departments.Add(Dept);
                Context.SaveChanges();*/
                departmentRepository.Insert(Dept);
                return RedirectToAction("Index");

            }
            return View("New", Dept);
        }
        public IActionResult Edit(int id)
        {
            Department department = departmentRepository.GetById(id); //Context.Departments.FirstOrDefault(s => s.Id == id);//Model
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Department newDept)
        {
            /*Department oldDept = Context.Departments.FirstOrDefault(s => s.Id == id);
            
                //get old object
                oldDept.Name = newDept.Name;
                oldDept.ManagerName = newDept.ManagerName;
                Context.SaveChanges();*/
                if (ModelState.IsValid == true)
                {

                    departmentRepository.Edit(id, newDept);
                    return RedirectToAction("Index");
               }
            //save

            //model 
            return View("Edit", newDept);
        }
        // Get: /Course/Delete/5
        public IActionResult Delete(int id)
        {
            /*Department department = Context.Departments.FirstOrDefault(d => d.Id == id);
            Context.Departments.Remove(department);
            Context.SaveChanges();*/
            try
            {
                departmentRepository.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return Content("Can't Delete This Record Because Related Other Table");
            }
        }
    }
}
