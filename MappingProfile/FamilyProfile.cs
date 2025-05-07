// مدل‌های پایه
using AutoMapper;

public partial class TripProfile
{
    public class FamilyProfile : Profile
    {
        public FamilyProfile()
        {
            CreateMap<Family, FamilyDTO>();
            CreateMap<AddFamilyDTO, FamilyDTO>();
            CreateMap<AddFamilyDTO, Family>();
            CreateMap<UpdateFamilyDTO, Family>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<FriendRequest, FriendRequest>();
            CreateMap<FriendRequest, FriendRequestDTO>()
                            .ForMember(dest => dest.ReceiverFamilyTitle, opt => opt.MapFrom(src => src.ReceiverFamily.Name))
                            .ForMember(dest => dest.SenderFamilyTitle, opt => opt.MapFrom(src => src.SenderFamily.Name));


        }
    }
}