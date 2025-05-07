using AutoMapper;



public class SettlementService : ISettlementService
{
    private readonly ITripRepository _tripRepository;
    private readonly IDebtRecordRepository _debtRecordRepository;

    private readonly IMapper _mapper;

    public SettlementService(ITripRepository tripRepository, IMapper mapper, IDebtRecordRepository debtRecordRepository)
    {
        _tripRepository = tripRepository;
        _mapper = mapper;
        _debtRecordRepository = debtRecordRepository;

    }

    public async Task<SettlementReportDto> GetTripSettlementAsync(int tripId)
    {
        var trip = await _tripRepository.GetByIdAsync(tripId, true);
        var report = _mapper.Map<SettlementReportDto>(trip);
        report.Families = CalculateFamilyBalances(trip, report.SharePerParticipant);
        report.OptimizedDebts = await _debtRecordRepository.RegenerateDebtRecords(tripId, OptimizeDebts(report.Families));
        return report;
    }
    public async Task<SettlementReportDto> SaveTripSettlementAsync(int tripId)
    {
        var trip = await _tripRepository.GetByIdAsync(tripId, true);
        var report = _mapper.Map<SettlementReportDto>(trip);
        report.Families = CalculateFamilyBalances(trip, report.SharePerParticipant);
        report.OptimizedDebts = OptimizeDebts(report.Families);

        return report;
    }

    public async Task<GlobalSettlementDto> GetGlobalSettlementAsync()
    {

        var trips = await _tripRepository.GetTripsForFamilyAsync();
        var globalBalances = new Dictionary<int, FamilyBalanceDto>();

        foreach (var trip in trips)
        {

            var sharePerParticipant = trip.Expenses.Sum(e => e.Amount) /
                                    trip.Families.Sum(tf => tf.MemberCount);

            var tripBalances = CalculateFamilyBalances(trip, sharePerParticipant);

            foreach (var balance in tripBalances)
            {
                if (!globalBalances.ContainsKey(balance.FamilyId))
                {
                    globalBalances[balance.FamilyId] = _mapper.Map<FamilyBalanceDto>(balance);
                    continue;
                }

                globalBalances[balance.FamilyId].TotalPaid += balance.TotalPaid;
                globalBalances[balance.FamilyId].TotalShare += balance.TotalShare;
                globalBalances[balance.FamilyId].Balance += balance.Balance;
            }
        }

        return new GlobalSettlementDto
        {
            FamilyBalances = globalBalances.Values.ToList(),
            OptimizedDebts = OptimizeDebts(globalBalances.Values.ToList())
        };
    }

    private List<FamilyBalanceDto> CalculateFamilyBalances(Trip trip, decimal sharePerParticipant)
    {
        var familyBalances = _mapper.Map<List<FamilyBalanceDto>>(trip.Families);

        foreach (var familyBalance in familyBalances)
        {
            familyBalance.TotalPaid = trip.Expenses
                .Where(e => e.FamilyId == familyBalance.FamilyId)
                .Sum(e => e.Amount);

            familyBalance.TotalShare = sharePerParticipant * familyBalance.ParticipantCount;
            familyBalance.AccountNumber = familyBalance.AccountNumber;

            familyBalance.Balance = familyBalance.TotalPaid - familyBalance.TotalShare;
        }

        return familyBalances;
    }

    private List<DebtRecordDto> OptimizeDebts(List<FamilyBalanceDto> balances)
    {
        var creditors = balances.Where(b => b.Balance > 0)
            .OrderByDescending(b => b.Balance)
            .ToList();

        var debtors = balances.Where(b => b.Balance < 0)
            .OrderBy(b => b.Balance)
            .ToList();

        var debts = new List<DebtRecordDto>();

        foreach (var debtor in debtors)
        {
            var remainingDebt = -debtor.Balance;

            foreach (var creditor in creditors.Where(c => c.Balance > 0))
            {
                if (remainingDebt <= 0) break;

                var amountToTransfer = Math.Min(creditor.Balance, remainingDebt);

                if (amountToTransfer > 0)
                {
                    debts.Add(new DebtRecordDto
                    {
                        FromFamilyId = debtor.FamilyId,
                        ParticipantCount = debtor.ParticipantCount,
                        FromFamilyName = debtor.FamilyName,
                        ToFamilyId = creditor.FamilyId,
                        ToFamilyName = creditor.FamilyName,
                        Amount = amountToTransfer,
                        AccountNumber = creditor.AccountNumber


                    });

                    remainingDebt -= amountToTransfer;
                    creditor.Balance -= amountToTransfer;
                }
            }
        }

        return debts;
    }
}