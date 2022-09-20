using System.ComponentModel.DataAnnotations;

namespace ASP.NetCore_MVC2.Models
{
    public class UniqueNameCrsAttribute:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value == null)
                return null;
            //int newId=int.Parse(value.ToString());
            string newName=value.ToString();
            ITIEntity Context=new ITIEntity();
            Course Crs = Context.Courses.FirstOrDefault(c => c.Name == newName);
            Course Crser= (Course)validationContext.ObjectInstance;
             if (Crs != null && Crser.Id!= Crs.Id)
            {
                return new ValidationResult("Name Must Be Unique");
            }
            
            return ValidationResult.Success;
        }
    }
}
