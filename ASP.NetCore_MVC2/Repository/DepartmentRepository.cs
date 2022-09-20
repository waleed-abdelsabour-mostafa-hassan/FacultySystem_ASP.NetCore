using ASP.NetCore_MVC2.Models;
namespace ASP.NetCore_MVC2.Repository
{
    
    public class DepartmentRepository: IDepartmentRepository
    {
        ITIEntity Context;
        public DepartmentRepository(ITIEntity _context)
        {
            Context = _context;
        }
        public List<Department> GetAll()
        {
            return Context.Departments.ToList();
        }
        
        public Department GetById(int id)
        {
            return Context.Departments.FirstOrDefault(x => x.Id == id);
        }
        public void Insert(Department item)
        {
            Context.Departments.Add(item);
            Context.SaveChanges();
        }
        public void Edit(int id, Department item)
        {
            Department oldDepartment = GetById(id);
            oldDepartment.Name = item.Name;
            oldDepartment.ManagerName = item.ManagerName; 
            Context.SaveChanges();
        }
        public void Delete(int id)
        {
            Department oldDepartment = GetById(id);
            Context.Departments.Remove(oldDepartment);
            Context.SaveChanges();
        }
    }
}
