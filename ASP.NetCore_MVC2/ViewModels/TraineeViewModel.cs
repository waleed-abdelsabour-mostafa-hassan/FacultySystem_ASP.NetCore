using ASP.NetCore_MVC2.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NetCore_MVC2.ViewModels
{
    public class TraineeViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Name must be less than 30 letter")]
        [MinLength(3, ErrorMessage = "NAme must be greater than 2 letter")]
        [UniqueNameTrain]
        public string Name { get; set; }
        public string Address { get; set; }
        public double Grade { get; set; }
        public string? Photo { get; set; }
        [Display(Name = "Profile Picture")]
        public IFormFile? Image { get; set; }

        [ForeignKey("Department")]
        [Display(Name = "Department Name")]
        public int Dept_Id { get; set; }
    }
}
