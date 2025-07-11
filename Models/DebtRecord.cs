using TripFront.Models;

public class DebtRecord: BaseEntity<int>
{

    public Family FromFamily { get; set; }
    public Family ToFamily { get; set; }
    public Trip Trip { get; set; }
    public int FromFamilyId { get; set; }
    public int ToFamilyId { get; set; }
    public int TripId { get; set; }
    public decimal Amount { get; set; }
    public string AccountNumber { get; set; }
    public bool IsPaid { get; set; }
    public int ParticipantCount { get; set; }
    public string? File { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? PaidAt { get; set; }
}


