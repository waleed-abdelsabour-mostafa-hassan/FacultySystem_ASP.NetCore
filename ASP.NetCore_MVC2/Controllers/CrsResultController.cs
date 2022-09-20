using ASP.NetCore_MVC2.Models;
using ASP.NetCore_MVC2.Repository;
using ASP.NetCore_MVC2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.NetCore_MVC2.Controllers
{
    [Authorize]
    public class CrsResultController : Controller
    {
       // ITIEntity Context = new ITIEntity();

        ICrsResultRepository ICrsResultRepository;
        ICourseRepository ICourseRepository;
        ITraineeRepository ITraineeRepository;
        //DI
        public CrsResultController(ICrsResultRepository _CrsResrepo, ICourseRepository _Crsrepo, ITraineeRepository _Trainrepo)
        {
            ICrsResultRepository = _CrsResrepo;
            ICourseRepository = _Crsrepo;//new CourseRepository();
            ITraineeRepository = _Trainrepo;//new DepartmentRepository();
        }
        public IActionResult Index()
        {
            return View(ICrsResultRepository.GetAll());//Context.CrsResults.Include(c => c.Course).Include(t=>t.Trainee).ToList());

        }
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CrsResult crsResult = ICrsResultRepository.GetById(id); //Context.CrsResults.FirstOrDefault(x => x.Id == id);
            ViewData["CrsList"] = ICrsResultRepository.GetAll(); //Context.Courses.ToList();
            ViewData["TrainList"] = ITraineeRepository.GetAll(); //Context.Trainees.ToList();
            if (crsResult == null)
            {
                return NotFound();
            }
            return View("Details", crsResult);
        }
        public IActionResult New()
        {
            ViewData["CrsList"] = ICourseRepository.GetAll(); //Context.Courses.ToList();
            ViewData["TrainList"] = ITraineeRepository.GetAll(); //Context.Trainees.ToList();
            return View(new CrsResult());//view="New",Model=null
        }

        [HttpPost]//<form method="post">
        [ValidateAntiForgeryToken]
        public IActionResult SaveNew(CrsResult CrsRes)
        {

            //if (CrsRes.Degree != null)
            if (ModelState.IsValid == true)
            {
                if (CrsRes.Dept_Id != 0 )
                {
                    if(CrsRes.Crs_Id != 0)
                    {
                        /*Context.CrsResults.Add(CrsRes);
                        Context.SaveChanges();*/
                        ICrsResultRepository.Insert(CrsRes);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("Crs_Id", "Select Course");
                    }
                    
                }
                else
                {
                    ModelState.AddModelError("Dept_Id", "Select Trainee");
                    
                }

             }
            ViewData["CrsList"] = ICourseRepository.GetAll(); //Context.Courses.ToList();
            ViewData["TrainList"] = ITraineeRepository.GetAll(); //Context.Trainees.ToList();
            return View("New", CrsRes);
        }
        public IActionResult Edit(int id)
        {
            CrsResult crsResult = ICrsResultRepository.GetById(id); //Context.CrsResults.FirstOrDefault(s => s.Id == id);//Model
            ViewData["CrsList"] = ICourseRepository.GetAll(); //Context.Courses.ToList();
            ViewData["TrainList"] = ITraineeRepository.GetAll(); //Context.Trainees.ToList();
            return View(crsResult);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, CrsResult newCrsRes)
        {
            if (ModelState.IsValid == true)
            {
                if (newCrsRes.Dept_Id != 0)
                {
                    if (newCrsRes.Crs_Id != 0)
                    {
                        ICrsResultRepository.Edit(id,newCrsRes);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("Crs_Id", "Select Course");
                    }

                }
                else
                {
                    ModelState.AddModelError("Dept_Id", "Select Trainee");

                }

            }

            //model 
            ViewData["CrsList"] = ICourseRepository.GetAll(); //Context.Courses.ToList();
            ViewData["TrainList"] = ITraineeRepository.GetAll(); //Context.Trainees.ToList();
            return View("Edit", newCrsRes);
        }
        // Get: /Course/Delete/5
        public IActionResult Delete(int id)
        {
            try
            {
                ICrsResultRepository.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return Content("Can't Delete This Record Because Related Other Table");
            }

        }
        // CrsResult/GetCrsResultUsingViewModel/Trainee_id //one trainee
        /*public IActionResult GetCrsResultUsingViewModel(int id)
        {
            Trainee TranModel = Context.Trainees.FirstOrDefault(t => t.Id == id);

            CrsResult CrsResModel = Context.CrsResults.FirstOrDefault(t => t.Dept_Id == TranModel.Id);
            Course CrsModel = Context.Courses.FirstOrDefault(t => t.Id == CrsResModel.Crs_Id);

            ResultTraineeCourse TraCrsViewModel = new ResultTraineeCourse();
            TraCrsViewModel.ResId = CrsResModel.Id;
            TraCrsViewModel.ResDeg = CrsResModel.Degree;
            TraCrsViewModel.CrsName = CrsModel.Name;
            TraCrsViewModel.TrainName = TranModel.Name;
            if (TraCrsViewModel.ResDeg >= CrsModel.MinDegree)
            {
                TraCrsViewModel.Color = "green";
            }
            else
            {
                TraCrsViewModel.Color = "red";
            }

            return View(TraCrsViewModel);
        }*/

        public ActionResult ListCompleteDetails(int id)
        {
            List<Trainee> TrainList = ITraineeRepository.GetAll();//Context.Trainees.ToList();
            List<Course>CrsList=ICourseRepository.GetAll();
            List<CrsResult>CrsResList=ICrsResultRepository.GetAll();
            List<ResultTraineeCourse> TraCrsListViewModel = new List<ResultTraineeCourse>();
            var listWithEmpty = (from t in TrainList
                                 join cr in CrsResList
                                 on t.Id equals cr.Dept_Id
                                 join c in CrsList
                                 on cr.Crs_Id equals c.Id
                                 where t.Id == id
                                 select new
                                 {
                                     ResId = cr.Id,
                                     ResDeg = cr.Degree,
                                     CrsName = c.Name,
                                     TrainName = t.Name,
                                     CrsMinDeg = c.MinDegree,
                                 }).ToList();
            foreach (var item in listWithEmpty)
            {
                ResultTraineeCourse TraCrsViewModel=new ResultTraineeCourse();
                TraCrsViewModel.ResId=item.ResId;
                TraCrsViewModel.ResDeg=item.ResDeg;
                TraCrsViewModel.CrsName=item.CrsName;
                TraCrsViewModel.TrainName=item.TrainName;
                if(TraCrsViewModel.ResDeg >= item.CrsMinDeg)
                {
                    TraCrsViewModel.Color = "green";
                }
                else
                {
                    TraCrsViewModel.Color = "red";
                }
                TraCrsListViewModel.Add(TraCrsViewModel);
            }
                       


            return View(TraCrsListViewModel);
        }
       
        public IActionResult GetCourses(int TranId)
        {
            return Json(ICourseRepository.GetCourse(TranId));//Context.CrsResults.Include(c => c.Course).Include(t => t.Trainee).Where(c => c.Dept_Id == TranId).Select(s => new Course {Id= s.Course.Id,Name= s.Course.Name }).ToList());
        }

    }
}
