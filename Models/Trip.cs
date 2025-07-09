public class Trip : BaseEntity<int>
{

    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsPaid{ get; set; }
    public Family OwnerFamily { get; set; }
    public int OwnerFamilyId { get; set; }
    public string TripProfileImage { get; set; }

    // Navigation properties
    public List<TripFamily> Families { get; set; } = new();
    public List<Expense> Expenses { get; set; } = new();
}