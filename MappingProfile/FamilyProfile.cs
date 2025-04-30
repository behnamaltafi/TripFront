// مدل‌های پایه
using AutoMapper;

public partial class TripProfile
{
    public class FamilyProfile : Profile
    {
        public FamilyProfile()
        {
            CreateMap<Family, FamilyDTO>();
            CreateMap<AddFamilyDTO, Family>();
            CreateMap<UpdateFamilyDTO, Family>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}