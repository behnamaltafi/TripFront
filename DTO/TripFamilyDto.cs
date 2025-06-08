
// مدل‌های پایه
public class TripFamilyDto
{
    public int FamilyId { get; set; }
    public int Id { get; set; }
    public int TripId { get; set; }
    public string TripName{ get; set; }

    public string FamilyName { get; set; }
    public int MemberCount { get; set; } // Total members in family
    public int ActualParticipantCount { get; set; } // Actual participants in this trip
    public string AccountNumber { get; set; }
    public bool IsActive { get; set; } = true;
}

