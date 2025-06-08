// مدل‌های پایه
using AutoMapper;

public partial class TripProfile : Profile
{
    public TripProfile()
    {
        CreateMap<CreateTripDto, Trip>();
        CreateMap<CreateTripDto, TripDTO>();
        // Trip mappings
        CreateMap<Trip, TripDTO>().ForMember(dest => dest.FamilyIds, opt => opt.MapFrom(src => src.Families.Select(f => f.FamilyId)));
        CreateMap<DebtRecord, DebtRecordDto>()
            .ForMember(dest => dest.FromFamilyName, opt => opt.MapFrom(src => src.FromFamily.Name))
            .ForMember(dest => dest.ToFamilyName, opt => opt.MapFrom(src => src.ToFamily.Name))
            .ForMember(dest => dest.TripName, opt => opt.MapFrom(src => src.Trip.Name))
        ;





        CreateMap<Trip, UpdateTripDto>();
        CreateMap<TripDTO, UpdateTripDto>();
        CreateMap<TripDTO, CreateTripDto>();
        CreateMap<CreateTripDto, UpdateTripDto>();
        CreateMap<TripFamily, TripFamilyDto>()
            .ForMember(dest => dest.FamilyName, opt => opt.MapFrom(src => src.Family.Name))
            .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.Family.AccountNumber))
            .ForMember(dest => dest.TripName, opt => opt.MapFrom(src => src.Trip.Name))
            .ForMember(dest => dest.TripId, opt => opt.MapFrom(src => src.Trip.Id));

        // Expense mappings
        CreateMap<Expense, Expense>();
        CreateMap<Expense, UpdateExpenseDTO>();
        CreateMap<Expense, ExpenseDTO>()
            .ForMember(dest => dest.FamilyName, opt => opt.MapFrom(src => src.Family.Name))
            .ForMember(dest => dest.ParticipatingFamilyIds, opt => opt.MapFrom(src => src.Participants.Select(p => p.FamilyId)));

        CreateMap<ExpenseParticipant, ExpenseParticipantDto>()
            .ForMember(dest => dest.FamilyName, opt => opt.MapFrom(src => src.Family.Name));

        // Settlement mappings
        CreateMap<FamilyBalanceDto, FamilyBalanceDto>(); // For settlement calculations
        CreateMap<DebtDto, DebtDto>(); // For debt optimization



        CreateMap<Trip, TripDetailsDto>();
        CreateMap<Trip, TripDTO>()
                 .ForMember(dest => dest.TotalFamilies, opt => opt.MapFrom(src => src.Families.Count))
                 .ForMember(dest => dest.TotalParticipants, opt => opt.MapFrom(src => src.Families.Sum(f => f.MemberCount)));


        // TripFamily -> TripFamilyDetailsDto
        CreateMap<TripFamily, TripFamilyDetailsDto>()
            .ForMember(dest => dest.FamilyName, opt => opt.MapFrom(src => src.Family.Name))
            .ForMember(dest => dest.MemberCount, opt => opt.MapFrom(src => src.MemberCount))
            .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.Family.AccountNumber));


        // Expense -> ExpenseDetailsDto
        CreateMap<AddExpenseDTO, Expense>().ReverseMap();
        CreateMap<AddExpenseDTO, ExpenseDTO>().ReverseMap();
        CreateMap<UpdateExpenseDTO, Expense>().ReverseMap();
        CreateMap<UpdateExpenseDTO, ExpenseDTO>().ReverseMap();
        CreateMap<Expense, ExpenseDetailsDto>()
            .ForMember(dest => dest.FamilyName, opt => opt.MapFrom(src => src.Family.Name));


        // ExpenseParticipant -> ExpenseParticipantDetailsDto
        CreateMap<ExpenseParticipant, ExpenseParticipantDetailsDto>()
            .ForMember(dest => dest.FamilyName, opt => opt.MapFrom(src => src.Family.Name))
            .ForMember(dest => dest.ShareAmount,
                opt => opt.MapFrom(src => src.Expense.Amount / src.Expense.Participants.Sum(p => p.ParticipantCount) * src.ParticipantCount));
    }
}