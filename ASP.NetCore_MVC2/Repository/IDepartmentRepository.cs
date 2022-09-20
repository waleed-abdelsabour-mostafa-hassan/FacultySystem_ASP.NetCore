using ASP.NetCore_MVC2.Models;
namespace ASP.NetCore_MVC2.Repository
{
    public interface IDepartmentRepository
    {
        List<Department> GetAll();
        Department GetById(int id);
        void Insert(Department item);
        void Edit(int id, Department item);
        void Delete(int id);
    }
}
