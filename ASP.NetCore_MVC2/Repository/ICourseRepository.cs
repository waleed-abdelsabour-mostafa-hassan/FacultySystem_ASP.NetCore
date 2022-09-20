using ASP.NetCore_MVC2.Models;
namespace ASP.NetCore_MVC2.Repository
{
    public interface ICourseRepository
    {
        List<Course> GetAll();
        Course GetById(int id);
        List<Course> GetCourses(int id);
        List<Course> GetCourse(int id);
        void Insert(Course item);
        void Edit(int id, Course item);
        void Delete(int id);
    }
}
