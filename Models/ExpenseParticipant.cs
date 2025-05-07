public class ExpenseParticipant:BaseEntity<int>
{
    public int ExpenseId { get; set; }
    public Expense Expense { get; set; }
    public int FamilyId { get; set; }
    public Family Family { get; set; }
    public int ParticipantCount { get; set; } = 1;
}