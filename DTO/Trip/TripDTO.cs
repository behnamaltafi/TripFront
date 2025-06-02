
// مدل‌های پایه
using System.ComponentModel.DataAnnotations;

public class TripDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "نام سفر الزامی است")]
    [StringLength(100)]
    public string Name { get; set; }
    public string ImageUrl { get; set; } // Avatar

    [StringLength(500)]
    public string Description { get; set; }

    [Required(ErrorMessage = "تاریخ شروع الزامی است")]
    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }
    public int OwnerFamily { get; set; }



    // Summary information
    public int TotalFamilies { get; set; }// => Families?.Count ?? 0;
    public int TotalParticipants { get; set; } //=> Families?.Sum(f => f.ActualParticipantCount) ?? 0;

    // For creation
    public List<int> FamilyIds { get; set; } = new List<int>();
}
