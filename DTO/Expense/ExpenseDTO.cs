using System.ComponentModel.DataAnnotations;
public class ExpenseDTO
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Description { get; set; }

    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Amount { get; set; }

    [Required]
    public DateTime Date { get; set; } = DateTime.Now;

    [Required]
    public int TripId { get; set; }

    [Required]
    public int FamilyId { get; set; }
    public string FamilyName { get; set; } // For display purposes

    public bool IsPaid { get; set; }
    public string PaymentReceiptUrl { get; set; }

    // Participants sharing this expense (who should contribute)
    public List<ExpenseParticipantDto> Participants { get; set; } = new List<ExpenseParticipantDto>();

    // For UI convenience when creating/editing
    public List<int> ParticipatingFamilyIds { get; set; } = new List<int>();

}
