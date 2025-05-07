public class Trip : BaseEntity<int>
{

    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsPaid{ get; set; }
    public int OwnerFamily { get; set; }

    // Navigation properties
    public List<TripFamily> Families { get; set; } = new();
    public List<Expense> Expenses { get; set; } = new();
}