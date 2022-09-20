using System.ComponentModel.DataAnnotations;

namespace ASP.NetCore_MVC2.Models
{
    public class Department
    {
        public int Id { get; set; }

        
        [Required]
        [MaxLength(30, ErrorMessage = "Name must be less than 30 letter")]
        [MinLength(2, ErrorMessage = "Name must be greater than 2 letter")]
        [UniqueNameDept]
        public string Name { get; set; }


        [Required]
        [MaxLength(30, ErrorMessage = "Name must be less than 30 letter")]
        [MinLength(3, ErrorMessage = "Name must be greater than 2 letter")]
        public string ManagerName { get; set; }

        public virtual List<Instructor>? Instructors { get; set; }
        public virtual List<Course>? Courses { get; set; }
        public virtual List<Trainee>? Trainees { get; set; }
    }
}
