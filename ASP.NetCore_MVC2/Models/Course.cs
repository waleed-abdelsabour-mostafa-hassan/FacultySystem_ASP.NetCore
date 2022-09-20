using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NetCore_MVC2.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [UniqueNameCrs]
        [MaxLength(30, ErrorMessage = "Name must be less than 30 letter")]
        [MinLength(3, ErrorMessage = "NAme must be greater than 2 letter")]
        public string Name { get; set; }

        [Required]
        [Range(minimum:50,maximum:100,ErrorMessage ="Degree Must Be Between 50 and 100")]
        
        public int Degree { get; set; }

        [Required]
        [Range(minimum:20,maximum:50, ErrorMessage = "MinDegree Must Be Between 20 and 50")]
        [Remote(action: "CheckDegree", controller: "Course"
            , AdditionalFields = "Degree"
            , ErrorMessage = "MinDegree Must Be Less Than Degree")]
        public double MinDegree { get; set; }

        [ForeignKey("Department")]
        [Display(Name = "Department Name")]
        public int Dept_Id { get; set; }

        public virtual Department? Department { get; set; }

        public virtual List<CrsResult>? CrsResults { get; set; }
    }
}
