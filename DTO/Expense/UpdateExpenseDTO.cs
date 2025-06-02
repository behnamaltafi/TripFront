using System.ComponentModel.DataAnnotations;

public class UpdateExpenseDTO
{
    public int Id { get; set; }
    [StringLength(100)]
    public  string Description { get; set; }

    [Range(0.01, double.MaxValue)]
    public decimal Amount { get; set; }

    public DateTime Date { get; set; }
    public int FamilyId { get; set; }


}