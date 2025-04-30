public class TripDetailsDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public List<TripFamilyDetailsDto> TripFamilyDetails { get; set; } = new();
    public List<ExpenseDetailsDto> ExpensesDetails { get; set; } = new();

    // Calculated properties
    public int TotalParticipants => TripFamilyDetails?.Sum(f => f.MemberCount) ?? 0;
    public decimal TotalExpenses => ExpensesDetails?.Sum(e => e.Amount) ?? 0;
}
