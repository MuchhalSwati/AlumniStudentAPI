using FluentValidation;
using FluentValidation.Results;

namespace StudentsAdminPortal.API.FluentValidation
{
    public class CustomValidator<T>:AbstractValidator<T>            
    {
        public CustomValidator() 
        {
            ValidatorOptions.Global.CascadeMode = CascadeMode.Stop;
        }

        public override ValidationResult Validate(ValidationContext<T> context)
        {
            var result = base.Validate(context);
            if (!result.IsValid) 
            {
                throw new ValidationException(result.Errors);
            }
            return result;
        }
    }
}
