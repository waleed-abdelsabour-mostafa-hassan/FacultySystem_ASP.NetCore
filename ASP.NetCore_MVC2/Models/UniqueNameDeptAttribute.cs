using System.ComponentModel.DataAnnotations;

namespace ASP.NetCore_MVC2.Models
{
    public class UniqueNameDeptAttribute: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return null;
            string newName = value.ToString();
            ITIEntity Context = new ITIEntity();
            Department Dept = Context.Departments.FirstOrDefault(c => c.Name == newName);
            Department DeptRe = (Department)validationContext.ObjectInstance;
            if (Dept != null && DeptRe.Id != Dept.Id)
            {
                return new ValidationResult("Name Must Be Unique");
            }
            return ValidationResult.Success;
        }
    }
}
