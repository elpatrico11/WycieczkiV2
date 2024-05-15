using FluentValidation;
using WycieczkiV2.ViewModel;

namespace WycieczkiV2.Validation
{
    public class ReservationValidator : AbstractValidator<ReservationViewModel>
    {
        public ReservationValidator()
        {
            RuleFor(x => x.StudentId).NotEmpty();
            RuleFor(x => x.TripId).NotEmpty();
            RuleFor(x => x.DateOfReservation).NotEmpty();
            RuleFor(x => x.StartDate).NotEmpty();
            RuleFor(x => x.EndDate).NotEmpty().GreaterThanOrEqualTo(x => x.StartDate).WithMessage("End date must be greater than or equal to start date");
            RuleFor(x => x.TotalPrice).NotEmpty().GreaterThanOrEqualTo(0).WithMessage("Total price must be non-negative");
            RuleFor(x => x.NumberOfPeople).NotEmpty().GreaterThan(0).WithMessage("Number of people must be greater than zero");
            
        }

      
    }
}
