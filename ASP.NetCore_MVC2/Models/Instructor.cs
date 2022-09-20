using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NetCore_MVC2.Models
{
    public class Instructor
    {
        public int Id { get; set; }

        [Required]
        [UniqueNameInst]
        [MaxLength(30, ErrorMessage = "Name must be less than 30 letter")]
        [MinLength(3, ErrorMessage = "NAme must be greater than 2 letter")]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public int Salary { get; set; }

        [Required]
        [RegularExpression(@"\w+\.(jpg|JPG|png)", ErrorMessage = "Image must be jpg or png")]
        public string Image { get; set; }

        [ForeignKey("Department")]
        [Display(Name = "Department Name")]
        public int Dept_Id { get; set; }

        [ForeignKey("Course")]
        [Display(Name = "Course Name")]
        public int Crs_Id { get; set; }

        public virtual Department? Department { get; set; }
        public virtual Course? Course { get; set; }
    }
}
