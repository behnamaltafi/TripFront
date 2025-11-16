using System.ComponentModel.DataAnnotations;

namespace TripFront.Api.Models;

public class PaymentReceiptRequest
{
    [Required]
    public string Base64Receipt { get; set; } = string.Empty;
}
