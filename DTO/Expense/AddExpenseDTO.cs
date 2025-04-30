using System.ComponentModel.DataAnnotations;

public class AddExpenseDTO
{
    [StringLength(100)]
    public  string Description { get; set; }

    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Amount { get; set; }

    [Required]
    public DateTime Date { get; set; } = DateTime.Now;

    [Required]
    public int TripId { get; set; }

    [Required]
    public int FamilyId { get; set; }

}
