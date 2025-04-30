
// Interfaces


public interface ISettlementService
{
    Task<SettlementReportDto> GetTripSettlementAsync(int tripId);
    Task<GlobalSettlementDto> GetGlobalSettlementAsync();
    //Task<byte[]> GenerateTripExpenseChart(int tripId);
}
