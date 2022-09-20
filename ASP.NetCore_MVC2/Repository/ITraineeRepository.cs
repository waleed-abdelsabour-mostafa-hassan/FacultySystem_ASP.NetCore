using ASP.NetCore_MVC2.Models;
using ASP.NetCore_MVC2.ViewModels;

namespace ASP.NetCore_MVC2.Repository
{
    public interface ITraineeRepository
    {
        List<Trainee> GetAll();
        Trainee GetById(int id);
        void Insert(TraineeViewModel TrainVM);
        void Edit(int id, TraineeViewModel TrainEVM);
        bool checkNull(TraineeViewModel TrainEVM);
        void Delete(int id);
    }
}
