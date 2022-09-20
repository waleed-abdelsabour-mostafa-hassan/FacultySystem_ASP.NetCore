using ASP.NetCore_MVC2.Models;
using ASP.NetCore_MVC2.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ASP.NetCore_MVC2.Repository
{
    public class InstructorRepository:IInstructorRepository
    {
        ITIEntity Context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public InstructorRepository(ITIEntity _context, IWebHostEnvironment webHostEnvironment)
        {
            Context = _context;
           this.webHostEnvironment = webHostEnvironment;
        }
        public List<Instructor> GetAll()
        {
            return Context.Instructors.Include(d => d.Department).Include(c => c.Course).ToList();
        }

        public Instructor GetById(int id)
        {
            return Context.Instructors.FirstOrDefault(x => x.Id == id);
        }

        public void Insert(InstructorCreateViewModel InstVM)
        {
            string stringFileName = UploadFile(InstVM);            
            Instructor instructor = new Instructor
            {
                Name = InstVM.Name,
                Address = InstVM.Address,
                Salary = InstVM.Salary,
                Image = stringFileName,
                Dept_Id = InstVM.Dept_Id,
                Crs_Id = InstVM.Crs_Id,
            };
            Context.Instructors.Add(instructor);
            Context.SaveChanges();
        }

        private string UploadFile(InstructorCreateViewModel instVM)
        {
            string fileName = null;
            if(instVM.Image != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images");

                 fileName = Guid.NewGuid().ToString() + "_" + instVM.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    instVM.Image.CopyTo(fileStream);
                    fileStream.Close();
                }
            }
            return fileName;
            
        }

        public void Edit(int id, InstructorCreateViewModel instEVM)
        {
            string stringFileName = UploadFile(instEVM);
            Instructor oldInstructor = GetById(id);
            oldInstructor.Name = instEVM.Name;
            oldInstructor.Address = instEVM.Address;
            oldInstructor.Salary = instEVM.Salary;
            oldInstructor.Image = (stringFileName == null)?oldInstructor.Image:stringFileName;
            oldInstructor.Dept_Id = instEVM.Dept_Id;
            oldInstructor.Crs_Id = instEVM.Crs_Id;
            Context.SaveChanges();
        }
        public void Delete(int id)
        {
            Instructor oldInstructor = GetById(id);
            Context.Instructors.Remove(oldInstructor);
            Context.SaveChanges();
        }

        bool IInstructorRepository.checkNull(InstructorCreateViewModel Instvm)
        {
            if (Instvm.Image == null)
            {
                return true;
            }
            else
            {
                return false;

            }
        }

    }
}
