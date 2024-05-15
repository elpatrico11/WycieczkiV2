using FluentValidation;
using WycieczkiV2.ViewModel;

namespace WycieczkiV2.Validation
{
    public class StudentValidator : AbstractValidator<StudentViewModel>
    {
        public StudentValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.DateOfBirth).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email is not valid");
            RuleFor(x => x.Citizenship).MaximumLength(50); 
        }
    }
}
