// مدل‌های پایه
using AutoMapper;

public partial class TripProfile
{
    public class FamilyProfile : Profile
    {
        public FamilyProfile()
        {
            CreateMap<Family, FamilyDTO>();
            CreateMap<FamilyFriendship ,FamilyDTO>() 
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Family2.Name))
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Family2.Id));
            CreateMap<FamilyDTO, TripFamilyDto>();


            CreateMap<TripFamilyDto, FamilyDTO>()
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FamilyName))
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.FamilyId)).ReverseMap();


            CreateMap<TripFamilyDto,Family>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FamilyName))
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.FamilyId)).ReverseMap();
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