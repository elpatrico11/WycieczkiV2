using global::AutoMapper;
using WycieczkiV2.Models;
using WycieczkiV2.ViewModel;

namespace WycieczkiV2.AutoMapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
             CreateMap<TripViewModel, Trip>()
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name.Length > 0 ? char.ToUpper(src.Name[0]) + src.Name.Substring(1).ToLower() : src.Name))
                .ForMember(x => x.Origin, opt => opt.MapFrom(src => src.Origin.Length > 0 ? char.ToUpper(src.Origin[0]) + src.Origin.Substring(1).ToLower() : src.Origin))
                .ForMember(x => x.Destination, opt => opt.MapFrom(src => src.Destination.Length > 0 ? char.ToUpper(src.Destination[0]) + src.Destination.Substring(1).ToLower() : src.Destination))
                .ReverseMap();

            CreateMap<StudentViewModel, Student>()
                .ForMember(x => x.FirstName, opt => opt.MapFrom(src => src.FirstName.Length > 0 ? char.ToUpper(src.FirstName[0]) + src.FirstName.Substring(1).ToLower() : src.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(src => src.LastName.Length > 0 ? char.ToUpper(src.LastName[0]) + src.LastName.Substring(1).ToLower() : src.LastName))
                .ReverseMap();

            CreateMap<ReservationViewModel, Reservation>()
                .ForMember(x => x.DateOfReservation, opt => opt.MapFrom(src => src.DateOfReservation.ToUniversalTime()))
                .ForMember(x => x.StartDate, opt => opt.MapFrom(src => src.StartDate.ToUniversalTime()))
                .ForMember(x => x.EndDate, opt => opt.MapFrom(src => src.EndDate.ToUniversalTime()))
                .ReverseMap();

        }
    }
}
