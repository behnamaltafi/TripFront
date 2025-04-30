public class ExpenseParticipantDto
{
    public int FamilyId { get; set; }
    public string FamilyName { get; set; }
    public int ParticipantCount { get; set; } = 1;
    public decimal ShareAmount { get; set; } // Will be calculated
}
