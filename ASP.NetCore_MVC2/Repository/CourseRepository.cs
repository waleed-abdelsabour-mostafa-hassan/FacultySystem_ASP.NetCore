using ASP.NetCore_MVC2.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NetCore_MVC2.Repository
{
    public class CourseRepository:ICourseRepository
    {
        ITIEntity Context;
        public CourseRepository(ITIEntity _context)
        {
            Context = _context;
        }
        public List<Course> GetAll()
        {
            return Context.Courses.Include(d => d.Department).ToList();
        }

        public Course GetById(int id)
        {
            return Context.Courses.FirstOrDefault(x => x.Id == id);
        }

        public void Insert(Course item)
        {
            Context.Courses.Add(item);
            Context.SaveChanges();
        }
        public void Edit(int id, Course item)
        {
            Course oldCourse = GetById(id);
            oldCourse.Name = item.Name;
            oldCourse.Degree = item.Degree;
            oldCourse.MinDegree = item.MinDegree;
            oldCourse.Dept_Id = item.Dept_Id;
            Context.SaveChanges();
        }
        public void Delete(int id)
        {
            Course oldCourse = GetById(id);
            Context.Courses.Remove(oldCourse);
            Context.SaveChanges();
        }
        //instructor table
        public List<Course> GetCourses(int id)
        {
            return Context.Courses.Where(c => c.Dept_Id == id).ToList();
        }

        // crsresult table
        public List<Course> GetCourse(int id)
        {
            return Context.CrsResults.Include(c => c.Course).Include(t => t.Trainee).Where(c => c.Dept_Id == id).Select(s => new Course { Id = s.Course.Id, Name = s.Course.Name }).ToList();
        }

    }
}
