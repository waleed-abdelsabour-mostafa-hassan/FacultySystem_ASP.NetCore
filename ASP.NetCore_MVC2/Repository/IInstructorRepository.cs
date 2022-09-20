using ASP.NetCore_MVC2.Models;
using ASP.NetCore_MVC2.ViewModels;

namespace ASP.NetCore_MVC2.Repository
{
    public interface IInstructorRepository
    {
        List<Instructor> GetAll();
        Instructor GetById(int id);
        void Insert(InstructorCreateViewModel InstVM);
        void Edit(int id, InstructorCreateViewModel InstEVM);
        bool checkNull(InstructorCreateViewModel InstEVM);
        void Delete(int id);
    }
}
