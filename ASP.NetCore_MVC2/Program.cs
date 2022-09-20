using ASP.NetCore_MVC2.Models;
using ASP.NetCore_MVC2.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ASP.NetCore_MVC2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            
            string ConnString = builder.Configuration.GetConnectionString("CS");
            builder.Services.AddDbContext<ITIEntity>(optionBuilder =>
            {
                optionBuilder.UseSqlServer(ConnString);
            });
            // register usermanager,rolemanager that use userstore,rolestore
            builder.Services.AddIdentity<ApplicationUser,IdentityRole>(
                options=>options.Password.RequireDigit=true
                ).
                AddEntityFrameworkStores<ITIEntity>();

            //Custom Service --REgister
            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<ITraineeRepository, TraineeRepository>();
            builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
            builder.Services.AddScoped<ICrsResultRepository, CrsResultRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}