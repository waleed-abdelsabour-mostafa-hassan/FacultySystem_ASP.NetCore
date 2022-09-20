using ASP.NetCore_MVC2.Models;
using ASP.NetCore_MVC2.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ASP.NetCore_MVC2.Repository
{
    public class TraineeRepository: ITraineeRepository
    {
        ITIEntity Context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public TraineeRepository(ITIEntity _context, IWebHostEnvironment webHostEnvironment)
        {
            Context = _context;
            this.webHostEnvironment = webHostEnvironment;
        }
        public List<Trainee> GetAll()
        {
            return Context.Trainees.Include(d => d.Department).ToList();
        }

        public Trainee GetById(int id)
        {
            return Context.Trainees.FirstOrDefault(x => x.Id == id);
        }
        public void Insert(TraineeViewModel TrainVM)
        {
            string stringFileName = UploadFile(TrainVM);

            Trainee trainee = new Trainee
            {
                Name = TrainVM.Name,
                Address = TrainVM.Address,
                Grade = TrainVM.Grade,
                Image = stringFileName,
                Dept_Id = TrainVM.Dept_Id,
            };
            Context.Trainees.Add(trainee);
            Context.SaveChanges();
        }
       
        public void Edit(int id, TraineeViewModel TrainEVM)
        {
            string stringFileName = UploadFile(TrainEVM);
            Trainee oldTrainee = GetById(id);
            oldTrainee.Name = TrainEVM.Name;
            oldTrainee.Address = TrainEVM.Address;
            oldTrainee.Grade = TrainEVM.Grade;
            oldTrainee.Image = (stringFileName == null)?oldTrainee.Image:stringFileName;
            oldTrainee.Dept_Id = TrainEVM.Dept_Id;
            Context.SaveChanges();
        }
        private string UploadFile(TraineeViewModel TrainVM)
        {
            string fileName = null;
            if (TrainVM.Image != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images");

                fileName = Guid.NewGuid().ToString() + "_" + TrainVM.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    TrainVM.Image.CopyTo(fileStream);
                    fileStream.Close();
                }
            }
            return fileName;

        }
        public void Delete(int id)
        {
            Trainee oldTrainee = GetById(id);
            Context.Trainees.Remove(oldTrainee);
            Context.SaveChanges();
        }

        bool ITraineeRepository.checkNull(TraineeViewModel traineevm)
        {
            if (traineevm.Image == null)
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
