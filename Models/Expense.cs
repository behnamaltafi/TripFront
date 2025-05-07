public class Expense:BaseEntity<int>
{

    public string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }


    // Foreign keys
    public int FamilyId { get; set; }
    public int TripId { get; set; }

    // Navigation properties
    public Family Family { get; set; }
    public Trip Trip { get; set; }
    public List<ExpenseParticipant> Participants { get; set; } = new();
}


