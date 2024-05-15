using FluentValidation;
using WycieczkiV2.ViewModel;

namespace WycieczkiV2.Validation
{
    public class TripValidator: AbstractValidator<TripViewModel>
    {
        public TripValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Price).NotEmpty().GreaterThanOrEqualTo(0).WithMessage("Price must be non-negative");
            RuleFor(x => x.Date).NotEmpty().Must(date => date > DateTime.Now).WithMessage("Date must be in the future");
            RuleFor(x => x.Origin).NotEmpty();
            RuleFor(x => x.Destination).NotEmpty();
            RuleFor(x => x.Country).NotEmpty();
           



        }
    }
}
