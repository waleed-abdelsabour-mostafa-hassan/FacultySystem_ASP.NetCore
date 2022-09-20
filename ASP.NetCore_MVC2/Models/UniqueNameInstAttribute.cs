using ASP.NetCore_MVC2.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace ASP.NetCore_MVC2.Models
{
    public class UniqueNameInstAttribute: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return null;
            string newName = value.ToString();
            ITIEntity Context = new ITIEntity();
            Instructor Inst = Context.Instructors.FirstOrDefault(c => c.Name == newName);
            InstructorCreateViewModel InstRe = (InstructorCreateViewModel)validationContext.ObjectInstance;
            if (Inst != null && InstRe.Id!=Inst.Id)
            {
                return new ValidationResult("Name Must Be Unique");
            }
            return ValidationResult.Success;
        }
    }
}
