using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NetCore_MVC2.Models
{
    public class CrsResult
    {
        public int Id { get; set; }

        [Required]
        public double Degree { get; set; }

        [ForeignKey("Trainee")]
        [Display(Name = "Trainee Name")]
        public int Dept_Id { get; set; }

        [ForeignKey("Course")]
        [Display(Name = "Course Name")]
        public int Crs_Id { get; set; }

        public virtual Trainee? Trainee { get; set; }
        public virtual Course? Course { get; set; }
    }
}
