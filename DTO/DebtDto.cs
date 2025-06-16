// مدل‌های پایه
using AutoMapper;

public class DebtDto
{
    public int FromFamilyId { get; set; }
    public string FromFamilyName { get; set; }
    public int ToFamilyId { get; set; }
    public string ToFamilyName { get; set; }
    public decimal Amount { get; set; }
    public string AccountNumber{ get; set; }
    public int ParticipantCount { get; set; }
}



// AutoMapper Profile
public class SettlementProfile : Profile
{
    public SettlementProfile()
    {
        CreateMap<Trip, SettlementReportDto>()
            .ForMember(dest => dest.TotalExpenses,opt => opt.MapFrom(src => src.Expenses.Sum(e => e.Amount)))
            .ForMember(dest => dest.TotalExpenses,opt => opt.MapFrom(src => src.Expenses.Sum(e => e.Amount)))
            .ForMember(dest => dest.TotalParticipants,
                opt => opt.MapFrom(src => src.Families.Sum(tf => tf.MemberCount)))
            .ForMember(dest => dest.SharePerParticipant,
                opt => opt.MapFrom(src => src.Expenses.Sum(e => e.Amount) /
                                       src.Families.Sum(tf => tf.MemberCount)))
                      .ForMember(dest => dest.TripName, opt => opt.MapFrom(src => src.Name))
                      .ForMember(dest => dest.TripId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Families, opt => opt.Ignore()) // Will be calculated
            .ForMember(dest => dest.OptimizedDebts, opt => opt.Ignore()).ReverseMap(); // Will be calculated

        CreateMap<TripFamily, FamilyBalanceDto>()
            .ForMember(dest => dest.FamilyId, opt => opt.MapFrom(src => src.FamilyId))
            .ForMember(dest => dest.FamilyName, opt => opt.MapFrom(src => src.Family.Name))
            .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.Family.AccountNumber))
            .ForMember(dest => dest.ParticipantCount, opt => opt.MapFrom(src => src.MemberCount))
            .ForMember(dest => dest.TotalPaid, opt => opt.Ignore())
            .ForMember(dest => dest.TotalShare, opt => opt.Ignore())
            .ForMember(dest => dest.Balance, opt => opt.Ignore());

        CreateMap<DebtDto, DebtDto>(); // For mapping optimization results
    }
}