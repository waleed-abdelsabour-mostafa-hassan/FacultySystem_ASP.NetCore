using ASP.NetCore_MVC2.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace ASP.NetCore_MVC2.Models
{
    public class UniqueNameTrainAttribute: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return null;
            string newName = value.ToString();
            ITIEntity Context = new ITIEntity();
            Trainee Train = Context.Trainees.FirstOrDefault(c => c.Name == newName);
            TraineeViewModel TrainRe = (TraineeViewModel)validationContext.ObjectInstance;
            if (Train != null && TrainRe.Id != Train.Id)
            {
                return new ValidationResult("Name Must Be Unique");
            }
            return ValidationResult.Success;
        }
    }
}
