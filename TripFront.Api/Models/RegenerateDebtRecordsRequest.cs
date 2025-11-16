using System.ComponentModel.DataAnnotations;

namespace TripFront.Api.Models;

public class RegenerateDebtRecordsRequest
{
    [Required]
    public List<DebtRecordDto> Debts { get; set; } = new();
}
