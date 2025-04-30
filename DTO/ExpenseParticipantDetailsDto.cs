
// Interfaces
using System.ComponentModel.DataAnnotations;

public class ExpenseParticipantDetailsDto
{
    public int FamilyId { get; set; }
    public string FamilyName { get; set; }

    [Range(1, int.MaxValue)]
    public int ParticipantCount { get; set; } = 1;

    // Calculated fields
    public decimal ShareAmount { get; set; }
}