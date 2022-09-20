using ASP.NetCore_MVC2.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASP.NetCore_MVC2.Models
{
    public class ITIEntity:IdentityDbContext<ApplicationUser>
    {
        public ITIEntity():base()
        {

        }

        public ITIEntity(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<CrsResult> CrsResults { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-S2RBUO6;Initial Catalog=Intake42Q3Assiut;Integrated Security=True");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
