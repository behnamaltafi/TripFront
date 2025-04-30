
// مدل‌های پایه
using System.ComponentModel.DataAnnotations;

public class TripDTO
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(500)]
    public string Description { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }
    public int OwnerFamily { get; set; }

    // Families participating in this trip with their actual participant counts
    public List<TripFamilyDto> Families { get; set; } = new List<TripFamilyDto>();

    // Summary information
    public int TotalFamilies => Families?.Count ?? 0;
    public int TotalParticipants => Families?.Sum(f => f.ActualParticipantCount) ?? 0;

    // For creation
    public List<int> FamilyIds { get; set; } = new List<int>();
}
