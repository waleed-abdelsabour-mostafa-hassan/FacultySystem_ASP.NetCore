using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NetCore_MVC2.Models
{
    public class Trainee
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Name must be less than 30 letter")]
        [MinLength(3, ErrorMessage = "NAme must be greater than 2 letter")]
        [UniqueNameTrain]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public double Grade { get; set; }

        [Required]
        [RegularExpression(@"\w+\.(jpg|png)", ErrorMessage = "Image must be jpg or png")]
        public string Image { get; set; }

        [ForeignKey("Department")]
        [Display(Name = "Department Name")]
        public int Dept_Id { get; set; }


        public virtual Department? Department { get; set; }

        public virtual List<CrsResult>? CrsResults { get; set; }
    }
}
