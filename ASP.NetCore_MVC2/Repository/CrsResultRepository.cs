using ASP.NetCore_MVC2.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NetCore_MVC2.Repository
{
    public class CrsResultRepository:ICrsResultRepository
    {
        ITIEntity Context;
        public CrsResultRepository(ITIEntity _context)
        {
            Context = _context;
        }
        public List<CrsResult> GetAll()
        {
            return Context.CrsResults.Include(c => c.Course).Include(t => t.Trainee).ToList();
        }

        public CrsResult GetById(int id)
        {
            return Context.CrsResults.FirstOrDefault(x => x.Id == id);
        }

        public void Insert(CrsResult item)
        {
            Context.CrsResults.Add(item);
            Context.SaveChanges();
        }
        public void Edit(int id, CrsResult item)
        {
            CrsResult oldCrsResult = GetById(id);
            oldCrsResult.Degree = item.Degree;
            oldCrsResult.Dept_Id = item.Dept_Id; // trainee id
            oldCrsResult.Crs_Id = item.Crs_Id;   // course id
            Context.SaveChanges();
        }
        public void Delete(int id)
        {
            CrsResult oldCrsResult = GetById(id);
            Context.CrsResults.Remove(oldCrsResult);
            Context.SaveChanges();
        }
        
    }
}
