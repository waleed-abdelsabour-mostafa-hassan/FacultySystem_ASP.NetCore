using ASP.NetCore_MVC2.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NetCore_MVC2.ViewModels
{
    public class InstructorCreateViewModel
    {
        public int Id { get; set; }

        [Required]
        [UniqueNameInst]
        [MaxLength(30, ErrorMessage = "Name must be less than 30 letter")]
        [MinLength(3, ErrorMessage = "NAme must be greater than 2 letter")]
        public string Name { get; set; }
        public string Address { get; set; }
        public int Salary { get; set; }
        public string? Photo { get; set; }

        [Display(Name = "Profile Picture")]
        public IFormFile? Image { get; set; }

        [ForeignKey("Department")]
        [Display(Name = "Department Name")]
        public int Dept_Id { get; set; }

        [ForeignKey("Course")]
        [Display(Name = "Course Name")]
        public int Crs_Id { get; set; }

    }
}
