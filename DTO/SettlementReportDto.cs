// مدل‌های پایه
public class SettlementReportDto
{
    public int TripId { get; set; }
    public string TripName { get; set; }
    public decimal TotalExpenses { get; set; }
    public int TotalParticipants { get; set; }
    public decimal SharePerParticipant { get; set; }
    public List<FamilyBalanceDto> Families { get; set; } = new();
    public List<DebtRecordDto> OptimizedDebts { get; set; } = new();
}