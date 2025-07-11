// مدل‌های پایه
using AutoMapper;
using TripFront.Models;

public partial class TripProfile
{
    public class FamilyProfile : Profile
    {
        public FamilyProfile()
        {
            CreateMap<Family, FamilyDTO>();

            CreateMap<Family, FamilyInfo>()
              .ForMember(dest => dest.Trips, opt => opt.MapFrom(src => src.TripFamilies.Count()))
              .ForMember(dest => dest.Interests, opt => opt.MapFrom(src => src.FamilyInterests.Select(x=>x.Interest.Title).ToList()))
              .ForMember(dest => dest.Budget, opt => opt.MapFrom(src =>
                  src.TripFamilies
                      .SelectMany(tf => tf.Trip.Expenses)
                      .Where(e => e.FamilyId == src.Id)
                      .Sum(e => e.Amount)))
              .ForMember(dest => dest.Friends, opt => opt.MapFrom(src => src.ReceivedFriendshipRequests.Count(f => f.Status == FriendshipStatus.Accepted) + src.SentFriendshipRequests.Count(f => f.Status == FriendshipStatus.Accepted)));

            CreateMap<FamilyDTO, TripFamilyDto>();


            CreateMap<TripFamilyDto, FamilyDTO>()
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FamilyName))
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.FamilyId)).ReverseMap();
            CreateMap<TripFamily, FamilyDTO>()
 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Family.Name))
 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.FamilyId))
 .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.Family.AccountNumber))
 
 .ReverseMap();

            CreateMap<TripFamilyDto,Family>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FamilyName))
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.FamilyId)).ReverseMap();
            CreateMap<AddFamilyDTO, Family>();
            CreateMap<UpdateFamilyDTO, Family>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
          


        }
    }
}