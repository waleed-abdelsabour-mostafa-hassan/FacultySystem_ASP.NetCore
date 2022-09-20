using ASP.NetCore_MVC2.Models;
using ASP.NetCore_MVC2.Repository;
using ASP.NetCore_MVC2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.NetCore_MVC2.Controllers
{
    [Authorize]
    public class TraineeController : Controller
    {
        // ITIEntity Context = new ITIEntity();
        ITraineeRepository ITraineeRepository;
        IDepartmentRepository IDepartmentRepository;
        //DI
        public TraineeController(ITraineeRepository _Tranrepo, IDepartmentRepository _Deptrepo)
        {
            ITraineeRepository = _Tranrepo;//new TraineeRepository();
            IDepartmentRepository = _Deptrepo;//new DepartmentRepository();
        }
        public IActionResult Index()
        {
            return View(ITraineeRepository.GetAll());//Context.Trainees.Include(d => d.Department).ToList());

        }
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Trainee trainee = ITraineeRepository.GetById(id);//Context.Trainees.FirstOrDefault(x => x.Id == id);
            if (trainee == null)
            {
                return NotFound();
            }
            return View("Details", trainee);
        }
        public IActionResult New()
        {
            ViewData["DeptList"] = IDepartmentRepository.GetAll();//Context.Departments.ToList();
            return View(new TraineeViewModel());//view="New",Model=null
        }

        [HttpPost]//<form method="post">
        [ValidateAntiForgeryToken]
        public IActionResult SaveNew(TraineeViewModel TrainVM)
        {

            //if (Train.Name != null)
            if (ModelState.IsValid == true)
            {
                if (TrainVM.Dept_Id != 0)
                {
                    /*Context.Trainees.Add(Train);
                    Context.SaveChanges();*/
                    ITraineeRepository.Insert(TrainVM);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Dept_Id", "Select Department");
                }

            }
            ViewData["DeptList"] = IDepartmentRepository.GetAll();//Context.Departments.ToList();
            return View("New", TrainVM);
        }
        public IActionResult Edit(int id)
        {
            Trainee trainee = ITraineeRepository.GetById(id); //Context.Trainees.FirstOrDefault(s => s.Id == id);//Model
            TraineeViewModel TrainEVM = new TraineeViewModel();
            TrainEVM.Id = id;
            TrainEVM.Name = trainee.Name;
            TrainEVM.Address = trainee.Address;
            TrainEVM.Grade = trainee.Grade;
            TrainEVM.Photo = trainee.Image;
            // image
            TrainEVM.Dept_Id = trainee.Dept_Id;
            ViewData["DeptList"] = IDepartmentRepository.GetAll(); //Context.Departments.ToList();
            return View(TrainEVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, TraineeViewModel newTrainVM)
        {
            if (ModelState.IsValid == true)
            {
                if (newTrainVM.Dept_Id != 0)
                {
                    ITraineeRepository.Edit(id, newTrainVM);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Dept_Id", "Select Department");
                }
            }


            //model 
            ViewData["DeptList"] = IDepartmentRepository.GetAll(); //Context.Departments.ToList();
            return View("Edit", newTrainVM);
        }
        // Get: /Course/Delete/5
        public IActionResult Delete(int id)
        {
            Trainee trainee = ITraineeRepository.GetById(id); //Context.Trainees.FirstOrDefault(d => d.Id == id);
            return View(trainee);
        }
        public IActionResult ConfirmDelete(int id)
        {
            try
            {
                /*Trainee trainee = Context.Trainees.FirstOrDefault(d => d.Id == id);
                Context.Trainees.Remove(trainee);
                Context.SaveChanges();*/
                ITraineeRepository.Delete(id);

                return RedirectToAction("Index");
            }
            catch(Exception )
            {
                return Content("Can't Delete This Record Because Related Other Table");
            }
        }

    }
}
