public class ExpenseDetailsDto
{
    public int Id { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public bool IsPaid { get; set; }
    public string PaymentReceiptUrl { get; set; }

    // Family who paid this expense
    public int FamilyId { get; set; }
    public string FamilyName { get; set; }

    // Participants
    public List<ExpenseParticipantDto> Participants { get; set; } = new();
}
