using ASP.NetCore_MVC2.Models;

namespace ASP.NetCore_MVC2.Repository
{
    public interface ICrsResultRepository
    {
        List<CrsResult> GetAll();
        CrsResult GetById(int id);
        void Insert(CrsResult item);
        void Edit(int id, CrsResult item);
        void Delete(int id);

    }
}
