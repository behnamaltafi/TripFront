using AutoMapper;
//using OxyPlot.Axes;
//using OxyPlot.Series;
//using OxyPlot;
//using OxyPlot.SkiaSharp;

//using OxyPlot.Annotations;
//using OxyPlot.Legends;
//using SixLabors.ImageSharp.Drawing.Processing;
using System.IO;


public class SettlementService : ISettlementService
{
    private readonly ITripRepository _tripRepository;
    private readonly IDebtRecordRepository _debtRecordRepository;
    private readonly IFriendshipService _friendshipService;
    private readonly IMapper _mapper;

    public SettlementService(ITripRepository tripRepository, IMapper mapper, IDebtRecordRepository debtRecordRepository, IFriendshipService friendshipService)
    {
        _tripRepository = tripRepository;
        _mapper = mapper;
        _debtRecordRepository = debtRecordRepository;
        _friendshipService = friendshipService;
    }

    public async Task<SettlementReportDto> GetTripSettlementAsync(int tripId)
    {
        var trip = await _tripRepository.GetByIdAsync(tripId, true);
        if (trip == null) return null;

        // Map basic trip info
        var report = _mapper.Map<SettlementReportDto>(trip);

        // Calculate family balances
        report.Families = CalculateFamilyBalances(trip, report.SharePerParticipant);

        // Optimize debts
        report.OptimizedDebts = await _debtRecordRepository.RegenerateDebtRecords(tripId, OptimizeDebts(report.Families));

        return report;
    }
    public async Task<SettlementReportDto> SaveTripSettlementAsync(int tripId)
    {
        var trip = await _tripRepository.GetByIdAsync(tripId, true);
        if (trip == null) return null;

        // Map basic trip info
        var report = _mapper.Map<SettlementReportDto>(trip);

        // Calculate family balances
        report.Families = CalculateFamilyBalances(trip, report.SharePerParticipant);

        // Optimize debts
        report.OptimizedDebts = OptimizeDebts(report.Families);

        return report;
    }

    //public async Task<byte[]> GenerateTripExpenseChart(int tripId)
    //{
    //    var report = await GetTripSettlementAsync(tripId);

    //    var arabicTitle = "خلاصه هزینه سفر - " + (report.TripName ?? "سفر " + report.TripId);
    //    var arabicDebts = string.Join("\n", report.OptimizedDebts.Select(d =>
    //        $"{d.FromFamilyName} بدهکار {d.ToFamilyName}: {d.Amount:N2} تومان"));

    //    var fullTitle = $"{arabicTitle}\n\n{arabicDebts}";

    //    var model = new PlotModel
    //    {
    //        Title = fullTitle,
    //        DefaultFontSize = 18,
    //        TitleHorizontalAlignment = TitleHorizontalAlignment.CenteredWithinView,
    //        DefaultFont = "B Nazanin", // or "Arial", or any Arabic-supporting font
    //        Background = OxyColors.White,
    //        Padding = new OxyThickness(10, 10, 10, 10),
    //        IsLegendVisible = true // Make sure the legend is visible
    //    };

    //    var categoryAxis = new CategoryAxis
    //    {
    //        Position = AxisPosition.Left,
    //        Key = "FamilyAxis",
    //        ItemsSource = report.Families.Select(f => f.FamilyName).ToList()
    //    };
    //    model.Axes.Add(categoryAxis);

    //    var valueAxis = new LinearAxis
    //    {
    //        Position = AxisPosition.Bottom,
    //        Title = "مبالغ به تومان",
    //        MajorGridlineStyle = LineStyle.Solid,
    //        MinorGridlineStyle = LineStyle.Dot
    //    };
    //    model.Axes.Add(valueAxis);

    //    // Paid series
    //    var paidSeries = new BarSeries
    //    {
    //        Title = "Paid",
    //        FillColor = OxyColors.SteelBlue,
    //        SeriesGroupName = "TripGroup",

    //        LabelFormatString = "{0:N1}" // Show value on top of the bar
    //    };

    //    // Share series
    //    var shareSeries = new BarSeries
    //    {
    //        Title = "Share",
    //        FillColor = OxyColors.LightGoldenrodYellow,
    //        SeriesGroupName = "TripGroup",

    //        LabelFormatString = "{0:N1}" // Show value on top of the bar
    //    };

    //    // Balance series
    //    var balanceSeries = new BarSeries
    //    {
    //        Title = "Balance",
    //        SeriesGroupName = "TripGroup",
    //        LabelFormatString = "{0:N1}" // Show value on top of the bar
    //    };

    //    foreach (var family in report.Families)
    //    {
    //        paidSeries.Items.Add(new BarItem((double)family.TotalPaid));
    //        shareSeries.Items.Add(new BarItem((double)family.TotalShare));
    //        balanceSeries.Items.Add(new BarItem
    //        {
    //            Value = (double)family.Balance,
    //            Color = family.Balance >= 0 ? OxyColors.Green : OxyColors.Red
    //        });
    //    }

    //    model.Series.Add(paidSeries);
    //    model.Series.Add(shareSeries);
    //    model.Series.Add(balanceSeries);

    //    using var stream = new MemoryStream();
    //    var exporter = new PngExporter
    //    {
    //        Width = 1280,
    //        Height = 400 + report.Families.Count * 150, // Dynamic height
    //        Dpi = 150
    //    };
    //    exporter.Export(model, stream);

    //    stream.Seek(0, SeekOrigin.Begin);
    //    var imageBytes = stream.ToArray();

    //    await File.WriteAllBytesAsync("GenerateFamilyBalancesChart.png", imageBytes);

    //    return imageBytes;
    //}



    public async Task<GlobalSettlementDto> GetGlobalSettlementAsync()
    {

        var trips = await _tripRepository.GetTripsForFamilyAsync();
        var globalBalances = new Dictionary<int, FamilyBalanceDto>();

        foreach (var trip in trips)
        {
            // Calculate share per participant for this trip
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
            // Calculate total paid by this family
            familyBalance.TotalPaid = trip.Expenses
                .Where(e => e.FamilyId == familyBalance.FamilyId)
                .Sum(e => e.Amount);

            // Calculate their fair share
            familyBalance.TotalShare = sharePerParticipant * familyBalance.ParticipantCount;
            familyBalance.AccountNumber = familyBalance.AccountNumber;

            // Calculate balance
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