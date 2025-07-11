using TripFront.Models;

public class TripFamily : BaseEntity<int>
{
    public int TripId { get; set; }
    public Trip Trip { get; set; }
    public int FamilyId { get; set; }
    public Family Family { get; set; }
    public int MemberCount { get; set; }
    public bool IsActive { get; set; } = true;
}